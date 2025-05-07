using UnityEngine;
using UnityEngine.EventSystems;

namespace FEV
{
    public class Selector : MonoBehaviour
    {
        public static System.Action<Cell> OnFaceHover;
        public static System.Action<Cell, int> OnEdgeHover;
        public static System.Action<Cell, int> OnVertexHover;

        [SerializeField] private Camera camera;
        [SerializeField] private InputController inputController;
        private void Update()
        {
            if (Physics.Raycast(camera.ScreenPointToRay(inputController.CursorPosition), out RaycastHit hit))
            {
                var coordinates = GridUtilities.GetCellCoordinatesFromWorldPosition(hit.point);
                OnFaceHover?.Invoke(new Cell(coordinates.x, coordinates.y));
            }
        }
    }
}