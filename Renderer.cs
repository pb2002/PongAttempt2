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
            buttonColor = new Color(160, 176, 192),
            buttonHoverColor = new Color(80, 184, 255);

        public SpriteBatch spriteBatch;
        public void Begin() => spriteBatch.Begin();
        public void End() => spriteBatch.End();
        
        /// <summary>
        /// Draw a sprite at the given position with the given scale
        /// </summary>
        /// <param name="sprite">The sprite to draw</param>
        /// <param name="position">The position of the top-left corner of the sprite</param>
        /// <param name="scale">The scale of the sprite</param>
        /// <param name="color">The color of the sprite</param>
        public void DrawSpriteScaled(Texture2D sprite, Vector2 position, Vector2 scale, Color color)
        {
            spriteBatch.Draw(sprite, position, null, color, 0, Vector2.Zero, scale, SpriteEffects.None, 0.5f);
        }
        /// <summary>
        /// Draw a sprite centered at the given position
        /// </summary>
        /// <param name="sprite">The sprite to draw</param>
        /// <param name="position">The position to draw the sprite at</param>
        /// <param name="color">The color of the sprite</param>
        /// <param name="flip">Flip the sprite horizontally</param>
        public void DrawSpriteCentered(Texture2D sprite, Vector2 position, Color color, bool flip = false)
        {
            // sprite gets drawn at top left corner, subtract half the size to get it centered.
            var offset = new Vector2(sprite.Width / 2f, sprite.Height / 2f);
            spriteBatch.Draw(
                sprite, 
                position-offset, 
                null, color, 0, Vector2.Zero, 1, 
                flip ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 
                0.5f);
        }
        /// <summary>
        /// Draw text on screen.
        /// </summary>
        /// <param name="font">The font to use</param>
        /// <param name="text">The text to draw</param>
        /// <param name="position">The position to draw the text at</param>
        /// <param name="color">The color of the text</param>
        /// <param name="align">The alignment of the text relative to the position</param>
        public void DrawText(SpriteFont font, string text, Vector2 position, Color color, TextAlign align = TextAlign.MidCenter)
        {
            int hAlign = (int)align % 3;
            int vAlign = ((int)align - hAlign)/3;
            
            string[] lines = text.Split('\n');
            
            // vertical offset (changes per line, starts depending on align
            float vOffset = -vAlign * font.LineSpacing * lines.Length/2f;
            // go through each line
            foreach (string line in lines)
            {
                (float width, float height) = font.MeasureString(line)/2;
                Vector2 anchor = new Vector2(width * hAlign, 0);
                spriteBatch.DrawString(font, line, position + vOffset * Vector2.UnitY, color, 0, anchor, 1.0f, SpriteEffects.None, 0.5f);
                vOffset += font.LineSpacing; // go one line down
            }
        }
    }
}