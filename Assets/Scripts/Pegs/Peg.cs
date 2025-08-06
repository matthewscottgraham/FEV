using Players;
using UnityEngine;

namespace Pegs
{
    public class Peg : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        private bool _isHighlighted;
        private bool _isSelected;
        private Player _player;
        
        public Player Owner => _player;
        
        public void Highlight(bool isHighlighted)
        {
            if (_isSelected) return;
            _isHighlighted = isHighlighted;
            SetMaterial();
        }

        public void Select(bool isSelected)
        {
            _isSelected = isSelected;
            if (_isSelected) _isHighlighted = false;
            SetMaterial();
        }

        public void Claim(Player player)
        {
            _player = player;
            SetMaterial();
        }

        public int GetScore()
        {
            return 1;
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
                _spriteRenderer.color = _player.Color;
                transform.localScale = Vector3.one * 2f;
                return;
            }
            
            if (_isSelected)
            {
                _spriteRenderer.color = Color.white;
                transform.localScale = Vector3.one * 0.75f;
            }
            else if (_isHighlighted)
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