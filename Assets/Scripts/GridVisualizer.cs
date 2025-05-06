using UnityEngine;
using UnityEngine.Serialization;

namespace FEV
{
    public class GridVisualizer : MonoBehaviour
    {
        [SerializeField] private Vector2Int gridDimensions;
        [SerializeField] private Vector3 faceSize = new Vector3(0.8f, 0.4f, 0.8f);
        [SerializeField] private float vertexRadius = 0.1f;
        
        GridFactory _gridFactory = new GridFactory();
        Cell[,] _cells;
        
        [ContextMenu("CreateNewGrid")]
        public void CreateNewGrid()
        {
            _cells = _gridFactory.CreateGrid(gridDimensions.x,gridDimensions.y);
        }

        private void OnDrawGizmos()
        {
            if (_cells == null)
                return;
            
            foreach (var cell in _cells)
            {
                Gizmos.color = Color.yellow;
                DrawFace(cell);
                
                Gizmos.color = Color.green;
                DrawEdges(cell);
                
                Gizmos.color = Color.red;
                DrawVertices(cell);
            }
        }

        private void DrawFace(Cell cell)
        {
            Gizmos.DrawCube(GridUtilities.GetCellPosition(cell), faceSize);
        }

        private void DrawEdges(Cell cell)
        {
            Gizmos.DrawCube(GridUtilities.GetEdgePosition(cell, 0), new Vector3(0.2f, 0.2f, 0.8f));
            Gizmos.DrawCube(GridUtilities.GetEdgePosition(cell, 1), new Vector3(0.8f, 0.2f, 0.2f));
            Gizmos.DrawCube(GridUtilities.GetEdgePosition(cell, 2), new Vector3(0.2f, 0.2f, 0.8f));
            Gizmos.DrawCube(GridUtilities.GetEdgePosition(cell, 3), new Vector3(0.8f, 0.2f, 0.2f));
        }

        private void DrawVertices(Cell cell)
        {
            Gizmos.DrawSphere(GridUtilities.GetVertexPosition(cell, 0), vertexRadius);
            Gizmos.DrawSphere(GridUtilities.GetVertexPosition(cell, 1), vertexRadius);
            Gizmos.DrawSphere(GridUtilities.GetVertexPosition(cell, 2), vertexRadius);
            Gizmos.DrawSphere(GridUtilities.GetVertexPosition(cell, 3), vertexRadius);
        }
    }
}