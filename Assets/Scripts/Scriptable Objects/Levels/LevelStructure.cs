using System.Collections.Generic;
using UnityEngine;


namespace Veganimus.NovaStar
{
    ///<summary>
    ///@author
    ///Aaron Grincewicz
    ///</summary>
    [CreateAssetMenu(fileName = "newLevel.asset", menuName = "Scriptable Objects/ Level Structure")]
    public class LevelStructure : ScriptableObject
    {
        public int levelNumber;
        public List<EnemyWave> _waveSequence;
        public BossWave bossWave;
    }
}
