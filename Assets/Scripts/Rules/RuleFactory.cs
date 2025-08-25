using System;
using Random = UnityEngine.Random;

namespace Rules
{
    public class RuleFactory
    {
        public static Type GetRandomIgnoredRuleType()
        {
            var rand = Random.Range(0, 100);
            return rand switch
            {
                > 90 => typeof(IsTileAdjacentToOwnedPegs),
                > 80 => typeof(IsTileObstructed),
                _ => null
            };
        }
    }
}