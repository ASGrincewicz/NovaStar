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
        [SerializeField] PlayerMovement _playerMovementSO;

        private void Start()=> _rigidbody = GetComponent<Rigidbody>();
        
        private void FixedUpdate()
        {
           _playerMovementSO.Movement(this.gameObject,_rigidbody);
           _playerMovementSO.Pitch(this.gameObject);
        }
    }
}
