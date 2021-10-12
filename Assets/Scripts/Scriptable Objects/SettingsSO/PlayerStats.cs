using UnityEngine;
using Grincewicz.Verify;
namespace Veganimus.NovaStar
{

    ///<summary>
    ///@author
    ///Aaron Grincewicz
    ///</summary>
    [CreateAssetMenu(menuName =("Settings/ Player Records"))]
    public class PlayerStats : ScriptableObject
    {
        [SerializeField] private int _checkpoint;
        [SerializeField] private int _currency;
        [SerializeField] private int _highScore;
        [SerializeField] private int _kills;
        [SerializeField] private int _powerUps;
        [SerializeField] private int _recentScore;
        [SerializeField] private int _spawns;
        [SerializeField] private int _upgrades;
        [SerializeField] private string _playerName;
        public int Checkpoint { get => _checkpoint; set => _checkpoint = value; }
        public int Currency { get => _currency; set => _currency = value; }
        public int HighScore { get => _highScore; set => _highScore = value; }
        public int Kills { get => _kills; set => _kills = value; }
        public int PowerUps { get => _powerUps; set => _powerUps = value; }
        public int RecentScore { get => _recentScore; set => _recentScore = value; }
        public int Spawns { get => _spawns; set => _spawns = value; }
        public int Upgrades { get => _upgrades; set => _upgrades = value; }
        public string PlayerName
        {
            get => _playerName;
            set
            {
                Verify.TextToVerify = value;
                Debug.Log($"Verifying...{value}");
                value = Verify.TextToVerify;
            }
        }

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
