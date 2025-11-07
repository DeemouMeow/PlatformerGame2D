using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game
{
    public class GameInput
    {
        private InputActions _inputActions;

        private event Action<Vector2> _move;
        private event Action _jump;
        private event Action _interact;
        private event Action _pause;

        public static GameInput Instance { get; private set; }

        public event Action<Vector2> Move
        {
            add
            {
                _move -= value;
                _move += value;
            }
            remove => _move -= value;
        }
        public event Action Jump
        {
            add
            {
                _jump -= value;
                _jump += value;
            }
            remove => _jump -= value;
        }
        public event Action Interact
        {
            add
            {
                _interact -= value;
                _interact += value;
            }
            remove => _interact -= value;
        }
        public event Action Pause
        {
            add
            {
                _pause -= value;
                _pause += value;
            }
            remove => _pause -= value;
        }

        public void Initialize()
        {
            if (Instance is not null)
                return;

            _inputActions = new InputActions();
            _inputActions.Enable();

            SubscribeActions();

            Instance = this;
        }

        public void Shutdown()
        {
            UnsubscribeActions();

            _inputActions.Disable();
            _inputActions = null;

            Instance = null;
        }

        public void Disable()
        {
            _inputActions.Disable();
        }

        private void SubscribeActions()
        {
            var characterMap = _inputActions.Character;

            characterMap.Move.performed += OnMove;
            characterMap.Move.canceled += OnMove;
            characterMap.Jump.performed += OnJump;
            characterMap.Interact.performed += OnInteract;
            characterMap.Pause.performed += OnPause;
        }

        private void UnsubscribeActions()
        {
            var characterMap = _inputActions.Character;

            characterMap.Move.performed -= OnMove;
            characterMap.Move.canceled -= OnMove;
            characterMap.Jump.performed -= OnJump;
            characterMap.Interact.performed -= OnInteract;
            characterMap.Pause.performed -= OnPause;

        }

        private void OnMove(InputAction.CallbackContext callbackContext)
        {
            Vector2 direction = callbackContext.ReadValue<Vector2>();

            _move?.Invoke(direction);
        }

        private void OnJump(InputAction.CallbackContext callbackContext)
        {
            _jump?.Invoke();
        }

        private void OnInteract(InputAction.CallbackContext callbackContext)
        {
            _interact?.Invoke();
        }

        private void OnPause(InputAction.CallbackContext callbackContext)
        {
            _pause?.Invoke();
        }
    }
}
