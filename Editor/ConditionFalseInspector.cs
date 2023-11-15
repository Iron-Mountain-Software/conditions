using UnityEditor;
using UnityEngine;

namespace IronMountain.Conditions.Editor
{
    [CustomEditor(typeof(ConditionFalse), true)]
    public class ConditionFalseInspector : ConditionInspector
    {
        protected override void DrawProperties()
        {
            GUILayout.Label("ALWAYS FALSE");
        }
    }
}