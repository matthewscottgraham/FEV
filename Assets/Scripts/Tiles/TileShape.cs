using UnityEngine;
using System.Linq;
using Unity.VisualScripting;

namespace FEV
{
    [CreateAssetMenu(fileName = "TileShape", menuName = "FEV/TileShape")]
    public class TileShape : ScriptableObject
    {
        [TextArea, SerializeField] private string shape;
        private Texture2D _texture = null;
        
        public Vector2Int GetShapeDimensions()
        {
            var dimensions = new Vector2Int();
            var rows = shape.Split("\n");

            if (rows == null || rows.Length <= 0) return dimensions;
            
            dimensions.x = rows[0].Length;
            dimensions.y = rows.Length;

            return dimensions;
        }

        public bool GetValue(int x, int y)
        {
            var rows = shape.Split("\n");

            if (rows == null || rows.Length <= 0) return false;
            if (y < 0 || y >= rows.Length) return false;
            if (x < 0 || x >= rows[y].Length) return false;
            return rows[y][x] == 'X';
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
            var dimensions = GetShapeDimensions();
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