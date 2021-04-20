using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace Veganimus.NovaStar
{
    public class LoadLevel : MonoBehaviour
    {

        [SerializeField] private Image _progessBar;
        [SerializeField] private Text _loadingProgress;

        void Start()
        {
            StartCoroutine(LoadLevelASync());
        }

        IEnumerator LoadLevelASync()
        {
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Level");

            while (asyncOperation.isDone == false)
            {
                _progessBar.fillAmount = asyncOperation.progress;
                _loadingProgress.text = $"Loading: {asyncOperation.progress * 100}%";
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
