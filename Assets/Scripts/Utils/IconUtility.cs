using UnityEngine;

namespace Utils
{
    public class IconUtility
    {
        public static Sprite GetPlayerSprite(int playerIndex)
        {
            return Resources.LoadAll<Sprite>($"Sprites/")[playerIndex];
        }
    }
}