using System;
using System.Collections.Generic;
using UnityEngine;
namespace Grincewicz.PoolManager
{
    ///<summary>
    ///@author
    ///Aaron Grincewicz
    /// @info: Holds and instantiates frequently needed destructible game objects.
    ///</summary>
    public class PoolManager : MonoBehaviour
    {
        [Header("Object Pooling")]
        [Tooltip("Do not assign an object to index 0, this is done in the script.")]
        [SerializeField] protected List<GameObject> _poolableObjects = new List<GameObject>();
        [Tooltip("This list should match the Poolable Objects list indexes.")]
        [SerializeField] protected List<Transform> _poolableObjectContainers = new List<Transform>();
        [Tooltip("This list should correspond with Index 0 of _poolableObjects and _poolableObjectContainers.")]
        [SerializeField] protected List<GameObject> _poolZero;
        [Tooltip("This list should correspond with Index 1 of _poolableObjects and _poolableObjectContainers.")]
        [SerializeField] protected List<GameObject> _poolOne;
        [Tooltip("This list should correspond with Index 2 of _poolableObjects and _poolableObjectContainers.")]
        [SerializeField] protected List<GameObject> _poolTwo;
        [Header("Listening to:")]
        [SerializeField] private PoolGORequest _pooledObjectRequest;
        public GameObject PoolableObjectZero { get => _poolableObjects[0]; set => _poolableObjects[0] = value; }
        public Transform PoolableObjectContainerZero { get => _poolableObjectContainers[0]; }
        public List<GameObject> PoolZero { get => _poolZero; set => _poolZero = value;}

        public static Action clearChildren;

        private void OnEnable() => _pooledObjectRequest.OnGameObjectIntRequested += RequestObject;

        private void OnDisable() => _pooledObjectRequest.OnGameObjectIntRequested -= RequestObject;

        private void Start()
        {
            _poolOne = GenerateObjects(_poolOne, _poolableObjects[1], _poolableObjectContainers[1], 10);
            _poolTwo =  GenerateObjects(_poolTwo, _poolableObjects[2], _poolableObjectContainers[2],10);
        }

        public List<GameObject> GenerateObjects(List<GameObject> pool, GameObject prefab, Transform container, int amount)
        {
            pool = new List<GameObject>();

            for (int i = 0; i < amount; i++)
            {
                if (pool.Count < amount)
                {
                    GameObject obj = Instantiate(prefab, container);
                    obj.SetActive(false);
                    pool.Add(obj);
                }
            }
            return pool;
        }
        private GameObject RequestObject(int objectIndex)
        {
            List<GameObject> pool = objectIndex switch
            {
                0 => _poolZero,
                1 => _poolOne,
                2 => _poolTwo,
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
