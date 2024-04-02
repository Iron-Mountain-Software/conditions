using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IronMountain.Conditions.Groups;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace IronMountain.Conditions.Editor
{
    public class AddConditionMenu
    {
        public static readonly List<Type> ConditionTypes;

        static AddConditionMenu()
        {
            ConditionTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.IsSubclassOf(typeof(Condition)) && !type.IsAbstract)
                .ToList();
        }

        public static void Open(Object asset, Action<Condition> onAdd)
        {
            if (!asset) return;
            GenericMenu menu = new GenericMenu();
            AddMenuItem(menu, typeof(ConditionTrue), "True", "Add True", asset, onAdd);
            AddMenuItem(menu, typeof(ConditionFalse), "False", "Add False", asset, onAdd);
            menu.AddSeparator("Groups");
            AddMenuItem(menu, typeof(ConditionEquation), "Equation", "Add Equation", asset, onAdd);
            AddMenuItem(menu, typeof(ConditionChecklist), "Checklist", "Add Checklist", asset, onAdd);
            
            Dictionary<string, List<Type>> entries = new Dictionary<string, List<Type>>();
            
            foreach (Type derivedType in ConditionTypes)
            {
                if (derivedType == null 
                    || derivedType == typeof(ConditionTrue)
                    || derivedType == typeof(ConditionFalse)
                    || derivedType == typeof(ConditionEquation)
                    || derivedType == typeof(ConditionChecklist)
                    || string.IsNullOrWhiteSpace(derivedType.FullName)) continue;
                
                List<string> pathSegments = derivedType.FullName.Split('.').ToList();
                pathSegments.RemoveAll(test => test is "Condition" or "Conditions");
                
                string root = pathSegments.Count > 1 ? pathSegments[0] : string.Empty;
                if (entries.ContainsKey(root))
                {
                    entries[root].Add(derivedType);
                }
                else entries.Add(root, new List<Type> { derivedType });
            }

            foreach (string key in entries.Keys)
            {
                if (key == string.Empty) continue;
                menu.AddSeparator(key);
                foreach (var derivedType in entries[key])
                {
                    AddType(menu, derivedType, asset, onAdd);
                }
            }

            if (entries.ContainsKey(string.Empty))
            {
                menu.AddSeparator("Other");
                foreach (var derivedType in entries[string.Empty])
                {
                    AddType(menu, derivedType, asset, onAdd);
                }
            }

            menu.ShowAsContext();
        }

        private static void AddType(GenericMenu menu, Type type, Object asset, Action<Condition> onAdd)
        {
            if (type == null) return;
            List<string> pathSegments = type.FullName.Split('.').ToList();
            pathSegments.RemoveAll(test => test is "Condition" or "Conditions");
            if (pathSegments.Count > 1) pathSegments.RemoveAt(0);
            
            string typeName = pathSegments[^1];
            typeName = AddSpacesToSentence(typeName);
            List<string> typeNameSegments = typeName.Split(' ').ToList();
            typeNameSegments.RemoveAll(test => test is "Condition" or "Conditions");
            pathSegments[^1] = "Add " + string.Join(' ', typeNameSegments);
                
            string path = string.Join('/', pathSegments);
            AddMenuItem(menu, type, typeName, path, asset, onAdd);
        }

        private static void AddMenuItem(GenericMenu menu, Type type, string typeName, string path, Object asset, Action<Condition> onAdd)
        {
            menu.AddItem(new GUIContent(path), false,
                () =>
                {
                    Condition condition = ScriptableObject.CreateInstance(type) as Condition;
                    if (!condition) return;
                    condition.name = "New " + typeName;
                    AssetDatabase.AddObjectToAsset(condition, asset);
                    onAdd?.Invoke(condition);
                    EditorUtility.SetDirty(asset);
                    AssetDatabase.SaveAssets();
                });
        }

        private static string AddSpacesToSentence(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;
            StringBuilder newText = new StringBuilder(text.Length * 2);
            newText.Append(text[0]);
            for (int i = 1; i < text.Length; i++)
            {
                if (char.IsUpper(text[i]))
                    if (text[i - 1] != ' ' && !char.IsUpper(text[i - 1]))
                        newText.Append(' ');
                newText.Append(text[i]);
            }
            return newText.ToString();
        }
    }
}
