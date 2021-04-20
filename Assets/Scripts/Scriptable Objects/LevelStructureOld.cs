using System.Collections.Generic;
using UnityEngine;


namespace Veganimus.NovaStar
{
    ///<summary>
    ///@author
    ///Aaron Grincewicz
    ///</summary>

    public class LevelStructureOld : ScriptableObject
    {
        public int levelNumber;
        public List<EnemyWave> _waveSequence;
        public GameObject levelBoss;
    }
}
