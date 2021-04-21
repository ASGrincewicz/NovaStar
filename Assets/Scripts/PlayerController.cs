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
        [Header("Sound Effects")]
        [SerializeField] private AudioClip _shieldSFX;
        [SerializeField] private AudioClip _powerUpSFX;
        [SerializeField] private AudioClip _damageSFX;
        [SerializeField] private AudioClip _deathSFX;
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
        [SerializeField] private PlaySFXEvent _playSFXEvent;
        [SerializeField] private GameEvent _playerDeadEvent;
        [SerializeField] private BoolEventSO _shieldUIEvent;
        [SerializeField] private WeaponChangeEvent _weaponChangeEvent;
        [SerializeField] private CoRoutineEvent _startPowerUpCoolDown;
        [SerializeField] private intEventSO _upgradeTracker;
        [SerializeField] private intEventSO _powerUpTracker;
        private WaitForSeconds _damageCoolDown;
        private WaitForSeconds _powerUpCoolDown;
        private WaitForSeconds _powerUpEffectTimer;
        private ShootOnInput _shootOnInput;


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
        
        private void Start()
        {
            _damageCoolDown = new WaitForSeconds(3.0f);
            _powerUpCoolDown = new WaitForSeconds(10.0f);
            _powerUpEffectTimer = new WaitForSeconds(1.0f);
            _shootOnInput = GetComponent<ShootOnInput>();
        }
        private void Update()
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, _xLeftBound, _xRightBound),
                Mathf.Clamp(transform.position.y, _yBottomBound, _yTopBound), 0);
        }
       private void CurrentWeaponTracker(int current)=> _currentWeaponID = current;
        
       private void PowerUpCollect(int id)
        {
            _playSFXEvent.OnSFXEventRaised("Player", _powerUpSFX);
            if (_powerUpActive == false)
            {
                switch (id)
                {
                    case 0:
                        if (_currentWeaponID < _maxUpgrade -2)
                        _weaponChangeEvent.RaiseWeaponChangeEvent(true, false, _currentWeaponID++);
                        _upgradeTracker.RaiseEvent(0);
                        // _playSFXEvent.OnSFXEventRaised("Player", _powerUpSFX);
                        break;
                    case 1:
                        if (_shieldActive == false)
                        {
                            _shield.SetActive(true);
                            _shieldActive = true;
                            _powerUpTracker.RaiseEvent(0);
                            //_playSFXEvent.RaiseSFXEvent("Player", _shieldSFX);
                            _shieldUIEvent.RaiseBoolEvent(true);
                        }
                        else
                            return;
                        break;
                    case 2:
                        _powerUpActive = true;
                        _lastWeaponID = _currentWeaponID;
                        _weaponChangeEvent.RaiseWeaponChangeEvent(false, true, 3);
                        _powerUpTracker.RaiseEvent(0);
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
                _playSFXEvent.RaiseSFXEvent("Player", _shieldSFX);
                _shieldUIEvent.RaiseBoolEvent(false);
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
                        _playSFXEvent.RaiseSFXEvent("Player", _damageSFX);
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

        private IEnumerator DamageCoolDown()
        {
            yield return _damageCoolDown;
            _canTakeDamage = true;
        }
        private IEnumerator PowerUpFlicker()
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
       private IEnumerator PowerUpCoolDown()
        {
            _startPowerUpCoolDown.RaiseEvent();
            yield return _powerUpCoolDown;
            _powerUpActive = false;
            _weaponChangeEvent.RaiseWeaponChangeEvent(false, true, _lastWeaponID);
        }
        private IEnumerator DeathRoutine()
        {
            _shootOnInput.enabled = false;
            _playSFXEvent.RaiseSFXEvent("Player", _deathSFX);
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            _shipModel.SetActive(false);
            yield return new WaitForSeconds(2.0f);
            _playerDeadEvent.RaiseEvent();
            Destroy(this.gameObject);
        }
    }
}
