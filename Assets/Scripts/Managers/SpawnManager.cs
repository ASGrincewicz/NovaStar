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
    public class SpawnManager : MonoBehaviour
    {
        [SerializeField] private bool _waveComplete = false;
        [SerializeField] private bool _levelComplete = false;
        //Reference to Level scriptable Object
        [SerializeField] private LevelStructure _activeLevel;
        //List of Levels
        [SerializeField] private List<LevelStructure> _levels;
        [SerializeField] private BossWave _levelBossWave;
        [SerializeField] private EnemyWave _enemyWave;
        [SerializeField] private List<EnemyWave> _enemyWaves;
        [SerializeField] private List<Enemy> _enemies;
        [SerializeField] private GameObject _enemyContainer;
        [SerializeField] private GameObject _bossSpawnPos;
        [SerializeField] private int _enemyToSpawn;
        [SerializeField] private int _currentWave = 1;
        [SerializeField] private int _currentLevel = 1;
        //Coroutine Timers
         private WaitForSeconds _spawnDelay;
         private WaitForSeconds _nextWaveDelay;
        [Header("Broadcasting On")]
        [SerializeField] private EnemyTrackerChannel _enemyTracking;
        [SerializeField] private TrackLevelEventSO _trackLevelEvent;
        [SerializeField] private intEventSO _trackWaveEvent;
        [SerializeField] private GameEvent _trackBossWaveEvent;
        [SerializeField] private GameEvent _endGameEvent;
        [Header("Listening To")]
        [SerializeField] private GameEvent _startNextLevelEvent;
        [SerializeField] private GameEvent _nextLevelRoutineEvent;
               
        private void OnEnable()
        {
            _startNextLevelEvent.OnEventRaised += StartNextLevel;
            _nextLevelRoutineEvent.OnEventRaised += () => StartCoroutine(NextLevelRoutine(true));
        }
        private void OnDisable()
        {
            _startNextLevelEvent.OnEventRaised -= StartNextLevel;
        }
        void Start()
        {
            _spawnDelay = new WaitForSeconds(5f);
            _nextWaveDelay = new WaitForSeconds(10f);
            _activeLevel = _levels[0];
            GetWaveFromLevel();
            RequestEnemyWave();
            RequestBossWave();
            _trackWaveEvent.RaiseEvent(_currentWave);
        }
        //Game Manager will call function to get next level from list
        public void StartNextLevel()
        {
            _activeLevel = _levels[_currentLevel];
            _trackLevelEvent.OnEventRaised(_currentLevel);
            if(_activeLevel != null)
            {
                GetWaveFromLevel();
                if(_enemyWaves.Count > 0)
                {
                    RequestEnemyWave();
                    _trackWaveEvent.RaiseEvent(_currentWave);
                }
                RequestBossWave();
            }
        }
          
        private List<EnemyWave> GetWaveFromLevel()
        {
            for (int i = 0; i < _activeLevel._waveSequence.Count; i++)
            {
                EnemyWave wave = _activeLevel._waveSequence[i];
                _enemyWaves.Add(wave);
            }
            return _enemyWaves;
        }
        void RequestEnemyWave()
        {
            _enemyWave = _enemyWaves[_currentWave - 1];
            if (_enemyWave != null)
            {
                PopulateEnemies();
            }
        }
        void PopulateEnemies()
        {
            if (_enemyWave != null)
            {
                _waveComplete = false;
                GetEnemyFromWave(_enemyWave.enemySequence.Count);
                _enemyToSpawn = _enemyWave.enemySequence.Count;
               StartCoroutine(SpawnEnemy());
            }
        }
       
        private List<Enemy> GetEnemyFromWave(int count)
        {
            for(int i = 0; i < count; i++)
            {
                Enemy enemy = Instantiate(_enemyWave.enemySequence[i], _enemyContainer.transform);
                enemy.gameObject.SetActive(false);
                _enemies.Add(enemy);
            }
            return _enemies;
        }
       
        private BossWave RequestBossWave()
        {
            if(_activeLevel.bossWave != null)
            {
                _levelBossWave = _activeLevel.bossWave;
            }
            return _levelBossWave;
        }
       
        private void SpawnLevelBoss()
        {
            if(_levelBossWave != null)
            {
                GameObject boss = Instantiate(_levelBossWave.levelBoss, _enemyContainer.transform);
                boss.transform.position = _bossSpawnPos.transform.position;
            }
        }
        public IEnumerator SpawnEnemy()
        {
            foreach (var enemy in _enemies)
            {
                yield return _spawnDelay;
                enemy.gameObject.transform.position = new Vector3(23.0f,UnityEngine.Random.Range(-7f,9f), 0);
                enemy.gameObject.SetActive(true);
                _enemyTracking.EnemySpawnedEvent();
                _enemyToSpawn--;
                if (_enemyToSpawn == 0)
                {
                    StartCoroutine(NextWaveRoutine());
                    yield break;
                }
            }
        }
        public IEnumerator NextWaveRoutine()
        {
            Debug.Log("Next Wave");
            _enemies.Clear();
            _waveComplete = true;
            _enemyWave = null;
            _currentWave++;
             if (_currentWave > _enemyWaves.Count)
            {
                StartCoroutine(BossWaveRoutine());
                yield break;
            }
            else
            {
                yield return _nextWaveDelay;
                _trackWaveEvent.RaiseEvent(_currentWave);
                RequestEnemyWave();
            }
        }
       public IEnumerator NextLevelRoutine(bool bossDead)
        {
            if (bossDead == true)
            {
                _enemyWaves.Clear();
                _levelComplete = true;
                _activeLevel = null;
                _currentLevel++;
                _currentWave = 1;
            }
            if(_currentLevel >= _levels.Count)
            {
                _endGameEvent.RaiseEvent();
                yield break;
            }
            else
            {
                _trackLevelEvent.OnBoolEventRaised(true);
                yield break;
            }
        }
        private IEnumerator BossWaveRoutine()
        {
            yield return _nextWaveDelay;
            SpawnLevelBoss();
            _trackBossWaveEvent.RaiseEvent();
            yield break;
        }
    }
}
