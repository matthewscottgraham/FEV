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
                > 90 => new RadialGrow(GetEffectResult(), 1),
                > 85 => new HorizontalFill(GetEffectResult()),
                > 80 => new VerticalFill(GetEffectResult()),
                > 75 => new DrawTileEffect(),
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