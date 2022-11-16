using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(Condition), true)]
    public class ConditionInspector : UnityEditor.Editor
    {
        protected static readonly GUIContent 
            MoveUpButtonContent = new ("↑", "Move up."),
            MoveDownButtonContent = new ("↓", "Move down."),
            AddNewButtonContent = new ("Add", "Add new."),
            DeleteButtonContent = new ("✕", "Delete.");
        
        private static Texture2D _redTexture;
        private static Texture2D _greenTexture;
        private static Texture2D _containerTexture;

        private static Texture2D RedTexture {
            get
            {
                if (!_redTexture) _redTexture = InitializeTexture(new Color(0.76f, 0.11f, 0f));
                return _redTexture;
            }
        }

        private static Texture2D GreenTexture {
            get
            {
                if (!_greenTexture) _greenTexture = InitializeTexture(new Color(0.09f, 0.78f, 0.11f));
                return _greenTexture;
            }
        }

        private static Texture2D ContainerTexture {
            get
            {
                if (!_containerTexture) _containerTexture = InitializeTexture(new Color(0.15f, 0.15f, 0.15f));
                return _containerTexture;
            }
        }
        
        private static Texture2D InitializeTexture(Color color)
        {
            Texture2D texture = new Texture2D(1, 1);
            texture.SetPixel(0,0, color);
            texture.Apply();
            return texture;
        }
        
        protected static readonly GUIStyle GreenBox = new ()
        {
            margin = new RectOffset(1, 1, 1, 1),
            alignment = TextAnchor.MiddleCenter,
            normal = {textColor = Color.white, background = GreenTexture}
        };
        
        protected static readonly GUIStyle RedBox = new ()
        {
            margin = new RectOffset(1, 1, 1, 1),
            alignment = TextAnchor.MiddleCenter,
            normal = {textColor = Color.white, background = RedTexture}
        };
        
        protected static readonly GUIStyle Container = new ()
        {
            padding = new RectOffset(7,7,7,7),
            normal = { background = ContainerTexture }
        };
        
        public override void OnInspectorGUI()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(15));
            bool valid = ((Condition) target).Evaluate();
            GUILayout.Label(valid ? "✓" : "✖", valid ? GreenBox : RedBox, GUILayout.ExpandHeight(true));
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginVertical();
            DrawDefaultInspector();
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();
        }
    }
}