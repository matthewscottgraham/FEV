using UnityEngine.UIElements;
using System;

public static class VisualElementExtensions
{
    public static VisualElement AddSpacer(this VisualElement visualElement)
    {
        var spacer = visualElement.AddNew<VisualElement>("spacer");
        spacer.pickingMode = PickingMode.Ignore;
        return spacer;
    }
    
    public static T AddNew<T>(this VisualElement visualElement) where T : VisualElement
    {
        var newElement = (T)Activator.CreateInstance(typeof(T));
        visualElement.Add(newElement);
        return newElement;
    }
    
    public static T AddNew<T>(this VisualElement visualElement, string className) where T : VisualElement
    {
        var newElement = AddNew<T>(visualElement);
        newElement.AddToClassList(className);
        return newElement;
    }
    
    public static T AddNew<T>(this VisualElement visualElement, string classNameA, string classNameB) where T : VisualElement
    {
        var newElement = AddNew<T>(visualElement, classNameA);
        newElement.AddToClassList(classNameB);
        return newElement;
    }
    
    public static T AddNew<T>(this VisualElement visualElement, string[] classNames) where T : VisualElement
    {
        var newElement = AddNew<T>(visualElement);
        if (classNames == null) return newElement;
        
        foreach (var className in classNames)
        {
            newElement.AddToClassList(className);        
        }

        return newElement;
    }

    public static void SetVisibility(this VisualElement visualElement, bool visible)
    {
        visualElement.style.display = visible ? DisplayStyle.Flex : DisplayStyle.None;
    }
}