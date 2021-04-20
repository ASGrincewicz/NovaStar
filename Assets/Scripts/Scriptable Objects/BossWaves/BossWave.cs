using UnityEngine;

namespace Veganimus.NovaStar
{
    ///<summary>
    ///@author
    ///Aaron Grincewicz
    ///</summary>
    [CreateAssetMenu(fileName = "newEnemyWave.asset", menuName = "Scriptable Objects/ Boss Wave")]
    public class BossWave : ScriptableObject
    {
        public GameObject levelBoss;
        public int levelNumber;
    }
}
