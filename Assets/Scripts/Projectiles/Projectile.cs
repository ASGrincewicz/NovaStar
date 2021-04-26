using UnityEngine;
using UnityEngine.Rendering.UI;

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
        private string Target => _projectileType.target;
        private float Speed => _projectileType.speed;
        private int DamageAmount => _projectileType.damageAmount;

        private void Update()=> Movement();

        private void FixedUpdate()
        {
            if (transform.position.x < -20.0f || transform.position.x > 25.0f)
                gameObject.SetActive(false);
        }
        private void Movement()=> transform.Translate(_projectileType.moveDirection * (Speed * Time.deltaTime));
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == Target)
            {
                IDamageable obj = other.GetComponentInParent<IDamageable>();
                if (obj != null)
                {
                    obj.Damage(DamageAmount);
                    GameObject impact = _projVFXRequest.RequestGameObject();
                    if (impact != null)
                    {
                        impact.transform.position = transform.position;
                        impact.transform.rotation = transform.rotation;
                        this.gameObject.SetActive(false);
                    }
                    else
                        impact = null;
                }
            }
            else
                return;
        }
    }
}
