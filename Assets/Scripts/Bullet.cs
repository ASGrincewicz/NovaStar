using System;
using UnityEngine;


namespace Veganimus.GDHQcert
{
    ///<summary>
    ///@author
    ///Aaron Grincewicz
    ///</summary>
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private BulletType _bulletType;
        private string _bulletName => _bulletType.bulletName;
        private float _speed => _bulletType.speed;
        private bool _isPlayerBullet => _bulletType.isPlayerBullet;
        [SerializeField] private static int _damage;
        private int _power => _bulletType.power;
        private GameObject _impactVFX => _bulletType.impactVFXPrefab;
        public static Action bulletDamage;
        
       
       
        void Update()
        {
            Movement();
            
            if(transform.position.x > 10.0f || transform.position.x < -10.0f)
                this.gameObject.SetActive(false);
           
            
        }
        void Movement()
        {
            if(_isPlayerBullet)
                transform.Translate(Vector3.right * _speed * Time.deltaTime);
            
            else
            transform.Translate(Vector3.left * _speed * Time.deltaTime);
            
        }

        public static int SetDamage() => _damage;
        
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Enemy" && _isPlayerBullet == true)
            {
                GameObject impact = Instantiate(_impactVFX, transform.position, Quaternion.identity);
                this.gameObject.SetActive(false);
            }
            else if (other.tag == "Player" && _isPlayerBullet == false)
            {
                GameObject impact = Instantiate(_impactVFX);
                this.gameObject.SetActive(false);
            }
            else if(other.tag == "Bullet")
            {
               var bullet = other.GetComponent<Bullet>();
                if (bullet._power >= this._power)
                {
                    GameObject impact = Instantiate(_impactVFX, transform.position, Quaternion.identity);
                    this.gameObject.SetActive(false);
                }
                else if(bullet._power < _power || bullet.transform.parent.CompareTag( "Player"))
                {
                    return;
                }
            }
           
            //Play impact sound
        }
    }
}
