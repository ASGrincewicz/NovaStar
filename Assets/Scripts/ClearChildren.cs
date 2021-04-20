using UnityEngine;

namespace Veganimus.NovaStar
{
    ///<summary>
    ///@author
    ///Aaron Grincewicz
    ///</summary>
    public class ClearChildren : MonoBehaviour
    {
        private void OnEnable()=>  PoolManager.clearChildren += ClearChildObjects;
     
        private void OnDisable()=> PoolManager.clearChildren -= ClearChildObjects;
        
        private void ClearChildObjects()
        {
            if (transform.childCount > 0)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    var child = transform.GetChild(i);
                    Destroy(child.gameObject);
                }
            }
        }
    }
}
