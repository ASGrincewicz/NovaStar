using UnityEngine;

namespace Veganimus.NovaStar
{
    ///<summary>
    ///@author
    ///Aaron Grincewicz
    ///</summary>
    public class AudioManager : MonoBehaviour
    {
        private static GameObject _instance;
        [SerializeField] private AudioSettingSO _audioSettings;
        [SerializeField] private AudioSource _playerAudio;
        [SerializeField] private AudioSource _enemyAudio;
        [SerializeField] private AudioSource _backgroundMusic;
        [Header("Listening To")]
        [SerializeField] private PlaySFXEvent _playSFXEvent;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            if(_instance == null)
             _instance = gameObject;
            
            else
            Destroy(gameObject);
        }
        private void OnEnable()
        {
            _playSFXEvent.OnSFXEventRaised += PlaySFX;
        }
        private void OnDisable()
        {
            _playSFXEvent.OnSFXEventRaised -= PlaySFX;
        }
        private void Start()
        {
            _playerAudio= GetComponentInChildren<AudioSource>();
            _enemyAudio = GetComponentInChildren<AudioSource>();
            _backgroundMusic = GetComponentInChildren<AudioSource>();
            _playerAudio.volume = _audioSettings.volume;
            _enemyAudio.volume = _audioSettings.volume;
            _backgroundMusic.volume = _audioSettings.volume;
            _backgroundMusic.Play();
        }
        private void Update()
        {
            _playerAudio.volume = _audioSettings.volume;
            _enemyAudio.volume = _audioSettings.volume;
            _backgroundMusic.volume = _audioSettings.volume;
        }

        private void PlaySFX(string source, AudioClip clipToPlay)
        {
            switch(source)
            {
                case "Player":
                    _playerAudio.PlayOneShot(clipToPlay);
                    break;
                case "Enemy":
                    _enemyAudio.PlayOneShot(clipToPlay);
                    break;
                default:
                    break;
            }
        }
        
    }
}
