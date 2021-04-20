using UnityEngine;

namespace Veganimus.NovaStar
{
    ///<summary>
    ///@author
    ///Aaron Grincewicz
    ///</summary>

    public class Movement :MonoBehaviour
    {
        [SerializeField] private InputReaderSO _inputReader;
        [SerializeField] private float _moveSpeed;
        private float _horizontal;
        private float _vertical;
        private float _tiltAngle = 25f;
        private float _smooth = 5f;
        private Rigidbody _rigidbody;
        private void OnEnable()
        {
            _inputReader.moveEvent += OnMove;
            
        }
        private void OnDisable()
        {
            _inputReader.moveEvent -= OnMove;
        }
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
        private void FixedUpdate()
        {
            Move();
            Pitch();
        }
        private void OnMove(float horizontal, float vertical)
        {
            _horizontal = horizontal;
            _vertical = vertical;
        }
       
        public void Move()
        {
            Vector3 movedirection = new Vector3(_horizontal, _vertical, 0);
            _rigidbody.MovePosition(transform.position + (movedirection * _moveSpeed * Time.deltaTime));
        }
        private void Spin()
        {
            float spin = 180.0f;
            transform.rotation = Quaternion.AngleAxis(spin, Vector3.right);
        }
        void Pitch()
        {
            float pitch = _vertical * _tiltAngle;
            Quaternion target = Quaternion.Euler(0, 0, pitch);
            transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * _smooth);
        }

    }
}
