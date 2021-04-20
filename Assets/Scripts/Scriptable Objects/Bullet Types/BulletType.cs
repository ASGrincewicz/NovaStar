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
        public string bulletName;
        public float speed;
        public bool isPlayerBullet;
        public int damage;
        public int power;
        public GameObject impactVFXPrefab;
       // public List<Color> colors = new List<Color>() {Color.yellow, Color.red, Color.green,Color.blue, Color.magenta};
    }
}
