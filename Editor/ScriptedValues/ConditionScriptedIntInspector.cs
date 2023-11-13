using IronMountain.Conditions.ScriptedValues;
using UnityEditor;
using UnityEngine;

namespace IronMountain.Conditions.Editor.ScriptedValues
{
    [CustomEditor(typeof(ConditionScriptedInt), true)]
    public class ConditionScriptedIntInspector : ConditionInspector
    {
        protected override void DrawProperties()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("scriptedInt"), GUIContent.none);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("comparison"), GUIContent.none, GUILayout.Width(165));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("target"), GUIContent.none, GUILayout.Width(50));
            EditorGUILayout.EndHorizontal();
        }
    }
}
