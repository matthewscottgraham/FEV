using Unity.Properties;
using UnityEngine;

namespace FEV
{
    [CreateAssetMenu(menuName = "FEV/MatchState", fileName = "New Match State")]
    public class MatchState : ScriptableObject
    {
        public static System.Action OnTurnUpdate;
        
        [CreateProperty] public Vector2Int GridSize = new Vector2Int(10, 10);
        [CreateProperty] public int PlayerCount = 2;
        public static Tile SelectedTile;
        public static bool TilesDrawn { get; set; }
        public static bool TilesPlayed
        {
            get => _tilesPlayed;
            set
            {
                _tilesPlayed = value;
                OnTurnUpdate?.Invoke();
            }
        }

        private static bool _tilesPlayed;
    }
}