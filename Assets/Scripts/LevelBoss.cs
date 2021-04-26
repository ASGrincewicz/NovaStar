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
        [SerializeField] private int _damageTaken;
        [SerializeField] private int _scoreTier;
        [SerializeField] private List<Transform> _firePositions;
        [SerializeField] private Vector3 _projectileShootPos;
        [SerializeField] private GameObject _cannonFireVFX;
        [SerializeField] private List<GameObject> _damageVFX;
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
        }
        private void Update()
        {
            //if (_hp < 25)
            //    _anim.SetInteger("Pattern", 4);
        }
        private void Damage()
        {
            _damageTaken++;
            if (_damageTaken == 25)
            {
                _damageTaken = 0;
                ActivateDamageVFX();
            }
            _playSFXEvent.RaiseSFXEvent(_damageSound);
            _hp--;
            _updateScoreChannel.RaiseEvent(_scoreTier / 40);
            _bossHealthUIEvent.RaiseEvent(_hp);
            if(_hp <= 0)
            {
                _playSFXEvent.RaiseSFXEvent( _deathSound);
                //play boss explosion cutscene
                //Boss leaves permanenet upgrade// reward?
                //Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
                _nextLevelEvent.RaiseEvent();
                Destroy(this.gameObject);
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
        // Attack Patterns trigger through Animation Event.
        private void TriggerAttackPattern(AttackPattern pattern)=> _anim.SetInteger("Pattern", (int)pattern);

        // Shoot is called through Animation Event.
        private void Shoot()
        {
            GameObject bossBullet = _bossProjRequest.RequestGameObject();
            _currentFirePositions = Random.Range(0, 3);
            GameObject cannonVFX = Instantiate(_cannonFireVFX, _firePositions[_currentFirePositions].transform.position, Quaternion.identity);
            bossBullet.transform.position = _firePositions[_currentFirePositions].transform.position;
            _playSFXEvent.RaiseSFXEvent(_shootSound);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player Projectile")
                Damage();
        }
    }
}
