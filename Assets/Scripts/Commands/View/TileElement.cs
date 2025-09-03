using UnityEngine;
using UnityEngine.UIElements;

namespace Commands.View
{
    public class TileElement : Button
    {
        private VisualElement _badgeContainer;
        public TileElement(Texture2D texture, bool hasEffect, bool canIgnoreRule)
        {
            iconImage = texture;
            
            _badgeContainer = new VisualElement();
            _badgeContainer.AddToClassList("tile-badge-container");
            Add(_badgeContainer);
            
            if (hasEffect) AddBadge("tile-badge__effect");
            if (canIgnoreRule) AddBadge("tile-badge__ignore-rule");
        }

        private void AddBadge(string className)
        {
            var element = new VisualElement();
            element.AddToClassList(className);
            _badgeContainer.Add(element);
        }
    }
}