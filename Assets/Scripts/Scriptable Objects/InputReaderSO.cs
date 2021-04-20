using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Veganimus
{
    ///<summary>
    ///@author
    ///Aaron Grincewicz
    ///</summary>
    [CreateAssetMenu(menuName = "InputReader")]
    public class InputReaderSO : ScriptableObject
    {
        public UnityAction<float, float> moveEvent;

        public UnityAction<float> rollEvent;

        public UnityAction shootEvent;
       
        public UnityAction pauseEvent;

        private Controls _controls;

        private void OnEnable()
        {
            _controls = new Controls();
            _controls.Player.Enable();
            _controls.Player.Movement.performed += OnMove;
            _controls.Player.Movement.canceled += OnMove;
           // _controls.Player.Roll.performed += OnRoll;
            _controls.Player.Shoot.performed += OnShoot;
            _controls.Player.Pause.performed += OnPause;

        }

        
        private void OnDisable()
        {
            _controls.Player.Disable();
            _controls.Player.Movement.performed -= OnMove;
            _controls.Player.Movement.canceled -= OnMove;
            _controls.Player.Shoot.performed -= OnShoot;
            _controls.Player.Pause.performed -= OnPause;
        }

        public void OnMove(InputAction.CallbackContext obj)
        {
            Vector2 moveInput = obj.ReadValue<Vector2>();
            if (moveEvent != null)
                moveEvent.Invoke(moveInput.x, moveInput.y);
        }

        private void OnShoot(InputAction.CallbackContext obj)
        {
            if(shootEvent != null)
            shootEvent.Invoke();
        }
        public void OnPause(InputAction.CallbackContext obj)
        {
            if (pauseEvent != null)
             pauseEvent.Invoke();
        }

    }
}
