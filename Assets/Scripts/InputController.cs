using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FEV
{
    public class InputController : MonoBehaviour, IDisposable
    {
        public Action<Vector2> Moved;
        public Action<float> Zoomed;
        public Action Clicked;
        
        private InputSystem_Actions _inputSystem;

        // TODO this needs to function from a touch or mouse
        public Vector2 CursorPosition => Mouse.current.position.ReadValue();
        
        public void Initialize()
        {
            _inputSystem = new InputSystem_Actions();
            _inputSystem.Enable();
            
            _inputSystem.Player.Move.performed += OnMove;
            _inputSystem.Player.Zoom.performed += OnZoom;
            _inputSystem.Player.Attack.performed += OnClick;
        }

        public void Dispose()
        {
            _inputSystem.Player.Move.performed -= OnMove;
            _inputSystem.Player.Zoom.performed -= OnZoom;
            _inputSystem.Player.Attack.performed -= OnClick;
        }

        private void OnClick(InputAction.CallbackContext context)
        {
            Clicked?.Invoke();
        }

        void OnMove(InputAction.CallbackContext context)
        {
            var move = context.ReadValue<Vector2>();
            Moved?.Invoke(move);
        }

        void OnZoom(InputAction.CallbackContext context)
        {
            var zoom = context.ReadValue<Vector2>();
            Zoomed?.Invoke(zoom.y);
        }
    }
}
