using UnityEngine;

namespace Utils
{
    public class IconUtility
    {
        private static Sprite _pegSprite;
        public static Sprite GetPlayerSprite(int playerIndex)
        {
            return Resources.LoadAll<Sprite>($"Sprites/")[playerIndex];
        }

        public static Sprite GetPegSprite()
        {
            if (!_pegSprite) _pegSprite = Resources.Load<Sprite>($"Sprites/PegIcon");
            return _pegSprite;
        }
    }
}