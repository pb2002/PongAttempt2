using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace Pong
{
    public static class Assets
    {
        public static Texture2D playerTexture;
        public static Texture2D ballTexture;
        public static Texture2D heartTexture;
        public static Texture2D fadeTexture;
        
        public static SpriteFont titleFont;
        public static SpriteFont subtitleFont;
        public static SpriteFont smallFont;
                
        public static AudioClip playerHitSFX;
        public static AudioClip wallHitSFX;
        public static AudioClip clickSFX;
        
        public static AudioClip musicBaseLayer;
        public static AudioClip musicTopLayer;

    }
}