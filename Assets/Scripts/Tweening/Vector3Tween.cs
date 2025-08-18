using UnityEngine;

namespace Tweening
{
    public class Vector3Tween : Tween<Vector3>
    {
        private static readonly LerpFunction<Vector3> LerpFunction = Vector3.Lerp;
        public Vector3Tween() : base(LerpFunction) { }
    }
}