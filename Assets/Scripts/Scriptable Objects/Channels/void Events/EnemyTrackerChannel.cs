using UnityEngine;
using UnityEngine.Events;

namespace Veganimus.NovaStar
{
    ///<summary>
    ///@author
    ///Aaron Grincewicz
    ///</summary>
    ///
    [CreateAssetMenu(menuName =("Void Event/ Enemy Tracking"))]
    public class EnemyTrackerChannel : VoidEventSO
    {

        public UnityAction OnEnemyDestroyed;
        public UnityAction OnEnemySpawned;

        public void EnemyDestroyedEvent()
        {
            if(OnEnemyDestroyed != null)
             OnEnemyDestroyed.Invoke();
        }
        public void EnemySpawnedEvent()
        {
            if(OnEnemySpawned != null)
             OnEnemySpawned.Invoke();
        }
    }
}
