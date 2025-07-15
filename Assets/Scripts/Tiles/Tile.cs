using System;
using Commands;
using Effects;
using FEV;

namespace Tiles
{
    public class Tile : ICommand
    {
        public static Action<Tile> OnTileSelected;
        private readonly Type _ignoredRule = null;
        private IEffect _effect = null;
        
        public TileShape Shape { get; private set; }
        public string Label => Shape.name;
        public bool CanIgnoreAnyRule => _ignoredRule != null;
        public bool HasEffect => _effect != null;
        
        public Tile(TileShape shape, Type ignoredRule = null, IEffect effect = null)
        {
            Shape = shape;
            _ignoredRule = ignoredRule;
            _effect = effect;
        }
        
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