using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Grincewicz.PoolManager;

namespace Veganimus.NovaStar
{
    ///<summary>
    ///@author
    ///Aaron Grincewicz
    ///</summary>
    public class Enemy : MonoBehaviour, IDamageable
    {
        [SerializeField] private bool _hasAltWeapon, _isShieldOn, _hasMirrorMove;
        [SerializeField] private int _hp, _shieldHP;
        [SerializeField] private float _sceneEntryTime;
        [SerializeField] private EnemyClass _enemyClass;
        [SerializeField] private GameObject _weapon, _shield;
        [SerializeField] private List<GameObject> _damageVFX;
        [SerializeField] private MirrorMoveSO _mirrorMoveSO;
        [SerializeField] private Transform _fireOffset, _fireOffset2;
        private bool _isMirrorMoveOn;
        private int _chance;
        private float _canFire = -1.0f;
        private float _deltaTime;
        private Rigidbody _rigidbody;
        private Transform _transform;
        private WaitForSeconds _fireDelay;
        private bool _hasWeapon => _enemyClass.hasWeapon;
        private bool _hasShield => _enemyClass.hasShield;
        private int scoreTier => _enemyClass.scoreTier;
        private float _speed => _enemyClass.speed;
        private float _fireRate => _enemyClass.fireRate;
        private AudioClip _shootSound => _enemyClass.shootSound;
        private AudioClip _damageSound => _enemyClass.damageSound;
        private AudioClip _deathSound => _enemyClass.deathSound;
        private GameObject _itemDrop => _enemyClass.itemDrop;
        private GameObject _explosionPrefab => _enemyClass.explosionPrefab;
        [Header("Broadcasting on")]
        [SerializeField] private EnemyTrackerChannel _enemyTracking;
        [SerializeField] private intEventSO _updateScoreChannel;
        [SerializeField] private PlaySFXEvent _playSFXEvent;
        [SerializeField] private PoolGORequest _requestPowerUpDrop;

        private void Awake()
        {
            _transform = transform;
            _hp = _enemyClass.hp;
            _weapon = _hasWeapon switch
            {
                true => _enemyClass.weapon,
                _ => null
            };

            switch (_hasShield)
            {
                case true:
                    _shieldHP = _enemyClass.shieldHP;
                    _isShieldOn = true;
                    break;
                default:
                    _shield = null;
                    break;
            }
        }
        private IEnumerator Start()
        {
            yield return null;
          
            _fireDelay = new WaitForSeconds(_enemyClass.fireRate);
            _chance = Random.Range(0, 100);
            _rigidbody = GetComponent<Rigidbody>();
            if (_hasMirrorMove)
                StartCoroutine(ActivateMirrorMovementRoutine());
        }
        private void Update()
        {
            _deltaTime = Time.deltaTime;
            
             Movement();

            if (_hasWeapon)
            {
                if (Time.time > _canFire)
                {
                    Shoot(_fireOffset.transform.position);
                    if (_hasAltWeapon)
                        Shoot(_fireOffset2.transform.position);
                }
            }
            if(_hp <= 0)
                Die();

            if (_shieldHP <= 0 && _shield)
            {
                _shield.SetActive(false);
                _isShieldOn = false;
            }
        }
        private void Movement()
        {
            if (!_isMirrorMoveOn)
            {
                _transform.Translate(Vector3.left * (_speed * _deltaTime));

                if (_transform.position.x < -20f)
                    _transform.position = new Vector3(13f, Random.Range(-3f, 5f), 0);
            }
            else
                MirrorMovement();
        }
        private void MirrorMovement()
        {
            _mirrorMoveSO.Pitch(_transform, _deltaTime);
            _mirrorMoveSO.MirrorMovement(_transform, _rigidbody, _deltaTime);
        }
        private void Shoot(Vector3 offset)
        {
            _canFire = Time.time + _fireRate;
           GameObject enemyBullet = Instantiate(_weapon, _transform);
            enemyBullet.transform.position = offset;
            _playSFXEvent.RaiseSFXEvent(_shootSound);
            StartCoroutine(EnemyFireRoutine());
        }
       
        private void Die()
        {
            _playSFXEvent.RaiseSFXEvent(_deathSound);
            _enemyTracking.EnemyDestroyedEvent();
            if (_chance >= 75)
            {
                GameObject itemDrop = _requestPowerUpDrop.RequestGameObjectInt(scoreTier);
                if (itemDrop == null) return;
                
                itemDrop.transform.position = _transform.position;
                itemDrop.SetActive(true);
            }
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        public void Damage(int amount)
        {
            if (_enemyClass.hasShield && _isShieldOn)
                _shieldHP -= amount;
            else
            {
                int damageVFX_chance = Random.Range(0, 10);
                if (damageVFX_chance > 5)
                    ActivateDamageVFX();
                
                _playSFXEvent.RaiseSFXEvent(_damageSound);
                _updateScoreChannel.RaiseEvent(scoreTier / 5);
                _hp -= amount;
            }
        }
        private GameObject ActivateDamageVFX()
        {
            for(int i = 0; i < _damageVFX.Count; i++)
            {
                if (!_damageVFX[i].activeSelf)
                {
                    _damageVFX[i].SetActive(true);
                    return _damageVFX[i];
                }
            }
            return null;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
               Damage(5);
        }
        private IEnumerator EnemyFireRoutine()
        {
            yield return _fireRate;
        }
        private IEnumerator ActivateMirrorMovementRoutine()
        {
            yield return new WaitForSeconds(_sceneEntryTime);
            _isMirrorMoveOn = true;
        }
    }
}
