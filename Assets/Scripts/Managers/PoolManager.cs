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
        [Header("Projectile Pool")]
        [SerializeField] private GameObject _projectilePrefab;
        [SerializeField] private GameObject _projectileContiner;
        [SerializeField] private GameObject _bulletContainerPrefab;
        [SerializeField] private List<GameObject> _projectilePool;
        [Header("Power Up Pool")]
        [SerializeField] private List<GameObject> _powerUps;
        [SerializeField] private GameObject _powerUpPrefab;
        public  GameObject powerUpContainer;
        public GameObject bossBulletMagazine;
        [Header("Listening to:")]
        [SerializeField] private PlayerWeaponEvent _playerWeaponEvent;
        public static Action clearChildren;
        void Awake() => _instance = this;

        private void OnEnable() => _playerWeaponEvent.OnPlayerWeaponChangeEventRaised += GetCurrentWeapon;
        
        private void OnDisable() => _playerWeaponEvent.OnPlayerWeaponChangeEventRaised -= GetCurrentWeapon;
        
        void Start() => _powerUpPrefab = _powerUps[0];
        
        private void Update()
        {
            if (_projectilePrefab != null)
             GenerateProjectile(10);
        }

        private void GetCurrentWeapon(WeaponType weapon)
        {
            clearChildren();
            _projectilePool.Clear();
            _projectilePrefab = weapon.projectilePrefab;
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
        public GameObject RequestProjectile()
        {
            foreach(var bullet in _projectilePool)
            {
                if (bullet.activeInHierarchy == false)
                {
                    bullet.SetActive(true);
                    return bullet;
                }
            }
            GameObject newBullet = _projectilePrefab;
            newBullet.SetActive(true);
            _projectilePool.Add(newBullet);
            return newBullet;
        }
        public GameObject RequestPowerUp(int scoreTier)
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
