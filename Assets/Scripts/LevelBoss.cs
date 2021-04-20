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

        public class LevelBoss : MonoBehaviour
        {
            public enum AttackPattern
            {
                Entry , First, Second, Third
            }
            [SerializeField] private AttackPattern _attackPattern;
            [SerializeField] private int _hp = 100;
            [SerializeField] private int _scoreTier;
            [SerializeField] private List<Transform> _firePositions;
            private int _currentFirePositions;
            [SerializeField] private GameObject _bossBulletPrefab;
            [SerializeField] private List<GameObject> _bossBulletPool;
            [SerializeField] private GameObject _explosionPrefab;
            //[SerializeField] private GameObject _bossBulletMagazine;
            //[SerializeField] private GameObject _enemyPrefab;
            //[SerializeField] private List<GameObject> _bossEnemyPool;
            [SerializeField] private Animator _anim;
            [Header("Broadcasting On")]
            [SerializeField] private intEventSO _updateScoreChannel;
            [SerializeField] private intEventSO _bossHealthUIEvent;
            [SerializeField] private GameEvent _nextLevelEvent;
            
            void Start()
            {
                _anim = GetComponent<Animator>();
                {
                    if(_anim == null)
                    {
                        Debug.LogError("Animator is null");
                    }
                }
                GenerateBossBullets(20);
            }
            void Damage()
            {
                _hp--;
                _updateScoreChannel.RaiseEvent(_scoreTier / 40);
                _bossHealthUIEvent.RaiseEvent(_hp);
               if(_hp <= 0)
                {
                    Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
                    _nextLevelEvent.RaiseEvent();
                    Destroy(this.gameObject);
                }
            }
            void TriggerAttackPattern(AttackPattern pattern)
            {
                _anim.SetInteger("Pattern", (int)pattern);
            }
            
            //Method for pooling bullets
            private List<GameObject> GenerateBossBullets(int amount)
            {
                for(int i = 0; i < amount; i++)
                {
                    if(_bossBulletPool.Count < amount)
                    {
                        GameObject bossBullet = Instantiate(_bossBulletPrefab, PoolManager.Instance.bossBulletMagazine.transform);
                        bossBullet.SetActive(false);
                        _bossBulletPool.Add(bossBullet);
                    }
                    else
                    {
                        return null;
                    }
                }
                return _bossBulletPool;
            }
            private GameObject GetBossBullet()
            {
                foreach (var bossBullet in _bossBulletPool)
                {
                    if (bossBullet.activeInHierarchy == false)
                    {
                        bossBullet.SetActive(true);
                        return bossBullet;
                    }
                }
                GameObject newBossBullet = _bossBulletPrefab;
                newBossBullet.SetActive(true);
                _bossBulletPool.Add(newBossBullet);
                return newBossBullet;
            }

            void Shoot()//Called with Animation Events
            {
                GameObject bossBullet = GetBossBullet();
                _currentFirePositions = UnityEngine.Random.Range(0, 3);
                bossBullet.transform.position = _firePositions[_currentFirePositions].transform.position;
            }
            private void OnTriggerEnter(Collider other)
            {
                if(other.tag == "Player Projectile")
                {
                    Damage();
                }
            }
        }
}
