using Unity.Properties;
using UnityEngine;

namespace FEV
{
    [CreateAssetMenu(menuName = "FEV/MatchState", fileName = "New Match State")]
    public class MatchState : ScriptableObject
    {
        [CreateProperty] public Vector2Int GridSize = new Vector2Int(10, 10);
        [CreateProperty] public int PlayerCount = 2;
    }
}