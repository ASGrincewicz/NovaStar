using UnityEngine;

namespace Veganimus
{
    ///<summary>
    ///@author
    ///Aaron Grincewicz
    ///</summary>
    ///

    public abstract class MovementSO : ScriptableObject
    {
        [Range(-1,1)]
        public float vertical, horizontal;
        public float tiltAngle, smooth, speed;
        public Vector3 moveDirection;
    }
}
