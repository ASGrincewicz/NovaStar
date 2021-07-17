using UnityEngine;
//using UnityEngine.Playables;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
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
        [SerializeField] private Slider _brightnessSlider, _volumeSlider;
        [SerializeField] private TMP_Dropdown _resolutionDropDown;
        [SerializeField] private TMP_Text _availableCurrency, _highScoreText, _playerName;
        [SerializeField] private Toggle _fullScreenToggle;
        [SerializeField] private Resolution[] _availableRes;

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
            GetScreenResolution();
            UpdateRecords();
        }
        private void UpdateRecords()
        {
            _playerName.text = $"Pilot Name: {_playerRecords.PlayerName}";
            _highScoreText.text = $"High Score:{_playerRecords.HighScore}";
            _availableCurrency.text = $"Credits: ${_playerRecords.Currency}";
        }

        private void SceneLoader(string value)
        {
            SceneManager.LoadScene("Loading");
        }
        public void QuitGame() => Application.Quit();
        public void GameDevHQButton() => Application.OpenURL("http://www.GameDevHQ.com");
#if UNITY_STANDALONE
#elif PLATFORM_WEBGL

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
