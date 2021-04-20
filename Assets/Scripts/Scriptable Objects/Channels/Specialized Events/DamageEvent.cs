using UnityEngine;
using UnityEngine.Events;

namespace Veganimus.NovaStar
{
    ///<summary>
    ///@author
    ///Aaron Grincewicz
    ///</summary>
    ///
    [CreateAssetMenu(menuName =("Specialized Events/Damage Event"))]
    public class DamageEvent : ScriptableObject

    {
        public UnityAction<int,Collision> OnDamaged;

        public void RaiseDamageEvent(int amount, Collision damaged)
        {
            if(OnDamaged!= null)
            {
                OnDamaged.Invoke(amount, damaged);
            }
        }
    }
}
