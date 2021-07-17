using UnityEngine;


namespace Veganimus.NovaStar
{
    ///<summary>
    ///@author
    ///Aaron Grincewicz
    ///</summary>
    [CreateAssetMenu(fileName = "newEnemyClass.asset", menuName = "Scriptable Objects/ EnemyClass")]
    public class EnemyClass : ScriptableObject
    {
        public bool hasShield, hasWeapon;
        public int hp, scoreTier, shieldHP;
        public float speed, fireRate;
        public string enemyName;
        public AudioClip damageSound, deathSound, shootSound;
        public GameObject explosionPrefab, itemDrop, weapon;
    }
}
