using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ThirdPersonTraversalandCombat.Core.Input
{
    public class InputReader : MonoBehaviour, Input.IPlayerActions
    {
        public event Action OnJumpEvent;
        public event Action OnDodgeEvent;
        public event Action OnTargetEvent;
        public event Action OnCancelEvent;

        private Input input;

        public Vector2 MovementValue { get; private set; }

        private void Start()
        {
            input = new Input();
            input.Player.SetCallbacks(this);
            input.Enable();
        }

        private void OnDestroy()
        {
            input.Disable();
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                OnJumpEvent?.Invoke();
            }
        }

        public void OnDodge(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                OnDodgeEvent?.Invoke();
            }
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            MovementValue = context.ReadValue<Vector2>();
        }

        public void OnLook(InputAction.CallbackContext context)
        {

        }

        public void OnTarget(InputAction.CallbackContext context)
        {
            OnTargetEvent?.Invoke();
        }

        public void OnCancel(InputAction.CallbackContext context)
        {
            OnCancelEvent?.Invoke();
        }
    }
}
