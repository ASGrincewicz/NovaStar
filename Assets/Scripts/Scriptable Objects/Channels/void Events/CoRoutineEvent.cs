using UnityEngine;
using UnityEngine.Events;


namespace Veganimus.NovaStar
{
    ///<summary>
    ///@author
    ///Aaron Grincewicz
    ///</summary>
    ///
    [CreateAssetMenu(menuName = ("Void Events/ CoRoutine Event"))]
    public class CoRoutineEvent : ScriptableObject
    {
        public UnityAction OnRoutineStart;

        public void RaiseEvent()
        {
            if (OnRoutineStart != null)
            {
                OnRoutineStart.Invoke();
            }
        }
    }
}
