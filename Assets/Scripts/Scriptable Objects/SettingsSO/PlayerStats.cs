using UnityEngine;


namespace Veganimus.NovaStar
{
    ///<summary>
    ///@author
    ///Aaron Grincewicz
    ///</summary>
    [CreateAssetMenu(menuName =("Settings/ Player Records"))]
    public class PlayerStats : ScriptableObject
    {
        public int HighScore { get; set; }
        public int RecentScore { get; set; }
        public int Currency { get; set; }
        public int Kills { get; set; }
        public int Spawns { get; set; }
        public int Upgrades { get; set; }
        public int PowerUps { get; set; }

        public void ResetRecords()
        {
            HighScore = 0;
            RecentScore = 0;
        }
    }
}
