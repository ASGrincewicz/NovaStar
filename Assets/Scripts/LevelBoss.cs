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
        [SerializeField] private Vector3 _projectileShootPos;
        [SerializeField] private GameObject _explosionPrefab;
        [SerializeField] private Animator _anim;
        [Header("Sound Effects")]
        [SerializeField] private AudioClip _shootSound;
        [SerializeField] private AudioClip _damageSound;
        [SerializeField] private AudioClip _deathSound;
        [Header("Broadcasting On")]
        [SerializeField] private intEventSO _updateScoreChannel;
        [SerializeField] private intEventSO _bossHealthUIEvent;
        [SerializeField] private GameEvent _nextLevelEvent;
        [SerializeField] private PoolGORequest _bossProjRequest;
        [SerializeField] private PlaySFXEvent _playSFXEvent;
        private int _currentFirePositions;

        private void Start()
        {
                _anim = GetComponent<Animator>();
                {
                    if(_anim == null)
                     Debug.LogError("Animator is null");
                }
            _projectileShootPos = new Vector3(_firePositions[_currentFirePositions].transform.position.x,
                                            _firePositions[_currentFirePositions].transform.position.y, 0);
        }
        private void Damage()
        {
            _playSFXEvent.RaiseSFXEvent("Enemy", _damageSound);
            _hp--;
            _updateScoreChannel.RaiseEvent(_scoreTier / 40);
            _bossHealthUIEvent.RaiseEvent(_hp);
            if(_hp <= 0)
            {
                _playSFXEvent.RaiseSFXEvent("Enemy", _deathSound);
                Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
                _nextLevelEvent.RaiseEvent();
                Destroy(this.gameObject);
            }
        }
        private void TriggerAttackPattern(AttackPattern pattern) => _anim.SetInteger("Pattern", (int)pattern);

        private void Shoot()//Called with Animation Events
        {
            GameObject bossBullet = _bossProjRequest.RequestGameObject();
            _currentFirePositions = Random.Range(0, 3);
            bossBullet.transform.position = _projectileShootPos;
            _playSFXEvent.RaiseSFXEvent("Enemy",_shootSound);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player Projectile")
                Damage();
        }
    }
}
