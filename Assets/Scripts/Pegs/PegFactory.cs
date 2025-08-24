using Effects;
using States;
using Unity.VisualScripting;
using UnityEngine;

namespace Pegs
{
    public class PegFactory : MonoBehaviour
    {
        private readonly Vector3 _pegOffset = new Vector3(-1f, 0, -1f);
        private Peg _pegPrototype;
        private Sprite _pegSprite;
        private Sprite _effectSprite;

        public void Initialize(Vector2Int gridSize)
        {
            _pegSprite = Resources.LoadAll<Sprite>("Sprites/icons")[4];
            _effectSprite = Resources.LoadAll<Sprite>("Sprites/icons")[34];
            CreatePegPrototype();
            new Board(CreatePegs(gridSize.x, gridSize.y));
        }

        private Peg[,] CreatePegs(int gridSizeX, int gridSizeY)
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
            peg.Init(new Vector2Int(x,y));
            if (!IsPegActive()) peg.Deactivate();
            TryAddEffect(peg);
            return peg;
        }

        private bool IsPegActive()
        {
            return Random.Range(0,100) >= 5;
        }

        private void TryAddEffect(Peg peg)
        {
            var random = Random.Range(0,100);
            if (random > 5) return;
            
            peg.AddEffect(new RadialGrow(1), _effectSprite);
        }
    }
}