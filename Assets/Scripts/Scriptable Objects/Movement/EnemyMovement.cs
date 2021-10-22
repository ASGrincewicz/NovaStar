using UnityEngine;

namespace Veganimus.NovaStar
{
    ///<summary>
    ///@author
    ///Aaron Grincewicz
    ///</summary>
    [CreateAssetMenu(fileName = "newMovement.asset", menuName = "Movement/ AIMovement")]
    public class EnemyMovement : MovementSO
    {
        public void Movement(Transform mover, float deltaTime)
        {
            moveDirection = new Vector3(horizontal, vertical,0) * speed * deltaTime;
            mover.Translate(moveDirection);
        }
    }
}
