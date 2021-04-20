using System.Collections;
using UnityEngine;
namespace Veganimus.NovaStar
{
    ///<summary>
    ///@author
    ///Aaron Grincewicz
    ///</summary>
    public class ImpactVFX : MonoBehaviour
    {
        private void OnEnable()
        {
            StartCoroutine(SetInactive());
        }
        private IEnumerator SetInactive()
        {
            yield return new WaitForSeconds(.05f);
            this.gameObject.SetActive(false);
        }
    }
}
