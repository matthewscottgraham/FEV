using Unity.VisualScripting;
using UnityEngine;

namespace Pegs
{
    public class PegFactory : MonoBehaviour
    {
        private readonly Vector3 _pegOffset = new Vector3(-1f, 0, -1f);
        private Peg _pegPrototype;
        private Sprite _pegSprite;

        public void Initialize()
        {
            _pegSprite = Resources.LoadAll<Sprite>("Sprites/icons")[4];
            CreatePegPrototype();
        }
        
        public Peg[,] CreatePegs(int gridSizeX, int gridSizeY)
        {
            var pegs = new Peg[gridSizeX, gridSizeY];
            for (int x = 0; x < gridSizeX; x++)
            {
                for (int y = 0; y < gridSizeY; y++)
                {
                    pegs[x,y] = CreatePeg(x,y);
                }
            }

            return pegs;
        }
        
        private void CreatePegPrototype()
        {
            var go = new GameObject();
            var spriteRenderer = go.AddComponent<SpriteRenderer>();
            _pegPrototype = go.AddComponent<Peg>();
            
            _pegPrototype.name = "Peg";
            _pegPrototype.transform.parent = transform;
            _pegPrototype.transform.position = new Vector3(1000, 0, 0);
            _pegPrototype.transform.rotation = Quaternion.Euler(90, 0, 0);
            
            spriteRenderer.sprite = _pegSprite;
        }
        
        private Peg CreatePeg(int x, int y)
        {
            var peg = Instantiate(_pegPrototype, transform);
            peg.transform.position = new Vector3(x, 0, y) + _pegOffset;
            return peg;
        }
    }
}