using UnityEngine;

namespace FEV
{
    public class Selector : MonoBehaviour
    {
        public static System.Action<Cell> OnFaceHover;
        public static System.Action<Cell, int> OnEdgeHover;
        public static System.Action<Cell, int> OnVertexHover;
        public static System.Action OnNullHover;

        [SerializeField] private Camera cam;
        [SerializeField] private InputController inputController;
        
        [SerializeField] private float distanceThreshold = 0.2f;
        
        private void Update()
        {
            if (Physics.Raycast(cam.ScreenPointToRay(inputController.CursorPosition), out RaycastHit hit))
            {
                var coordinates = GridUtilities.GetCellCoordinatesFromWorldPosition(hit.point);
                var cell = new Cell(coordinates);

                if (IsCursorOverVertex(cell, hit.point)) return;
                if (IsCursorOverEdge(cell, hit.point)) return;
                
                OnFaceHover?.Invoke(cell);
            }
            else
            {
                OnNullHover?.Invoke();
            }
        }

        private bool IsCursorOverEdge(Cell cell, Vector3 cursorPosition)
        {
            for (int i = 0; i < 4; i++)
            {
                var distance = Vector3.Distance(GridUtilities.GetEdgePosition(cell, i), cursorPosition);
                if (distance < distanceThreshold)
                {
                    OnEdgeHover(cell, i);
                    return true;
                }
            }

            return false;
        }

        private bool IsCursorOverVertex(Cell cell, Vector3 cursorPosition)
        {
            for (int i = 0; i < 4; i++)
            {
                var distance = Vector3.Distance(GridUtilities.GetVertexPosition(cell, i), cursorPosition);
                if (distance < distanceThreshold)
                {
                    OnVertexHover(cell, i);
                    return true;
                }
            }

            return false;
        }
    }
}