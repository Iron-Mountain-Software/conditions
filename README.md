# Conditions

_Scriptable-object conditions that make it easy to reuse gameplay logic._

SOME CODING REQUIRED

When implementing abstract systems that require conditional logic (i.e. quests, dialogue, story events), it is helpful to separate conditional logic from the systems themselves. For example, each of my Dialogue Interaction objects references a Condition that determines if the interaction should play or not.

This separation of responsibilities makes it easy to reuse conditional logic and conditional objects across multiple systems.

---

### Use Cases
* Abstract systems that require conditional logic.
* Determining what dialogue to play.
* Determining when quests or missions should activate.
* Determining when quest/mission requirements have been satisfied.
* Anytime anything needs to be decided ¯\_(ツ)_/¯

---

### Directions for Use

1. To create a new type of condition, first determine the gameplay logic that your condition will reference.
1. Create a new script, and inherit from Condition.
1. Fill out the 5 abstract pieces:
   * DefaultName
     * Makes conditions easy to read in the inspector.
     * Example: "Player has more than X coins."
   * NegatedName
     * Makes negated conditions readable in the inspector.
     * Example: "Player doesn't have more than X coins."
   * Depiction
     * Useful when conditions need to be graphically rendered.
   * Evaluate()
     * Returns true if a condition is met, false if not.
   * HasErrors()
     * Returns true if the condition has errors (i.e. null reference), false if not.
1. Conditions also have a public event, OnConditionStateChanged, that can be subscribed to for updates of the state of the condition. However, conditions must call FireOnConditionStateChanged() at the right time for this event to work properly. For example, a PlayerHasCoins condition would listen to all player coin count changes, and call FireOnConditionStateChanged() when the condition is satisfied or unsatisfied.

---

### Key Components

1. Condition Scriptable Object base class
   * Inherit from this class and implement the logic of your gameplay condition.
1. ConditionChecklist
   * A scriptable object condition that builds checklists out of other conditions.
1. ConditionEquation
   * A scriptable object condition that builds and/or equations out of other conditions.
