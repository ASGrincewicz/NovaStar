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
        [SerializeField] private bool _autoShootRaycastOn, _aIAutoShootOn, _multiShotOn;
        [SerializeField] private float _sphereCastRadius = 1f, _sphereCastDistance = 20f;
        [SerializeField] private float _accuracyOffsetMin, _accuracyOffsetMax, _firePower;
        [SerializeField] private string _weaponName;
        [SerializeField] private LayerMask _targetLayer;
        [SerializeField] private Vector3 _shootDirection;//get from input reader or set default
        [SerializeField] private AudioClip _fireSound;
        [SerializeField] private GameObject _projectilePrefab, _multiShotOffset, _multiShotOffset2;
        [SerializeField] private InputReaderSO _inputReader;
        [SerializeField] private List<WeaponType> _weaponType = new List<WeaponType>();
        [SerializeField] private Transform _fireOffset;
        [SerializeField] private WeaponType _currentWeapon;
        [Header("Broadcasting On")]
        [SerializeField] private intEventSO _currentWeaponIDEvent;
        [SerializeField] private intEventSO _maxUpgradeChannel;
        [SerializeField] private PlaySFXEvent _playSFXEvent;
        [SerializeField] private PlayerWeaponEvent _playerWeaponEvent;
        [SerializeField] private PoolGORequest _requestProjectile;
        [Header("Listening To")]
        [SerializeField] private WeaponChangeEvent _weaponChangeEvent;
        private int _currentWeaponID, _lastWeaponID;
        private float _fireRate;
        private float _canFire = -1.0f;
        private Rigidbody _projectileRigidbody;
        private WaitForSeconds _fireCoolDown;

        private void OnEnable()
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
        private void Start()
        {
            _maxUpgradeChannel.RaiseEvent(_weaponType.Count);
            _fireCoolDown = new WaitForSeconds(_fireRate);
            WeaponStatUpdate();
        }
        private void Update()
        {
           if(_autoShootRaycastOn)
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

        private void FireWeapon()
        {
            if (Time.time > _canFire)
            {
                _canFire = Time.time + _fireRate;
                _projectilePrefab.transform.position =  _fireOffset.transform.position;
                _projectilePrefab.transform.rotation = Quaternion.identity;
                _projectileRigidbody = _projectilePrefab.GetComponent<Rigidbody>();
                _shootDirection.y = Random.Range(_accuracyOffsetMin, _accuracyOffsetMax);
                _projectileRigidbody.AddForce(_shootDirection * _firePower, ForceMode.Impulse);
                StartCoroutine(FireCoolDownRoutine());
            }
        }
        private void AcquireTarget()
        {
            _canFire = Time.time + _fireRate;
            RaycastHit hitInfo;
            if (Physics.SphereCast(_fireOffset.position, _sphereCastRadius, Vector3.right, out hitInfo, _sphereCastDistance, _targetLayer))
            {
                if (hitInfo.collider != null)
                {
                    if (!_multiShotOn)
                    {
                        _projectilePrefab = _requestProjectile.RequestGameObjectInt(0);
                        if (_projectilePrefab != null)
                        {
                            Transform projTransform = _projectilePrefab.transform;
                            projTransform.position = _fireOffset.transform.position;
                            projTransform.rotation = Quaternion.identity;
                            _playSFXEvent.RaiseSFXEvent(_fireSound);
                            StartCoroutine(FireCoolDownRoutine());
                        }
                    }
                    else
                    {
                        GameObject proj1 = _requestProjectile.RequestGameObjectInt(0);
                        GameObject proj2 = _requestProjectile.RequestGameObjectInt(0);
                        if (proj1 != null && proj2 != null)
                        {
                            Transform[] projTransforms = new Transform[]{ proj1.transform, proj2.transform};
                            Transform[] offsetTransforms = new Transform[] { _multiShotOffset.transform, _multiShotOffset2.transform };

                            projTransforms[0].position = offsetTransforms[0].position;
                            projTransforms[0].rotation = offsetTransforms[0].rotation;
                            _playSFXEvent.RaiseSFXEvent(_fireSound);
                            projTransforms[1].position = offsetTransforms[1].position;
                            projTransforms[1].rotation = offsetTransforms[1].rotation;
                            _playSFXEvent.RaiseSFXEvent(_fireSound);
                            StartCoroutine(FireCoolDownRoutine());
                        }
                    }
                }
            }
        }
        private void ChangeWeapon(bool isUpgrade, bool isPowerUp, int changeTo)
        {
            if (isUpgrade && !isPowerUp)
            {
                if (_currentWeaponID < _weaponType.Count - 1)
                    _currentWeaponID++;
                else
                    return;
            }
            else if (!isUpgrade  && !isPowerUp)
                _currentWeaponID = 0;

            else if (isPowerUp && !isUpgrade)
                _currentWeaponID = changeTo;

            WeaponStatUpdate();
        }
        private void WeaponStatUpdate()
        {
            _currentWeaponIDEvent.RaiseEvent(_currentWeaponID);
            _currentWeapon = _weaponType[_currentWeaponID];
            _projectilePrefab = _weaponType[_currentWeaponID].projectilePrefab;
            _weaponName = _weaponType[_currentWeaponID].weaponName;
            _accuracyOffsetMin = _weaponType[_currentWeaponID].accuracyOffsetMin;
            _accuracyOffsetMax = _weaponType[_currentWeaponID].accuracyOffsetMax;
            _fireRate = _weaponType[_currentWeaponID].fireRate;
            _fireSound = _weaponType[_currentWeaponID].fireSound;
            _multiShotOn = _weaponType[_currentWeaponID].isMultiShot;
            _playerWeaponEvent.RaiseWeaponNameEvent(_weaponType[_currentWeaponID].weaponName);
            _playerWeaponEvent.RaiseWeaponChangeEvent(_weaponType[_currentWeaponID]);
        }
       private IEnumerator FireCoolDownRoutine()
        {
            yield return _fireCoolDown;
        }
    }
}
