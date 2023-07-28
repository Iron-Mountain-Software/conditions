using System;

namespace IronMountain.Conditions
{
    public interface ICondition
    {
        public event Action OnConditionStateChanged;
        public bool Evaluate();
    }
}