using UnityEditor;
using UnityEngine;

namespace IronMountain.Conditions.Editor
{
    [CustomEditor(typeof(Condition), true)]
    public class ConditionInspector : UnityEditor.Editor
    {
        protected static readonly GUIContent 
            MoveUpButtonContent = new ("↑", "Move up."),
            MoveDownButtonContent = new ("↓", "Move down."),
            AddNewButtonContent = new ("Add", "Add new."),
            DeleteButtonContent = new ("✕", "Delete.");

        public override void OnInspectorGUI()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(15));
            bool valid = ((Condition) target).Evaluate();
            GUILayout.Label(valid ? "✓" : "✖", valid ? Styles.GreenBox : Styles.RedBox, GUILayout.ExpandHeight(true));
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginVertical();
            DrawDefaultInspector();
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();
        }
    }
}