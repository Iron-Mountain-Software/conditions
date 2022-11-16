using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Core/Conditions/Types/Always False")]
public class ConditionFalse : Condition
{
    public override bool Evaluate() => false;
    public override string DefaultName => "False";
    public override string NegatedName => "NOT False";
    public override Sprite Depiction { get; }
    public override bool HasErrors() => false;
}