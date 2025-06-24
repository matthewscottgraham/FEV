using System;
using UnityEngine;

namespace FEV
{
    public class Peg : MonoBehaviour
    {
        private static readonly int BaseColor = Shader.PropertyToID("_BaseColor");
        private MeshRenderer _meshRenderer;
        private bool _isHighlighted;
        private bool _isSelected;
        
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
        
        private void OnEnable()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
        }

        private void SetMaterial()
        {
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