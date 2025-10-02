using System;
using System.Text;
using Commands;
using Effects;
using Rules;

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

        public void Rotate(int direction)
        {
            Shape.Rotate(direction);
            OnTileSelected?.Invoke(this);
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
            if (_ignoredRule != null) sb.AppendLine("Ignore: " + RuleFactory.GetRuleText(_ignoredRule));
            if (Effect != null) sb.AppendLine(Effect.ToString());
            return sb.ToString();
        }
    }
}