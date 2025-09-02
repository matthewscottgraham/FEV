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

        public void AddEffect(IEffect effect, Sprite sprite)
        {
            Effect = effect;
            _spriteRenderer.sprite = sprite;
        }

        public void ConsumeEffect(Player player, List<Peg> pegs)
        {
            if (Effect == null) return;
            Effect.Apply(player, pegs);
            Effect = null;
        }
        
        public void Highlight(bool isHighlighted, bool ignoreClaimedPegs)
        {
            if (PegState is PegState.Deactivated) return;
            if (PegState is PegState.Claimed && !ignoreClaimedPegs) return;
            
            if (isHighlighted)
            {
                PegState = PegState.Highlighted;
            }
            else
            {
                PegState = Owner ? PegState.Claimed : PegState.Normal;
            }
            
            SetMaterial();
        }

        public bool Claim(Player player)
        {
            if (PegState == PegState.Deactivated) return false;
            PegState = PegState.Claimed;
            Owner = player;
            SetMaterial();
            return true;
        }

        public void Unclaim()
        {
            if (PegState == PegState.Deactivated) return;
            PegState = PegState.Normal;
            Owner = null;
            SetMaterial();
        }

        public void Deactivate()
        {
            PegState = PegState.Deactivated;
            SetMaterial();
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