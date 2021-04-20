using UnityEngine;
using UnityEngine.Events;

namespace Veganimus.NovaStar
{
    ///<summary>
    ///@author
    ///Aaron Grincewicz
    ///</summary>
    public abstract class VoidEventSO : ScriptableObject
    {
        public UnityAction OnEventRaised;
        public void RaiseEvent()
        {
            if(OnEventRaised != null)
             OnEventRaised.Invoke();
        }
    }
}
