using Pegs;
using Players;

namespace Effects
{
    public class DisablePegResult : IEffectResult
    {
        public override string ToString()
        {
            return "Disables";
        }
        
        public void AffectPeg(Player player, Peg peg)
        {
            peg.Deactivate();
        }
    }
}