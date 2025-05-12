using UnityEngine;

namespace FEV
{
    public struct Cell
    {
        public Cell(int x, int y)
        {
            Coordinates = new Vector2Int(x, y);
        }
        public Cell(Vector2Int coordinates)
        {
            Coordinates = coordinates;
        }
        
        public Vector2Int Coordinates { get; private set; }
    }
}