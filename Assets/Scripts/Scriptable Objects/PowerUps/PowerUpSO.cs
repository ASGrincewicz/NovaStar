using UnityEngine;

namespace Veganimus.NovaStar
{
    ///<summary>
    ///@author
    ///Aaron Grincewicz
    ///</summary>
    ///
    [CreateAssetMenu(menuName = "PowerUp Type")]
    public class PowerUpSO : ScriptableObject
    {
        public string powerUpName;
        public int powerUpID;
        public float speed;
        public GameObject powerUpPrefab;
        public GameObject colectedAnimPrefab;
        public AudioClip collectedSound;
    }
}
