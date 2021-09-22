using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;

namespace PongAttempt2
{
    public class Renderer : Singleton<Renderer>
    {
        public SpriteBatch spriteBatch;

        public void DrawSpriteCentered(Texture2D sprite, Color color, bool flip = false)
        {
            var offset = new Vector2(sprite.Width / 2f, sprite.Height / 2f);
            spriteBatch.Draw(
                sprite, 
                -offset, 
                null, color, 0, Vector2.Zero, 1, 
                flip ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 
                0.5f);
        }
    }
}