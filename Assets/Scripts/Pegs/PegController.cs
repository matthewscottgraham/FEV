using System.Collections.Generic;
using System.Linq;
using FEV;
using Rules;
using Tiles;
using UnityEngine;

namespace Pegs
{
    public class PegController : MonoBehaviour
    {
        private MatchConfiguration _matchConfiguration;
        private PlayerController _playerController;
        private IRule[] _placementRules;
        
        private Peg[,] _pegs;
        private readonly List<Peg> _highlightedPegs = new();
        private readonly List<Peg> _selectedPegs = new();
        
        public void Initialize(MatchConfiguration matchConfiguration, PlayerController playerController)
        {
            PlaceTileCommand.OnConfirmPlaceTile += HandlePlaceTile;
            
            _matchConfiguration = matchConfiguration;
            _playerController = playerController;

            _placementRules = new IRule[]
            {
                new IsTileInBounds(),
                new IsTileObstructed(),
                new IsTileTouchingOwnedPegs()
            };

            var pegFactory = gameObject.AddComponent<PegFactory>();
            pegFactory.Initialize();
            _pegs = pegFactory.CreatePegs(matchConfiguration.gridSize.x, matchConfiguration.gridSize.y);
        }

        public void SetHighlight(Vector2Int coordinates, Tile tile)
        {
            ClearHighlight();
            
            if (tile == null) return;
            if (!IsValidCoordinate(coordinates, tile)) return;
            
            var pegs = GetTilePegs(coordinates, tile);
            foreach (var peg in pegs)
            {
                peg.Highlight(true);
                _highlightedPegs.Add(peg);
            }
        }

        public void SetSelected(Vector2Int coordinates, Tile tile)
        {
            ClearSelected();
            
            if (tile == null) return;
            if (!IsValidCoordinate(coordinates, tile)) return;

            var pegs = GetTilePegs(coordinates, tile);
            foreach (var peg in pegs)
            {
                peg.Select(true);
                _selectedPegs.Add(peg);
            }
            
            _matchConfiguration.TilesPlayed = true;
        }

        private List<Peg> GetTilePegs(Vector2Int coordinates, Tile tile)
        {
            var dimensions = tile.Shape.GetShapeDimensions();
            var pegs = new List<Peg>();
            
            for (int y = 0; y < dimensions.y; y++)
            {
                for (int x = 0; x < dimensions.x; x++)
                {
                    if (!tile.Shape.GetValue(x,y)) continue;
                    pegs.Add(_pegs[coordinates.x + x, coordinates.y + y]);
                }
            }
            return pegs;
        }

        private void OnDestroy()
        {
            PlaceTileCommand.OnConfirmPlaceTile -= HandlePlaceTile;
        }

        private void ClearHighlight()
        {
            foreach (var peg in _highlightedPegs)
            {
                peg.Highlight(false);
            }
            _highlightedPegs.Clear();
        }
        private void ClearSelected()
        {
            foreach (var peg in _selectedPegs)
            {
                peg.Select(false);
            }
            _selectedPegs.Clear();
        }
        
        private bool IsValidCoordinate(Vector2Int coordinates, Tile tile)
        {
            return _placementRules.All(rule => rule.IsSatisfied(coordinates, tile, _pegs));
        }

        private void HandlePlaceTile()
        {
            var player = _playerController.GetCurrentPlayer();
            foreach (var peg in _selectedPegs)
            {
                peg.Claim(player);
            }
            ClearSelected();
            CalculateScores();
        }

        private void CalculateScores()
        {
            var scores = new Dictionary<Player, int>();
            foreach (var peg in _pegs)
            {
                if (!peg.Owner) continue;
                if (!scores.TryAdd(peg.Owner, 1))
                {
                    scores[peg.Owner] += 1;
                }
            }
            
            _playerController.SetScores(scores);
        }
    }
}