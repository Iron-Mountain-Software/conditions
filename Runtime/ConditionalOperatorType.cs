using UnityEngine;

namespace IronMountain.Conditions
{
    public enum ConditionalOperatorType
    {
        [InspectorName("")]
        None = 0,
        [InspectorName("AND")]
        And = 1,
        [InspectorName("OR")]
        Or = 2
    }
}