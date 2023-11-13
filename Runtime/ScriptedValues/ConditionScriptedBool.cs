using IronMountain.SaveSystem;
using UnityEngine;

namespace IronMountain.Conditions.ScriptedValues
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Conditions/Scripted Values/Bool")]
    public class ConditionScriptedBool : Condition
    {
        [SerializeField] private ScriptedSavedBool scriptedBool;
        [SerializeField] private BooleanComparisonType comparison;
        [SerializeField] private bool target;

        private void OnEnable()
        {
            if (scriptedBool) scriptedBool.OnValueChanged += FireOnConditionStateChanged;
        }

        private void OnDisable()
        {
            if (scriptedBool) scriptedBool.OnValueChanged -= FireOnConditionStateChanged;
        }
        
        public override bool Evaluate() => scriptedBool && EvaluationUtilities.Compare(scriptedBool.Value, target, comparison);

        public override string DefaultName => (scriptedBool ? scriptedBool.name : "NULL") 
                                              + " is " + comparison + " " + target;
        public override string NegatedName => (scriptedBool ? scriptedBool.name : "NULL") 
                                              + " is NOT " + comparison + " " + target;
        public override Sprite Depiction => null;
        
        public override bool HasErrors() => !scriptedBool;
    }
}