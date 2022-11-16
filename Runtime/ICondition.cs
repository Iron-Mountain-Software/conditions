using System;

namespace SpellBoundAR.Conditions
{
    public interface ICondition
    {
        public event Action OnConditionStateChanged;
        public bool Evaluate();
    }
}