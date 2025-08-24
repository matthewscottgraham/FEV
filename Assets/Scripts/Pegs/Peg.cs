using System;
using Players;
using UnityEngine;
using DG.Tweening;

namespace Pegs
{
    public class Peg : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        private PegState _pegState;
        private Player _player;
        private int _multiplier;
        private Tween _tween;
        
        private static readonly Color DefaultColor = Color.gray;
        private static readonly Color HighlightColor = Color.cyan;
        private static readonly Color SelectedColor = Color.magenta;
        private static readonly Color DeactivatedColor = Color.black;
        
        public PegState PegState => _pegState;
        public Player Owner => _player;
        public Vector2Int Coordinates { get; private set; }

        public void Init(Vector2Int coordinates)
        {
            Coordinates = coordinates;
        }

        public void SetMultiplier(int multiplier)
        {
            _multiplier = multiplier;
        }
        
        public void Highlight(bool isHighlighted)
        {
            if (_pegState == PegState.Deactivated) return;
            _pegState = isHighlighted? PegState.Highlighted : PegState.Normal;
            
            SetMaterial();
            if (_pegState == PegState.Highlighted)
            {
                _tween = transform.DOScale(1.5f, 0.2f)
                    .SetEase(Ease.OutCubic);
            }
            else { _tween?.Kill(); }
        }

        public bool Claim(Player player)
        {
            if (_pegState == PegState.Deactivated) return false;
            _pegState = PegState.Claimed;
            _player = player;
            _spriteRenderer.sprite = _player.Icon;
            SetMaterial();
            transform.localScale = Vector3.one * 2f;
            transform.DOScale(Vector3.one * 3, 0.3f)
                .SetEase(Ease.OutCubic)
                .SetLoops(2, LoopType.Yoyo);
            return true;
        }

        public void Deactivate()
        {
            _pegState = PegState.Deactivated;
            SetMaterial();
        }
        public int GetScore()
        {
            return 1 * _multiplier;
        }
        
        private void OnEnable()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            SetMaterial();
        }

        private void SetMaterial()
        {
            _spriteRenderer.color = _pegState switch
            {
                PegState.Normal => DefaultColor,
                PegState.Highlighted => HighlightColor,
                PegState.Selected => SelectedColor,
                PegState.Claimed => _player.Color,
                PegState.Deactivated => DeactivatedColor,
                _ => throw new ArgumentOutOfRangeException()
            };
            transform.localScale = Vector3.one * 0.75f;
        }
    }
}