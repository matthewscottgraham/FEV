using System;
using FEV;
using Players;
using UnityEngine;

namespace Pegs
{
    public class SelectionController : MonoBehaviour, IDisposable
    {
        private PlayerController _playerController;
        private InputController _inputController;
        private PegController _pegController;
        private Camera _camera;
        
        private Vector2Int? _lastHoveredCoordinate;
        private float _lastZoomTime = 0;
        private const float ZoomDelay = 0.5f;
        
        public void Initialize(PlayerController playerController, InputController inputController, PegController pegController)
        {
            _playerController = playerController;
            _inputController = inputController;
            _pegController = pegController;
            _camera = Camera.main;
            
            _inputController.Clicked += HandleCursorClick;
            _inputController.Zoomed += HandleZoom;
        }

        public void Dispose()
        {
            _inputController.Clicked -= HandleCursorClick;
            _inputController.Zoomed -= HandleZoom;
            _playerController = null;
            _inputController = null;
            _pegController = null;
        }
        
        private static Vector2Int GetPegCoordinatesFromWorldPosition(Vector3 worldPosition)
        {
            return new Vector2Int(Mathf.RoundToInt(worldPosition.x + 1f), Mathf.RoundToInt(worldPosition.z + 1f));
        }
        
        private void Update()
        {
            if (_playerController.GetCurrentPlayer().SelectedTile == null) return;
            
            var hoveredPegCoords = GetHoveredPegCoordinates(_inputController.CursorPosition);
            if (hoveredPegCoords == null || hoveredPegCoords == _lastHoveredCoordinate) return;
            
            _pegController.SetHighlight(hoveredPegCoords.Value, _playerController.GetCurrentPlayer().SelectedTile);
            _lastHoveredCoordinate = hoveredPegCoords;
        }

        private Vector2Int? GetHoveredPegCoordinates(Vector2 screenPosition)
        {
            if (Physics.Raycast(_camera.ScreenPointToRay(screenPosition), out RaycastHit hit))
            {
                return GetPegCoordinatesFromWorldPosition(hit.point);
            }
            return null;
        }

        private void HandleCursorClick(Vector2 screenPosition)
        {
            if (_playerController.GetCurrentPlayer().SelectedTile == null) return;
            
            var clickedCoords = GetHoveredPegCoordinates(screenPosition);
            if (clickedCoords != null)
                _pegController.ClaimPegs(clickedCoords.Value, _playerController.GetCurrentPlayer().SelectedTile);
        }

        private void HandleZoom(float delta)
        {
            if (Time.time - _lastZoomTime < ZoomDelay) return;
            _playerController.GetCurrentPlayer().SelectedTile.Rotate(delta >= 0);
            _lastHoveredCoordinate = null;
            _lastZoomTime = Time.time;
        }
    }
}