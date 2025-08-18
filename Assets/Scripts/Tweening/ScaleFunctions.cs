using System;

namespace Tweening
{
    public static class ScaleFunctions
    {
        public static readonly ScaleFunction Linear = LinearImpl;
        public static readonly ScaleFunction QuadraticEaseIn = QuadraticEaseInImpl;
        public static readonly ScaleFunction QuadraticEaseOut = QuadraticEaseOutImpl;
        public static readonly ScaleFunction QuadraticEaseInOut = QuadraticEaseInOutImpl;
        public static readonly ScaleFunction CubicEaseIn = CubicEaseInImpl;
        public static readonly ScaleFunction CubicEaseOut = CubicEaseOutImpl;
        public static readonly ScaleFunction CubicEaseInOut = CubicEaseInOutImpl;
        public static readonly ScaleFunction QuarticEaseIn = QuarticEaseInImpl;
        public static readonly ScaleFunction QuarticEaseOut = QuarticEaseOutImpl;
        public static readonly ScaleFunction QuarticEaseInOut = QuarticEaseInOutImpl;
        public static readonly ScaleFunction QuinticEaseIn = QuinticEaseInImpl;
        public static readonly ScaleFunction QuinticEaseOut = QuinticEaseOutImpl;
        public static readonly ScaleFunction QuinticEaseInOut = QuinticEaseInOutImpl;
        public static readonly ScaleFunction SineEaseIn = SineEaseInImpl;
        public static readonly ScaleFunction SineEaseOut = SineEaseOutImpl;
        public static readonly ScaleFunction SineEaseInOut = SineEaseInOutImpl;

        private const float Pi = (float)Math.PI;
        private const float HalfPi = Pi / 2f;

        private static float LinearImpl(float progress) { return progress; }
        private static float QuadraticEaseInImpl(float progress) { return EaseInPower(progress, 2); }
        private static float QuadraticEaseOutImpl(float progress) { return EaseOutPower(progress, 2); }
        private static float QuadraticEaseInOutImpl(float progress) { return EaseInOutPower(progress, 2); }
        private static float CubicEaseInImpl(float progress) { return EaseInPower(progress, 3); }
        private static float CubicEaseOutImpl(float progress) { return EaseOutPower(progress, 3); }
        private static float CubicEaseInOutImpl(float progress) { return EaseInOutPower(progress, 3); }
        private static float QuarticEaseInImpl(float progress) { return EaseInPower(progress, 4); }
        private static float QuarticEaseOutImpl(float progress) { return EaseOutPower(progress, 4); }
        private static float QuarticEaseInOutImpl(float progress) { return EaseInOutPower(progress, 4); }
        private static float QuinticEaseInImpl(float progress) { return EaseInPower(progress, 5); }
        private static float QuinticEaseOutImpl(float progress) { return EaseOutPower(progress, 5); }
        private static float QuinticEaseInOutImpl(float progress) { return EaseInOutPower(progress, 5); }

        private static float EaseInPower(float progress, int power)
        {
            return (float)Math.Pow(progress, power);
        }

        private static float EaseOutPower(float progress, int power)
        {
            int sign = power % 2 == 0 ? -1 : 1;
            return (float)(sign * (Math.Pow(progress - 1, power) + sign));
        }

        private static float EaseInOutPower(float progress, int power)
        {
            progress *= 2;
            if (progress < 1)
            {
                return (float)Math.Pow(progress, power) / 2f;
            }
            
            int sign = power % 2 == 0 ? -1 : 1;
            return (float)(sign / 2.0 * (Math.Pow(progress - 2, power) + sign * 2));
        }

        private static float SineEaseInImpl(float progress)
        {
            return (float)Math.Sin(progress * HalfPi - HalfPi) + 1;
        }

        private static float SineEaseOutImpl(float progress)
        {
            return (float)Math.Sin(progress * HalfPi);
        }

        private static float SineEaseInOutImpl(float progress)
        {
            return (float)(Math.Sin(progress * Pi - HalfPi) + 1) / 2;
        }
    }
}