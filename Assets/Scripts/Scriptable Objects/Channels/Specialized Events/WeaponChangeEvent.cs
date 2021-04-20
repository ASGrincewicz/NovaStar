using UnityEngine;
using UnityEngine.Events;

namespace Veganimus.NovaStar
{
    ///<summary>
    ///@author
    ///Aaron Grincewicz
    ///</summary>
    ///
    [CreateAssetMenu(menuName =("Specialized Events/ Weapon Change"))]
    public class WeaponChangeEvent : ScriptableObject
    {
        public UnityAction<bool, bool, int> OnWeaponChanged;

        public void RaiseWeaponChangeEvent(bool isUpgrade, bool isPowerUp, int changeTo)
        {
            if(OnWeaponChanged != null)
             OnWeaponChanged.Invoke(isUpgrade, isPowerUp, changeTo);
        }
    }
}
