using UnityEngine;

namespace SpellBoundAR.Conditions.Editor
{
    public static class Textures
    {
        
        private static Texture2D _redTexture;
        private static Texture2D _greenTexture;
        private static Texture2D _containerTexture;

        public static Texture2D RedTexture {
            get
            {
                if (!_redTexture) _redTexture = InitializeTexture(new Color(0.76f, 0.11f, 0f));
                return _redTexture;
            }
        }

        public static Texture2D GreenTexture {
            get
            {
                if (!_greenTexture) _greenTexture = InitializeTexture(new Color(0.09f, 0.78f, 0.11f));
                return _greenTexture;
            }
        }

        public static Texture2D ContainerTexture {
            get
            {
                if (!_containerTexture) _containerTexture = InitializeTexture(new Color(0.15f, 0.15f, 0.15f));
                return _containerTexture;
            }
        }
        
        private static Texture2D InitializeTexture(Color color)
        {
            Texture2D texture = new Texture2D(1, 1);
            texture.SetPixel(0,0, color);
            texture.Apply();
            return texture;
        }
    }
}
