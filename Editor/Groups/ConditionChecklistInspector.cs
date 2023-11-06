using System;
using System.Collections.Generic;
using IronMountain.Conditions.Groups;
using UnityEditor;
using UnityEngine;

namespace IronMountain.Conditions.Editor.Groups
{
    [CustomEditor(typeof(ConditionChecklist), true)]
    public class ConditionChecklistInspector : ConditionInspector
    {
        private readonly Dictionary<Condition, UnityEditor.Editor> _cachedEditors = new ();

        private ConditionChecklist _checklist;

        protected override void OnEnable()
        {
            base.OnEnable();
            if (target) _checklist = (ConditionChecklist) target;
        }

        private void DrawButtons(SerializedProperty list, int i)
        {
            if (GUILayout.Button(MoveUpButtonContent, GUILayout.ExpandHeight(true), GUILayout.MaxWidth(20)))
            {
                list.MoveArrayElement(i, i - 1);
            }            
            if (GUILayout.Button(DeleteButtonContent, GUILayout.ExpandHeight(true), GUILayout.MaxWidth(20)))
            {
                Condition condition = _checklist.Conditions[i].condition;
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
            DrawList();
            DrawAmountRequired();
            serializedObject.ApplyModifiedProperties();
        }

        private void DrawTitle()
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Checklist");
            if (GUILayout.Button(AddNewButtonContent, GUILayout.MaxWidth(50)))
            {
                AddConditionMenu.Open(target, "Condition", newCondition =>
                {
                    _checklist.Conditions.Add(new ConditionChecklist.Entry { condition = newCondition });
                });
            }
            EditorGUILayout.EndHorizontal();
        }

        private void DrawAmountRequired()
        {
            SerializedProperty allRequiredProperty = serializedObject.FindProperty("allRequired");
            EditorGUILayout.PropertyField(allRequiredProperty);
            if (!allRequiredProperty.boolValue) EditorGUILayout.PropertyField(serializedObject.FindProperty("amountRequired"));
        }

        private void DrawList()
        {
            EditorGUILayout.BeginHorizontal();
            
            bool globalValid = _checklist && _checklist.Evaluate();
            GUILayout.Label(globalValid ? "✓" : "✖", globalValid ? Styles.GreenBox : Styles.RedBox, GUILayout.MaxWidth(15), GUILayout.ExpandHeight(true));
            
            EditorGUILayout.BeginVertical();
            SerializedProperty list = serializedObject.FindProperty("conditions");
            for (int i = 0; i < list.arraySize; i++)
            {
                bool not = _checklist.Conditions[i].not;
                Condition condition = _checklist.Conditions[i].condition;
                
                EditorGUILayout.BeginHorizontal();
                
                bool localEvaluation = condition && condition.Evaluate();
                bool localValid = !not && localEvaluation || not && !localEvaluation;
                GUILayout.Label(localValid ? "✓" : "✖", localValid ? Styles.GreenBox : Styles.RedBox, GUILayout.MaxWidth(15), GUILayout.ExpandHeight(true));
                
                EditorGUILayout.BeginVertical();
                string label = condition
                    ? _checklist.Conditions[i].not ? condition.NegatedName :  condition.DefaultName
                    : _checklist.Conditions[i].not ? "NOT NULL" : "NULL";
                EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i), new GUIContent(label));
                if (list.GetArrayElementAtIndex(i).isExpanded && condition)
                {
                    UnityEditor.Editor cachedEditor = _cachedEditors.ContainsKey(condition)
                        ? _cachedEditors[condition] : null;
                    CreateCachedEditor(condition, null, ref cachedEditor);
                    cachedEditor.OnInspectorGUI();
                    if (!_cachedEditors.ContainsKey(condition)) _cachedEditors.Add(condition, cachedEditor);
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
        }
    }
}