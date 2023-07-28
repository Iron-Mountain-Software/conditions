using UnityEngine;

namespace IronMountain.Conditions.Editor
{
    public static class Styles
    {
        public static readonly GUIStyle GreenBox = new ()
        {
            margin = new RectOffset(1, 1, 1, 1),
            alignment = TextAnchor.MiddleCenter,
            normal = {textColor = Color.white, background = Textures.GreenTexture}
        };
        
        public static readonly GUIStyle RedBox = new ()
        {
            margin = new RectOffset(1, 1, 1, 1),
            alignment = TextAnchor.MiddleCenter,
            normal = {textColor = Color.white, background = Textures.RedTexture}
        };
        
        public static readonly GUIStyle Container = new ()
        {
            padding = new RectOffset(7,7,7,7),
            normal = { background = Textures.ContainerTexture }
        };
    }
}
