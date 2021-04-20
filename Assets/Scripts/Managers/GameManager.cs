using System;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Veganimus.NovaStar
{
    ///<summary>
    ///@author
    ///Aaron Grincewicz
    ///</summary>
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private int _enemiesDestroyed, _enemiesSpawned;
        [SerializeField] private int _currentLevel = 1;
        public bool levelComplete;
        [Header("Listening On")]
        [SerializeField] private LoadSceneEventSO _loadSceneEventSO;
        [SerializeField] private EnemyTrackerChannel _enemyTracking;
        [SerializeField] private GameEvent _playerDeadEvent;
        [SerializeField] private GameEvent _endGameEvent;
        [Header("Broadcasting On")]
        [SerializeField] private TrackLevelEventSO _trackLevelEvent;
        [SerializeField] private GameEvent _nextLevelEvent;
        [Space]
        [SerializeField] private InputReaderSO _inputReader;

        public static Action<bool> isPaused;
        public static Action<bool> gameOver;

        private void OnEnable()
        {
            _inputReader.pauseEvent += OnPauseInput;
            _enemyTracking.OnEnemySpawned += EnemySpawns;
            _enemyTracking.OnEnemyDestroyed += EnemyDestroyed;
            _trackLevelEvent.OnBoolEventRaised += LevelComplete;
            UIManager.continueButton += ContinueInput;
            _playerDeadEvent.OnEventRaised += GameOver;
            _loadSceneEventSO.OnEventRaised += LoadSceneCalled;
            _endGameEvent.OnEventRaised += EndGame;
        }
        private void OnDisable()
        {
            _inputReader.pauseEvent -= OnPauseInput;
            _enemyTracking.OnEnemySpawned -= EnemySpawns;
            _enemyTracking.OnEnemyDestroyed -= EnemyDestroyed;
            _trackLevelEvent.OnBoolEventRaised -= LevelComplete;
            UIManager.continueButton -= ContinueInput;
            _playerDeadEvent.OnEventRaised -= GameOver;
            _loadSceneEventSO.OnEventRaised -= LoadSceneCalled;
            _endGameEvent.OnEventRaised -= EndGame;
        }
        void Start()
        {
            Time.timeScale = 1.0f;
            _trackLevelEvent.OnEventRaised(_currentLevel);
        }
        void Update()
        {
            if(_enemiesDestroyed == _enemiesSpawned && levelComplete == true)
            EndGame();
            
        }
        void LoadSceneCalled(string scene)
        {
            SceneManager.LoadScene(scene);
            Time.timeScale = 1;
        }
      
        void ContinueInput(Canvas activeCanvas)
        {
            switch(activeCanvas.name)
            {
                case "Level_Complete":
                    NextLevel();
                    break;
                case "Pause_Menu":
                    OnPauseInput();
                    break;
            }
        }
       
        public void OnPauseInput()
        {
            if(Time.timeScale != 0)
             Time.timeScale = 0;
            
            else if(Time.timeScale == 0 )
            Time.timeScale = 1;
            
        }
        public void GameOver()
        {
            gameOver(true);
            Time.timeScale = 0;
        }
       
        public void LevelComplete(bool isComplete)
        {
            if (isComplete == true)
            {
                levelComplete = true;
                Time.timeScale = 0;
            }
            else
            {
                return;
            }
        }
        public void NextLevel()=> _nextLevelEvent.RaiseEvent();
        
        public void EndGame() => _loadSceneEventSO.RaiseEvent("EndGame");
        
        public void EnemyDestroyed()=> _enemiesDestroyed++;
       
        public void EnemySpawns() => _enemiesSpawned++;
    }
}
