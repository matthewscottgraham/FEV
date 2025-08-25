using Pegs;
using Players;

namespace Effects
{
    public class UnclaimPegResult : IEffectResult
    {
        public void AffectPeg(Player player, Peg peg)
        {
            if (peg.Owner != null) peg.Unclaim();
        }
    }
}