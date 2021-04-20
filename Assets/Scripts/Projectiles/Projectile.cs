using UnityEngine;

namespace Veganimus.NovaStar
{
    ///<summary>
    ///@author
    ///Aaron Grincewicz
    ///</summary>
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private ProjectileTypeSO _projectileType;
       
        private string _target => _projectileType.target;
        private float _speed => _projectileType.speed;
        [SerializeField] private GameObject _impactVFX;
        private void Update()
        {
            Movement();
        }
        private void Movement()
        {
            transform.Translate(_projectileType.moveDirection * _speed * Time.deltaTime);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == _target)
            {
                GameObject impact = Instantiate(_impactVFX, transform.position, Quaternion.identity);
                this.gameObject.SetActive(false);
            }
            else

                return;
            
        }
            private void FixedUpdate()
        {
            if (transform.position.x < -20.0f || transform.position.x > 25.0f)
            {
               
                gameObject.SetActive(false);
            }
        }
       
        
        //Play impact sound
    }
}
