using UnityEngine;

namespace Veganimus.NovaStar
{
    ///<summary>
    ///@author
    ///Aaron Grincewicz
    ///</summary>

    public class Movement :MonoBehaviour
    {
        private Rigidbody _rigidbody;
        private float _deltaTime;
        private Transform _transform;
        [SerializeField] PlayerMovement _playerMovementSO;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _transform = transform;
        }

        private void Update() => _deltaTime = Time.deltaTime;

        private void FixedUpdate()
        {
           _playerMovementSO.Movement(_transform,_rigidbody,_deltaTime);
           _playerMovementSO.Pitch(_transform, _deltaTime);
        }
    }
}
