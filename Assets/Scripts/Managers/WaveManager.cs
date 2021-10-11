using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Veganimus.NovaStar
{
    ///<summary>
    ///@author
    ///Aaron Grincewicz
    ///</summary>
    public class WaveManager : MonoBehaviour
    {
       
        [SerializeField]
        private List<EnemyWave> _enemyWaves;
        [SerializeField]
        private EnemyWave _activeWave;
       
        [SerializeField]
        private int _currentWave = 0;
        public static Action levelComplete;
       
        private void OnEnable()
        {
           
        }
        private void Update()
        {
            
        }
       
        void NextWave()
        {
            if (_currentWave < _enemyWaves.Count)
            {
                _currentWave++;
            }
            else if(_currentWave >= _enemyWaves.Count)
            {
                levelComplete();
            }
        }
        public EnemyWave RequestEnemyWave()
        {
            return _enemyWaves[_currentWave];
        }
    }
}
