using UnityEditor;
using UnityEngine;

namespace IronMountain.Conditions.Editor
{
    [CustomEditor(typeof(Condition), true)]
    public class ConditionInspector : UnityEditor.Editor
    {
        private Condition _condition;
        
        protected static readonly GUIContent 
            MoveUpButtonContent = new ("↑", "Move up."),
            MoveDownButtonContent = new ("↓", "Move down."),
            AddNewButtonContent = new ("Add", "Add new."),
            DeleteButtonContent = new ("✕", "Delete.");

        protected virtual void OnEnable()
        {
            if (target) _condition = (Condition) target;
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.BeginHorizontal(GUILayout.ExpandHeight(false));
            bool valid = _condition && _condition.Evaluate();
            GUILayout.Label(
                valid ? "✓" : "✖",
                valid ? Styles.GreenBox : Styles.RedBox,
                GUILayout.Width(20),
                GUILayout.Height(20));
            EditorGUILayout.BeginVertical();
            DrawProperties();
            EditorGUILayout.EndVertical();
            if (GUILayout.Button(EditorGUIUtility.IconContent("cs Script Icon"), GUILayout.MaxWidth(25), GUILayout.Height(20)))
            {
                Selection.activeObject = MonoScript.FromScriptableObject(_condition);
            }
            EditorGUILayout.EndHorizontal();
            serializedObject.ApplyModifiedProperties();
        }

        protected virtual void DrawProperties()
        {
            DrawDefaultInspector();
        }
    }
}