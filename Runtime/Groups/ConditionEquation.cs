using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace IronMountain.Conditions.Groups
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Conditions/Groups/Equation")]
    public class ConditionEquation : Condition
    {
        [Serializable]
        public class Entry
        {
            public ConditionalOperatorType conditionalOperatorType;
            public Condition condition;
        }

        [SerializeField] private List<Entry> conditions = new ();

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
            List<Tuple<ConditionalOperatorType, bool>> states = new List<Tuple<ConditionalOperatorType, bool>>();
            foreach (Entry entry in conditions)
            {
                if (!entry.condition) return false;
                states.Add(new Tuple<ConditionalOperatorType, bool>(
                    entry.conditionalOperatorType, 
                    entry.condition.Evaluate()));
            }
            for (int i = states.Count - 1; i > 0; i--)
            {
                if (states[i].Item1 == ConditionalOperatorType.Or) continue;
                states[i - 1] = new Tuple<ConditionalOperatorType, bool>(
                    states[i - 1].Item1,
                    states[i - 1].Item2 && states[i].Item2);
                states.RemoveAt(i);
            }
            return states.Any(test => test.Item2);
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
        
        public override string ToString() => "Equation";

#if UNITY_EDITOR
        
        private void OnValidate()
        {
            if (conditions.Count > 0) conditions[0].conditionalOperatorType = ConditionalOperatorType.None;
            for (int i = 1; i < conditions.Count; i++)
            {
                if (conditions[i].conditionalOperatorType == ConditionalOperatorType.None)
                    conditions[i].conditionalOperatorType = ConditionalOperatorType.And;
            }
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
