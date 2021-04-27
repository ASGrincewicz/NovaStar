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
        [SerializeField] private AudioSource _audio;
        [Header("Background Music")]
        [SerializeField] private AudioClip _mainBGMusic;
        [SerializeField] private AudioClip _secondaryBGMusic;
        [SerializeField] private AudioClip _thirdBGMusic;
        [SerializeField] private AudioClip _bossMusic;
        [SerializeField] private AudioClip _endMusic;
        [Header("Listening To")]
        [SerializeField] private PlaySFXEvent _playSFXEvent;
        [SerializeField] private intEventSO _trackWaveEvent;
        [SerializeField] private GameEvent _trackBossWave;
        [SerializeField] private GameEvent _endGameEvent;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            if (_instance == null)
                _instance = gameObject;
            else
             Destroy(gameObject);
        }
        private void OnEnable()
        {
            _playSFXEvent.OnSFXEventRaised += PlaySFX;
            _trackWaveEvent.OnEventRaised += ChangeMusic;
            _endGameEvent.OnEventRaised += () =>
            {
                _audio.clip = _endMusic;
                _audio.Play();
            };
            _trackBossWave.OnEventRaised += () =>
            {
                _audio.clip = _bossMusic;
                _audio.Play();
            };
        }
        private void OnDisable()
        {
            _audio.clip = null;
            _playSFXEvent.OnSFXEventRaised -= PlaySFX;
            _trackWaveEvent.OnEventRaised -= ChangeMusic;
        }
        private void Start()
        {
            _audio = GetComponentInChildren<AudioSource>();
            _audio.clip = null;
            _audio.volume = _audioSettings.volume;
            _audio.clip = _mainBGMusic;
            _audio.Play();
        }
        private void Update()=> _audio.volume = _audioSettings.volume;
       
        private void ChangeMusic(int wave)
        {
            switch (wave)
                {
                    case 1:
                        _audio.clip = _mainBGMusic;
                        _audio.Play();
                        break;
                    case 6:
                        _audio.clip = _secondaryBGMusic;
                        _audio.Play();
                        break;
                    case 12:
                        _audio.clip = _thirdBGMusic;
                        _audio.Play();
                        break;
                    default:
                        break;
                }
        }
        public void StopTrack()=> _audio.Stop();
        public void PlaySFX(AudioClip clipToPlay) => _audio.PlayOneShot(clipToPlay);
    }
}
