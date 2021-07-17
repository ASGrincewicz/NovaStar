using UnityEngine;

namespace Veganimus.NovaStar
{
    ///<summary>
    ///@author
    ///Aaron Grincewicz
    ///</summary>
    ///
    [CreateAssetMenu(menuName =("Projectile Type"))]
    public class ProjectileTypeSO :ScriptableObject
    {
        public bool isMultiShot;
        public int damageAmount;
        public float speed;
        public string projectileName, target;
        public Vector2 moveDirection;
    }
}
