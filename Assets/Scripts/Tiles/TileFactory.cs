using Effects;
using Rules;
using System;
using UnityEngine;

namespace Tiles
{
    public class TileFactory
    {
        private static TileShape[] _tileShapes = null;

        public static Tile DrawRandomTile()
        {
            _tileShapes ??= Resources.LoadAll<TileShape>("TileShapes");
            var shape = ScriptableObject.CreateInstance<TileShape>();
            shape.SetData(_tileShapes[UnityEngine.Random.Range(0, _tileShapes.Length)]);
            var ignoredRuleType = RuleFactory.GetRandomIgnoredRuleType();
            return new Tile(shape, ignoredRuleType, EffectFactory.GetRandomEffect());
        }
    }
}