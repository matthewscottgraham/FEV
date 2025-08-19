using Players;
using UnityEngine;
using DG.Tweening;

namespace Pegs
{
    public class Peg : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        private bool _isHighlighted;
        private Player _player;
        private int _multiplier;
        private Tween _tween;
        
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
            _isHighlighted = isHighlighted;
            SetMaterial();
            if (_isHighlighted)
            {
                _tween = transform.DOScale(1.5f, 0.2f)
                    .SetEase(Ease.OutCubic);
            }
            else { _tween?.Kill(); }
        }

        public void Claim(Player player)
        {
            _player = player;
            SetMaterial();
            transform.localScale = Vector3.one * 2f;
            transform.DOScale(Vector3.one * 3, 0.3f)
                .SetEase(Ease.OutCubic)
                .SetLoops(2, LoopType.Yoyo);
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
            if (_player)
            {
                _spriteRenderer.sprite = _player.Icon;
                _spriteRenderer.color = _isHighlighted ? Color.cyan : _player.Color;
                return;
            }
            
            if (_isHighlighted)
            {
                _spriteRenderer.color = Color.cyan;
            }
            else
            {
                _spriteRenderer.color = Color.gray;
                transform.localScale = Vector3.one * 0.75f;
            }
        }
    }
}