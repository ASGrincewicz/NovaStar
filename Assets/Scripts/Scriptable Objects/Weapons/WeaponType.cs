using UnityEngine;

namespace Veganimus.NovaStar
{
    ///<summary>
    ///@author
    ///Aaron Grincewicz
    ///</summary>
    ///
    [CreateAssetMenu(menuName=("Weapon Type"))]
    public class WeaponType :ScriptableObject
    {
        public string weaponName;
        public int weaponID;
        public float accuracyOffsetMin ;
        public float accuracyOffsetMax ;
        public float fireRate ;
        public GameObject projectilePrefab;
        public AudioClip fireSound;
    }
}
