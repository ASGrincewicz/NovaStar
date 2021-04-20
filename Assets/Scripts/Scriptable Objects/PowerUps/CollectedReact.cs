using UnityEngine;

namespace Veganimus.NovaStar
{
    ///<summary>
    ///@author
    ///Aaron Grincewicz
    ///</summary>
    public class CollectedReact : MonoBehaviour
    {
        [SerializeField] private float _destroyDelay;
        private void Start()=> Destroy(this.gameObject, _destroyDelay);
        
    }
}
