using Tiles;
using Unity.Properties;
using UnityEngine;

namespace FEV
{
    public class MatchConfiguration
    {
        public System.Action OnTilePlayed;
        
        public Vector2Int GridSize => new Vector2Int(8, 12);
        public int PlayerCount => 2;
        public int MaxPlayerTileCount => 3;

        public Tile SelectedTile;

        public bool TilesPlayed
        {
            get => _tilesPlayed;
            set
            {
                _tilesPlayed = value;
                OnTilePlayed?.Invoke();
            }
        }
        
        private bool _tilesPlayed;
    }
}