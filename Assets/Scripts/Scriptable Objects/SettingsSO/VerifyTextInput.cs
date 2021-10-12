using UnityEngine;
using UnityEngine.UI;
namespace Grincewicz.Verify
{
    public class VerifyTextInput : MonoBehaviour
    {
        [SerializeField] private InputField _textInputField;

        public void InputTextToVerify(string text)
        {
            Verify.TextToVerify = text;
            _textInputField.text = Verify.TextToVerify;
        }
    }
}
