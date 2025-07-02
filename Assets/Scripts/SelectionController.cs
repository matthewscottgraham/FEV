using System;
using UnityEngine;

namespace FEV
{
    public class SelectionController : MonoBehaviour, IDisposable
    {
        private MatchConfiguration _matchConfiguration;
        private InputController _inputController;
        private PegController _pegController;
        private Camera _camera;
        
        private Vector2Int? _lastHoveredCoordinate;
        
        public void Initialize(MatchConfiguration matchConfiguration, InputController inputController, PegController pegController)
        {
            _matchConfiguration = matchConfiguration;
            _inputController = inputController;
            _pegController = pegController;
            _camera = Camera.main;
            
            _inputController.Clicked += HandleCursorClick;
        }

        public void Dispose()
        {
            _inputController.Clicked -= HandleCursorClick;
            _matchConfiguration = null;
            _inputController = null;
            _pegController = null;
        }
        
        private static Vector2Int GetPegCoordinatesFromWorldPosition(Vector3 worldPosition)
        {
            return new Vector2Int(Mathf.RoundToInt(worldPosition.x + 1f), Mathf.RoundToInt(worldPosition.z + 1f));
        }
        
        private void Update()
        {
            if (_matchConfiguration.SelectedTile == null) return;
            
            var hoveredPegCoords = GetHoveredPegCoordinates();
            if (hoveredPegCoords == null || hoveredPegCoords == _lastHoveredCoordinate) return;
            
            _pegController.SetHighlight(hoveredPegCoords.Value, _matchConfiguration.SelectedTile);
            _lastHoveredCoordinate = hoveredPegCoords;
        }

        private Vector2Int? GetHoveredPegCoordinates()
        {
            if (Physics.Raycast(_camera.ScreenPointToRay(_inputController.CursorPosition), out RaycastHit hit))
            {
                return GetPegCoordinatesFromWorldPosition(hit.point);
            }
            return null;
        }

        private void HandleCursorClick()
        {
            if (_matchConfiguration.SelectedTile == null) return;
            
            var hoveredPegCoords = GetHoveredPegCoordinates();
            if (hoveredPegCoords != null) _pegController.SetSelected(hoveredPegCoords.Value, _matchConfiguration.SelectedTile);
        }
    }
}