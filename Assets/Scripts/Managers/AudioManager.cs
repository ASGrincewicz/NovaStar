using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Veganimus.NovaStar
{
    ///<summary>
    ///@author
    ///Aaron Grincewicz
    ///</summary>
    public class AudioManager : MonoBehaviour
    {
        private static GameObject _instance;
        [SerializeField] private AudioSettingSO _audioSettings;
        [SerializeField] private AudioSource _audioSource;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            if(_instance == null)
             _instance = gameObject;
            
            else
            Destroy(gameObject);
        }
        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.volume = _audioSettings.volume;
            _audioSource.Play();
        }
        private void Update()=> _audioSource.volume = _audioSettings.volume;
        
    }
}
