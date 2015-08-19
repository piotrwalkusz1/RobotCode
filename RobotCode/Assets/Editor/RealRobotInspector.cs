using UnityEditor;
using System.Collections;
using UnityEngine;

[CustomEditor(typeof(RealRobot))]
public class RealRobotInspector : Editor
{
    public SerializedProperty _firstCode;

    public void OnEnable()
    {
        _firstCode = serializedObject.FindProperty("_firstCode");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        serializedObject.Update();
        _firstCode.stringValue = EditorGUILayout.TextArea(_firstCode.stringValue, GUILayout.Height(120), GUILayout.Width(350));
        serializedObject.ApplyModifiedProperties();
    }
}
