using UnityEngine;

namespace Veganimus.NovaStar
{
    ///<summary>
    ///@author
    ///Aaron Grincewicz
    ///</summary>

    public class Movement :MonoBehaviour
    {
        [SerializeField] PlayerMovement _playerMovementSO;
        private float _deltaTime;
        private Rigidbody _rigidbody;
        private Transform _transform;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _transform = transform;
        }

        private void FixedUpdate()
        {
            _playerMovementSO.Movement(_transform, _rigidbody, _deltaTime);
            _playerMovementSO.Pitch(_transform, _deltaTime);
        }

        private void Update() => _deltaTime = Time.deltaTime;

       
    }
}
