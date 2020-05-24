//author: Tim Bouwman
//Github: https://github.com/TimBouwman
using UnityEngine;
using UnityEditor;

/// <summary>
/// 
/// </summary>
[CustomEditor(typeof(Item), true)]
public class ItemEditor : Editor
{
    private SerializedProperty id;
    private SerializedProperty position;
    private SerializedProperty rotation;
    private SerializedProperty handPose;
    private Item item;

    #region Unity Methods
    private void OnEnable()
    {
        item = (Item)target;
        id = serializedObject.FindProperty("id");
        position = serializedObject.FindProperty("position");
        rotation = serializedObject.FindProperty("rotation");
        handPose = serializedObject.FindProperty("handPose");
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.PropertyField(id, new GUIContent("Item Id"), GUILayout.Height(20));

        //Show value and button for position
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(position, new GUIContent("Position"), GUILayout.Height(20));
        if (GUILayout.Button("Set Position", GUILayout.Width(85)))
            item.SetPosition();
        EditorGUILayout.EndHorizontal();

        //Show value and button for rotation
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(rotation, new GUIContent("Rotation"), GUILayout.Height(20));
        if (GUILayout.Button("Set Rotation", GUILayout.Width(85)))
            item.SetRotation();
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Reset", GUILayout.Width(276)))
            item.ResetPosRot();
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.PropertyField(handPose, new GUIContent("HandPose"), GUILayout.Height(20));

        EditorGUILayout.Space(10f);
        base.OnInspectorGUI();
    }
    #endregion
}