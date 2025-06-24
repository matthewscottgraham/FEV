using UnityEngine;

namespace FEV
{
    public class PegController : MonoBehaviour
    {
        private readonly Vector3 _pegOffset = new Vector3(-1f, 0, -1f);
        private Peg _pegPrototype;
        private Peg[,] _pegs;
        private Peg _highlightedPeg;
        private Peg _selectedPeg;
        
        public void Initialize(MatchState matchState)
        {
            CreatePegPrototype();
            CreatePegs(matchState.GridSize.x, matchState.GridSize.y);
        }

        public void SetHighlight(Vector2Int coordinates)
        {
            _highlightedPeg?.Highlight(false);
            if (!IsValidCoordinate(coordinates)) return;
            _pegs[coordinates.x, coordinates.y].Highlight(true);
            _highlightedPeg = _pegs[coordinates.x, coordinates.y];
        }

        public void SetSelected(Vector2Int coordinates)
        {
            _selectedPeg?.Select(false);
            if (!IsValidCoordinate(coordinates)) return;
            _pegs[coordinates.x, coordinates.y].Select(true);
            _selectedPeg = _pegs[coordinates.x, coordinates.y];
        }

        private bool IsValidCoordinate(Vector2Int coordinates)
        {
            if (coordinates.x < 0 || coordinates.y < 0)
                return false;
            if (coordinates.x >= _pegs.GetLength(0) || coordinates.y >= _pegs.GetLength(1))
                return false;
            return true;
        }
        
        private void CreatePegPrototype()
        {
            var go = GameObject.CreatePrimitive(PrimitiveType.Cube);
            _pegPrototype = go.AddComponent<Peg>();
            _pegPrototype.name = "Peg";
            _pegPrototype.transform.parent = transform;
            _pegPrototype.transform.position = new Vector3(1000, 0, 0);
            _pegPrototype.transform.localScale = Vector3.one * 0.1f;
            
            Destroy(_pegPrototype.GetComponent<Collider>());
            
            var material = Resources.Load<Material>("Materials/mat_peg");
            _pegPrototype.GetComponent<Renderer>().material = material;
        }
        private void CreatePegs(int gridSizeX, int gridSizeY)
        {
            _pegs = new Peg[gridSizeX, gridSizeY];
            for (int x = 0; x < gridSizeX; x++)
            {
                for (int y = 0; y < gridSizeY; y++)
                {
                    CreatePeg(x,y);
                }
            }
        }
        private void CreatePeg(int x, int y)
        {
            var peg = Instantiate(_pegPrototype, transform);
            peg.transform.position = new Vector3(x, 0, y) + _pegOffset;
            _pegs[x, y] = peg;
        }
    }
}