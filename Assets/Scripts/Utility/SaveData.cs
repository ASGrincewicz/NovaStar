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
        private int _currency;
        private int _highScore;
        private string _playerName;
        public int Currency { get=> _currency; private set=> _currency = value; }
        public int HighScore { get=> _highScore; private set=> _highScore = value; }
        public string PlayerName { get=> _playerName; private set => _playerName = value; }

        public SaveData(PlayerStats playerStats)
        {
            _currency = playerStats.Currency;
            _highScore = playerStats.HighScore;
            _playerName = playerStats.PlayerName;
        }
    }
}