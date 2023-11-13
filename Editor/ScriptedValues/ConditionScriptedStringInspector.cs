using IronMountain.Conditions.ScriptedValues;
using UnityEditor;
using UnityEngine;

namespace IronMountain.Conditions.Editor.ScriptedValues
{
    [CustomEditor(typeof(ConditionScriptedString), true)]
    public class ConditionScriptedStringInspector : ConditionInspector
    {
        protected override void DrawProperties()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("scriptedString"), GUIContent.none);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("comparison"), GUIContent.none, GUILayout.Width(60));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("target"), GUIContent.none, GUILayout.Width(200));
            EditorGUILayout.EndHorizontal();
        }
    }
}