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
    public class Enemy : MonoBehaviour
    {
        private int _damageAmount;
        
        [SerializeField] private EnemyClass _enemyClass;
        [SerializeField] private Transform _fireOffset;
        [SerializeField] private Vector3 _shootDirection;
        [SerializeField] private float _firePower;
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
        [SerializeField] private GameObject _explosionPrefab => _enemyClass.explosionPrefab;
        private List<GameObject> _enemyBullets;
        private Rigidbody _rigidbody;
        [SerializeField] private Vector3 _moveDirection;
        [Header("Broadcasting on")]
        [SerializeField] private intEventSO _updateScoreChannel;
        [SerializeField] private EnemyTrackerChannel _enemyTracking;
        public static Action damaged;
       
        
        private void Awake()
        {
            _hp = _enemyClass.hp;
            
            _fireDelay = new WaitForSeconds(_enemyClass.fireRate);
            if (_hasWeapon == true)
            {
                _weapon = _enemyClass.weapon;
            }
            else
            {
                _weapon = null;
            }
            if (_hasShield == true)
            {
                //shield = _enemyClass.shield;
                _shieldHP = _enemyClass.shieldHP;
                _shieldOn = true;
            }
            else
            {
                _shield = null;
            }
        }
        void Start()
        {
            _chance = UnityEngine.Random.Range(0, 20);
            _rigidbody = GetComponent<Rigidbody>();
        }
        void Update()
        {
            Movement();
            if (_hasWeapon)
            {
                if (Time.time > _canFire)
                {
                    Shoot();
                }
            }
            if(_hp <= 0)
            {
                _enemyTracking.EnemyDestroyedEvent();
                if (_chance >= 10)
                {
                    Instantiate(_itemDrop, transform.position, Quaternion.identity, PoolManager.Instance.powerUpContainer.transform);
                }
                Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
            if(_shieldHP <= 0 && _shield != null)
            {
                _shield.SetActive(false);
                _shieldOn = false;
            }
        }
        void Movement()
        {
            transform.Translate(Vector3.left * _speed * Time.deltaTime);
           
          
            if (transform.position.x < -20f)
            {
                _hp -= _hp;
            }
        }
        void Shoot()
        {
            _canFire = Time.time + _fireRate;
            GameObject enemyBullet = Instantiate(_weapon, this.gameObject.transform);
            enemyBullet.transform.position = _fireOffset.transform.position;
            StartCoroutine(EnemyFireRoutine());
        }
        public void Damage()
        {
           
           
            if (_enemyClass.hasShield == true && _shieldOn == true)
            {
                _shieldHP --;
            }
            else
            {
                _updateScoreChannel.RaiseEvent(scoreTier / 5);
                _hp --;
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Player")
            {
                Damage();
            }
            if (other.tag == "Player Projectile")
            {
                Damage();
            }
        }

        private IEnumerator EnemyFireRoutine()
        {
            yield return _fireRate;
        }
       
        
    }
}
