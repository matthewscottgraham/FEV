using UnityEngine;

namespace Utils
{
    public static class IconUtility
    {
        private static Sprite[] _sprites = null;

        public static Sprite GetPlayerSprite(int playerIndex)
        {
            return GetSprite(playerIndex + 8);
        }

        public static Sprite GetPegSprite()
        {
            return GetSprite(1);
        }

        public static Sprite GetEffectPegSprite()
        {
            return GetSprite(3);
        }
        
        public static Sprite GetInactivePegSprite()
        {
            return GetSprite(0);
        }

        public static Sprite GetSelectedPegSprite()
        {
            return GetSprite(2);
        }

        private static Sprite GetSprite(int index)
        {
            _sprites ??= Resources.LoadAll<Sprite>("Sprites/sprite_atlas");
            return _sprites[index];
        }
    }
}