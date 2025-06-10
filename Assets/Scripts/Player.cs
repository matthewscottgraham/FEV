using System.Collections.Generic;
using UnityEngine;

namespace FEV
{
    public class Player : ScriptableObject
    {
        public static System.Action OnCardsModified;
        public int Index { get; private set; }
        public Color Color { get; private set; }
        public bool IsHuman { get; private set; }
        public List<ICommand> Cards { get; private set; } = new();
        public void Initialize(int index, Color color, bool isHuman = true)
        {
            Index = index;
            Color = color;
            IsHuman = isHuman;
        }

        public void AddCard(ICommand card)
        {
            Cards.Add(card);
            OnCardsModified?.Invoke();
        }

        public void RemoveCard(ICommand card)
        {
            if (Cards.Contains(card))
                Cards.Remove(card);
            OnCardsModified?.Invoke();
        }

        public override string ToString()
        {
            return $"Player: {Index}";
        }
    }
}