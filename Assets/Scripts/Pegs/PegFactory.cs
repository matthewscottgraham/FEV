using UnityEngine;

namespace Pegs
{
    public class PegFactory : MonoBehaviour
    {
        private readonly Vector3 _pegOffset = new Vector3(-1f, 0, -1f);
        private Peg _pegPrototype;

        public void Initialize()
        {
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
        
        private Peg CreatePeg(int x, int y)
        {
            var peg = Instantiate(_pegPrototype, transform);
            peg.transform.position = new Vector3(x, 0, y) + _pegOffset;
            return peg;
        }
    }
}