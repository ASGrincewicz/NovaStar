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
        public string PlayerName { get; set; }
        public int HighScore { get; set; }
        public int RecentScore { get; set; }
        public int Currency { get; set; }
        public int Kills { get; set; }
        public int Spawns { get; set; }
        public int Upgrades { get; set; }
        public int PowerUps { get; set; }

        public void ResetRecords()
        {
            PlayerName = " ";
            HighScore = 0;
            RecentScore = 0;
        }
        public void NewGame()
        {
            Kills = 0;
            Spawns = 0;
            Upgrades = 0;
            PowerUps = 0;
        }
    }
}
