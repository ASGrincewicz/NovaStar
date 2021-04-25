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
        public int HighScore { get; set; }
        public int Currency { get; set; }
        public string PlayerName { get; set; }

        public SaveData(PlayerStats playerStats)
        {
            HighScore = playerStats.HighScore;
            Currency = playerStats.Currency;
            PlayerName = playerStats.PlayerName;
        }
    }
}