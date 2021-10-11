using UnityEngine;
using Veganimus.GDHQcert;
using Veganimus.NovaStar;

namespace Veganimus
{
    ///<summary>
    ///@author
    ///Aaron Grincewicz
    ///</summary>
    public class WallTest : MonoBehaviour, IDamageable
    {
        [SerializeField] private int _hp;

        private void OnCollisionEnter(Collision other)
        {
            if (other.transform.CompareTag("Bullet"))
             other.gameObject.SetActive(false);
        }
        public void Damage(int amount) => _hp -= amount;
    }
}
