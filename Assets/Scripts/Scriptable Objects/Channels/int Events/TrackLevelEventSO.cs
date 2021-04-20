using UnityEngine;
using UnityEngine.Events;

namespace Veganimus.NovaStar
{
    ///<summary>
    ///@author
    ///Aaron Grincewicz
    ///</summary>
    ///
    [CreateAssetMenu(menuName =("Bool Events/ Track Level"))]
    public class TrackLevelEventSO : intEventSO
    {
        public UnityAction<bool> OnBoolEventRaised;

        public void RaiseBoolEvent(bool value)
        {
            if(OnBoolEventRaised != null)
             OnBoolEventRaised.Invoke(value);
        }
    }
}
