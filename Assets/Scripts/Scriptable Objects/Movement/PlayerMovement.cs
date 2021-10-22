using UnityEngine;

namespace Veganimus.NovaStar
{
    ///<summary>
    ///@author
    ///Aaron Grincewicz
    ///</summary>
    ///
    [CreateAssetMenu(fileName = "newMovement.asset", menuName = "Movement/ PlayerMovement")]
    public class PlayerMovement : MovementSO
    {
       [SerializeField] private InputReaderSO inputReader;

        private void OnEnable()=> inputReader.moveEvent += OnMoveInput;
        
        private void OnDisable()=> inputReader.moveEvent -= OnMoveInput;

        private void OnMoveInput(float h, float v)
        {
            horizontal = h;
            vertical = v;
        }

        public void  Movement(Transform mover, Rigidbody rigidbody, float deltaTime)
        {
            moveDirection = new Vector3(horizontal, vertical,0);
            rigidbody.MovePosition(mover.localPosition + (moveDirection * speed * deltaTime));
        }
        public void Pitch(Transform mover, float deltaTime)
        {
            float pitch = vertical * tiltAngle;
            Quaternion target = Quaternion.Euler(0, 0, pitch);
            mover.rotation = Quaternion.Slerp(mover.rotation, target, deltaTime * smooth);
        }
    }
}
