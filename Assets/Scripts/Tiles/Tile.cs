using System;
using System.Text;
using Commands;
using Effects;
using FEV;

namespace Tiles
{
    public class Tile : ICommand
    {
        public static Action<Tile> OnTileSelected;
        private readonly Type _ignoredRule = null;
        
        public TileShape Shape { get; private set; }
        public string Label => Shape.name;
        public bool CanIgnoreAnyRule => _ignoredRule != null;
        public bool HasEffect => Effect != null;
        public IEffect Effect {get;}
        
        public Tile(TileShape shape, Type ignoredRule = null, IEffect effect = null)
        {
            Shape = shape;
            _ignoredRule = ignoredRule;
            Effect = effect;
        }
        
        public bool CanTileIgnoreRule(Type type)
        {
            return _ignoredRule != null && _ignoredRule == type;
        }
        
        public void Execute()
        {
            OnTileSelected?.Invoke(this);
        }

        public void Destroy()
        {
            Shape = null;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            if (_ignoredRule != null) sb.AppendLine(_ignoredRule.ToString());
            if (Effect != null) sb.AppendLine(Effect.ToString());
            return sb.ToString();
        }
    }
}