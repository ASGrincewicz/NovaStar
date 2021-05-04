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
        [Header("Player Projectile Pool")]
        [SerializeField] private GameObject _projectilePrefab;
        [SerializeField] private GameObject _projectileContainer;
        [SerializeField] private List<GameObject> _projectilePool;
        [Header("Enemy Projectiles")] [SerializeField]
        private GameObject _enemyProjectileContainer;
        [Header("Boss Projectile Pool")]
        [SerializeField] private GameObject _bossProjectilePrefab;
        [SerializeField] private GameObject _bossProjectileContainer;
        [SerializeField] private List<GameObject> _bossProjectilePool;
        [Header("Power Up Pool")]
        [SerializeField] private List<GameObject> _powerUps;
        [SerializeField] private GameObject _powerUpPrefab;
        [SerializeField] private GameObject powerUpContainer;
        [Header("VFX Pool")]
        [SerializeField] private GameObject _projectileVFXPrefab;
        [SerializeField] private GameObject _projectileVFXContainer;
        [SerializeField] private List<GameObject> _projectileVFXPool;
        [Header("Listening to:")]
        [SerializeField] private PlayerWeaponEvent _playerWeaponEvent;
        [SerializeField] private PoolGORequest _projectileRequest;
        [SerializeField] private PoolGORequest _projectileVFXRequest;
        [SerializeField] private PoolGORequest _bossProjectileRequest;
        [SerializeField] private PoolGORequest _powerUpRequest;

        public static Action clearChildren;
       
        private void OnEnable()
        {
            _playerWeaponEvent.OnPlayerWeaponChangeEventRaised += GetCurrentWeapon;
            _projectileRequest.OnGameObjectRequested += RequestProjectile;
            _projectileVFXRequest.OnGameObjectRequested += RequestProjectileVFX;
            _bossProjectileRequest.OnGameObjectRequested += RequestBossProjectile;
            _powerUpRequest.OnGameObjectIntRequested += RequestPowerUp;
        }

        private void OnDisable()
        {
            _playerWeaponEvent.OnPlayerWeaponChangeEventRaised -= GetCurrentWeapon;
            _projectileRequest.OnGameObjectRequested -= RequestProjectile;
            _projectileVFXRequest.OnGameObjectRequested -= RequestProjectileVFX;
            _bossProjectileRequest.OnGameObjectRequested -= RequestBossProjectile;
            _powerUpRequest.OnGameObjectIntRequested -= RequestPowerUp;
        }

        private void Start()
        {
            _powerUpPrefab = _powerUps[0];
            GenerateProjectileVFX(5);
            GenerateBossProjectile(10);
        }
        private void GetCurrentWeapon(WeaponType weapon)
        {
            clearChildren();
            _projectilePool.Clear();
            _projectilePrefab = weapon.projectilePrefab;
            if(_projectilePrefab != null)
             GenerateProjectile(20);
        }
       private List<GameObject> GenerateProjectile(int amount)
        {
            for(int i = 0; i< amount; i++)
            {
                if (_projectilePool.Count < amount)
                {
                    GameObject bullet = Instantiate(_projectilePrefab, _projectileContainer.transform);
                    bullet.SetActive(false);
                    _projectilePool.Add(bullet);
                }
                else
                 return null;
            }
            return _projectilePool;
        }
        private List<GameObject> GenerateBossProjectile(int amount)
        {
            for(int i = 0; i < amount; i++)
            {
                if (_bossProjectilePool.Count < amount)
                {
                    var bossProj = Instantiate(_bossProjectilePrefab, _bossProjectileContainer.transform);
                    bossProj.SetActive(false);
                    _bossProjectilePool.Add(bossProj);
                }
                else
                    return null;
            }
            return _bossProjectilePool;
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
        private GameObject RequestBossProjectile()
        {
            foreach(GameObject bossProj in _bossProjectilePool)
            {
                if(bossProj.activeInHierarchy == false)
                {
                    bossProj.SetActive(true);
                    return bossProj;
                }
            }
            GameObject newBossProj = _bossProjectilePrefab;
            newBossProj.SetActive(true);
            _bossProjectilePool.Add(newBossProj);
            return newBossProj;
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
            _powerUpPrefab = _powerUps[UnityEngine.Random.Range(0,3)];
           GameObject powerUp = Instantiate(_powerUpPrefab, powerUpContainer.transform);
           return powerUp;
        }
    }
}
