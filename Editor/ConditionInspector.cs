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
            EditorGUILayout.BeginHorizontal();
            bool valid = _condition && _condition.Evaluate();
            GUILayout.Label(
                valid ? "✓" : "✖",
                valid ? Styles.GreenBox : Styles.RedBox,
                GUILayout.MaxWidth(15),
                GUILayout.ExpandHeight(true));
            EditorGUILayout.BeginVertical();
            DrawProperties();
            EditorGUILayout.EndVertical();
            if (GUILayout.Button(EditorGUIUtility.IconContent("cs Script Icon"), GUILayout.MaxWidth(25), GUILayout.Height(20)))
            {
                Selection.activeObject = MonoScript.FromScriptableObject(_condition);
            }
            EditorGUILayout.EndHorizontal();
        }

        protected virtual void DrawProperties()
        {
            DrawDefaultInspector();
        }
    }
}