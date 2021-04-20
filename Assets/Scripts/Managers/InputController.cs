using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;


namespace Veganimus.NovaStar
{
    ///<summary>
    ///@author
    ///Aaron Grincewicz
    ///</summary>
    ///
    [Serializable] public class MoveInputEvent: UnityEvent<float, float> { }
   
    [Serializable] public class PauseInputEvent: UnityEvent { }
    public class InputController : MonoBehaviour
    {
        private Controls _controls;
        public MoveInputEvent moveInputEvent;
        public PauseInputEvent pauseInputEvent;
        void Awake()
        {
            _controls = new Controls();
        }
        void OnEnable()
        {
            _controls.Player.Enable();
            _controls.Player.Movement.performed += OnMove;
            _controls.Player.Movement.canceled += OnMove;
            _controls.Player.Pause.performed += OnPause;
        }
        
        private void OnMove(InputAction.CallbackContext obj)
        {
            Vector2 moveInput = obj.ReadValue<Vector2>();
            moveInputEvent.Invoke(moveInput.x, moveInput.y);
        }
        private void OnPause(InputAction.CallbackContext obj)
        {
            pauseInputEvent.Invoke();
        }
        private void OnDisable()
        {
            _controls.Player.Movement.performed -= OnMove;
            _controls.Player.Movement.canceled -= OnMove;
            _controls.Player.Pause.performed -= OnPause;
        }
    }
}
