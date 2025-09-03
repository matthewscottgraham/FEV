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
            return GetValue(x, y, dimensions.x, values);
        }

        public Texture2D GetTexture()
        {
            if (_texture == null)
                _texture = CreateTexture(dimensions.x, dimensions.y, values);
            return _texture;
        }

        private static bool GetValue(int x, int y, int width, bool[] valueArray)
        {
            return valueArray[y * width + x];
        }

        private static Texture2D CreateTexture(int width, int height, bool[] valueArray)
        {
            var texture = new Texture2D(width, height, TextureFormat.ARGB32, false);
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    var value = GetValue(x, y, width, valueArray);
                    texture.SetPixel(x, y, value ? Color.white: Color.clear);
                }
            }
            texture.filterMode = FilterMode.Point;
            texture.Apply(false);
            return texture;
        }
    }
}