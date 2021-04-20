using UnityEngine;
using Veganimus.GDHQcert;
namespace Veganimus
{
    ///<summary>
    ///@author
    ///Aaron Grincewicz
    ///</summary>
    public class WallTest : MonoBehaviour, IDamageable
    {
        
        private void OnCollisionEnter(Collision other)
        {
            if (other.transform.CompareTag("Bullet"))
            {
                other.gameObject.SetActive(false);
            }
           
        }

        public void Damage(IAttacker attacker)
        {
            Debug.Log("Damage Taken" + attacker.DamageAmount);
        }
    }
}
