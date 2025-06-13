using System;
using UnityEngine;

namespace FEV
{
    public class Selector : MonoBehaviour
    {
        [SerializeField] private Camera cam;
        [SerializeField] private InputController inputController;
        
        [SerializeField] private float distanceThreshold = 0.2f;

        private void OnEnable()
        {
            inputController.Clicked += HandleCursorClick;
        }

        private void OnDisable()
        {
            inputController.Clicked -= HandleCursorClick;
        }

        private void Update()
        {
            if (Physics.Raycast(cam.ScreenPointToRay(inputController.CursorPosition), out RaycastHit hit))
            {
                var coordinates = GridUtilities.GetCellCoordinatesFromWorldPosition(hit.point);
                var cell = new Cell(coordinates);

                if (IsCursorOverVertex(cell, hit.point)) return;
                if (IsCursorOverEdge(cell, hit.point)) return;
                if (IsCursorOverFace(cell)) return;
            }

            Blackboard.Instance.ClearHovered();
        }

        private bool IsCursorOverFace(Cell cell)
        {
            if (Blackboard.Instance.FeatureMode != FeatureMode.Face)
                return false;
            
            Blackboard.Instance.SetHovered(cell, 0);
            return true;
        }

        private bool IsCursorOverEdge(Cell cell, Vector3 cursorPosition)
        {
            if (Blackboard.Instance.FeatureMode != FeatureMode.Edge)
                return false;
            
            for (int i = 0; i < 4; i++)
            {
                var distance = Vector3.Distance(GridUtilities.GetEdgePosition(cell, i), cursorPosition);
                if (distance < distanceThreshold)
                {
                    Blackboard.Instance.SetHovered(cell, i);
                    return true;
                }
            }

            return false;
        }

        private bool IsCursorOverVertex(Cell cell, Vector3 cursorPosition)
        {
            if (Blackboard.Instance.FeatureMode != FeatureMode.Vertex)
                return false;
            
            for (int i = 0; i < 4; i++)
            {
                var distance = Vector3.Distance(GridUtilities.GetVertexPosition(cell, i), cursorPosition);
                if (distance < distanceThreshold)
                {
                    Blackboard.Instance.SetHovered(cell, i);
                    return true;
                }
            }

            return false;
        }

        private void HandleCursorClick()
        {
            Blackboard.Instance.SelectHovered();
        }
    }
}