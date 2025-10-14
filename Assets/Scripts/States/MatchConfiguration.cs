using UnityEngine;

namespace States
{
    public class MatchConfiguration
    {
        public Vector2Int GridSize => new Vector2Int(18, 12);
        public int PlayerCount => 2;
        public int MaxPlayerTileCount => 3;
        public int StartingPegCount => 3;
    }
}