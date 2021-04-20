using System;
using System.Collections;
using System.Collections.Generic;
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
public class LoadLevelAlt : MonoBehaviour
    {
        public Image progressBar;
        public TMP_Text loadingText;
        //public int sceneToLoad;
        private void OnEnable()
        {
            //Time.timeScale = 0;
        }
        void Start()
        {
            StartCoroutine(LoadSceneAsync());
            //Time.timeScale = 1.0f;
        }
       
        IEnumerator LoadSceneAsync()
        {
            Debug.Log("coroutine started");
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Level");

            while(asyncOperation.isDone == false)
            {
                progressBar.fillAmount = asyncOperation.progress;
                loadingText.text = $"Loading: {asyncOperation.progress}";
            }
            yield return null;
        }
    }
}
