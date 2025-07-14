using System;
using Tiles;
using Rules;
using UnityEngine;

namespace FEV
{
    public class TileFactory : IDisposable
    {
        private TileShape[] _tileShapes = Resources.LoadAll<TileShape>("TileShapes");

        public Tile DrawRandomTile()
        {
            var shape = _tileShapes[UnityEngine.Random.Range(0, _tileShapes.Length)];
            var ignoredRuleType = GetIgnoredRuleType();
            return new Tile(shape, ignoredRuleType);
        }

        public void Dispose()
        {
            _tileShapes = null;
        }

        private Type GetIgnoredRuleType()
        {
            var rand = UnityEngine.Random.Range(0, 100);
            return rand switch
            {
                > 80 => typeof(IsTileAdjacentToOwnedPegs),
                > 70 => typeof(IsTileObstructed),
                _ => null
            };
        }
    }
}