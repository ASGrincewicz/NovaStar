using UnityEngine;
using UnityEngine.Events;

namespace Veganimus.NovaStar
{
    ///<summary>
    ///@author
    ///Aaron Grincewicz
    ///</summary>
    [CreateAssetMenu(menuName =("Collect Event"))]
    public class CollectEvent : ScriptableObject
    {
        public UnityAction<int> OnPowerUpCollect;

        public void RaiseCollectEvent(int powerUpID)
        {
            if (OnPowerUpCollect != null)
             OnPowerUpCollect.Invoke(powerUpID);
        }
    }
}
