using Pegs;
using Players;

namespace Effects
{
    public class DisablePegResult : IEffectResult
    {
        public void AffectPeg(Player player, Peg peg)
        {
            peg.Deactivate();
        }
    }
}