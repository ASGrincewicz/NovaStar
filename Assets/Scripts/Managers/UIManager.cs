using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Veganimus.NovaStar
{
    ///<summary>
    ///@author
    ///Aaron Grincewicz
    ///</summary>
    public class UIManager : MonoBehaviour
    {
        [Header("GamePlay UI")]
        [SerializeField] Canvas _hUD_Canvas;
        public Canvas pauseMenu;
        public Canvas levelSummary;
        public Canvas gameOverScreen;
        public Canvas endOfGameScreen;
        [SerializeField] private TMP_Text _levelText, _scoreText, _weaponText;
        [SerializeField] private TMP_Text _incomingWaveText;
        [SerializeField] private Image _bossHealthBar;
        [SerializeField] private GameObject _incomingWaveTextGO;
        [SerializeField] private GameObject _powerUpTimer;
        [SerializeField] private GameObject _bossHealthUI;
        private int _bossCurrentHealth = 100;
        private int _bossMaxHealth = 100;
        private int _currentWave;
        private int _currentLevel;
        private float _cooldownTime = 10.0f;
        private bool _countdownStarted = false;
        private bool _bossWave;
        public static Action<Canvas> continueButton;

        [Header("Listening To")]
        [SerializeField] private intEventSO _updateScoreChannel;
        [SerializeField] private intEventSO _trackWaveEvent;
        [SerializeField] private intEventSO _bossHealthUIEvent;
        [SerializeField] private TrackLevelEventSO _trackLevelEvent;
        [SerializeField] private GameEvent _trackBossWave;
        [SerializeField] private CoRoutineEvent _startCoolDownTimer;
        [SerializeField] private PlayerWeaponEvent _playerWeaponEvent;
        [SerializeField] private InputReaderSO _inputReader;
        [Header("Broadcasting On")]
        [SerializeField] private LoadSceneEventSO _loadSceneEvent;
        [Space]
        [SerializeField] private int _playerScore;
        private bool _gamePaused;

        private void OnEnable()
        {
            _updateScoreChannel.OnEventRaised += UpdateScore;
            _trackWaveEvent.OnEventRaised += TrackWave;
            _trackBossWave.OnEventRaised += TrackBossWave;
            _trackLevelEvent.OnEventRaised += TrackLevel;
            GameManager.gameOver += GameOver;
            _playerWeaponEvent.OnPlayerWeaponNameEventRaised += UpdateWeaponName;
            _inputReader.pauseEvent += OnPauseInput;
            _startCoolDownTimer.OnRoutineStart += StartTimer;
            _bossHealthUIEvent.OnEventRaised += UpdateBossHealth;
        }
        private void OnDisable()
        {
            _updateScoreChannel.OnEventRaised -= UpdateScore;
            _trackWaveEvent.OnEventRaised -= TrackWave;
            _trackLevelEvent.OnEventRaised += TrackLevel;
            GameManager.gameOver -= GameOver;
            _playerWeaponEvent.OnPlayerWeaponNameEventRaised -= UpdateWeaponName;
            _inputReader.pauseEvent -= OnPauseInput;
            _startCoolDownTimer.OnRoutineStart -= StartTimer;
            _bossHealthUIEvent.OnEventRaised -= UpdateBossHealth;
        }
        private void OnPauseInput()
        {
            switch (_gamePaused)
            {
                case true:
                    pauseMenu.gameObject.SetActive(false);
                    _gamePaused = false;
                    break;
                case false:
                    pauseMenu.gameObject.SetActive(true);
                    _gamePaused = true;
                    break;
            }
        }
        private void Start()
        {
            _playerScore = 0;
            UpdateScore(0);
        }
       
        void StartTimer() => _powerUpTimer.SetActive(true);

        void UpdateScore(int amount)
        {
            _playerScore += amount;
            _scoreText.text = $"Score: { _playerScore}";
        }
        void UpdateWeaponName(string name)=> _weaponText.text = $"Weapon: {name}";
        void UpdateBossHealth(int bossHealth)
        {
            _bossCurrentHealth = bossHealth;
            float normalizedValue = Mathf.Clamp((float)_bossCurrentHealth / (float)_bossMaxHealth, 0.0f, 1.0f);
            _bossHealthBar.fillAmount = normalizedValue;
        }
        
        void TrackWave(int wave)
        {
            _currentWave = wave;
            _levelText.text = $"Level:  { _currentLevel} - { _currentWave}";
            StartCoroutine(IncomingWaveText());
        }
        void TrackBossWave()
        {
            _levelText.text = $"Level: {_currentLevel} - Boss";
            _bossWave = true;
            _bossHealthUI.SetActive(true);
            StartCoroutine(IncomingWaveText());
        }
        
        void TrackLevel(int level)
        {
            _currentLevel = level;
            _levelText.text = $"Level:  { _currentLevel } -  {_currentWave}";
        }
        public void RestartGame()=> _loadSceneEvent.RaiseEvent("Main_Menu");
            
        
        public void ContinueButton(Canvas activeCanvas)
        {
            activeCanvas.gameObject.SetActive(false);
            continueButton(activeCanvas);
        }
        public void GameOver(bool isOver)
        {
            gameOverScreen.gameObject.SetActive(isOver);
            _hUD_Canvas.gameObject.SetActive(false);
            _playerScore = 0;
            UpdateScore(0);
        }
        public void QuitGame() => Application.Quit();

        IEnumerator IncomingWaveText()
        {
            if(_bossWave == false)
             _incomingWaveText.text = $"Wave: {_currentWave} Incoming!";
            
            else if(_bossWave == true)
            _incomingWaveText.text = $"Enemy Boss Incoming!";
           
            _incomingWaveTextGO.SetActive(true);
            yield return new WaitForSeconds(3.0f);
            _incomingWaveTextGO.SetActive(false);
        }
        
    }
}
