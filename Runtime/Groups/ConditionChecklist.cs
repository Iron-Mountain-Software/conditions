using System;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace IronMountain.Conditions.Groups
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Conditions/Groups/Checklist")]
    public class ConditionChecklist : Condition
    {
        [Serializable]
        public class Entry
        {
            public Condition condition;
        }

        [SerializeField] private bool allRequired = true;
        [SerializeField] private int amountRequired;
        [SerializeField] private List<Entry> conditions = new ();
        
        public bool AllRequired => allRequired;
        public int AmountRequired => amountRequired;
        public List<Entry> Conditions => conditions;

        private void OnEnable()
        {
            foreach (Entry entry in conditions)
            {
                if (entry.condition) entry.condition.OnConditionStateChanged += FireOnConditionStateChanged;
            }
        }

        private void OnDisable()
        {
            foreach (Entry entry in conditions)
            {
                if (entry.condition) entry.condition.OnConditionStateChanged -= FireOnConditionStateChanged;
            }
        }

        public override bool Evaluate()
        {
            int amountSatisfied = 0;
            foreach (Entry entry in conditions)
            {
                if (entry == null || !entry.condition) continue;
                if (entry.condition.Evaluate()) amountSatisfied++;
            }
            return allRequired ? amountSatisfied == conditions.Count : amountSatisfied >= amountRequired;
        }


        public override Sprite Depiction => conditions.Count > 0 && conditions[0].condition 
                ? conditions[0].condition.Depiction 
                : null;
        
        public override bool HasErrors()
        {
            int validCount = 0;
            foreach (Entry entry in conditions)
            {
                if (entry == null || !entry.condition) return true;
                if (entry.condition.HasErrors()) return true;
                validCount++;
            }
            return validCount < 1;
        }
        
        public override string ToString() => "Checklist";

#if UNITY_EDITOR

        private void OnValidate()
        {
            amountRequired = allRequired 
                ? conditions.Count
                : Mathf.Clamp(amountRequired, 0, conditions.Count);
        }
        
        private void OnDestroy()
        {
            foreach (Entry entry in conditions)
            {
                if (entry == null || !entry.condition) continue;
                AssetDatabase.RemoveObjectFromAsset(entry.condition);
                DestroyImmediate(entry.condition);
            }
            AssetDatabase.SaveAssets();
        }

#endif
    }
}
