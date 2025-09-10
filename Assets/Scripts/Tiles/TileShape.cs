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
        private int _rotation = 0;

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

        public void Rotate(int direction)
        {
            _rotation = (_rotation + direction + 4) % 4;

            var rotated = _rotation % 2 == 0 ?
                new bool[dimensions.y, dimensions.x] : 
                new bool[dimensions.x, dimensions.y];
            
            for (var y = 0; y < dimensions.y; y++)
            {
                for (var x = 0; x < dimensions.x; x++)
                {
                    Rotate(rotated, x, y);
                }
            }

            FlattenArray(rotated);

            dimensions = new Vector2Int(rotated.GetLength(1), rotated.GetLength(0));
            _texture = CreateTexture(dimensions.x, dimensions.y, values);
        }

        private void FlattenArray(bool[,] rotated)
        {
            values = new bool[rotated.Length];
            var i = 0;
            foreach (var val in rotated)
            {
                values[i++] = val;
            }
        }

        private void Rotate(bool[,] array, int x, int y)
        {
            switch (_rotation)
            {
                case 0:
                    array[y, x] = GetValue(x, y);
                    break;
                case 1:
                    array[x, dimensions.y - 1 - y] = GetValue(x, y);
                    break;
                case 2:
                    array[dimensions.y - 1 - y, dimensions.x - 1 - x] = GetValue(x, y);
                    break;
                case 3:
                    array[dimensions.x - 1 - x, y] = GetValue(x, y);
                    break;
            }
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