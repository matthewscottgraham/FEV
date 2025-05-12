using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace FEV
{
    public class GridVisualizer : MonoBehaviour
    {
        [SerializeField] private Vector2Int gridDimensions;
        private Vector3 _faceSize = new Vector3(0.7f, 0.2f, 0.7f);
        private float _vertexRadius = 0.1f;
        
        private readonly GridFactory _gridFactory = new GridFactory();
        private Cell[,] _cells;
        
        [ContextMenu("CreateNewGrid")]
        public void CreateNewGrid()
        {
            _cells = _gridFactory.CreateGrid(gridDimensions.x,gridDimensions.y);
        }

        private void Start()
        {
            CreateNewGrid();
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
            Gizmos.DrawWireCube(GridUtilities.GetCellPosition(cell), _faceSize);
        }

        private void DrawEdges(Cell cell)
        {
            Gizmos.DrawWireCube(GridUtilities.GetEdgePosition(cell, 0), new Vector3(0.2f, 0.2f, 0.8f));
            Gizmos.DrawWireCube(GridUtilities.GetEdgePosition(cell, 1), new Vector3(0.8f, 0.2f, 0.2f));
            Gizmos.DrawWireCube(GridUtilities.GetEdgePosition(cell, 2), new Vector3(0.2f, 0.2f, 0.8f));
            Gizmos.DrawWireCube(GridUtilities.GetEdgePosition(cell, 3), new Vector3(0.8f, 0.2f, 0.2f));
        }

        private void DrawVertices(Cell cell)
        {
            Gizmos.DrawWireSphere(GridUtilities.GetVertexPosition(cell, 0), _vertexRadius);
            Gizmos.DrawWireSphere(GridUtilities.GetVertexPosition(cell, 1), _vertexRadius);
            Gizmos.DrawWireSphere(GridUtilities.GetVertexPosition(cell, 2), _vertexRadius);
            Gizmos.DrawWireSphere(GridUtilities.GetVertexPosition(cell, 3), _vertexRadius);
        }
    }
}