using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PongAttempt2
{
    public class LifeCounter
    {
        public Vector2 position;
        public float spacing;
        public Texture2D sprite;

        public LifeCounter(Vector2 position, float spacing)
        {
            this.position = position;
            this.spacing = spacing;
        }

        public void Draw(int lives)
        {
            var drawPos = position;
            float widthOffset = sprite.Width;
            drawPos.X -= (spacing + widthOffset) * (lives-1) * 0.5f;
            for (int i = 0; i < lives; i++)
            {
                Renderer.instance.DrawSpriteCentered(sprite, drawPos, new Color(255,32,64), false);
                drawPos.X += spacing + widthOffset;
            }
        }
    }
}