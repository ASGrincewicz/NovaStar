using UnityEngine;
namespace Grincewicz.Verify
{
    public static class Verify
    {
        private static string _verifiedText;
        public static string TextToVerify
        {
            get => _verifiedText;
            set
            {
                foreach (var word in _bannedWords)
                {
                    if (value.ToLower().Contains(word))
                    {
                        _verifiedText = "Invalid";
                        Debug.Log("Banned word detected.");
                    }
                    else
                        _verifiedText = value;
                }
            }
        }
        private static string[] _bannedWords = { "fuck", "cunt", "ass", "cock", "pussy", "bitch", "dick", "asshole" };
    }
}
