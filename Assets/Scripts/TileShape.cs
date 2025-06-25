using UnityEngine;
using System.Linq;

namespace FEV
{
    [CreateAssetMenu(fileName = "TileShape", menuName = "FEV/TileShape")]
    public class TileShape : ScriptableObject
    {
        [TextArea, SerializeField] private string shape;

        public Vector2Int GetShapeDimensions()
        {
            var dimensions = new Vector2Int();
            var rows = shape.Split("\n");

            if (rows == null || rows.Length <= 0) return dimensions;
            
            dimensions.x = rows[0].Length;
            dimensions.y = rows.Length;

            return dimensions;
        }

        public bool GetValue(int x, int y)
        {
            var rows = shape.Split("\n");

            if (rows == null || rows.Length <= 0) return false;
            if (y < 0 || y >= rows.Length) return false;
            if (x < 0 || x >= rows[y].Length) return false;
            return rows[y][x] == 'X';
        }
    }
}