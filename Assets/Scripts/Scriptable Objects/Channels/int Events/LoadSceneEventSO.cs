using UnityEngine;
using UnityEngine.Events;

namespace Veganimus.NovaStar
{ 
    ///<summary>
    ///@author
    ///Aaron Grincewicz
    ///</summary>
    ///
[CreateAssetMenu(menuName = "Int Events/ Load Scene")]
    public class LoadSceneEventSO : ScriptableObject
    {
        public UnityAction<string> OnEventRaised;
        public void RaiseEvent(string sceneName)
        {
            if(OnEventRaised != null)
            OnEventRaised.Invoke(sceneName);
        }
    }
}
