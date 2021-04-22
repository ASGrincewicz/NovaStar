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
        [SerializeField] private TMP_Text _finalScore;
        [SerializeField] private Slider _enemyKilledSlider;
        [SerializeField] private Slider _weaponUpgradesSlider;
        [SerializeField] private Slider _powerUpsCollectedSlider;
        [Header("Rewards")]
        [SerializeField] private TMP_Text _enemyReward;
        [SerializeField] private TMP_Text _powerUpReward;
        [SerializeField] private TMP_Text _upgradeReward;
        [SerializeField] private TMP_Text _scoreMultiplier;
        [SerializeField] private TMP_Text _totalReward;
        [Space]
        [SerializeField] private LoadSceneEventSO _loadSceneEvent;
        [SerializeField] private PlayerStats _playerRecords;
        private float _sliderMax = 100f;
        private void OnEnable() => _loadSceneEvent.OnEventRaised += LoadScene;
        private void OnDisable() => _loadSceneEvent.OnEventRaised -= LoadScene;

        private void Start()
        {
            _finalScore.text = ($"Final Score:{_playerRecords.RecentScore}");
            if (_playerRecords.RecentScore > _playerRecords.HighScore)
                _playerRecords.HighScore = _playerRecords.RecentScore;

            _enemyKilledSlider.value = (float)_playerRecords.Kills / _playerRecords.Spawns;
            _weaponUpgradesSlider.value = (float)_playerRecords.Upgrades/_sliderMax;
            _powerUpsCollectedSlider.value = (float)_playerRecords.PowerUps/_sliderMax;
            CalculateRewards();
        }
        private void CalculateRewards()
        {
            var kills = _playerRecords.Kills * 10;
            var powerUps = _playerRecords.PowerUps * 10;
            var upgrades = _playerRecords.Upgrades * 10;
            var bonus = _playerRecords.RecentScore / 1000;
            var total = kills + powerUps + upgrades + bonus;
            _playerRecords.Currency += total;
            RewardsTextUpdate(kills, powerUps, upgrades, bonus, total);
        }
        private void RewardsTextUpdate(int kills,int powerUps,int upgrades,int bonus,int total)
        {
            _enemyReward.text = $"Enemies Destroyed: ${kills}";
            _powerUpReward.text = $"PowerUps Collected: ${powerUps}";
            _upgradeReward.text = $"Upgrades Collected: ${upgrades}";
            _scoreMultiplier.text = $"Score Bonus: ${bonus}";
            _totalReward.text = $"Total: ${total}";
        }

        public void LoadScene(string scene)
        {
            SceneManager.LoadScene(scene);
        }

    }
}
