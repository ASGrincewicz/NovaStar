using UnityEngine;
using UnityEngine.Events;


namespace Veganimus.NovaStar
{
    ///<summary>
    ///@author
    ///Aaron Grincewicz
    ///</summary>
    ///
    [CreateAssetMenu(menuName =("Specialized Events/ Player Weapon Event"))]
    public class PlayerWeaponEvent : ScriptableObject
    {
        public UnityAction<WeaponType> OnPlayerWeaponChangeEventRaised;
        public UnityAction<string> OnPlayerWeaponNameEventRaised;

        public void RaiseWeaponChangeEvent(WeaponType weapon)
        {
            if(OnPlayerWeaponChangeEventRaised != null)
             OnPlayerWeaponChangeEventRaised.Invoke(weapon);
        }
        public void RaiseWeaponNameEvent(string name)
        {
            if(OnPlayerWeaponNameEventRaised != null)
             OnPlayerWeaponNameEventRaised.Invoke(name);
        }
    }
}
