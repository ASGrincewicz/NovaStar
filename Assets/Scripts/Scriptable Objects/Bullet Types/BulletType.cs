using UnityEngine;


namespace Veganimus.GDHQcert
{
    ///<summary>
    ///@author
    ///Aaron Grincewicz
    ///</summary>
    [CreateAssetMenu(fileName = "newBulletType.asset", menuName = "Scriptable Objects/ Bullet Type")]
    public class BulletType : ScriptableObject
    {
        public bool isPlayerBullet;
        public int damage, power;
        public float speed;
        public string bulletName;
        public GameObject impactVFXPrefab;
    }
}
