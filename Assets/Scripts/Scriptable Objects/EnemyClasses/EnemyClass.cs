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
        public string enemyName;
        public int scoreTier;
        public int hp;
        public int shieldHP;
        public float speed;
        public float fireRate;
        public bool hasShield;
        public bool hasWeapon;
        public GameObject weapon;
        public GameObject itemDrop;
        public GameObject explosionPrefab;
        public AudioClip shootSound;
    }
}
