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
        [SerializeField] private AudioClip _secondaryBGmusic;
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
            if(_instance == null)
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
                _audio.Stop();
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
            if (wave == 1 || wave == 4)
            {
                switch (wave)
                {
                    case 1:
                        _audio.clip = _mainBGMusic;
                        _audio.Play();
                        break;
                    case 4:
                        _audio.clip = _secondaryBGmusic;
                        _audio.Play();
                        break;
                    default:
                        break;
                }
            }
        }
        public void StopTrack()=> _audio.Stop();
        
        public void PlaySFX(AudioClip clipToPlay) => _audio.PlayOneShot(clipToPlay);
    }
}
