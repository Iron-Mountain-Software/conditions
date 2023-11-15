using IronMountain.SaveSystem;
using UnityEngine;

namespace IronMountain.Conditions.ScriptedValues
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Conditions/Scripted Values/String")]
    public class ConditionScriptedString : Condition
    {
        [SerializeField] private ScriptedSavedString scriptedString;
        [SerializeField] private BooleanComparisonType comparison;
        [SerializeField] private string target;

        private void OnEnable()
        {
            if (scriptedString) scriptedString.OnValueChanged += FireOnConditionStateChanged;
        }

        private void OnDisable()
        {
            if (scriptedString) scriptedString.OnValueChanged -= FireOnConditionStateChanged;
        }
        
        public override bool Evaluate() => scriptedString && EvaluationUtilities.Compare(scriptedString.Value, target, comparison);

        public override Sprite Depiction => null;
        
        public override bool HasErrors() => !scriptedString;

        public override string ToString() => (scriptedString ? scriptedString.name : "NULL") 
                                             + " is " + comparison + " " + target;
    }
}