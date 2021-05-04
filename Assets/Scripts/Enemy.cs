using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Veganimus.NovaStar
{
    ///<summary>
    ///@author
    ///Aaron Grincewicz
    ///</summary>
    public class Enemy : MonoBehaviour, IDamageable
    {
        [SerializeField] private MirrorMoveSO _mirrorMoveSO;
        [SerializeField] private bool _mirror;
        private bool _mirrorMoveOn;
        [SerializeField] private float _sceneEntryTime;
        private AudioClip _shootSound => _enemyClass.shootSound;
        private AudioClip _damageSound => _enemyClass.damageSound;
        private AudioClip _deathSound => _enemyClass.deathSound;
        [SerializeField] private List<GameObject> _damageVFX;
        private int _damageAmount;
        [SerializeField] private EnemyClass _enemyClass;
        [SerializeField] private Transform _fireOffset;
        [SerializeField] private Transform _fireOffset2;
        [SerializeField] private bool _hasAltWeapon;
        private string _enemyName => _enemyClass.enemyName;
        private int scoreTier => _enemyClass.scoreTier;
        [SerializeField] private int _hp;
        [SerializeField] private int _shieldHP;
        [SerializeField] private bool _shieldOn;
        private int _chance;
        private float _speed => _enemyClass.speed;
        private float _canFire = -1.0f;
        private float _fireRate => _enemyClass.fireRate;
        private WaitForSeconds _fireDelay;
        private GameObject _itemDrop => _enemyClass.itemDrop;
        private bool _hasWeapon => _enemyClass.hasWeapon;
        private bool _hasShield => _enemyClass.hasShield;
        [SerializeField] private GameObject _weapon;
        [SerializeField] private GameObject _shield;
        private GameObject _explosionPrefab => _enemyClass.explosionPrefab;
        private List<GameObject> _enemyBullets;
        private Rigidbody _rigidbody;
        [SerializeField] private Vector3 _moveDirection;
        [Header("Broadcasting on")]
        [SerializeField] private intEventSO _updateScoreChannel;
        [SerializeField] private EnemyTrackerChannel _enemyTracking;
        [SerializeField] private PlaySFXEvent _playSFXEvent;
        [SerializeField] private PoolGORequest _requestPowerUpDrop;

        private void Awake()
        {
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
                    _shieldOn = true;
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
            if (_mirror)
                StartCoroutine(ActivateMirrorMovementRoutine());
        }
        private void Update()
        {
            if (_mirrorMoveOn)
             MirrorMovement();

            else
                Movement();

            if (_hasWeapon)
            {
                if (Time.time > _canFire)
                {
                    Shoot();
                    if (_hasAltWeapon)
                        AltShoot();
                }
            }
            if(_hp <= 0)
            {
               Die();
            }
            if(_shieldHP <= 0 && _shield == true)
            {
                _shield.SetActive(false);
                _shieldOn = false;
            }
        }
        private void Movement()
        {
            transform.Translate(Vector3.left * (_speed * Time.deltaTime));

            if (transform.position.x < -20f)
               transform.position = new Vector3(13f, Random.Range(-3f, 5f), 0);
        }
        private void MirrorMovement()
        {
            _mirrorMoveSO.Pitch(this.gameObject);
            _mirrorMoveSO.MirrorMovement(this.gameObject, _rigidbody);
        }
        private void Shoot()
        {
            _canFire = Time.time + _fireRate;
            GameObject enemyBullet = Instantiate(_weapon, this.gameObject.transform);
            enemyBullet.transform.position = _fireOffset.transform.position;
            _playSFXEvent.RaiseSFXEvent(_shootSound);
            StartCoroutine(EnemyFireRoutine());
        }
        private void AltShoot()
        {
            _canFire = Time.time + (_fireRate);
            GameObject enemyBullet = Instantiate(_weapon, this.gameObject.transform);
            enemyBullet.transform.position = _fireOffset2.transform.position;
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
                if (itemDrop != null)
                {
                    itemDrop.transform.position = transform.position;
                    itemDrop.SetActive(true);
                }
                else
                    return;
            }
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        public void Damage(int amount)
        {
            if (_enemyClass.hasShield && _shieldOn)
                _shieldHP -= amount;
            else
            {
                int damageVFX_chance = Random.Range(0, 10);
                if (damageVFX_chance > 5)
                {
                    GameObject dVFX = ActivateDamageVFX();
                }
                _playSFXEvent.RaiseSFXEvent(_damageSound);
                _updateScoreChannel.RaiseEvent(scoreTier / 5);
                _hp -= amount;
            }
        }
        private GameObject ActivateDamageVFX()
        {
            foreach (GameObject obj in _damageVFX)
            {
                if (obj.activeSelf == false)
                {
                    obj.SetActive(true);
                    return obj;
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
            _mirrorMoveOn = true;
        }
    }
}
