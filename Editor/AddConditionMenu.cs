using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            foreach (Type derivedType in ConditionTypes)
            {
                if (derivedType == null || string.IsNullOrWhiteSpace(derivedType.FullName)) continue;
                
                List<string> pathSegments = derivedType.FullName.Split('.').ToList();
                pathSegments.RemoveAll(test => test is "Condition" or "Conditions");
                
                string typeName = pathSegments[^1];
                typeName = AddSpacesToSentence(typeName);
                List<string> typeNameSegments = typeName.Split(' ').ToList();
                typeNameSegments.RemoveAll(test => test is "Condition" or "Conditions");
                pathSegments[^1] = "Add " + string.Join(' ', typeNameSegments);;
                
                string path = string.Join('/', pathSegments);
                menu.AddItem(new GUIContent(path), false,
                    () =>
                    {
                        Condition condition = ScriptableObject.CreateInstance(derivedType) as Condition;
                        if (!condition) return;
                        condition.name = "New " + typeName;
                        AssetDatabase.AddObjectToAsset(condition, asset);
                        onAdd?.Invoke(condition);
                        EditorUtility.SetDirty(asset);
                        AssetDatabase.SaveAssets();
                    });
            }
            menu.ShowAsContext();
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
