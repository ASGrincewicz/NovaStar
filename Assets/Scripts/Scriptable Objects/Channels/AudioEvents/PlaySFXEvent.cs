using UnityEngine;
using UnityEngine.Events;

namespace Veganimus.NovaStar
{
    ///<summary>
    ///@author
    ///Aaron Grincewicz
    ///</summary>
    ///
    [CreateAssetMenu(menuName =("Audio Events/ SFX Event"))]
    public class PlaySFXEvent : ScriptableObject
    {
        public UnityAction<string ,AudioClip> OnSFXEventRaised;

        public void RaiseSFXEvent(string source, AudioClip clipToPlay)
        {
            if(OnSFXEventRaised!= null)
            {
                OnSFXEventRaised.Invoke(source, clipToPlay);
            }
        }
    }
}
