# Scriptable Conditions
*Version: 1.5.7*
## Description: 
Scriptable-object conditions that make it easy to reuse gameplay logic.
## Dependencies: 
* com.iron-mountain.save-system (1.0.4)
---
## Key Scripts & Components: 
1. public enum **BooleanComparisonType** : Enum
1. public abstract class **Condition** : ScriptableObject
   * Actions: 
      * public event Action ***OnConditionStateChanged*** 
   * Properties: 
      * public Sprite ***Depiction***  { get; }
   * Methods: 
      * public abstract Boolean ***Evaluate***()
      * public abstract Boolean ***HasErrors***()
1. public class **ConditionFalse** : Condition
   * Properties: 
      * public Sprite ***Depiction***  { get; }
   * Methods: 
      * public override Boolean ***Evaluate***()
      * public override Boolean ***HasErrors***()
      * public override String ***ToString***()
1. public class **ConditionTrue** : Condition
   * Properties: 
      * public Sprite ***Depiction***  { get; }
   * Methods: 
      * public override Boolean ***Evaluate***()
      * public override Boolean ***HasErrors***()
      * public override String ***ToString***()
1. public enum **ConditionalOperatorType** : Enum
1. public static class **EvaluationUtilities**
1. public interface **ICondition**
   * Actions: 
      * public event Action ***OnConditionStateChanged*** 
   * Methods: 
      * public abstract Boolean ***Evaluate***()
1. public enum **NullComparisonType** : Enum
1. public enum **NumericalComparisonType** : Enum
### Groups
1. public class **ConditionChecklist** : Condition
   * Properties: 
      * public Boolean ***AllRequired***  { get; }
      * public Int32 ***AmountRequired***  { get; }
      * public List<Entry> ***Conditions***  { get; }
      * public Sprite ***Depiction***  { get; }
   * Methods: 
      * public override Boolean ***Evaluate***()
      * public override Boolean ***HasErrors***()
      * public override String ***ToString***()
1. public class **ConditionEquation** : Condition
   * Properties: 
      * public List<Entry> ***Conditions***  { get; }
      * public Sprite ***Depiction***  { get; }
   * Methods: 
      * public override Boolean ***Evaluate***()
      * public override Boolean ***HasErrors***()
      * public override String ***ToString***()
### Scripted Values
1. public class **ConditionScriptedBool** : Condition
   * Properties: 
      * public Sprite ***Depiction***  { get; }
   * Methods: 
      * public override Boolean ***Evaluate***()
      * public override Boolean ***HasErrors***()
      * public override String ***ToString***()
1. public class **ConditionScriptedFloat** : Condition
   * Properties: 
      * public Sprite ***Depiction***  { get; }
   * Methods: 
      * public override Boolean ***Evaluate***()
      * public override Boolean ***HasErrors***()
      * public override String ***ToString***()
1. public class **ConditionScriptedInt** : Condition
   * Properties: 
      * public Sprite ***Depiction***  { get; }
   * Methods: 
      * public override Boolean ***Evaluate***()
      * public override Boolean ***HasErrors***()
      * public override String ***ToString***()
1. public class **ConditionScriptedString** : Condition
   * Properties: 
      * public Sprite ***Depiction***  { get; }
   * Methods: 
      * public override Boolean ***Evaluate***()
      * public override Boolean ***HasErrors***()
      * public override String ***ToString***()
