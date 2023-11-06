using System;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace IronMountain.Conditions.Editor
{
    public class ConditionEditor
    {
        private readonly string _name;
        private readonly ScriptableObject _parent;
        private readonly Action<Condition> _onAdd;
        
        private UnityEditor.Editor _cachedEditor = new();

        private readonly GUIStyle _header;
        private readonly GUIStyle _h1;
        
        public ConditionEditor(string name, ScriptableObject parent, Action<Condition> onAdd)
        {
            _name = name;
            _parent = parent;
            _onAdd = onAdd;
            
            Texture2D headerTexture = new Texture2D(1, 1);
            headerTexture.SetPixel(0,0, new Color(0.12f, 0.12f, 0.12f));
            headerTexture.Apply();
            _header = new GUIStyle
            {
                padding = new RectOffset(5, 5, 5, 5),
                normal = new GUIStyleState
                {
                    background = headerTexture
                }
            };
            _h1 = new GUIStyle
            {
                fontSize = 16,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleLeft,
                normal = new GUIStyleState
                {
                    textColor = new Color(0.36f, 0.36f, 0.36f)
                }
            };
        }

        public void Draw(ref Condition condition)
        {
            GUILayout.Space(10);
            
            EditorGUILayout.BeginHorizontal(_header,GUILayout.ExpandWidth(true));
            GUILayout.Label(_name, _h1, GUILayout.ExpandWidth(true));
            if (condition && GUILayout.Button("Delete Condition", GUILayout.MaxWidth(150)))
            {
                AssetDatabase.RemoveObjectFromAsset(condition);
                Object.DestroyImmediate(condition);
                condition = null;
                AssetDatabase.SaveAssets();
            }
            else if (!condition && GUILayout.Button("Add Condition", GUILayout.MaxWidth(150)))
            {
                AddConditionMenu.Open(_parent, "Condition", _onAdd);
            }
            EditorGUILayout.EndHorizontal();
            
            if (condition)
            {
                UnityEditor.Editor.CreateCachedEditor(condition, null, ref _cachedEditor);
                EditorGUILayout.BeginVertical(Styles.Container);
                _cachedEditor.OnInspectorGUI();
                EditorGUILayout.EndVertical();
            }
            else _cachedEditor = null;
        }
    }
}