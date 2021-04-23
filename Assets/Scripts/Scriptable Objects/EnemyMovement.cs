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
        public void Movement(GameObject mover)
        {
            moveDirection = new Vector3(horizontal, vertical,0) * speed * Time.deltaTime;
            mover.transform.Translate(moveDirection);
        }
    }
}
