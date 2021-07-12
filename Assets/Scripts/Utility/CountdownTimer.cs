using UnityEngine;
using UnityEngine.UI;

namespace Veganimus.NovaStar
{
    ///<summary>
    ///@author
    ///Aaron Grincewicz
    ///</summary>
    public class CountdownTimer : MonoBehaviour
    {
        [SerializeField] private Image _countDownTimer;
        //[SerializeField] private Text countdownText;
        [SerializeField] private float _startTime = 10.0f;
        [SerializeField] private float _currentTime;
        [SerializeField] private bool _updateTime;
        private float _deltaTime;

        private void OnEnable()
        {
            _currentTime = _startTime;
            _countDownTimer.fillAmount = 10.0f;
            _updateTime = true;
        }
       
        private void Update()
        {
            _deltaTime = Time.deltaTime;
            if (_updateTime)
            {
            _currentTime -= _deltaTime;
                if (_currentTime <= 0.0f)
                {
                // Stop the countdown timer              
                _updateTime = false;
                _currentTime = 0.0f;
                this.gameObject.SetActive(false);
                }
            // countdownText.text = (int)currentTime + "s";
            float normalizedValue = Mathf.Clamp(_currentTime / _startTime, 0.0f, 1.0f);
            _countDownTimer.fillAmount = normalizedValue;
            }
        }
        }
    }

