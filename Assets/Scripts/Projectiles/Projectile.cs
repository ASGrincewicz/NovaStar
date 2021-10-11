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
        public ProjectileTypeSO projectileType;
        [SerializeField] private GameObject _impactVFX;
        [SerializeField] private PoolGORequest _projVFXRequest;
        private string Target => projectileType.target;
        private float Speed => projectileType.speed;
        private int DamageAmount => projectileType.damageAmount;
        private Transform _transform;
        private float _deltaTime;

        private void Start() => _transform = transform;

        private void Update()
        {
            _deltaTime = Time.deltaTime * Speed;
            Movement();
        }

        private void FixedUpdate()
        {
            if (_transform.position.x < -20.0f || _transform.position.x > 25.0f)
                gameObject.SetActive(false);
        }
        private void Movement()=> _transform.Translate(projectileType.moveDirection *  _deltaTime);
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == Target)
            {
                IDamageable obj = other.GetComponentInParent<IDamageable>();
                if (obj != null)
                {
                    obj.Damage(DamageAmount);
                    GameObject impact = _projVFXRequest.RequestGameObjectInt(2);
                    if (impact != null)
                    {
                        impact.transform.position = _transform.position;
                        impact.transform.rotation = _transform.rotation;
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
