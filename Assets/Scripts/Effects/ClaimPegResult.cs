using Pegs;
using Players;

namespace Effects
{
    public class ClaimPegResult : IEffectResult
    {
        public void AffectPeg(Player player, Peg peg)
        {
            if (peg.Owner == null) peg.Claim(player);
        }
    }
}