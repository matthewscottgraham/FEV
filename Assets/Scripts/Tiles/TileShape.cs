using System;
using UnityEngine;

namespace Tiles
{
    [CreateAssetMenu(fileName = "TileShape", menuName = "FEV/TileShape")]
    public class TileShape : ScriptableObject
    {
        [SerializeField] private Vector2Int dimensions = new Vector2Int(3, 3);
        [SerializeField] private bool[] values = new bool[9];
        private Texture2D _texture = null;
        
        public Vector2Int GetShapeDimensions()
        {
            return dimensions;
        }

        public bool GetValue(int x, int y)
        {
            return values[y * dimensions.x + x];
        }

        public Texture2D GetTexture()
        {
            if (_texture == null)
                CreateTexture();
            return _texture;
        }

        private void CreateTexture()
        {
            var textureSize = 5;
            var texture = new Texture2D(textureSize, textureSize, TextureFormat.ARGB32, false);
            Vector2Int offset = new Vector2Int((textureSize - dimensions.x) / 2, (textureSize - dimensions.y) / 2);
            for (int y = offset.y; y < dimensions.y + offset.y; y++)
            {
                for (int x = offset.x; x < dimensions.x + offset.x; x++)
                {
                    texture.SetPixel(x, y, GetValue(x - offset.x, y - offset.y) ? Color.black: Color.white);
                }
            }
            texture.filterMode = FilterMode.Point;
            texture.Apply(false);
            _texture = texture;
        }
    }
}