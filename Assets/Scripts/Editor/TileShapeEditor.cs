using Tiles;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TileShape))]
public class TileShapeEditor : Editor
{
    SerializedProperty _dimensionsProperty;
    SerializedProperty _valuesProperty;

    private void OnEnable()
    {
        _dimensionsProperty = serializedObject.FindProperty("dimensions");
        _valuesProperty = serializedObject.FindProperty("values");
    }
    
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(_dimensionsProperty);
        var size = _dimensionsProperty.vector2IntValue;
        var total = Mathf.Max(0, size.x * size.y);
        
        if (_valuesProperty.arraySize != total) _valuesProperty.arraySize = total;

        GUILayout.Space(10);

        var index = 0;
        for (var y = 0; y < size.y; y++)
        {
            EditorGUILayout.BeginHorizontal();
            for (var x = 0; x < size.x; x++)
            {
                var property = _valuesProperty.GetArrayElementAtIndex(index);
                property.boolValue = GUILayout.Toggle(property.boolValue, GUIContent.none, GUILayout.Width(20), GUILayout.Height(20));
                index++;
            }
            EditorGUILayout.EndHorizontal();
        }

        serializedObject.ApplyModifiedProperties();
    }
}