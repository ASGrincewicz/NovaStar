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
        [SerializeField] private GameObject _impactVFX;
        [SerializeField] private PoolGORequest _projVFXRequest;
        private string _target => _projectileType.target;
        private float _speed => _projectileType.speed;

        private void Update()=> Movement();

        private void FixedUpdate()
        {
            if (transform.position.x < -20.0f || transform.position.x > 25.0f)
                gameObject.SetActive(false);
        }

        private void Movement()=> transform.Translate(_projectileType.moveDirection * _speed * Time.deltaTime);
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == _target)
            {
                GameObject impact = _projVFXRequest.RequestGameObject();
                impact.transform.position = transform.position;
                impact.transform.rotation = transform.rotation;
                this.gameObject.SetActive(false);
            }
            else
                return;
        }
    }
}
