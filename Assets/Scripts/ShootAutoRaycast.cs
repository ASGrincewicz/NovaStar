using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Veganimus.NovaStar
{
    ///<summary>
    ///@author
    ///Aaron Grincewicz
    ///</summary>
    public class ShootAutoRaycast : MonoBehaviour
    {
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
        [SerializeField] private float _sphereCastRadius= 0.5f;
        [SerializeField] private float _sphereCastDistance= 12f;
        [SerializeField] private LayerMask _targetLayer;
        private float _fireRate;
        private float _canFire = -1.0f;
        private Rigidbody _projectileRigidbody;
        private WaitForSeconds _fireCoolDown;
        [Header("Broadcasting On")]
        [SerializeField] private PlayerWeaponEvent _playerWeaponEvent;
        [Header("Listening To")]
        [SerializeField] private WeaponChangeEvent _weaponChangeEvent;
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(_fireOffset.transform.position, _sphereCastRadius);
        }
        private void OnEnable()
        {
            _weaponChangeEvent.OnWeaponChanged += ChangeWeapon;
        }
        private void OnDisable()
        {
            _weaponChangeEvent.OnWeaponChanged -= ChangeWeapon;
        }
        private void Start()
        {
            //_projectileRigidbody = _projectilePrefab.GetComponent<Rigidbody>();
            _fireCoolDown = new WaitForSeconds(_fireRate);
            WeaponStatUpdate();
        }
        private void Update()
        {
            if (Time.time > _canFire)
            {
                AcquireTarget();
            }
               

            if (_currentWeaponID > _weaponType.Count - 1)
                _currentWeaponID = _weaponType.Count - 1;
        }

        private void AcquireTarget()
        {
            _canFire = Time.time + _fireRate;
            RaycastHit hitInfo;
            if (Physics.SphereCast(_fireOffset.transform.position, _sphereCastRadius, Vector3.right, out hitInfo, _sphereCastDistance, _targetLayer))
            {
                if (hitInfo.collider != null)
                {
                    Debug.Log(hitInfo.collider.name);
                    //Request projectile from PoolManager
                    //_projectilePrefab = PoolManager.Instance.RequestProjectile();

                    _projectilePrefab.transform.position = _fireOffset.transform.position;
                    _projectilePrefab.transform.rotation = Quaternion.identity;
                    _projectileRigidbody = _projectilePrefab.GetComponent<Rigidbody>();
                    _shootDirection.y = UnityEngine.Random.Range(_accuracyOffsetMin, _accuracyOffsetMax);
                    _projectileRigidbody.AddForce(_shootDirection * _firePower, ForceMode.Impulse);
                    StartCoroutine(FireCoolDownRoutine());
                }
            }
            else
                return;
        }
        public void ChangeWeapon(bool isUpgrade, bool isPowerUp, int changeTo)
        {
            //_canTakeDamage = false;
            //Spin();//GameEventCalls
            if (isUpgrade == true && isPowerUp == false)
                _currentWeaponID++;

            else if (isUpgrade == false && isPowerUp == false)
                _currentWeaponID--;

            else if (isPowerUp == true && isUpgrade == false)
                _currentWeaponID = changeTo;

            WeaponStatUpdate();

            //StartCoroutine(DamageCoolDown());
        }
        public void WeaponStatUpdate()
        {
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
