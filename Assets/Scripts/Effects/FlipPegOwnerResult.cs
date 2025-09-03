using Pegs;
using Players;

namespace Effects
{
    public class FlipPegOwnerResult : IEffectResult
    {
        public override string ToString()
        {
            return "Swizzles peg owner";
        }
        public void AffectPeg(Player player, Peg peg)
        {
            // TODO: get next player
            //peg.Claim();
        }
    }
}