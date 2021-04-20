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
        public string projectileName;
        public float speed;
        public string target;
        public Vector2 moveDirection;
    }
}
