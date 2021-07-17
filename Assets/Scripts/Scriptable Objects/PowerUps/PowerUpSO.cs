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
        public int powerUpID;
        public float speed;
        public string powerUpName;
        public AudioClip collectedSound;
        public GameObject powerUpPrefab, colectedAnimPrefab;
    }
}
