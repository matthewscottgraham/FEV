using Players;
using UnityEngine;

namespace Pegs
{
    public class Peg : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        private bool _isHighlighted;
        private Player _player;
        private int _multiplier;
        
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
        }

        public void Claim(Player player)
        {
            _player = player;
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
            if (_player)
            {
                _spriteRenderer.sprite = _player.Icon;
                _spriteRenderer.color = _isHighlighted ? Color.cyan : _player.Color;
                transform.localScale = Vector3.one * 2f;
                return;
            }
            
            if (_isHighlighted)
            {
                _spriteRenderer.color = Color.cyan;
                transform.localScale = Vector3.one * 0.75f;
            }
            else
            {
                _spriteRenderer.color = Color.gray;
                transform.localScale = Vector3.one * 0.75f;
            }
        }
    }
}