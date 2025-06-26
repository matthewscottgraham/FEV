using System;
using UnityEngine;

namespace FEV
{
    public class TileFactory : IDisposable
    {
        public TileFactory()
        {
            _tileShapes = Resources.LoadAll<TileShape>("TileShapes");
        }
        
        private TileShape[] _tileShapes;

        public Tile DrawRandomTile()
        {
            var shape = _tileShapes[UnityEngine.Random.Range(0, _tileShapes.Length)];
            return new Tile(shape);
        }

        public void Dispose()
        {
            _tileShapes = null;
        }
    }
}