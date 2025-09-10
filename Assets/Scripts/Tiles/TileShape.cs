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
        private bool _rotated = false;

        public void SetData(TileShape tileShape)
        {
            dimensions = tileShape.dimensions;
            values = tileShape.values.Clone() as bool[];
        }
        
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

        public void Rotate(bool clockwise = true)
        {
            var rotated = new bool[dimensions.y, dimensions.x];

            for (int y = 0; y < dimensions.y; y++)
            {
                for (int x = 0; x < dimensions.x; x++)
                {
                    if (clockwise) rotated[y, dimensions.x - 1 - x] = GetValue(x, y);
                    else rotated[dimensions.y - 1 - y, x] = GetValue(x, y);
                }
            }

            values = new bool[rotated.Length];
            int i = 0;
            foreach (var val in rotated)
            {
                values[i++] = val;
            }
            
            dimensions = new Vector2Int(rotated.GetLength(1), rotated.GetLength(0));
            _texture = CreateTexture(dimensions.x, dimensions.y, values);
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