using System;
using UnityEngine;

namespace FEV
{
    public static class GridUtilities
    {
        public static Vector2Int GetCellCoordinatesFromWorldPosition(Vector3 worldPosition)
        {
            return new Vector2Int(Mathf.RoundToInt(worldPosition.x), Mathf.RoundToInt(worldPosition.z));
        }

        public static Vector3 GetCellPosition(Cell cell)
        {
            return new Vector3(cell.Coordinates.x, 0, cell.Coordinates.y);
        }

        /// <summary>
        /// Vertex indices are arranged clockwise from the north-east
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="vertexIndex"></param>
        /// <returns></returns>
        public static Vector3 GetVertexPosition(Cell cell, int vertexIndex)
        {
            var position = GetCellPosition(cell);
            return vertexIndex switch
            {
                0 => position + new Vector3(0.5f, 0, 0.5f),
                1 => position + new Vector3(0.5f, 0, -0.5f),
                2 => position + new Vector3(-0.5f, 0, -0.5f),
                3 => position + new Vector3(-0.5f, 0, 0.5f),
                _ => throw new IndexOutOfRangeException()
            };
        }
        
        /// <summary>
        /// Edge indices are arranged clockwise from the east
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="edgeIndex"></param>
        /// <returns></returns>
        public static Vector3 GetEdgePosition(Cell cell, int edgeIndex)
        {
            var position = GetCellPosition(cell);
            return edgeIndex switch
            {
                0 => position + new Vector3(0.5f, 0, 0),
                1 => position + new Vector3(0, 0, -0.5f),
                2 => position + new Vector3(-0.5f, 0, 0),
                3 => position + new Vector3(0, 0, 0.5f),
                _ => throw new IndexOutOfRangeException()
            };
        }
    }
}