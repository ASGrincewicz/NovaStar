using System;
using System.Collections.Generic;
using UnityEngine;
namespace Veganimus.NovaStar
{
    ///<summary>
    ///@author
    ///Aaron Grincewicz
    /// @info: Holds and instantiates frequently needed destructible game objects.
    ///</summary>
    public class PoolManager : MonoBehaviour
    {
        [Header("Object Pooling")]
        [Tooltip("Do not assign an object to index 0, this is done in the script. " +
            "Assign the Boss Projectile to index 1 and Projectile VFX to index 2.")]
        [SerializeField] private GameObject[] _poolableObjects = new GameObject[3];
        [Tooltip("This list should match the Poolable Objects list indexes.")]
        [SerializeField] private Transform[] _poolableObjectContainers = new Transform[3];
        private List<GameObject> _projectilePool;
        private List<GameObject> _bossProjectilePool;
        private List<GameObject> _projectileVFXPool;
        [Header("Listening to:")]
        [SerializeField] private PlayerWeaponEvent _playerWeaponEvent;
        [SerializeField] private PoolGORequest _pooledObjectRequest;

        public static Action clearChildren;
       
        private void OnEnable()
        {
            _playerWeaponEvent.OnPlayerWeaponChangeEventRaised += GetCurrentWeapon;
            _pooledObjectRequest.OnGameObjectIntRequested += RequestObject;
        }

        private void OnDisable()
        {
            _playerWeaponEvent.OnPlayerWeaponChangeEventRaised -= GetCurrentWeapon;
            _pooledObjectRequest.OnGameObjectIntRequested -= RequestObject;
        }

        private void Start()
        {
            GenerateObjects(_projectileVFXPool, _poolableObjects[2], _poolableObjectContainers[2].transform,5);
            GenerateObjects(_bossProjectilePool, _poolableObjects[1], _poolableObjectContainers[1].transform, 10);
        }
        private void GetCurrentWeapon(WeaponType weapon)
        {
            clearChildren();
            _projectilePool.Clear();
            _poolableObjects[0] = weapon.projectilePrefab;
            if(_poolableObjects[0] != null)
                GenerateObjects(_projectilePool, _poolableObjects[0], _poolableObjectContainers[0].transform, 20);
        }

        private List<GameObject> GenerateObjects(List<GameObject> pool, GameObject prefab, Transform container, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                if (pool.Count >= amount) return null;
               
                GameObject obj = Instantiate(prefab, container);
                obj.SetActive(false);
                pool.Add(obj);
            }
            return pool;
        }
        private GameObject RequestObject(int objectIndex)
        {
            List<GameObject> pool = objectIndex switch
            {
                0 => _projectilePool,
                1 => _bossProjectilePool,
                2 => _projectileVFXPool,
                _ => null,
            };
            for (int i = 0; i < pool.Count; i++)
            {
                if (!pool[i].activeInHierarchy)
                {
                    pool[i].SetActive(true);
                    return pool[i];
                }
            }
            GameObject newObject = Instantiate(_poolableObjects[objectIndex], _poolableObjectContainers[objectIndex]);
            newObject.SetActive(true);
            pool.Add(newObject);
            return newObject;
        }
    }
}
