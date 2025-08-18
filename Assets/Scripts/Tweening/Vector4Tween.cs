using UnityEngine;

namespace Tweening
{
    public class Vector4Tween : Tween<Vector4>
    {
        private static readonly LerpFunction<Vector4> LerpFunction = Vector4.Lerp;
        public Vector4Tween() : base(LerpFunction) { }
    }
}