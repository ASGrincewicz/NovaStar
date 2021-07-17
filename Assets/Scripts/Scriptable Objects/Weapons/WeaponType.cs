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
        public bool isMultiShot;
        public int weaponID;
        public float accuracyOffsetMin, accuracyOffsetMax, fireRate;
        public string weaponName;
        public AudioClip fireSound;
        public GameObject projectilePrefab;
    }
}
