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
    public class PlayerRecordsManager : MonoBehaviour
    {
        private static GameObject _instance;
        private int _currency => _playerRecords.Currency;
        [SerializeField] private PlayerStats _playerRecords;
        [SerializeField] private intEventSO _updateScoreChannel;
        [SerializeField] private EnemyTrackerChannel _enemyTracking;
        [SerializeField] private intEventSO _upgradeTracker;
        [SerializeField] private intEventSO _powerUpTracker;
        
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
        }
        private void TrackScore(int score)=> _playerRecords.RecentScore += score;

        private void TrackKills() => _playerRecords.Kills++;

        private void TrackSpawns() => _playerRecords.Spawns++;

        private void TrackUpgrade() => _playerRecords.Upgrades++;

        private void TrackPowerUp() => _playerRecords.PowerUps++;
        
    }
}
