using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace Rules
{
    public static class RuleFactory
    {
        private static Dictionary<Type, IRule> _rules = new();
        
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

        public static string GetRuleText(Type ruleType)
        {
            return GetRule(ruleType).ToString();
        }

        private static IRule GetRule(Type ruleType)
        {
            if (_rules.ContainsKey(ruleType)) return _rules[ruleType];
            var rule = (IRule)Activator.CreateInstance(ruleType);
            _rules.Add(ruleType, rule);
            return _rules[ruleType];
        }
    }
}