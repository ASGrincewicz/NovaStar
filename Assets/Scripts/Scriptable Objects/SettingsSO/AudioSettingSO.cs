using UnityEngine;
using UnityEngine.Audio;

namespace Veganimus.NovaStar
{
    ///<summary>
    ///@author
    ///Aaron Grincewicz
    ///</summary>
    ///
    [CreateAssetMenu(menuName =("Settings/ Audio"))]
    public class AudioSettingSO : ScriptableObject
    {
        [SerializeField] private AudioSettings _audioSettings;
        [SerializeField] private AudioMixer _audioMixer;
        
        [Range(0,1)]
        public float volume = 0.5f;

        public void ChangeVolume(float value)=> volume = value;
        
    }
}
