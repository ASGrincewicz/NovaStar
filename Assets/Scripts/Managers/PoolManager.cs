using System;
using System.Collections.Generic;
using UnityEngine;


namespace Veganimus.NovaStar
{
    ///<summary>
    ///@author
    ///Aaron Grincewicz
    ///</summary>
    public class PoolManager : MonoBehaviour
    {
        private static PoolManager _instance;
        public static PoolManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    Debug.LogError("Pool Manager is Null");
                }
                return _instance;
            }
        }
        [Header("Player Projectile Pool")]
        [SerializeField] private GameObject _projectilePrefab;
        [SerializeField] private GameObject _projectileContiner;
        [SerializeField] private GameObject _bulletContainerPrefab;
        [SerializeField] private List<GameObject> _projectilePool;
        [Header("Power Up Pool")]
        [SerializeField] private List<GameObject> _powerUps;
        [SerializeField] private GameObject _powerUpPrefab;
        public  GameObject powerUpContainer;
        public GameObject bossBulletMagazine;
        [Header("VFX Pool")]
        [SerializeField] private GameObject _projectileVFXPrefab;
        [SerializeField] private GameObject _projectileVFXContainer;
        [SerializeField] private List<GameObject> _projectileVFXPool;
        [Header("Listening to:")]
        [SerializeField] private PlayerWeaponEvent _playerWeaponEvent;
        [SerializeField] private PoolGORequest _projectileRequest;
        [SerializeField] private PoolGORequest _projectileVFXRequest;
        
        public static Action clearChildren;
        void Awake() => _instance = this;

        private void OnEnable()
        {
            _playerWeaponEvent.OnPlayerWeaponChangeEventRaised += GetCurrentWeapon;
            _projectileRequest.OnGameObjectRequested += RequestProjectile;
            _projectileVFXRequest.OnGameObjectRequested += RequestProjectileVFX;
        }

        private void OnDisable()
        {
            _playerWeaponEvent.OnPlayerWeaponChangeEventRaised -= GetCurrentWeapon;
            _projectileRequest.OnGameObjectRequested -= RequestProjectile;
            _projectileVFXRequest.OnGameObjectRequested -= RequestProjectileVFX;
        }

        private void Start()
        {
            _powerUpPrefab = _powerUps[0];
            GenerateProjectileVFX(5);
        }
        private void GetCurrentWeapon(WeaponType weapon)
        {
            clearChildren();
            _projectilePool.Clear();
            _projectilePrefab = weapon.projectilePrefab;
            if(_projectilePrefab != null)
             GenerateProjectile(10);
        }
       
       private List<GameObject> GenerateProjectile(int amount)
        {
            for(int i = 0; i< amount; i++)
            {
                if (_projectilePool.Count < amount)
                {
                    GameObject bullet = Instantiate(_projectilePrefab, _projectileContiner.transform);
                    bullet.SetActive(false);
                    _projectilePool.Add(bullet);
                }
                else
                 return null;
            }
            return _projectilePool;
        }
        private List<GameObject> GenerateProjectileVFX(int amount)
        {
            for(int i = 0; i< amount; i++)
            {
                if (_projectileVFXPool.Count < amount)
                {
                    GameObject projVFX = Instantiate(_projectileVFXPrefab, _projectileVFXContainer.transform);
                    projVFX.SetActive(false);
                    _projectileVFXPool.Add(projVFX);
                }
                else
                    return null;
            }
            return _projectileVFXPool;
        }
        private GameObject RequestProjectile()
        {
            foreach(GameObject obj in _projectilePool)
            {
                if (obj.activeInHierarchy == false)
                {
                    obj.SetActive(true);
                    return obj;
                }
            }
            GameObject newBullet = _projectilePrefab;
            newBullet.SetActive(true);
            _projectilePool.Add(newBullet);
            return newBullet;
        }

        private GameObject RequestProjectileVFX()
        {
            foreach(GameObject projVFX in _projectileVFXPool)
            {
                if (projVFX.activeInHierarchy == false)
                {
                    projVFX.SetActive(true);
                    return projVFX;
                }
            }
            GameObject newProjVFX = _projectileVFXPrefab;
            newProjVFX.SetActive(true);
            _projectileVFXPool.Add(newProjVFX);
            return newProjVFX;
        }

        private GameObject RequestPowerUp(int scoreTier)
        {
           switch(scoreTier)
            {
                case 250:
                    _powerUpPrefab = _powerUps[0];
                    break;
                case 500:
                    _powerUpPrefab = _powerUps[1];
                    break;
                case 750:
                    _powerUpPrefab = _powerUps[2];
                    break;
                case 1000:
                    _powerUpPrefab = _powerUps[3];
                    break;
            }
            
            return _powerUpPrefab;
        }
        
    }
}
