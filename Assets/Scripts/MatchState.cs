using Tiles;
using Unity.Properties;
using UnityEngine;

namespace FEV
{
    [CreateAssetMenu(menuName = "FEV/MatchState", fileName = "New Match State")]
    public class MatchState : ScriptableObject
    {
        public System.Action OnTileDrawn;
        public System.Action OnTilePlayed;
        
        [CreateProperty] public Vector2Int gridSize = new Vector2Int(12, 12);
        [CreateProperty] public int playerCount = 2;
        public Tile SelectedTile;
        public bool TilesDrawn
        {
            get => _tilesDrawn;
            set
            {
                _tilesDrawn = value;
                OnTileDrawn?.Invoke();
            }
            
        }
        public bool TilesPlayed
        {
            get => _tilesPlayed;
            set
            {
                _tilesPlayed = value;
                OnTilePlayed?.Invoke();
            }
        }
        
        private bool _tilesDrawn;
        private bool _tilesPlayed;
    }
}