using System.Collections.Generic;
using IronMountain.Conditions.Groups;
using UnityEditor;
using UnityEngine;

namespace IronMountain.Conditions.Editor.Groups
{
    [CustomEditor(typeof(ConditionEquation), true)]
    public class ConditionEquationInspector : ConditionInspector
    {
        private readonly Dictionary<Condition, UnityEditor.Editor> _cachedEditors = new ();
        
        private ConditionEquation _equation;

        protected override void OnEnable()
        {
            base.OnEnable();
            if (target) _equation = (ConditionEquation) target;
        }
        
        private void DrawButtons(SerializedProperty list, int i)
        {
            if (GUILayout.Button(MoveUpButtonContent, GUILayout.ExpandHeight(true), GUILayout.MaxWidth(20)))
            {
                list.MoveArrayElement(i, i - 1);
            }            
            if (GUILayout.Button(DeleteButtonContent, GUILayout.ExpandHeight(true), GUILayout.MaxWidth(20)))
            { 
                Condition condition = _equation.Conditions[i].condition;
                if (condition)
                {
                    AssetDatabase.RemoveObjectFromAsset(condition);
                    DestroyImmediate(condition);
                }
                list.DeleteArrayElementAtIndex(i);
                AssetDatabase.SaveAssets();
            }            
            if (GUILayout.Button(MoveDownButtonContent, GUILayout.ExpandHeight(true), GUILayout.MaxWidth(20)))
            { 
                list.MoveArrayElement(i, i + 1);
            }
        }
        
        public override void OnInspectorGUI()
        {
            DrawTitle();
            
            EditorGUILayout.BeginHorizontal();
            
            bool globalValid = _equation && _equation.Evaluate();
            GUILayout.Label(globalValid ? "✓" : "✖", globalValid ? Styles.GreenBox : Styles.RedBox, GUILayout.MaxWidth(15), GUILayout.ExpandHeight(true));
            
            EditorGUILayout.BeginVertical();

            SerializedProperty list = serializedObject.FindProperty("conditions");
            for (int i = 0; i < list.arraySize; i++)
            {
                bool not = _equation.Conditions[i].not;
                Condition condition = _equation.Conditions[i].condition;

                EditorGUILayout.BeginHorizontal();
                
                bool localEvaluation = condition && condition.Evaluate();
                bool localValid = !not && localEvaluation || not && !localEvaluation;
                GUILayout.Label(localValid ? "✓" : "✖", localValid ? Styles.GreenBox : Styles.RedBox, GUILayout.MaxWidth(15), GUILayout.ExpandHeight(true));
                
                EditorGUILayout.BeginVertical();
                string operatorName = i > 0
                    ? _equation.Conditions[i].conditionalOperatorType + " "
                    : string.Empty;
                string conditionName = condition
                    ? _equation.Conditions[i].not ? condition.NegatedName : condition.DefaultName
                    : _equation.Conditions[i].not ? "NOT NULL" : "NULL";
                string label = operatorName + conditionName;
                EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i), new GUIContent(label));
                if (list.GetArrayElementAtIndex(i).isExpanded && condition)
                {
                    UnityEditor.Editor cachedEditor = _cachedEditors.ContainsKey(condition)
                        ? _cachedEditors[condition] : null;
                    CreateCachedEditor(condition, null, ref cachedEditor);
                    cachedEditor.OnInspectorGUI();
                    if (!_cachedEditors.ContainsKey(condition))  _cachedEditors.Add(condition, cachedEditor);
                }
                EditorGUILayout.EndVertical();

                if (list.GetArrayElementAtIndex(i).isExpanded)
                {
                    EditorGUILayout.BeginVertical(GUILayout.MaxWidth(30));
                    DrawButtons(list, i);
                    EditorGUILayout.EndVertical();
                }
                else
                {
                    EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(30));
                    DrawButtons(list, i);
                    EditorGUILayout.EndHorizontal();
                }

                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();

            serializedObject.ApplyModifiedProperties();
        }
        
        private void DrawTitle()
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Equation");
            if (GUILayout.Button(AddNewButtonContent, GUILayout.MaxWidth(50)))
            {
                AddConditionMenu.Open(target, "Condition", newCondition =>
                {
                    _equation.Conditions.Add(new ConditionEquation.Entry { condition = newCondition });
                });
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}