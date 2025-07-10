using Players;
using UnityEngine;

namespace Pegs
{
    public class Peg : MonoBehaviour
    {
        private static readonly int BaseColor = Shader.PropertyToID("_BaseColor");
        private MeshRenderer _meshRenderer;
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
        private void OnEnable()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
        }

        private void SetMaterial()
        {
            if (_player != null)
            {
                _meshRenderer.material.SetColor(BaseColor, _player.Color);
                transform.localScale = Vector3.one * 0.5f;
                return;
            }
            
            if (_isSelected)
            {
                _meshRenderer.material.SetColor(BaseColor, Color.white);
                transform.localScale = Vector3.one * 0.3f;
            }
            else if (_isHighlighted)
            {
                _meshRenderer.material.SetColor(BaseColor, Color.cyan);
                transform.localScale = Vector3.one * 0.2f;
            }
            else
            {
                _meshRenderer.material.SetColor(BaseColor, Color.gray);
                transform.localScale = Vector3.one * 0.1f;
            }
        }
    }
}