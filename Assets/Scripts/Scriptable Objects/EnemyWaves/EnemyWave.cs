using System.Collections.Generic;
using UnityEngine;


namespace Veganimus.NovaStar
{
    ///<summary>
    ///@author
    ///Aaron Grincewicz
    ///</summary>
    [CreateAssetMenu(fileName = "newEnemyWave.asset", menuName = "Scriptable Objects/ Enemy Wave")]
    public class EnemyWave : ScriptableObject
    {
        public List<Enemy> enemySequence;
       
        public int waveNumber;
    }
}
