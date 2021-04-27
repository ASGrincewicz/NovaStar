using UnityEngine;


namespace Veganimus.NovaStar
{
    ///<summary>
    ///@author
    ///Aaron Grincewicz
    ///</summary>
    public class PlayerRecordsManager : MonoBehaviour
    {
        private static GameObject _instance;
        private int _currency => _playerRecords.Currency;
        [SerializeField] private PlayerStats _playerRecords;
        [SerializeField] private intEventSO _updateScoreChannel;
        [SerializeField] private EnemyTrackerChannel _enemyTracking;
        [SerializeField] private intEventSO _upgradeTracker;
        [SerializeField] private intEventSO _powerUpTracker;
        [SerializeField] private intEventSO _trackCheckpoint;
        private SaveData _saveData;
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
            _updateScoreChannel.OnEventRaised += TrackScore;
            _enemyTracking.OnEnemyDestroyed += TrackKills;
            _enemyTracking.OnEnemySpawned += TrackSpawns;
            _upgradeTracker.OnEventRaised += TrackUpgrade;
            _powerUpTracker.OnEventRaised += TrackPowerUp;
            _trackCheckpoint.OnEventRaised += TrackCheckpoint;
        }
        private void OnDisable()
        {
            _updateScoreChannel.OnEventRaised -= TrackScore;
            _enemyTracking.OnEnemyDestroyed -= TrackKills;
            _enemyTracking.OnEnemySpawned -= TrackSpawns;
            _upgradeTracker.OnEventRaised -= TrackUpgrade;
            _powerUpTracker.OnEventRaised -= TrackPowerUp;
            _trackCheckpoint.OnEventRaised -= TrackCheckpoint;
        }
        private void Start()=> LoadRecords();

        private void LoadRecords()
        {
            SaveData data = SaveSystem.LoadData();
            if(data != null)
            {
                _playerRecords.PlayerName = data.PlayerName;
                _playerRecords.HighScore = data.HighScore;
                _playerRecords.Currency = data.Currency;
            }
            else
             SaveSystem.SaveRecords(_playerRecords);
        }
        private void TrackScore(int score)=> _playerRecords.RecentScore += score;

        private void TrackKills() => _playerRecords.Kills++;

        private void TrackSpawns() => _playerRecords.Spawns++;

        private void TrackUpgrade(int value) => _playerRecords.Upgrades++;

        private void TrackPowerUp(int value) => _playerRecords.PowerUps++;
        private void TrackCheckpoint(int wave) => _playerRecords.Checkpoint = wave;
        public void SaveRecords()=> SaveSystem.SaveRecords(_playerRecords);
    }
}
