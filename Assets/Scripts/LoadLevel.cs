using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
namespace Veganimus.NovaStar
{
    public class LoadLevel : MonoBehaviour
    {

        [SerializeField] private Image _progessBar;
        [SerializeField] private TMP_Text _loadingProgress;

        private void Start() => StartCoroutine(LoadLevelASync());

        private IEnumerator LoadLevelASync()
        {
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Level");

            while (!asyncOperation.isDone)
            {
                _progessBar.fillAmount = asyncOperation.progress;
                _loadingProgress.text = $"Loading: {asyncOperation.progress * 100}%";
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
