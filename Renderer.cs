using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong
{
    public enum TextAlign
    {
        TopLeft = 0,
        TopCenter = 1,
        TopRight = 2,
        MidLeft = 3,
        MidCenter = 4,
        MidRight = 5,
        BottomLeft = 6,
        BottomCenter = 7,
        BottomRight = 8
    }
    public class Renderer : Singleton<Renderer>
    {
        public static readonly Color 
            titleColor = new Color(240, 174, 64),
            subtitleColor = new Color(255, 208, 80),
            buttonColor = new Color(48, 48, 48),
            buttonHoverColor = new Color(80, 184, 255);

        public SpriteBatch spriteBatch;
        public void Begin() => spriteBatch.Begin();
        public void End() => spriteBatch.End();
        
        public void DrawSpriteScaled(Texture2D sprite, Vector2 position, Vector2 scale, Color color)
        {
            spriteBatch.Draw(sprite, position, null, color, 0, Vector2.Zero, scale, SpriteEffects.None, 0.5f);
        }
        public void DrawSpriteCentered(Texture2D sprite, Vector2 position, Color color, bool flip = false)
        {
            var offset = new Vector2(sprite.Width / 2f, sprite.Height / 2f);
            spriteBatch.Draw(
                sprite, 
                position-offset, 
                null, color, 0, Vector2.Zero, 1, 
                flip ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 
                0.5f);
        }
        public void DrawSpriteCentered(Texture2D sprite, Vector2 position, Color color, int rotation)
        {
            
            var offset = new Vector2(sprite.Width / 2f, sprite.Height / 2f);
            switch (rotation)
            {
                case 0: break;
                case 1:
                    offset = new Vector2(-offset.Y, offset.X);
                    break;
                case 2:
                    offset = new Vector2(-offset.X, -offset.Y);
                    break;
                case 3:
                    offset = new Vector2(offset.Y, -offset.X);
                    break;
            }
            spriteBatch.Draw(
                sprite, 
                position-offset, 
                null, color, MathHelper.PiOver2*rotation, Vector2.Zero, 1, 
                SpriteEffects.None, 
                0.5f);
        }
        public void DrawText(SpriteFont font, string text, Vector2 position, Color color, TextAlign align = TextAlign.MidCenter)
        {
            int hAlign = (int)align % 3;
            int vAlign = ((int)align - hAlign)/3;

            (float width, float height) = font.MeasureString(text)/2;
            Vector2 anchor = new Vector2(width * hAlign, height * vAlign);
            spriteBatch.DrawString(font, text, position, color, 0, anchor, 1.0f, SpriteEffects.None, 0.5f);
        }
    }
}