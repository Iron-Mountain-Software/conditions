using UnityEngine;

namespace IronMountain.Conditions
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Conditions/True", order = 0)]
    public class ConditionTrue : Condition
    {
        public override bool Evaluate() => true;
        public override Sprite Depiction => null;
        public override bool HasErrors() => false;
        public override string ToString() => "True";
    }
}