using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Project.Scripts.Input
{
    [CreateAssetMenu(fileName = "InputReader", menuName = "ScriptableObjects/Input/InputReader", order = 0)]
    public class InputReader : ScriptableObject, InputSystem_Actions.IPlayerActions
    {
        private InputSystem_Actions inputActions;
        public event Action<Vector2> Move;
        public event Action Jump;
        public event Action Attack;
        public void Initialize()
        {
            if (inputActions == null)
            {
                inputActions = new InputSystem_Actions();
                inputActions.Player.SetCallbacks(this);
                // inputActions.UI.SetCallbacks(this);
            }
            EnablePlayerActions();
        }
        public void EnablePlayerActions()
        {
            inputActions.UI.Disable();
            inputActions.Player.Enable();
        }
        public void EnableUIActions()
        {
            inputActions.Player.Disable();
            inputActions.UI.Enable();
        }
        public void OnMove(InputAction.CallbackContext context)
        {
            Move?.Invoke(context.ReadValue<Vector2>());
        }

        public void OnLook(InputAction.CallbackContext context)
        {
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Attack?.Invoke();
            }
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
        }

        public void OnCrouch(InputAction.CallbackContext context)
        {
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Jump?.Invoke();
            }
        }

        public void OnPrevious(InputAction.CallbackContext context)
        {
        }

        public void OnNext(InputAction.CallbackContext context)
        {
        }

        public void OnSprint(InputAction.CallbackContext context)
        {
        }
    }
}