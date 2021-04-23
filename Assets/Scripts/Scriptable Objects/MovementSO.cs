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
        public float speed;
        [Range(-1,1)]
        public float vertical, horizontal;
        public Vector3 moveDirection;
        public float tiltAngle;
        public float smooth;
    }
}
