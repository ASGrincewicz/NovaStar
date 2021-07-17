using UnityEngine;


namespace Veganimus.NovaStar
{
    ///<summary>
    ///@author
    ///Aaron Grincewicz
    ///</summary>
    public class PlayerRecordsManager : MonoBehaviour
    {
        [SerializeField] private EnemyTrackerChannel _enemyTracking;
        [SerializeField] private intEventSO _powerUpTracker, _trackCheckpoint, _upgradeTracker, _updateScoreChannel;
        [SerializeField] private PlayerStats _playerRecords;
        private SaveData _saveData;
        private int _currency => _playerRecords.Currency;
        private static GameObject _instance;

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
            _enemyTracking.OnEnemyDestroyed += TrackKills;
            _enemyTracking.OnEnemySpawned += TrackSpawns;
            _powerUpTracker.OnEventRaised += TrackPowerUp;
            _trackCheckpoint.OnEventRaised += TrackCheckpoint;
            _updateScoreChannel.OnEventRaised += TrackScore;
            _upgradeTracker.OnEventRaised += TrackUpgrade;
        }
        private void OnDisable()
        {
            _enemyTracking.OnEnemyDestroyed -= TrackKills;
            _enemyTracking.OnEnemySpawned -= TrackSpawns;
            _powerUpTracker.OnEventRaised -= TrackPowerUp;
            _trackCheckpoint.OnEventRaised -= TrackCheckpoint;
            _updateScoreChannel.OnEventRaised -= TrackScore;
            _upgradeTracker.OnEventRaised -= TrackUpgrade;
        }
        private void Start()=> LoadRecords();

        private void LoadRecords()
        {
            SaveData data = SaveSystem.LoadData();
            if(data != null)
            {
                _playerRecords.Currency = data.Currency;
                _playerRecords.HighScore = data.HighScore;
                _playerRecords.PlayerName = data.PlayerName;
            }
            else
             SaveSystem.SaveRecords(_playerRecords);
        }

        private void TrackCheckpoint(int wave) => _playerRecords.Checkpoint = wave;

        private void TrackKills() => _playerRecords.Kills++;

        private void TrackPowerUp(int value) => _playerRecords.PowerUps++;

        private void TrackScore(int score)=> _playerRecords.RecentScore += score;

        private void TrackSpawns() => _playerRecords.Spawns++;

        private void TrackUpgrade(int value) => _playerRecords.Upgrades++;

        

        

        public void SaveRecords()=> SaveSystem.SaveRecords(_playerRecords);
    }
}
