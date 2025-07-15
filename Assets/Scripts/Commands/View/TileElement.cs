using UnityEngine;
using UnityEngine.UIElements;

namespace Commands.View
{
    public class TileElement : Button
    {
        public TileElement(Texture2D texture, bool hasEffect, bool canIgnoreRule)
        {
            iconImage = texture;
            if (hasEffect) AddBadge("tile-badge__effect");
            if (canIgnoreRule) AddBadge("tile-badge__ignore-rule");
        }

        private void AddBadge(string className)
        {
            var element = new VisualElement();
            element.AddToClassList(className);
            Add(element);
        }
    }
}