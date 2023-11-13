using IronMountain.Conditions.ScriptedValues;
using UnityEditor;
using UnityEngine;

namespace IronMountain.Conditions.Editor.ScriptedValues
{
    [CustomEditor(typeof(ConditionScriptedBool), true)]
    public class ConditionScriptedBoolInspector : ConditionInspector
    {
        protected override void DrawProperties()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("scriptedBool"), GUIContent.none);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("comparison"), GUIContent.none, GUILayout.Width(60));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("target"), GUIContent.none, GUILayout.Width(25));
            EditorGUILayout.EndHorizontal();
        }
    }
}