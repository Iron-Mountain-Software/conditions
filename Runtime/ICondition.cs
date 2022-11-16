using System;

public interface ICondition
{
    public event Action OnConditionStateChanged;
    public bool Evaluate();
}