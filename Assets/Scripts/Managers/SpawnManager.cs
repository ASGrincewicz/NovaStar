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
        [Header("Spawn Bounds")]
        [SerializeField] private float _topBound;
        [SerializeField] private float _bottomBound;
        [SerializeField] private float _rightBound;
        [SerializeField] private float _leftBound;
        [Space]
        [SerializeField] private bool _waveComplete, _levelComplete;
        [SerializeField] private LevelStructure _activeLevel;
        [SerializeField] private List<LevelStructure> _levels;
        [Space]
        [SerializeField] private BossWave _levelBossWave;
        [SerializeField] private EnemyWave _enemyWave;
        [SerializeField] private List<EnemyWave> _enemyWaves;
        [Space]
        [SerializeField] private List<Enemy> _enemies;
        [SerializeField] private GameObject _bossSpawnPos, _enemyContainer;
        [SerializeField] private int _enemyToSpawn, _enemyDestroyed;
        private bool _isBossWave;
        [SerializeField] private int _enemySpawns;
        [Space]
        [SerializeField] private int _currentWave = 1;
        [SerializeField] private int _currentLevel = 1;
        [Header("Broadcasting On")]
        [SerializeField] private EnemyTrackerChannel _enemyTracking;
        [SerializeField] private TrackLevelEventSO _trackLevelEvent;
        [SerializeField] private intEventSO _trackWaveEvent;
        [SerializeField] private GameEvent _trackBossWaveEvent, _endGameEvent;
        [Header("Listening To")]
        [SerializeField] private GameEvent _startNextLevelEvent;
        [SerializeField] private GameEvent _nextLevelRoutineEvent;
        //Coroutine Timers
        private WaitForSeconds _spawnDelay, _nextWaveDelay;
       

        private void OnEnable()
        {
            _startNextLevelEvent.OnEventRaised += StartNextLevel;
            _nextLevelRoutineEvent.OnEventRaised += () => StartCoroutine(NextLevelRoutine(true));
            _enemyTracking.OnEnemyDestroyed += () => _enemyDestroyed++;
            _enemyTracking.OnEnemySpawned += () => _enemySpawns++;
        }
        private void OnDisable()=> _startNextLevelEvent.OnEventRaised -= StartNextLevel;
        
        private void Start()
        {
            _spawnDelay = new WaitForSeconds(5f);
            _nextWaveDelay = new WaitForSeconds(5f);
            _activeLevel = _levels[0];
            GetWaveFromLevel();
            RequestEnemyWave();
            RequestBossWave();
            _trackWaveEvent.RaiseEvent(_currentWave);
        }

        private void StartNextLevel()
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
        private void Update() => KillTracking();
        
        private void KillTracking()
        {
            if (_enemyDestroyed == _enemies.Count && !_isBossWave)
                StartCoroutine(NextWaveRoutine());
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
        private void RequestEnemyWave()
        {
            _enemyWave = _enemyWaves[_currentWave - 1];
            if (_enemyWave != null)
                PopulateEnemies();
        }
        private void PopulateEnemies()
        {
            if (_enemyWave != null)
            {
                _waveComplete = false;
                GetEnemyFromWave(_enemyWave.enemySequence.Count);
                _enemyToSpawn = _enemyWave.enemySequence.Count;
                _enemyDestroyed = 0;
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
                _levelBossWave = _activeLevel.bossWave;
            
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
       private IEnumerator SpawnEnemy()
        {
            for(int i = 0; i < _enemies.Count; i++)
            {
                yield return _spawnDelay;
                _enemies[i].gameObject.transform.position = new Vector3(_rightBound,Random.Range(_bottomBound,_topBound), 0);
                _enemies[i].gameObject.SetActive(true);
                _enemyTracking.EnemySpawnedEvent();
            }
        }
        private IEnumerator NextWaveRoutine()
        {
            _enemies.Clear();
            _waveComplete = true;
            _enemyWave = null;
           
             if (_currentWave == _enemyWaves.Count)
            {
                _isBossWave = true;
                StartCoroutine(BossWaveRoutine());
            }
            else
            {
                _currentWave++;
                yield return _nextWaveDelay;
                _trackWaveEvent.RaiseEvent(_currentWave);
                RequestEnemyWave();
            }
        }
       private IEnumerator NextLevelRoutine(bool bossDead)
        {
            if (bossDead)
            {
                _enemyWaves.Clear();
                _levelComplete = true;
                _activeLevel = null;
                _currentLevel++;
                _currentWave = 1;
            }
            if(_currentLevel >= _levels.Count)
                _endGameEvent.RaiseEvent();
            
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
        }
    }
}
