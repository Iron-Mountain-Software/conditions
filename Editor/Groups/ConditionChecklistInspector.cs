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
            if (GUILayout.Button(MoveUpButtonContent, GUILayout.Width(20), GUILayout.Height(20)))
            {
                list.MoveArrayElement(i, i - 1);
            }            
            if (GUILayout.Button(DeleteButtonContent, GUILayout.Width(20), GUILayout.Height(20)))
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
            if (GUILayout.Button(MoveDownButtonContent, GUILayout.Width(20), GUILayout.Height(20)))
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
            bool globalValid = _checklist && _checklist.Evaluate();
            GUILayout.Label(globalValid ? "✓" : "✖", globalValid ? Styles.GreenBox : Styles.RedBox, GUILayout.Width(20), GUILayout.Height(20));
            GUILayout.Label("Checklist");
            if (GUILayout.Button(AddNewButtonContent, GUILayout.MaxWidth(50)))
            {
                AddConditionMenu.Open(target, newCondition =>
                {
                    _checklist.Conditions.Add(new ConditionChecklist.Entry { condition = newCondition });
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
                Condition condition = _checklist.Conditions[i].condition;
                
                EditorGUILayout.BeginHorizontal();

                EditorGUILayout.LabelField(i < list.arraySize - 1 ? " ┣━━" : " ┗━━", GUILayout.Width(20));

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
        
        private void DrawAmountRequired()
        {
            SerializedProperty allRequiredProperty = serializedObject.FindProperty("allRequired");
            EditorGUILayout.PropertyField(allRequiredProperty);
            if (!allRequiredProperty.boolValue) EditorGUILayout.PropertyField(serializedObject.FindProperty("amountRequired"));
        }
    }
}