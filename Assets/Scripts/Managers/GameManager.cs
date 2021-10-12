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
        [SerializeField] GameObject _bossCutscene;

        public static Action<bool> isPaused;
        public static Action<bool> gameOver;

        private void OnEnable()
        {
           
            _enemyTracking.OnEnemySpawned += EnemySpawns;
            _enemyTracking.OnEnemyDestroyed += EnemyDestroyed;
            _trackLevelEvent.OnBoolEventRaised += LevelComplete;
            _playerDeadEvent.OnEventRaised += GameOver;
            _loadSceneEventSO.OnEventRaised += LoadSceneCalled;
            _endGameEvent.OnEventRaised += EndGame;
        }
        private void OnDisable()
        {
            _enemyTracking.OnEnemySpawned -= EnemySpawns;
            _enemyTracking.OnEnemyDestroyed -= EnemyDestroyed;
            _trackLevelEvent.OnBoolEventRaised -= LevelComplete;
            _playerDeadEvent.OnEventRaised -= GameOver;
            _loadSceneEventSO.OnEventRaised -= LoadSceneCalled;
            _endGameEvent.OnEventRaised -= EndGame;
        }
       private void Start()
        {
            Time.timeScale = 1.0f;
            _trackLevelEvent.OnEventRaised(_currentLevel);
        }
        private void Update()
        {
            if(_enemiesDestroyed == _enemiesSpawned && levelComplete == true)
                EndGame();
        }
        private void LoadSceneCalled(string scene)
        {
            SceneManager.LoadScene(scene);
            Time.timeScale = 1;
        }
      
        private void GameOver()
        {
            gameOver(true);
            Time.timeScale = 0;
        }

        private void LevelComplete(bool isComplete)
        {
            if (!isComplete) return;
            
            levelComplete = true;
            Time.timeScale = 0;
        }

        private void NextLevel()=> _nextLevelEvent.RaiseEvent();
        private void EndGame()=> _bossCutscene.SetActive(true);
        private void EnemyDestroyed()=> _enemiesDestroyed++;
        private void EnemySpawns() => _enemiesSpawned++;
    }
}
