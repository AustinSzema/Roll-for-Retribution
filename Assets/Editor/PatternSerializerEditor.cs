using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(PatternSerializer)), CanEditMultipleObjects]
class PatternSerializerEditor : Editor
{
    public override VisualElement CreateInspectorGUI()
    {
        // Create a new VisualElement to be the root of the Inspector UI.
        Button updateButton = new Button((() =>
        {
            PatternSerializer pattern = (PatternSerializer)target;
            pattern.UpdatePoints();
        }));
        updateButton.style.height = 36;
        updateButton.text = "SerializePoints";
        
        VisualElement root = new();
        VisualElement baseInspector = new();
        InspectorElement.FillDefaultInspector(baseInspector, serializedObject, this);
        root.Add(updateButton);
        root.Add(baseInspector);
        
        // Return the finished Inspector UI.
        return root;
    }
}