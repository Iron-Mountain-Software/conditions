using IronMountain.Conditions.ScriptedValues;
using UnityEditor;
using UnityEngine;

namespace IronMountain.Conditions.Editor.ScriptedValues
{
    [CustomEditor(typeof(ConditionScriptedFloat), true)]
    public class ConditionScriptedFloatInspector : ConditionInspector
    {
        protected override void DrawProperties()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("scriptedFloat"), GUIContent.none);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("comparison"), GUIContent.none, GUILayout.Width(165));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("target"), GUIContent.none, GUILayout.Width(50));
            EditorGUILayout.EndHorizontal();
        }
    }
}