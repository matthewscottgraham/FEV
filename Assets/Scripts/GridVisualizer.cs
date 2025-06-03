using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace FEV
{
    public class GridVisualizer : MonoBehaviour
    {
        [SerializeField] private Vector2Int gridDimensions;
        [SerializeField] private Material gridMaterial;
        private Vector3 _faceSize = new Vector3(0.7f, 0.2f, 0.7f);
        private float _vertexRadius = 0.1f;
        
        private readonly GridFactory _gridFactory = new GridFactory();
        private Cell[,] _cells;
        
        [ContextMenu("CreateNewGrid")]
        public void CreateNewGrid()
        {
            _cells = _gridFactory.CreateGrid(gridDimensions.x,gridDimensions.y);
            CreateLines();
        }

        private void Start()
        {
            CreateNewGrid();
        }

        private void CreateLines()
        {
            if (_cells == null)
                return;

            for (int x = 0; x <= _cells.GetLength(1); x++)
            {
                DrawHorizontalEdge(x);
            }
            for (int y = 0; y <= _cells.GetLength(0); y++)
            {
                DrawVerticalEdge(y);
            }
        }

        private void DrawHorizontalEdge(int index)
        {
            var lineRenderer = CreateLineObject();
            lineRenderer.SetPosition(0, new Vector3(0, 0, 0));
            lineRenderer.SetPosition(1, new Vector3(_cells.GetLongLength(0), 0, 0));
            lineRenderer.transform.position = new Vector3(-0.5f, 0, index - 0.5f);
        }
        
        private void DrawVerticalEdge(int index)
        {
            var lineRenderer = CreateLineObject();
            lineRenderer.SetPosition(0, new Vector3(0, 0, 0));
            lineRenderer.SetPosition(1, new Vector3(0, 0, _cells.GetLongLength(1)));
            lineRenderer.transform.position = new Vector3(index - 0.5f, 0, - 0.5f);
        }

        private LineRenderer CreateLineObject()
        {
            var lineObject = new GameObject();
            lineObject.transform.SetParent(this.transform);
            
            var lineRenderer = lineObject.AddComponent<LineRenderer>();
            lineRenderer.useWorldSpace = false;
            lineRenderer.widthMultiplier = 0.1f;
            lineRenderer.material = gridMaterial;
            
            return lineRenderer;
        }
    }
}