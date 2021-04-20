using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


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
        [SerializeField] private LoadSceneEventSO _loadSceneEvent;
        private void OnEnable() => _loadSceneEvent.OnEventRaised += LoadScene;
        private void OnDisable() => _loadSceneEvent.OnEventRaised -= LoadScene;
        
        public void LoadScene(string scene)
        {
            SceneManager.LoadScene(scene);
        }

    }
}
