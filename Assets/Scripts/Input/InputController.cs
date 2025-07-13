using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace FEV
{
    public class InputController : MonoBehaviour, IDisposable
    {
        public Action<Vector2> Moved;
        public Action<float> Zoomed;
        public Action<Vector2> Clicked;
        
        private EventSystem _eventSystem;
        private InputSystem_Actions _inputSystem;

        // TODO this needs to function from a touch or mouse
        public Vector2 CursorPosition => Mouse.current.position.ReadValue();
        
        public void Initialize()
        {
            _eventSystem = EventSystem.current;
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
            
            _inputSystem.Disable();
        }

        private bool IsPointerOverUIObject()
        {
            var pointerEventData = new PointerEventData(_eventSystem)
            {
                position = Pointer.current.position.value
            };
            var results = new List<RaycastResult>();
            _eventSystem.RaycastAll(pointerEventData, results);
            return results.Count > 0;
        }
        
        private void OnClick(InputAction.CallbackContext context)
        {
            if (IsPointerOverUIObject()) return;
            
            Clicked?.Invoke(context.ReadValue<Vector2>());
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
