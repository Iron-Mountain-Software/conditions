namespace SpellBoundAR.Conditions
{
    public static class ConditionEvaluation
    {
        public static bool Compare(int test, int target, ConditionComparisonType comparison)
        {
            switch (comparison)
            {
                case ConditionComparisonType.EqualTo:
                    return test == target;
                case ConditionComparisonType.NotEqualTo:
                    return test != target;
                case ConditionComparisonType.GreaterThan:
                    return test > target;
                case ConditionComparisonType.GreaterThanOrEqualTo:
                    return test >= target;
                case ConditionComparisonType.LessThan:
                    return test < target;
                case ConditionComparisonType.LessThanOrEqualTo:
                    return test <= target;
                default:
                    return false;
            }
        }
        
        public static bool Compare(float test, float target, ConditionComparisonType comparison)
        {
            switch (comparison)
            {
                case ConditionComparisonType.EqualTo:
                    return test == target;
                case ConditionComparisonType.NotEqualTo:
                    return test != target;
                case ConditionComparisonType.GreaterThan:
                    return test > target;
                case ConditionComparisonType.GreaterThanOrEqualTo:
                    return test >= target;
                case ConditionComparisonType.LessThan:
                    return test < target;
                case ConditionComparisonType.LessThanOrEqualTo:
                    return test <= target;
                default:
                    return false;
            }
        }
    }
}