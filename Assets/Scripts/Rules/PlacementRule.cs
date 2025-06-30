using UnityEngine;
using Pegs;
using Tiles;

namespace Rules
{
    [CreateAssetMenu(fileName = "PlacementRule", menuName = "FEV/PlacementRule")]
    public class PlacementRule : ScriptableObject, IRule
    {
        [SerializeField] private RuleOperatorType _ruleOperatorType;
        [SerializeField] private RuleSpacingType _ruleSpacingType;
        [SerializeField] private RulePlayerType _rulePlayerType;
        [SerializeField] private int _pegValue;
        
        public bool IsSatisfied(Vector2Int coordinate, Tile tile, Peg[,] board)
        {
            throw new System.NotImplementedException();
        }
    }
}
