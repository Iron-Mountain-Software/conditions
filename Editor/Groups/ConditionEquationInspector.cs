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
            if (GUILayout.Button(MoveUpButtonContent, GUILayout.Width(20), GUILayout.Height(20)))
            {
                list.MoveArrayElement(i, i - 1);
            }            
            if (GUILayout.Button(DeleteButtonContent, GUILayout.Width(20), GUILayout.Height(20)))
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
            if (GUILayout.Button(MoveDownButtonContent, GUILayout.Width(20), GUILayout.Height(20)))
            { 
                list.MoveArrayElement(i, i + 1);
            }
        }
        
        public override void OnInspectorGUI()
        {
            DrawTitle();
            DrawList();
            serializedObject.ApplyModifiedProperties();
        }
        
        private void DrawTitle()
        {
            EditorGUILayout.BeginHorizontal();
            bool globalValid = _equation && _equation.Evaluate();
            GUILayout.Label(globalValid ? "✓" : "✖", globalValid ? Styles.GreenBox : Styles.RedBox, GUILayout.Width(20), GUILayout.Height(20));
            GUILayout.Label("Equation");
            if (GUILayout.Button(AddNewButtonContent, GUILayout.MaxWidth(50)))
            {
                AddConditionMenu.Open(target, newCondition =>
                {
                    _equation.Conditions.Add(new ConditionEquation.Entry { condition = newCondition });
                    serializedObject.Update();
                });
            }
            EditorGUILayout.EndHorizontal();
        }

        private void DrawList()
        { 
            EditorGUILayout.BeginVertical();
            
            SerializedProperty list = serializedObject.FindProperty("conditions");
            for (int i = 0; i < list.arraySize; i++)
            {
                Condition condition = _equation.Conditions[i].condition;

                EditorGUILayout.BeginHorizontal();
                
                if (i > 0)
                {
                    EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i).FindPropertyRelative("conditionalOperatorType"), GUIContent.none, GUILayout.Width(55));
                }
                else EditorGUILayout.LabelField(string.Empty, GUILayout.Width(55));
                
                EditorGUILayout.BeginVertical();
                if (condition)
                {
                    UnityEditor.Editor cachedEditor = _cachedEditors.ContainsKey(condition)
                        ? _cachedEditors[condition] : null;
                    CreateCachedEditor(condition, null, ref cachedEditor);
                    cachedEditor.OnInspectorGUI();
                    if (!_cachedEditors.ContainsKey(condition))  _cachedEditors.Add(condition, cachedEditor);
                }
                else EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i).FindPropertyRelative("condition"), GUIContent.none);
                EditorGUILayout.EndVertical();

                EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(30));
                DrawButtons(list, i);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndVertical();
        }
    }
}