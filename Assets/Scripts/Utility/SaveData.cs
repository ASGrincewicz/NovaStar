namespace Veganimus.NovaStar
{
    ///<summary>
    ///@author
    ///Aaron Grincewicz
    ///</summary>
    ///
    [System.Serializable]
    public class SaveData
    {
        public int Currency { get; set; }
        public int HighScore { get; set; }
        public string PlayerName { get; set; }

        public SaveData(PlayerStats playerStats)
        {
            Currency = playerStats.Currency;
            HighScore = playerStats.HighScore;
            PlayerName = playerStats.PlayerName;
        }
    }
}