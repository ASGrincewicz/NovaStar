using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Veganimus.NovaStar
{
    ///<summary>
    ///@author
    ///Aaron Grincewicz
    ///</summary>
    public class ShootOnInput : MonoBehaviour
    {
        [SerializeField] private bool _autoShootRaycastOn;
        [SerializeField] private float _sphereCastRadius = 1f;
        [SerializeField] private float _sphereCastDistance = 20f;
        [SerializeField] private LayerMask _targetLayer;
        [SerializeField] private bool _aIAutoShootOn;
        [SerializeField] private InputReaderSO _inputReader;
        [SerializeField] private List<WeaponType> _weaponType = new List<WeaponType>();
        [SerializeField] private WeaponType _currentWeapon;
        private int _currentWeaponID;
        private int _lastWeaponID;
        [SerializeField] private GameObject _projectilePrefab;
        [SerializeField] private Vector3 _shootDirection;//get from input reader or set default
        [SerializeField] private string _weaponName;
        [SerializeField] private float _accuracyOffsetMin;
        [SerializeField] private float _accuracyOffsetMax;
        [SerializeField] private Transform _fireOffset;
        [SerializeField] private float _firePower;
        private float _fireRate;
        private float _canFire = -1.0f;
        private Rigidbody _projectileRigidbody;
        private WaitForSeconds _fireCoolDown;
        [Header("Broadcasting On")]
        [SerializeField] private PlayerWeaponEvent _playerWeaponEvent;
        [SerializeField] private intEventSO _currentWeaponIDEvent;
        [SerializeField] private intEventSO _maxUpgradeChannel;
        [Header("Listening To")]
        [SerializeField] private WeaponChangeEvent _weaponChangeEvent;
        void OnEnable()
        {
            _inputReader.shootEvent += FireWeapon;
            _weaponChangeEvent.OnWeaponChanged += ChangeWeapon;
            if (_aIAutoShootOn)
                _autoShootRaycastOn = false;
        }
        private void OnDisable()
        {
            _inputReader.shootEvent -= FireWeapon;
            _weaponChangeEvent.OnWeaponChanged -= ChangeWeapon;
        }
        void Start()
        {
            _maxUpgradeChannel.RaiseEvent(_weaponType.Count);
            _fireCoolDown = new WaitForSeconds(_fireRate);
            WeaponStatUpdate();
        }
        private void Update()
        {
           if(_autoShootRaycastOn == true)
            {
                if (Time.time > _canFire)
                AcquireTarget();
            }
           else if(_aIAutoShootOn)
            {
                if (Time.time > _canFire)
                 FireWeapon();
            }
            if (_currentWeaponID > _weaponType.Count - 1)
                _currentWeaponID = _weaponType.Count - 1;
        }

        void FireWeapon()
        {
            if (Time.time > _canFire)
            {
                _canFire = Time.time + _fireRate;
                _projectilePrefab = PoolManager.Instance.RequestProjectile();
                _projectilePrefab.transform.position =  _fireOffset.transform.position;
                _projectilePrefab.transform.rotation = Quaternion.identity;
                _projectileRigidbody = _projectilePrefab.GetComponent<Rigidbody>();
                _shootDirection.y = UnityEngine.Random.Range(_accuracyOffsetMin, _accuracyOffsetMax);
                _projectileRigidbody.AddForce(_shootDirection * _firePower, ForceMode.Impulse);
                StartCoroutine(FireCoolDownRoutine());
            }
            else
                return;
        }
        void AcquireTarget()
        {
            _canFire = Time.time + _fireRate;
            RaycastHit hitInfo;
            if (Physics.SphereCast(_fireOffset.transform.position, _sphereCastRadius, Vector3.right, out hitInfo, _sphereCastDistance, _targetLayer))
            {
                if (hitInfo.collider != null)
                {
                    _projectilePrefab = PoolManager.Instance.RequestProjectile();

                    _projectilePrefab.transform.position = _fireOffset.transform.position;
                    _projectilePrefab.transform.rotation = Quaternion.identity;
                    StartCoroutine(FireCoolDownRoutine());
                }
            }
            else
                return;
        }
        public void ChangeWeapon(bool isUpgrade, bool isPowerUp, int changeTo)
        {
            if (isUpgrade == true && isPowerUp == false)
            {
                if (_currentWeaponID < _weaponType.Count - 1)
                    _currentWeaponID++;
                else
                    return;
            }

            else if (isUpgrade == false && isPowerUp == false)
                _currentWeaponID = 0;

            else if (isPowerUp == true && isUpgrade == false)
                _currentWeaponID = changeTo;

            WeaponStatUpdate();
        }
        public void WeaponStatUpdate()
        {
            _currentWeaponIDEvent.RaiseEvent(_currentWeaponID);
            _currentWeapon = _weaponType[_currentWeaponID];
            _projectilePrefab = _weaponType[_currentWeaponID].projectilePrefab;
            _weaponName = _weaponType[_currentWeaponID].weaponName;
            _accuracyOffsetMin = _weaponType[_currentWeaponID].accuracyOffsetMin;
            _accuracyOffsetMax = _weaponType[_currentWeaponID].accuracyOffsetMax;
           
            _fireRate = _weaponType[_currentWeaponID].fireRate;
            _playerWeaponEvent.RaiseWeaponNameEvent(_weaponType[_currentWeaponID].weaponName);
            _playerWeaponEvent.RaiseWeaponChangeEvent(_weaponType[_currentWeaponID]);
        }
        IEnumerator FireCoolDownRoutine()
        {
            yield return _fireCoolDown;
        }
    }
}
