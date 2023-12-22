using UnityEngine;

namespace IronMountain.Conditions
{
    public enum NumericalComparisonType
    {
        [InspectorName("==")]
        EqualTo = 0,
        [InspectorName("!=")]
        NotEqualTo = 1,
        [InspectorName(">")]
        GreaterThan = 2,
        [InspectorName(">=")]
        GreaterThanOrEqualTo = 3,
        [InspectorName("<")]
        LessThan = 4,
        [InspectorName("<=")]
        LessThanOrEqualTo = 5
    }
}