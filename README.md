# Scriptable Conditions
*Version: 1.5.9*
## Description: 
Scriptable-object conditions that make it easy to reuse gameplay logic.

SOME CODING REQUIRED

When implementing abstract systems that require conditional logic (i.e. quests, dialogue, story events), it is helpful to separate conditional logic from the systems themselves. For example, each of my Dialogue Interaction objects references a Condition that determines if the interaction should play or not.

This separation of responsibilities makes it easy to reuse conditional logic and conditional objects across multiple systems.
## Use Cases: 
* Abstract systems that require conditional logic.
* Determining what dialogue to play.
* Determining when quests or missions should activate.
* Determining when quest/mission requirements have been satisfied. 
* Anytime anything needs to be decided ¯\_(ツ)_/¯ 
## Dependencies: 
* com.iron-mountain.save-system (1.0.4)
## Package Mirrors: 
[<img src='https://img.itch.zone/aW1nLzEzNzQ2ODg3LnBuZw==/original/npRUfq.png'>](https://github.com/Iron-Mountain-Software/conditions.git)[<img src='https://img.itch.zone/aW1nLzEzNzQ2ODk4LnBuZw==/original/Rv4m96.png'>](https://iron-mountain.itch.io/scripted-conditions)[<img src='https://img.itch.zone/aW1nLzEzNzQ2ODkyLnBuZw==/original/Fq0ORM.png'>](https://www.npmjs.com/package/com.iron-mountain.conditions)
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
