using System;
using UnityEngine;

namespace IronMountain.Conditions
{
    public abstract class Condition : ScriptableObject, ICondition
    {
        public event Action OnConditionStateChanged;
        public abstract bool Evaluate();

        public abstract Sprite Depiction { get; }

        protected void FireOnConditionStateChanged()
        {
            OnConditionStateChanged?.Invoke();
        }

        public abstract bool HasErrors();

#if UNITY_EDITOR
        
        [ContextMenu("Refresh Name")]
        private void RefreshName()
        {
            string path = UnityEditor.AssetDatabase.GetAssetPath(this);
            name = ToString();
            UnityEditor.AssetDatabase.RenameAsset(path, name);
            UnityEditor.AssetDatabase.SaveAssets();
        }

#endif

    }
}