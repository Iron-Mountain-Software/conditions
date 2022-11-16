using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Editor
{
    public class AddConditionMenu
    {
        public event Action<Condition> OnConditionCreated;

        private static void AddConditionToAsset(Object asset, Condition condition, string name)
        {
            if (!asset || !condition || string.IsNullOrEmpty(AssetDatabase.GetAssetPath(asset))) return;
            AssetDatabase.AddObjectToAsset(condition, asset);
            condition.name = name;
            AssetDatabase.SaveAssets();
        }
        
        public AddConditionMenu(Object asset, string name)
        {
            GenericMenu menu = new GenericMenu();
            foreach (Type derivedType in GetDerivedTypes<Condition>())
            {
                if (derivedType == null || string.IsNullOrWhiteSpace(derivedType.FullName)) continue;
                
                List<string> pathSegments = derivedType.FullName.Split('.').ToList();
                pathSegments.RemoveAll(test => test is "ARISE" or "Condition" or "Conditions");
                
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
                        AddConditionToAsset(asset, condition, name);
                        OnConditionCreated?.Invoke(condition);
                    });
            }
            menu.ShowAsContext();
        }
        
        public static List<Type> GetDerivedTypes<T>()
        {
            List<Type> derivedTypes = new List<Type>();
            
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                IEnumerable<Type> assemblyTypes = assembly.GetTypes()
                    .Where(type => type.IsSubclassOf(typeof(T)) && !type.IsAbstract);
                derivedTypes.AddRange(assemblyTypes);
            }
            
            return derivedTypes;
        }
        
        public static string AddSpacesToSentence(string text)
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
