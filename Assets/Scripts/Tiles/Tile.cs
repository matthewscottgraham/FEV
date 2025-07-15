using System;
using Commands;
using FEV;
using Rules;
using UnityEngine;

namespace Tiles
{
    public class Tile : ICommand
    {
        public static Action<Tile> OnTileSelected;
        public Tile(TileShape shape, Type ignoredRule = null)
        {
            Shape = shape;
            _ignoredRule = ignoredRule;
            if (_ignoredRule != null)
                Debug.Log(_ignoredRule.Name);
        }

        public TileShape Shape { get; private set; }
        public string Label => Shape.name;
        
        private readonly Type _ignoredRule = null;
        
        public bool CanTileIgnoreRule(Type type)
        {
            return _ignoredRule != null && _ignoredRule == type;
        }

        public string GetIgnoredRuleLabel()
        {
            if (_ignoredRule == null)
                return "";
            return "X";
        }
        public void Execute()
        {
            OnTileSelected?.Invoke(this);
        }

        public void Destroy()
        {
            Shape = null;
        }
    }
}