using Effects;
using Rules;
using System;
using UnityEngine;

namespace Tiles
{
    public class TileFactory : IDisposable
    {
        private TileShape[] _tileShapes = Resources.LoadAll<TileShape>("TileShapes");

        public Tile DrawRandomTile()
        {
            var shape = _tileShapes[UnityEngine.Random.Range(0, _tileShapes.Length)];
            var ignoredRuleType = RuleFactory.GetRandomIgnoredRuleType();
            return new Tile(shape, ignoredRuleType, EffectFactory.GetRandomEffect());
        }

        public void Dispose()
        {
            _tileShapes = null;
        }
    }
}