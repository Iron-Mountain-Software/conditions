using UnityEngine;

namespace IronMountain.Conditions
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Conditions/True", order = 0)]
    public class ConditionTrue : Condition
    {
        public override bool Evaluate() => true;
        public override string DefaultName => "True";
        public override string NegatedName => "NOT True";
        public override Sprite Depiction => null;
        public override bool HasErrors() => false;
    }
}