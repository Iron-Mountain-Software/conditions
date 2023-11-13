using IronMountain.SaveSystem;
using UnityEngine;

namespace IronMountain.Conditions.ScriptedValues
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Conditions/Scripted Values/Float")]
    public class ConditionScriptedFloat : Condition
    {
        [SerializeField] private ScriptedSavedFloat scriptedFloat;
        [SerializeField] private NumericalComparisonType comparison;
        [SerializeField] private float target;

        private void OnEnable()
        {
            if (scriptedFloat) scriptedFloat.OnValueChanged += FireOnConditionStateChanged;
        }

        private void OnDisable()
        {
            if (scriptedFloat) scriptedFloat.OnValueChanged -= FireOnConditionStateChanged;
        }

        public override bool Evaluate() => scriptedFloat && EvaluationUtilities.Compare(scriptedFloat.Value, target, comparison);

        public override string DefaultName => (scriptedFloat ? scriptedFloat.name : "NULL") 
                                              + " is " + comparison + " " + target;
        public override string NegatedName => (scriptedFloat ? scriptedFloat.name : "NULL") 
                                              + " is NOT " + comparison + " " + target;
        public override Sprite Depiction => null;
        
        public override bool HasErrors() => !scriptedFloat;
    }
}