using Pegs;
using Players;

namespace Effects
{
    public interface IEffectResult
    {
        public void AffectPeg(Player player, Peg peg);
    }
}