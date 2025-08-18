using UnityEngine;

namespace Tweening
{
    public class ColorTween : Tween<Color>
    {
        private static readonly LerpFunction<Color> LerpFunction = Color.Lerp;
        public ColorTween() : base(LerpFunction) { }
    }
}