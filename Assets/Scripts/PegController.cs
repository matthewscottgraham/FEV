using UnityEngine;

namespace FEV
{
    public class PegController : MonoBehaviour
    {
        private const int PegDensity = 3;
        private readonly Vector3 _pegOffset = new Vector3(-1f, 0, -1f);
        private Transform _pegPrototype;
        public void Initialize()
        {
            var matchState = Resources.Load<MatchState>("MatchState");
            CreatePegPrototype();
            CreatePegs(matchState.GridSize.x, matchState.GridSize.y);
        }
        private void CreatePegPrototype()
        {
            _pegPrototype = GameObject.CreatePrimitive(PrimitiveType.Cube).transform;
            _pegPrototype.name = "Peg";
            _pegPrototype.parent = transform;
            _pegPrototype.position = new Vector3(1000, 0, 0);
            _pegPrototype.localScale = Vector3.one * 0.1f;
            
            Destroy(_pegPrototype.GetComponent<Collider>());
            
            var material = Resources.Load<Material>("Materials/mat_peg");
            _pegPrototype.GetComponent<Renderer>().material = material;
        }
        private void CreatePegs(int gridSizeX, int gridSizeY)
        {
            for (int x = 0; x < gridSizeX * PegDensity; x++)
            {
                for (int y = 0; y < gridSizeY * PegDensity; y++)
                {
                    CreatePeg(x,y);
                }
            }
        }
        private void CreatePeg(int x, int y)
        {
            var peg = Instantiate(_pegPrototype, transform);
            peg.position = (new Vector3(x, 0, y) +_pegOffset) / PegDensity;
        }
    }
}