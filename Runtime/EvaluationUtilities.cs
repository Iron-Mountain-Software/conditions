using IronMountain.Conditions.Runtime;
using UnityEngine;

namespace IronMountain.Conditions
{
    public static class EvaluationUtilities
    {
        public static bool Compare(bool test, bool target, BooleanComparisonType comparison)
        {
            switch (comparison)
            {
                case BooleanComparisonType.Is:
                    return test == target;
                case BooleanComparisonType.IsNot:
                    return test != target;
                default:
                    return false;
            }
        }
        
        public static bool Compare(int test, int target, BooleanComparisonType comparison)
        {
            switch (comparison)
            {
                case BooleanComparisonType.Is:
                    return test == target;
                case BooleanComparisonType.IsNot:
                    return test != target;
                default:
                    return false;
            }
        }
        
        public static bool Compare(float test, float target, BooleanComparisonType comparison)
        {
            switch (comparison)
            {
                case BooleanComparisonType.Is:
                    return test == target;
                case BooleanComparisonType.IsNot:
                    return test != target;
                default:
                    return false;
            }
        }
        
        public static bool Compare(string test, string target, BooleanComparisonType comparison)
        {
            switch (comparison)
            {
                case BooleanComparisonType.Is:
                    return string.Equals(test, target);
                case BooleanComparisonType.IsNot:
                    return !string.Equals(test, target);
                default:
                    return false;
            }
        }
        
        public static bool Compare(Object test, Object target, BooleanComparisonType comparison)
        {
            switch (comparison)
            {
                case BooleanComparisonType.Is:
                    return test == target;
                case BooleanComparisonType.IsNot:
                    return test != target;
                default:
                    return false;
            }
        }

        public static bool Compare(int test, int target, NumericalComparisonType comparison)
        {
            switch (comparison)
            {
                case NumericalComparisonType.EqualTo:
                    return test == target;
                case NumericalComparisonType.NotEqualTo:
                    return test != target;
                case NumericalComparisonType.GreaterThan:
                    return test > target;
                case NumericalComparisonType.GreaterThanOrEqualTo:
                    return test >= target;
                case NumericalComparisonType.LessThan:
                    return test < target;
                case NumericalComparisonType.LessThanOrEqualTo:
                    return test <= target;
                default:
                    return false;
            }
        }
        
        public static bool Compare(float test, float target, NumericalComparisonType comparison)
        {
            switch (comparison)
            {
                case NumericalComparisonType.EqualTo:
                    return test == target;
                case NumericalComparisonType.NotEqualTo:
                    return test != target;
                case NumericalComparisonType.GreaterThan:
                    return test > target;
                case NumericalComparisonType.GreaterThanOrEqualTo:
                    return test >= target;
                case NumericalComparisonType.LessThan:
                    return test < target;
                case NumericalComparisonType.LessThanOrEqualTo:
                    return test <= target;
                default:
                    return false;
            }
        }
    }
}