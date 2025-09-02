using DG.Tweening;
using UnityEngine;

namespace Pegs
{
    public struct PegStyle
    {
        public readonly Color Color;
        public readonly Sprite Sprite;
        public readonly float Scale;
        public readonly float TransitionDuration;
        public readonly Ease Ease;
        
        public PegStyle(Color color, Sprite sprite, float scale = 0.8f,
            float transitionDuration = 0.2f, Ease ease = Ease.InOutCubic)
        {
            Color = color;
            Sprite = sprite;
            Scale = scale;
            TransitionDuration = transitionDuration;
            Ease = ease;
        }
    }
}