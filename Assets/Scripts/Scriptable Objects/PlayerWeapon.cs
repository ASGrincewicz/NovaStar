using UnityEngine;
namespace Veganimus.NovaStar
{
    ///<summary>
    ///@author
    ///Aaron Grincewicz
    ///</summary>
    [CreateAssetMenu(fileName = "newEnemyWave.asset", menuName = "Scriptable Objects/ Player Weapon")]
    public class PlayerWeapon : ScriptableObject
    { 
        public int damage, weaponID;
        public float fireRate;
        public string weaponName;
        public GameObject projectile;
    }
}
