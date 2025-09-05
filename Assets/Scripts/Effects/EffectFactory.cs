using Random = UnityEngine.Random;

namespace Effects
{
    public static class EffectFactory
    {
        public static IEffect GetRandomEffect()
        {
            var random = Random.Range(0, 100);
            return random switch
            {
                > 90 => new ScoreMultiplier(2),
                > 80 => new RadialGrow(GetEffectResult(), 1),
                > 75 => new HorizontalFill(GetEffectResult()),
                > 70 => new VerticalFill(GetEffectResult()),
                > 65 => new DrawTileEffect(),
                _ => null
            };
        }

        private static IEffectResult GetEffectResult()
        {
            var random = Random.Range(0, 100);
            return random switch
            {
                > 90 => new UnclaimPegResult(),
                > 80 => new FlipPegOwnerResult(),
                > 70 => new DisablePegResult(),
                _ => new ClaimPegResult()
            };
        }
    }
}