using System;
using UnityEngine;

public abstract class Condition : ScriptableObject, ICondition
{
    public event Action OnConditionStateChanged;
    public abstract bool Evaluate();

    public abstract string DefaultName { get; }
    public abstract string NegatedName { get; }
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
        name = DefaultName;
        UnityEditor.AssetDatabase.RenameAsset(path, name);
        UnityEditor.AssetDatabase.SaveAssets();
    }

#endif

}