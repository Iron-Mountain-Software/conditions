using IronMountain.SaveSystem;
using UnityEngine;

namespace IronMountain.Conditions.ScriptedValues
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Conditions/Scripted Values/Int")]
    public class ConditionScriptedInt : Condition
    {
        [SerializeField] private ScriptedSavedInt scriptedInt;
        [SerializeField] private NumericalComparisonType comparison;
        [SerializeField] private int target;

        private void OnEnable()
        {
            if (scriptedInt) scriptedInt.OnValueChanged += FireOnConditionStateChanged;
        }

        private void OnDisable()
        {
            if (scriptedInt) scriptedInt.OnValueChanged -= FireOnConditionStateChanged;
        }
        
        public override bool Evaluate() => scriptedInt && EvaluationUtilities.Compare(scriptedInt.Value, target, comparison);

        public override string DefaultName => (scriptedInt ? scriptedInt.name : "NULL") 
                                              + " is " + comparison + " " + target;
        public override string NegatedName => (scriptedInt ? scriptedInt.name : "NULL") 
                                              + " is NOT " + comparison + " " + target;
        public override Sprite Depiction => null;
        
        public override bool HasErrors() => !scriptedInt;
    }
}
