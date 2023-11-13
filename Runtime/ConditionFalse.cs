using UnityEngine;

namespace IronMountain.Conditions
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Conditions/False", order = 1)]
    public class ConditionFalse : Condition
    {
        public override bool Evaluate() => false;
        public override string DefaultName => "False";
        public override string NegatedName => "NOT False";
        public override Sprite Depiction { get; }
        public override bool HasErrors() => false;
    }
}