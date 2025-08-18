using UnityEngine;

namespace Tweening
{
    public class Vector2Tween : Tween<Vector2>
    {
        private static readonly LerpFunction<Vector2> LerpFunction = Vector2.Lerp;
        public Vector2Tween() : base(LerpFunction) { }
    }
}