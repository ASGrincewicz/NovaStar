using System.Collections;
using UnityEngine;


namespace Veganimus.NovaStar
{
    ///<summary>
    ///@author
    ///Aaron Grincewicz
    ///</summary>
    public class PlayerController : MonoBehaviour
    {
        private bool _canTakeDamage = true;
        private bool _powerUpActive;
        private bool _shieldActive;
        private int _currentWeaponID;
        private int _lastWeaponID;
        private int _maxUpgrade;
        [Header("Screen Bounds")]
        [SerializeField] private float _yTopBound;
        [SerializeField] private float _yBottomBound;
        [SerializeField] private float _xLeftBound;
        [SerializeField] private float _xRightBound;
        [Space]
        [SerializeField] private GameObject _shipModel;
        [SerializeField] private GameObject _shield;
        [SerializeField] private GameObject _explosionPrefab;
        [SerializeField] private GameObject _powerUpEffect;
        [Header("Two-way Channels")]
        [SerializeField] private intEventSO _currentWeaponIDEvent;
        [Header("Listening On")]
        [SerializeField] private CollectEvent _collectEvent;
        [SerializeField] private intEventSO _maxUpgradeChannel;
        [Header("Broadcasting On")]
        [SerializeField] private GameEvent _playerDeadEvent;
        [SerializeField] private PlayerWeaponEvent _playerWeaponEvent;//replace with below
        [SerializeField] private WeaponChangeEvent _weaponChangeEvent;
        [SerializeField] private CoRoutineEvent _startPowerUpCoolDown;
        private WaitForSeconds _damageCoolDown;
        private WaitForSeconds _powerUpCoolDown;
        private WaitForSeconds _powerUpEffectTimer;


        private void OnEnable()
        {
            _collectEvent.OnPowerUpCollect += PowerUpCollect;
            _maxUpgradeChannel.OnEventRaised += (int max) => _maxUpgrade = max;
            _currentWeaponIDEvent.OnEventRaised += CurrentWeaponTracker;
        }

        private void OnDisable()
        {
            _collectEvent.OnPowerUpCollect -= PowerUpCollect;
            _currentWeaponIDEvent.OnEventRaised -= CurrentWeaponTracker;
        }
        
        void Start()
        {
            _damageCoolDown = new WaitForSeconds(3.0f);
            _powerUpCoolDown = new WaitForSeconds(10.0f);
            _powerUpEffectTimer = new WaitForSeconds(1.0f);
        }
        void Update()
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, _xLeftBound, _xRightBound),
                Mathf.Clamp(transform.position.y, _yBottomBound, _yTopBound), 0);
        }
        void CurrentWeaponTracker(int current)=> _currentWeaponID = current;
        
        void PowerUpCollect(int id)
        {
            if (_powerUpActive == false)
            {
                switch (id)
                {
                    case 0:
                        if (_currentWeaponID < _maxUpgrade -1)
                        _weaponChangeEvent.RaiseWeaponChangeEvent(true, false, _currentWeaponID++);
                        break;
                    case 1:
                        if (_shieldActive == false)
                        {
                            _shield.SetActive(true);
                            _shieldActive = true;
                        }
                        else
                            return;
                        break;
                    case 2:
                        _powerUpActive = true;
                        _lastWeaponID = _currentWeaponID;
                        _weaponChangeEvent.RaiseWeaponChangeEvent(false, true, 3);
                        StartCoroutine(PowerUpCoolDown());
                        break;
                }
                StartCoroutine(PowerUpFlicker());
            }
            else
                return;
        }
        public void Damage()
        {
            if (_shieldActive)
            {
                _shield.SetActive(false);
                _shieldActive = false;
            }
            else
            {//Camera shake
                if (_canTakeDamage)
                {
                    if (_currentWeaponID == 0)
                    {
                        StartCoroutine(DeathRoutine());
                    }
                    else
                    {
                        _weaponChangeEvent.RaiseWeaponChangeEvent(false, false, 0);
                        _canTakeDamage = false;
                        StartCoroutine(DamageCoolDown());
                    }
                }
                else
                    return;
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy")|| other.CompareTag("Enemy Projectile"))
                Damage();
            else
                return;
        }

        IEnumerator DamageCoolDown()
        {
            yield return _damageCoolDown;
            _canTakeDamage = true;
        }
        IEnumerator PowerUpFlicker()
        {
            _powerUpEffect.SetActive(true);
            yield return _powerUpEffectTimer;
            _powerUpEffect.SetActive(false);
            yield return _powerUpEffectTimer;
            _powerUpEffect.SetActive(true);
            yield return _powerUpEffectTimer;
            _powerUpEffect.SetActive(false);
            yield return null;

        }
        IEnumerator PowerUpCoolDown()
        {
            _startPowerUpCoolDown.RaiseEvent();
            yield return _powerUpCoolDown;
            _powerUpActive = false;
            _weaponChangeEvent.RaiseWeaponChangeEvent(false, true, _lastWeaponID);
        }
        IEnumerator DeathRoutine()
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            _shipModel.SetActive(false);
            yield return new WaitForSeconds(2.0f);
            _playerDeadEvent.RaiseEvent();
            Destroy(this.gameObject);
        }
    }
}
