using UnityEngine;

namespace Pegs
{
    public class PegShadow : MonoBehaviour
    {
        private GameObject _shadow;
    
        public void Initialize(SpriteRenderer spriteRenderer)
        {
            _shadow = new GameObject("Shadow");
            _shadow.transform.SetParent(transform);
            _shadow.transform.localRotation = Quaternion.identity;
            _shadow.transform.localPosition = Vector3.zero;
            
            var shadowSpriteRenderer = _shadow.AddComponent<SpriteRenderer>();
            shadowSpriteRenderer.sprite = spriteRenderer.sprite;
            shadowSpriteRenderer.color = Color.black;
            shadowSpriteRenderer.sortingLayerName = spriteRenderer.sortingLayerName;
            shadowSpriteRenderer.sortingOrder = spriteRenderer.sortingOrder - 1;
        }

    }
}
