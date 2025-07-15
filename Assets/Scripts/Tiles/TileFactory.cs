using System;
using Effects;
using FEV;
using Rules;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Tiles
{
    public class TileFactory : IDisposable
    {
        private TileShape[] _tileShapes = Resources.LoadAll<TileShape>("TileShapes");

        public Tile DrawRandomTile()
        {
            var shape = _tileShapes[UnityEngine.Random.Range(0, _tileShapes.Length)];
            var ignoredRuleType = GetRandomIgnoredRuleType();
            var effect = GetRandomEffect();
            return new Tile(shape, ignoredRuleType, effect);
        }

        public void Dispose()
        {
            _tileShapes = null;
        }

        private Type GetRandomIgnoredRuleType()
        {
            var rand = Random.Range(0, 100);
            return rand switch
            {
                > 90 => typeof(IsTileAdjacentToOwnedPegs),
                > 80 => typeof(IsTileObstructed),
                _ => null
            };
        }

        private IEffect GetRandomEffect()
        {
            var rand = Random.Range(0, 100);
            return rand switch
            {
                > 90 => new ScoreMultiplier(Random.Range(2,4)),
                > 80 => new RadialGrow(Random.Range(1,3)),
                _ => null
            };
        }
    }
}