using Effects;
using States;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Pegs
{
    public class PegFactory : MonoBehaviour
    {
        private readonly Vector3 _pegOffset = new Vector3(-1f, 0, -1f);
        private Peg _pegPrototype;
        private static readonly Dictionary<PegState, PegStyle> PegStyles = new();
        
        public static PegStyle? GetStyle(PegState pegState)
        {
            if (!PegStyles.TryGetValue(pegState, out var pegStyle)) return null;
            return pegStyle;
        }
        
        public void Initialize(Vector2Int gridSize)
        {
            CreateStyles();
            CreatePegPrototype();
            new Board(CreatePegs(gridSize.x, gridSize.y));
        }
        
        private void CreateStyles()
        {
            PegStyles.Add(PegState.Normal, new PegStyle(new Color(0.8f,0.8f, 0.8f), IconUtility.GetPegSprite(), 0.6f));
            PegStyles.Add(PegState.Highlighted, new PegStyle(Color.cyan, IconUtility.GetSelectedPegSprite()));
            PegStyles.Add(PegState.Effect, new PegStyle(Color.white, IconUtility.GetEffectPegSprite()));
            PegStyles.Add(PegState.HighlightedEffect, new PegStyle(Color.cyan, IconUtility.GetEffectPegSprite()));
            PegStyles.Add(PegState.Deactivated, new PegStyle(new Color(0.4f,0.4f, 0.4f), IconUtility.GetInactivePegSprite(), 0.6f));
            PegStyles.Add(PegState.Invalid, new PegStyle(Color.red, IconUtility.GetInactivePegSprite()));
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
            go.AddComponent<SpriteRenderer>();
            _pegPrototype = go.AddComponent<Peg>();
            
            _pegPrototype.name = "Peg";
            _pegPrototype.transform.parent = transform;
            _pegPrototype.transform.position = new Vector3(1000, 0, 0);
            _pegPrototype.transform.rotation = Quaternion.Euler(90, 0, 0);
            
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
            
            peg.AddEffect(EffectFactory.GetRandomEffect());
        }
    }
}