using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

namespace Veganimus.NovaStar
{
    ///<summary>
    ///@author
    ///Aaron Grincewicz
    ///</summary>
    public class EndGame : MonoBehaviour
    {
        [SerializeField] private Canvas _endGameScreen;
        [SerializeField] private Slider _enemyKilledSlider, _powerUpsCollectedSlider, _weaponUpgradesSlider;
        [SerializeField] private TMP_Text _finalScore;
        [Header("Rewards")]
        [SerializeField] private TMP_Text _enemyReward;
        [SerializeField] private TMP_Text _powerUpReward;
        [SerializeField] private TMP_Text _scoreMultiplier;
        [SerializeField] private TMP_Text _totalReward;
        [SerializeField] private TMP_Text _upgradeReward;
        [Space]
        [SerializeField] private AudioClip _winSound;
        [SerializeField] private LoadSceneEventSO _loadSceneEvent;
        [SerializeField] private PlaySFXEvent _playSFXEvent;
        [SerializeField] private PlayerStats _playerRecords;
        private float _sliderMax = 100f;

        private void OnEnable() => _loadSceneEvent.OnEventRaised += LoadScene;

        private void OnDisable() => _loadSceneEvent.OnEventRaised -= LoadScene;

        private void Start()
        {
            _playSFXEvent.RaiseSFXEvent(_winSound);
            _finalScore.text = ($"Final Score:{_playerRecords.RecentScore}");
            if (_playerRecords.RecentScore > _playerRecords.HighScore)
                _playerRecords.HighScore = _playerRecords.RecentScore;

            _enemyKilledSlider.value = (float)_playerRecords.Kills / _playerRecords.Spawns;
            _powerUpsCollectedSlider.value = (float)_playerRecords.PowerUps / _sliderMax;
            _weaponUpgradesSlider.value = (float)_playerRecords.Upgrades/_sliderMax;
           
            CalculateRewards();
            SaveSystem.SaveRecords(_playerRecords);
        }
        private void CalculateRewards()
        {
            var bonus = _playerRecords.RecentScore / 1000;
            var kills = _playerRecords.Kills * 10;
            var powerUps = _playerRecords.PowerUps * 10;
            var upgrades = _playerRecords.Upgrades * 10;
            var total = bonus + kills + powerUps + upgrades;
            _playerRecords.Currency += total;
            RewardsTextUpdate(bonus, kills, powerUps, upgrades, total);
        }
        private void RewardsTextUpdate(int bonus,int kills,int powerUps,int upgrades,int total)
        {
            _scoreMultiplier.text = $"Score Bonus: ${bonus}";
            _enemyReward.text = $"Enemies Destroyed: ${kills}";
            _powerUpReward.text = $"PowerUps Collected: ${powerUps}";
            _upgradeReward.text = $"Upgrades Collected: ${upgrades}";
            _totalReward.text = $"Total: ${total}";
        }

        public void LoadScene(string scene) => SceneManager.LoadScene(scene);
    }
}
