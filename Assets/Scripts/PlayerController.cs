using System.Collections;
using UnityEngine;


namespace Veganimus.NovaStar
{
    ///<summary>
    ///@author
    ///Aaron Grincewicz
    ///</summary>
    public class PlayerController : MonoBehaviour, IDamageable
    {
        [SerializeField] private int _lives = 3;
        [SerializeField] private float _yTopBound, _yBottomBound, _xLeftBound, _xRightBound;
        [SerializeField] private AudioClip _shieldSFX, _powerUpSFX, _damageSFX, _deathSFX;
        [SerializeField] private GameObject _shipModel, _shield, _explosionPrefab, _powerUpEffect;
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
        [SerializeField] private intEventSO _upgradeTracker, _powerUpTracker;
        private bool _canTakeDamage = true, _powerUpActive, _shieldActive;
        private int _currentWeaponID, _lastWeaponID, _maxUpgrade;
        private ShootOnInput _shootOnInput;
        private Transform _transform;
        private WaitForSeconds _damageCoolDown, _powerUpCoolDown, _powerUpEffectTimer;

        private void OnEnable()
        {
            _collectEvent.OnPowerUpCollect += PowerUpCollect;
            _maxUpgradeChannel.OnEventRaised += max => _maxUpgrade = max;
            _currentWeaponIDEvent.OnEventRaised += CurrentWeaponTracker;
        }

        private void OnDisable()
        {
            _collectEvent.OnPowerUpCollect -= PowerUpCollect;
            _maxUpgradeChannel.OnEventRaised -= max => _maxUpgrade = max;
            _currentWeaponIDEvent.OnEventRaised -= CurrentWeaponTracker;
        }
        
        private void Start()
        {
            _transform = transform;
            _damageCoolDown = new WaitForSeconds(3.0f);
            _powerUpCoolDown = new WaitForSeconds(10.0f);
            _powerUpEffectTimer = new WaitForSeconds(1.0f);
            _shootOnInput = GetComponent<ShootOnInput>();
        }
        private void Update()
        {
            var position = _transform.localPosition;
            position = new Vector3(Mathf.Clamp(position.x, _xLeftBound, _xRightBound),
                Mathf.Clamp(position.y, _yBottomBound, _yTopBound), 20);
            _transform.localPosition = position;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Enemy")
                Damage(0);
        }

        private void CurrentWeaponTracker(int current)=> _currentWeaponID = current;
        
        private void PowerUpCollect(int id)
        {
            _playSFXEvent.OnSFXEventRaised(_powerUpSFX);
            if (_powerUpActive) return;
            
            switch (id)
            {
                case 0:
                    if (_currentWeaponID > _maxUpgrade - 2) break;
                     _weaponChangeEvent.RaiseWeaponChangeEvent(true, false, _currentWeaponID++);
                    _upgradeTracker.RaiseEvent(0);
                    break;
                case 1:
                    if (_shieldActive) break;
                    
                    _shield.SetActive(true);
                    _shieldActive = true;
                    _powerUpTracker.RaiseEvent(0);
                    _shieldUIEvent.RaiseBoolEvent(true);
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
            _lives--;
            _shootOnInput.enabled = false;
            _playSFXEvent.RaiseSFXEvent(_deathSFX);
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            _shipModel.SetActive(false);
            yield return new WaitForSeconds(2.0f);
            _playerDeadEvent.RaiseEvent();
            Destroy(gameObject);
        }
        public void Damage(int amount)
        {
            if (_shieldActive)
            {
                _shield.SetActive(false);
                _shieldActive = false;
                _playSFXEvent.RaiseSFXEvent(_shieldSFX);
                _shieldUIEvent.RaiseBoolEvent(false);
            }
            else
            {//Camera shake
                if (!_canTakeDamage) return;
                
                if (_currentWeaponID == 0)
                    StartCoroutine(DeathRoutine());

                else
                {
                    _playSFXEvent.RaiseSFXEvent(_damageSFX);
                    _weaponChangeEvent.RaiseWeaponChangeEvent(false, false, 0);
                    _canTakeDamage = false;
                    StartCoroutine(DamageCoolDown());
                }
                
            }
        }
    }
}
