using System.Collections.Generic;
using System.Linq;
using Players;
using Rules;
using States;
using Tiles;
using UnityEngine;

namespace Pegs
{
    public class PegController : MonoBehaviour
    {
        private PlayerController _playerController;
        private IRule[] _placementRules;
        
        private readonly List<Peg> _highlightedPegs = new();
        
        public void Initialize(MatchConfiguration matchConfiguration, PlayerController playerController)
        {
            StateMachine.OnStateChanged += CalculateScores;
            
            _playerController = playerController;

            _placementRules = new IRule[]
            {
                new IsTileInBounds(),
                new IsTileObstructed(),
                new IsTileAdjacentToOwnedPegs(_playerController)
            };

            var pegFactory = gameObject.AddComponent<PegFactory>();
            pegFactory.Initialize(matchConfiguration.GridSize);

            ClaimInitialPegs(matchConfiguration.PlayerCount, matchConfiguration.GridSize);
        }

        public bool CanTileBePlaced(Tile tile)
        {
            var unclaimedPegs = Board.Instance.GetUnclaimedPegs();
            if (unclaimedPegs.Length == 0) return false;
            foreach (var peg in unclaimedPegs)
            {
                if (IsValidCoordinate(peg.Coordinates, tile)) return true;
            }
            return false;
        }
        
        public void SetHighlight(Vector2Int coordinates, Tile tile)
        {
            ClearHighlight();
            
            if (tile == null) return;
            if (!IsValidCoordinate(coordinates, tile)) return;
            
            var pegs = GetTilePegs(coordinates, tile);
            foreach (var peg in pegs)
            {
                peg.Highlight(true, tile.CanTileIgnoreRule(typeof(IsTileObstructed)));
                _highlightedPegs.Add(peg);
            }
        }

        public void ClaimPegs(Vector2Int coordinates, Tile tile)
        {
            ClearHighlight();
            
            if (tile == null) return;
            if (!IsValidCoordinate(coordinates, tile)) return;

            var pegs = GetTilePegs(coordinates, tile);
            var currentPlayer = _playerController.GetCurrentPlayer();
            foreach (var peg in pegs)
            {
                peg.Claim(currentPlayer);
                peg.ConsumeEffect(currentPlayer, pegs);
            }
            _playerController.RemoveTile(tile, currentPlayer);
            tile.Effect?.Apply(currentPlayer, pegs);
        }

        private List<Peg> GetTilePegs(Vector2Int coordinates, Tile tile)
        {
            var dimensions = tile.Shape.GetShapeDimensions();
            var pegs = new List<Peg>();
            
            var offset = dimensions / 2;
            for (int y = 0; y < dimensions.y; y++)
            {
                for (int x = 0; x < dimensions.x; x++)
                {
                    if (!tile.Shape.GetValue(x,y)) continue;
                    pegs.Add(Board.Instance.GetPeg(coordinates.x + x - offset.x, coordinates.y + y - offset.y));
                }
            }
            return pegs;
        }

        private void OnDestroy()
        {
            StateMachine.OnStateChanged -= CalculateScores;
        }

        private void ClaimInitialPegs(int playerCount, Vector2Int boardSize)
        {
            for (var playerIndex = 0; playerIndex < playerCount; playerIndex++)
            {
                var player = _playerController.GetPlayer(playerIndex);
                var claimed = false;
                while (!claimed)
                {
                    // get random peg coordinate that is not around the border
                    var randomX = Random.Range(3, boardSize.x - 3);
                    var randomY = Random.Range(3, boardSize.y - 3);
                    
                    var peg = Board.Instance.GetPeg(randomX, randomY);
                    if (peg.Owner != null) continue;
                    peg.Claim(player);
                    claimed = true;
                }
            }
        }

        private void ClearHighlight()
        {
            foreach (var peg in _highlightedPegs)
            {
                peg.Highlight(false, false);
            }
            _highlightedPegs.Clear();
        }
        
        private bool IsValidCoordinate(Vector2Int coordinates, Tile tile)
        {
            return _placementRules.All(rule => rule.IsSatisfied(coordinates, tile));
        }

        private void CalculateScores()
        {
            if (StateMachine.CurrentState.GetType() != typeof(EndTurnState))
                return;
            
            _playerController.UpdateScores(Board.Instance.CalculateScores());
        }
    }
}