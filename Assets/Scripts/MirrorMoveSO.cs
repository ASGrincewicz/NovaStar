using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Veganimus.NovaStar
{
    ///<summary>
    ///@author
    ///Aaron Grincewicz
    ///</summary>
    [CreateAssetMenu(fileName = "newMovement.asset", menuName = "Movement/ Mirror Movement")]
    public class MirrorMoveSO : MovementSO
    {
        [SerializeField] private InputReaderSO inputReader;

        private void OnEnable() => inputReader.moveEvent += OnMoveInput;

        private void OnDisable() => inputReader.moveEvent -= OnMoveInput;

        public void MirrorMovement(GameObject mover, Rigidbody rigidbody)
        {
            moveDirection = new Vector3(-horizontal, -vertical, 0);
            rigidbody.MovePosition(mover.transform.position + (moveDirection * speed * Time.deltaTime));
        }
        public void Pitch(GameObject mover)
        {
            float pitch = vertical * tiltAngle;
            Quaternion target = Quaternion.Euler(0, mover.transform.rotation.y , pitch);
            mover.transform.rotation = Quaternion.Slerp(mover.transform.rotation, target, Time.deltaTime * smooth);
        }
        public void OnMoveInput(float h, float v)
        {
            horizontal = h;
            vertical = v;
        }
    }
}
