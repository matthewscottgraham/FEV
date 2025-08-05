using Tiles;
using Unity.Properties;
using UnityEngine;

namespace FEV
{
    public class MatchConfiguration
    {
        public Vector2Int GridSize => new Vector2Int(8, 12);
        public int PlayerCount => 2;
        public int MaxPlayerTileCount => 3;
    }
}