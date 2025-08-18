using UnityEngine;

namespace Tweening
{
    public class QuaternionTween : Tween<Quaternion>
    {
        private static readonly LerpFunction<Quaternion> LerpFunction = Quaternion.Lerp;
        public QuaternionTween() : base(LerpFunction) { }
    }
}