namespace Tweening
{
    public class FloatTween : Tween<float>
    {
        private static float LerpFloat(float start, float end, float progress) { return start + (end - start) * progress; }
        private static readonly LerpFunction<float> LerpFunction = LerpFloat;
        public FloatTween() : base(LerpFunction) { }
    }
}