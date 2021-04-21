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
        [SerializeField] private Slider _enemyKilledSlider;
        [SerializeField] private Slider _weaponUpgradesSlider;
        [SerializeField] private Slider _powerUpsCollectedSlider;
        [Header("Rewards")]
        [SerializeField] private TMP_Text _enemyReward;
        [SerializeField] private TMP_Text _powerUpReward;
        [SerializeField] private TMP_Text _upgradeReward;
        [Space]
        [SerializeField] private LoadSceneEventSO _loadSceneEvent;
        [SerializeField] private PlayerStats _playerRecords;
        private void OnEnable() => _loadSceneEvent.OnEventRaised += LoadScene;
        private void OnDisable() => _loadSceneEvent.OnEventRaised -= LoadScene;

        private void Start()
        {
          
        }


        public void LoadScene(string scene)
        {
            SceneManager.LoadScene(scene);
        }

    }
}
