using UnityEngine;
using UnityEngine.Events;

namespace Veganimus.NovaStar
{
    ///<summary>
    ///@author
    ///Aaron Grincewicz
    ///</summary>
    [CreateAssetMenu(menuName = ("Bool Events/ Bool Event"))]
    public class BoolEventSO : ScriptableObject
    {
        public UnityAction<bool> OnBoolEventRaised;

        public void RaiseBoolEvent(bool value)
        {
            if (OnBoolEventRaised != null)
                OnBoolEventRaised.Invoke(value);
        }
    }
}
