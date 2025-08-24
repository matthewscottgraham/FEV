using DG.Tweening;
using Effects;
using Players;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pegs
{
    public class Peg : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        private int _multiplier;
        private Tween _tween;
        
        private static readonly Color DefaultColor = Color.gray;
        private static readonly Color HighlightColor = Color.cyan;
        private static readonly Color SelectedColor = Color.magenta;
        private static readonly Color DeactivatedColor = Color.yellow;
        
        public PegState PegState { get; private set; }
        public Player Owner { get; private set; }
        public IEffect Effect { get; private set; }
        public Vector2Int Coordinates { get; private set; }

        public void Init(Vector2Int coordinates)
        {
            Coordinates = coordinates;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            SetMaterial();
        }

        public void SetMultiplier(int multiplier)
        {
            _multiplier = multiplier;
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
        
        public void Highlight(bool isHighlighted)
        {
            if (PegState == PegState.Deactivated) return;
            PegState = isHighlighted? PegState.Highlighted : PegState.Normal;
            
            SetMaterial();
            if (PegState == PegState.Highlighted)
            {
                _tween = transform.DOScale(1.5f, 0.2f)
                    .SetEase(Ease.OutCubic);
            }
            else { _tween?.Kill(); }
        }

        public bool Claim(Player player)
        {
            if (PegState == PegState.Deactivated) return false;
            PegState = PegState.Claimed;
            Owner = player;
            _spriteRenderer.sprite = Owner.Icon;
            SetMaterial();
            transform.localScale = Vector3.one * 2f;
            transform.DOScale(Vector3.one * 3, 0.3f)
                .SetEase(Ease.OutCubic)
                .SetLoops(2, LoopType.Yoyo);
            return true;
        }

        public void Deactivate()
        {
            PegState = PegState.Deactivated;
            SetMaterial();
        }
        
        public int GetScore()
        {
            return 1 * _multiplier;
        }
        

        private void SetMaterial()
        {
            _spriteRenderer.color = PegState switch
            {
                PegState.Normal => DefaultColor,
                PegState.Highlighted => HighlightColor,
                PegState.Selected => SelectedColor,
                PegState.Claimed => Owner.Color,
                PegState.Deactivated => DeactivatedColor,
                _ => throw new ArgumentOutOfRangeException()
            };
            transform.localScale = Vector3.one * 0.75f;
        }
    }
}