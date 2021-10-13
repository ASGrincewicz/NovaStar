using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif
using Grincewicz.Verify;

namespace Veganimus.NovaStar
{
    ///<summary>
    ///@author
    ///Aaron Grincewicz
    ///</summary>
    public class MainMenu : MonoBehaviour
    {
        [Header("Main Menu UI")]
        [SerializeField] private GameObject _optionsMenu;
        [SerializeField] private Slider  _volumeSlider;
        [SerializeField] private TMP_Text _availableCurrency, _highScoreText, _playerName;
#if UNITY_EDITOR
        [SerializeField] private Slider _brightnessSlider;
        [SerializeField] private TMP_Dropdown _resolutionDropDown;
        
        [SerializeField] private Toggle _fullScreenToggle;
        [SerializeField] private Resolution[] _availableRes;
#endif
        [Header("Listening To")]
        [SerializeField] private AudioSettingSO _audioSettingsSO;
        [SerializeField] private LoadSceneEventSO _loadSceneEventSO;
        [SerializeField] private PlayerStats _playerRecords;
        [Header("Broadcasting On")]
        [SerializeField] private PlaySFXEvent _playSFXEvent;

        private void OnEnable()=> _loadSceneEventSO.OnEventRaised += SceneLoader; 
        
        private void OnDisable()=> _loadSceneEventSO.OnEventRaised -= SceneLoader;

        private void Start()
        {
#if UNITY_STANDALONE
            GetScreenResolution();
#endif
            UpdateRecords();
        }
        private void UpdateRecords()
        {
            _playerName.text = $"Pilot Name: {Verify.TextToVerify}";
            _highScoreText.text = $"High Score:{_playerRecords.HighScore}";
            _availableCurrency.text = $"Credits: ${_playerRecords.Currency}";
        }

        private void SceneLoader(string value) => SceneManager.LoadScene("Loading");

        public void QuitGame()
        {
#if UNITY_STANDALONE

            Application.Quit();
#endif
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#endif

        }

#if UNITY_STANDALONE


        public void ChangeBrightness() => Screen.brightness = _brightnessSlider.value;

        public void ChangeResolution()
        {
            var height = _availableRes[_resolutionDropDown.value - 1].height;
            var width = _availableRes[_resolutionDropDown.value - 1].width;
            Screen.SetResolution(width, height, false);
        }

        public void FullScreenToggle()
        {
            if (_fullScreenToggle.isOn == true)
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
            else
                Screen.fullScreenMode = FullScreenMode.Windowed;
        }

        public void GetScreenResolution()
        {
            _availableRes = Screen.resolutions;
            List<string> reso = new List<string>();
            foreach (var res in _availableRes)
             reso.Add(res.ToString());
            _resolutionDropDown.AddOptions(reso);
        }
#endif
    }
}
