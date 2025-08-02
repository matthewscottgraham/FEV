using System.Collections.Generic;
using System.Linq;
using Commands;
using FEV;
using Players;
using Rules;
using States;
using Tiles;
using UnityEditor.ProjectWindowCallback;
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
        
        public void Initialize(MatchConfiguration matchConfiguration, PlayerController playerController)
        {
            StateMachine.OnStateChanged += CalculateScores;
            
            _matchConfiguration = matchConfiguration;
            _playerController = playerController;

            _placementRules = new IRule[]
            {
                new IsTileInBounds(),
                new IsTileObstructed(),
                new IsTileAdjacentToOwnedPegs(_playerController)
            };

            var pegFactory = gameObject.AddComponent<PegFactory>();
            pegFactory.Initialize();
            _pegs = pegFactory.CreatePegs(matchConfiguration.GridSize.x, matchConfiguration.GridSize.y);

            ClaimInitialPegs();
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

        public void ClaimPegs(Vector2Int coordinates, Tile tile)
        {
            if (tile == null) return;
            if (!IsValidCoordinate(coordinates, tile)) return;

            var pegs = GetTilePegs(coordinates, tile);
            var currentPlayer = _playerController.GetCurrentPlayer();
            foreach (var peg in pegs)
            {
                peg.Claim(currentPlayer);
            }
            _playerController.RemoveTile(tile, currentPlayer);
            StateMachine.NextState();
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
            StateMachine.OnStateChanged -= CalculateScores;
        }

        private void ClaimInitialPegs()
        {
            for (var playerIndex = 0; playerIndex < _matchConfiguration.PlayerCount; playerIndex++)
            {
                var player = _playerController.GetPlayer(playerIndex);
                var claimed = false;
                while (!claimed)
                {
                    // get random peg coordinate that is not around the border
                    var randomX = Random.Range(3, _pegs.GetLength(0) - 3);
                    var randomY = Random.Range(3, _pegs.GetLength(1) - 3);

                    if (_pegs[randomX, randomY].Owner != null) continue;
                    _pegs[randomX, randomY].Claim(player);
                    claimed = true;
                }
            }
        }

        private void ClearHighlight()
        {
            foreach (var peg in _highlightedPegs)
            {
                peg.Highlight(false);
            }
            _highlightedPegs.Clear();
        }
        
        private bool IsValidCoordinate(Vector2Int coordinates, Tile tile)
        {
            return _placementRules.All(rule => rule.IsSatisfied(coordinates, tile, _pegs));
        }

        private void CalculateScores()
        {
            if (StateMachine.CurrentState.GetType() != typeof(EndTurnPhase))
                return;
            
            var scores = new Dictionary<Player, int>();
            foreach (var peg in _pegs)
            {
                if (!peg.Owner) continue;
                if (!scores.TryAdd(peg.Owner, peg.GetScore()))
                {
                    scores[peg.Owner] += peg.GetScore();
                }
            }
            
            _playerController.UpdateScores(scores);
        }
    }
}