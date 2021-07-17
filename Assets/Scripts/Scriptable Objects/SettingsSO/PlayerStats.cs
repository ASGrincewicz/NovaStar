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
        public int Checkpoint { get; set; }
        public int Currency { get; set; }
        public int HighScore { get; set; }
        public int Kills { get; set; }
        public int PowerUps { get; set; }
        public int RecentScore { get; set; }
        public int Spawns { get; set; }
        public int Upgrades { get; set; }
        public string PlayerName { get; set; }

        public void NewGame()
        {
            Kills = 0;
            PowerUps = 0;
            Spawns = 0;
            Upgrades = 0;
        }

        public void ResetRecords()
        {
            Currency = 0;
            HighScore = 0;
            PlayerName = " ";
            RecentScore = 0;
        }
    }
}
