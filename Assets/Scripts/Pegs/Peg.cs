using DG.Tweening;
using Effects;
using Players;
using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Pegs
{
    public class Peg : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        private Tween _tween;
        private float _randomDelay = 0.1f;
        private bool _isDeactivated = false;

        public int Score { get; set; } = 1;
        public PegState PegState { get; private set; }
        public Player Owner { get; private set; }
        public IEffect Effect { get; private set; }
        public Vector2Int Coordinates { get; private set; }

        public void Init(Vector2Int coordinates)
        {
            Coordinates = coordinates;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _randomDelay = UnityEngine.Random.Range(0f, 0.1f);
            SetMaterial();
        }

        public void AddEffect(IEffect effect)
        {
            Effect = effect;
            SetState();
            SetMaterial();
        }

        public void ConsumeEffect(Player player, List<Peg> pegs)
        {
            if (Effect == null) return;
            Effect.Apply(player, pegs);
            Effect = null;
            SetState();
            SetMaterial();
        }
        
        public void Highlight(bool isHighlighted, bool isValidHighlight)
        {
            //if (PegState is PegState.Deactivated) return;
            SetState(isHighlighted, isValidHighlight);
            SetMaterial();
        }

        public bool Claim(Player player)
        {
            if (PegState == PegState.Deactivated) return false;
            Owner = player;
            SetState();
            SetMaterial();
            return true;
        }

        public void Unclaim()
        {
            if (PegState == PegState.Deactivated) return;
            Owner = null;
            SetState();
            SetMaterial();
        }

        public void Deactivate()
        {
            _isDeactivated = true;
            SetState();
            SetMaterial();
        }

        private void SetState(bool isHighlighted = false, bool isValidHighlight = false)
        {
            switch (isHighlighted)
            {
                case true when !isValidHighlight:
                    PegState = PegState.Invalid;
                    break;
                case true when Effect != null:
                    PegState = PegState.HighlightedEffect;
                    break;
                case true:
                    PegState = PegState.Highlighted;
                    break;
                default:
                {
                    if (_isDeactivated) PegState = PegState.Deactivated;
                    else if (Owner) PegState = PegState.Claimed;
                    else if (Effect != null) PegState = PegState.Effect;
                    else PegState = PegState.Normal;
                    break;
                }
            }
        }
        
        private void SetMaterial()
        {
            var pegStyle = PegFactory.GetStyle(PegState);
            if (PegState == PegState.Claimed) pegStyle = Owner.PegStyle;
            if (pegStyle == null) return;

            var style = pegStyle.Value;
            
            _spriteRenderer.color = style.Color;
            _spriteRenderer.sprite = style.Sprite;
            
            _tween?.Kill();
            _tween = transform.DOScale(style.Scale, style.TransitionDuration)
                .SetDelay(_randomDelay).SetEase(style.Ease);
        }
    }
}