using UnityEngine;

namespace Veganimus.NovaStar
{
    ///<summary>
    ///@author
    ///Aaron Grincewicz
    ///</summary>
    public class Player_thruster : MonoBehaviour
    {
        [SerializeField] private Animator _anim;
        [SerializeField] private AudioClip _boostSound;
        [SerializeField] private InputReaderSO _inputReader;
        private void OnEnable()=> _inputReader.moveEvent += OnMoveInput;
        
        private void OnDisable()=> _inputReader.moveEvent -= OnMoveInput;
        
        private void Start()=> _anim = GetComponentInChildren<Animator>();

        private void OnMoveInput(float h, float v)
        {
            _anim.SetFloat("horizontal", h);
            //play boost sound
        }
    }
}
