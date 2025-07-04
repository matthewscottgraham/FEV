using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Pegs;
using Rules;
using Tiles;

namespace FEV
{
    public class PegController : MonoBehaviour
    {
        private MatchConfiguration _matchConfiguration;
        private PlayerController _playerController;
        private IRule[] _placementRules;
        
        
        private readonly Vector3 _pegOffset = new Vector3(-1f, 0, -1f);
        private Peg _pegPrototype;
        private Peg[,] _pegs;
        private List<Peg> _highlightedPegs = new();
        private List<Peg> _selectedPegs = new();
        
        public void Initialize(MatchConfiguration matchConfiguration, PlayerController playerController)
        {
            PlaceTileCommand.OnConfirmPlaceTile += HandlePlaceTile;
            
            _matchConfiguration = matchConfiguration;
            _playerController = playerController;

            _placementRules = new IRule[]
            {
                new IsTileInBounds(),
                new IsTileObstructed() 
            };
            
            CreatePegPrototype();
            CreatePegs(matchConfiguration.gridSize.x, matchConfiguration.gridSize.y);
        }

        public void SetHighlight(Vector2Int coordinates, Tile tile)
        {
            ClearHighlight();
            if (tile == null) return;
            
            var dimensions = tile.Shape.GetShapeDimensions();

            if (_placementRules.Any(rule => !rule.IsSatisfied(coordinates, tile, _pegs)))
            {
                return;
            }
            
            for (int y = 0; y < dimensions.y; y++)
            {
                for (int x = 0; x < dimensions.x; x++)
                {
                    if (!tile.Shape.GetValue(x,y))
                        continue;
                    _pegs[coordinates.x + x - 1, coordinates.y + y - 1].Highlight(true);
                    _highlightedPegs.Add(_pegs[coordinates.x + x - 1, coordinates.y + y - 1]);
                }
            }
        }

        public void SetSelected(Vector2Int coordinates, Tile tile)
        {
            ClearSelected();
            if (tile == null) return;
            
            var dimensions = tile.Shape.GetShapeDimensions();
            if (!IsValidCoordinate(coordinates, dimensions)) return;
            
            for (int y = 0; y < dimensions.y; y++)
            {
                for (int x = 0; x < dimensions.x; x++)
                {
                    if (!tile.Shape.GetValue(x,y))
                        continue;
                    _pegs[coordinates.x + x - 1, coordinates.y + y - 1].Select(true);
                    _selectedPegs.Add(_pegs[coordinates.x + x - 1, coordinates.y + y - 1]);
                }
            }
            
            _matchConfiguration.TilesPlayed = true;
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
        
        private bool IsValidCoordinate(Vector2Int coordinates, Vector2Int dimensions)
        {
            if (coordinates.x < 0 || coordinates.y < 0)
                return false;
            if (coordinates.x + dimensions.x - 1 > _pegs.GetLength(0) 
                || coordinates.y + dimensions.y - 1 > _pegs.GetLength(1))
                return false;
            return true;
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
        
        private void CreatePegPrototype()
        {
            var go = GameObject.CreatePrimitive(PrimitiveType.Cube);
            _pegPrototype = go.AddComponent<Peg>();
            _pegPrototype.name = "Peg";
            _pegPrototype.transform.parent = transform;
            _pegPrototype.transform.position = new Vector3(1000, 0, 0);
            _pegPrototype.transform.localScale = Vector3.one * 0.1f;
            
            Destroy(_pegPrototype.GetComponent<Collider>());
            
            var material = Resources.Load<Material>("Materials/mat_peg");
            _pegPrototype.GetComponent<Renderer>().material = material;
        }
        private void CreatePegs(int gridSizeX, int gridSizeY)
        {
            _pegs = new Peg[gridSizeX, gridSizeY];
            for (int x = 0; x < gridSizeX; x++)
            {
                for (int y = 0; y < gridSizeY; y++)
                {
                    CreatePeg(x,y);
                }
            }
        }
        private void CreatePeg(int x, int y)
        {
            var peg = Instantiate(_pegPrototype, transform);
            peg.transform.position = new Vector3(x, 0, y) + _pegOffset;
            _pegs[x, y] = peg;
        }
    }
}