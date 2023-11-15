using UnityEditor;
using UnityEngine;

namespace IronMountain.Conditions.Editor
{
    [CustomEditor(typeof(ConditionTrue), true)]
    public class ConditionTrueInspector : ConditionInspector
    {
        protected override void DrawProperties()
        {
            GUILayout.Label("ALWAYS TRUE");
        }
    }
}