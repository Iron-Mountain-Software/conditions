using System.Collections.Generic;
using Groups;
using UnityEditor;
using UnityEngine;

namespace Editor.Groups
{
    [CustomEditor(typeof(ConditionEquation), true)]
    public class ConditionEquationInspector : ConditionInspector
    {
        private readonly Dictionary<Condition, UnityEditor.Editor> _cachedEditors = new ();
        
        private void DrawButtons(SerializedProperty list, int i)
        {
            if (GUILayout.Button(MoveUpButtonContent, GUILayout.ExpandHeight(true), GUILayout.MaxWidth(20)))
            {
                list.MoveArrayElement(i, i - 1);
            }            
            if (GUILayout.Button(DeleteButtonContent, GUILayout.ExpandHeight(true), GUILayout.MaxWidth(20)))
            { 
                list.DeleteArrayElementAtIndex(i);
            }            
            if (GUILayout.Button(MoveDownButtonContent, GUILayout.ExpandHeight(true), GUILayout.MaxWidth(20)))
            { 
                list.MoveArrayElement(i, i + 1);
            }
        }
        
        public override void OnInspectorGUI()
        {
            ConditionEquation equation = (ConditionEquation) target;
            SerializedObject serializedEquation = new SerializedObject(equation);
            
            EditorGUILayout.BeginHorizontal(Container);
            
            EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(15));
            bool globalValid = equation && equation.Evaluate();
            GUILayout.Label(globalValid ? "✓" : "✖", globalValid ? GreenBox : RedBox, GUILayout.ExpandHeight(true));
            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.BeginVertical();

            SerializedProperty list = serializedEquation.FindProperty("conditions");
            for (int i = 0; i < list.arraySize; i++)
            {
                bool not = equation.Conditions[i].not;
                Condition condition = equation.Conditions[i].condition;

                EditorGUILayout.BeginHorizontal();
                
                EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(15));
                bool localEvaluation = condition && condition.Evaluate();
                bool localValid = !not && localEvaluation || not && !localEvaluation;
                GUILayout.Label(localValid ? "✓" : "✖", localValid ? GreenBox : RedBox, GUILayout.ExpandHeight(true));
                EditorGUILayout.EndHorizontal();
                
                EditorGUILayout.BeginVertical();
                string operatorName = i > 0
                    ? equation.Conditions[i].conditionalOperatorType + " "
                    : string.Empty;
                string conditionName = condition
                    ? equation.Conditions[i].not ? condition.NegatedName : condition.DefaultName
                    : equation.Conditions[i].not ? "NOT NULL" : "NULL";
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
            EditorGUILayout.Space();

            if (GUILayout.Button(AddNewButtonContent))
            {
                AddConditionMenu addConditionMenu = new AddConditionMenu(target, "Condition");
                addConditionMenu.OnConditionCreated += (condition) =>
                {
                    equation.Conditions.Add(new ConditionEquation.Entry { condition = condition });
                };
            }
            
            serializedEquation.ApplyModifiedProperties();
        }
    }
}