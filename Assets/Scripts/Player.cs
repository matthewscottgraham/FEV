using UnityEngine;

namespace FEV
{
    public class Player : ScriptableObject
    {
        public int Index { get; private set; }
        public Color Color { get; private set; }
        public bool IsHuman { get; private set; }
        
        public void Initialize(int index, Color color, bool isHuman = true)
        {
            Index = index;
            Color = color;
            IsHuman = isHuman;
        }
    }
}