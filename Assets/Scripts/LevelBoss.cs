using System.Collections.Generic;
using UnityEngine;
using Grincewicz.PoolManager;

namespace Veganimus.NovaStar
{
    ///<summary>
    ///@author
    ///Aaron Grincewicz
    ///</summary>

    public class LevelBoss : MonoBehaviour, IDamageable
    {
       public enum AttackPattern
       {
        Entry , First, Second, Third
       }
        [SerializeField] private AttackPattern _attackPattern;
        [SerializeField] private Vector3 _projectileShootPos;
        [SerializeField] private int _hp = 100, _damageTaken, _scoreTier;
        [SerializeField] private List<Transform> _firePositions;
        [SerializeField] private GameObject _cannonFireVFX, _explosionPrefab;
        [SerializeField] private List<GameObject> _damageVFX;
        [SerializeField] private Animator _anim;
        [SerializeField] private AudioClip _shootSound, _damageSound, _deathSound;
        [Header("Broadcasting On")]
        [SerializeField] private intEventSO _updateScoreChannel;
        [SerializeField] private intEventSO _bossHealthUIEvent;
        [SerializeField] private GameEvent _nextLevelEvent;
        [SerializeField] private PoolGORequest _bossProjRequest;
        [SerializeField] private PlaySFXEvent _playSFXEvent;
        private int _currentFirePositions;
        private int _patternAP = Animator.StringToHash("Pattern");

        private void Start()
        {
            _anim = GetComponent<Animator>();
            if (_anim == null)
                Debug.LogError("Animator is null");
        }

        ///<summary>BossShoot is called through Animation Event.</summary>
        private void BossShoot()
        {
            GameObject bossBullet = _bossProjRequest.RequestGameObjectInt(1);
            _currentFirePositions = Random.Range(0, 3);
            GameObject cannonVFX = Instantiate(_cannonFireVFX, _firePositions[_currentFirePositions].transform.position, Quaternion.identity);
            bossBullet.transform.position = _firePositions[_currentFirePositions].transform.position;
            _playSFXEvent.RaiseSFXEvent(_shootSound);
        }

        private void TriggerAttackPattern(AttackPattern pattern) => _anim.SetInteger(_patternAP, (int)pattern);

        private GameObject ActivateDamageVFX()
        {
            foreach (GameObject obj in _damageVFX)
            {
                if (obj.activeSelf) continue;
                obj.SetActive(true);
                return obj;
            }
            return null;
        }
        public void Damage(int amount)
        {
            _damageTaken += amount;
            if (_damageTaken >= 25)
            {
                _damageTaken = 0;
                ActivateDamageVFX();
            }
            _playSFXEvent.RaiseSFXEvent(_damageSound);
            _hp -= amount;
            _updateScoreChannel.RaiseEvent(_scoreTier / 40);
            _bossHealthUIEvent.RaiseEvent(_hp);
            if (_hp <= 0)
            {
                _playSFXEvent.RaiseSFXEvent(_deathSound);
                _nextLevelEvent.RaiseEvent();
                Destroy(this.gameObject);
            }
            else if (_hp <= 66)
                TriggerAttackPattern(AttackPattern.Second);
            else if (_hp <= 33)
                TriggerAttackPattern(AttackPattern.Third);
        }
    }
}
