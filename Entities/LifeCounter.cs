using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong
{
    public class LifeCounter : Entity
    {
        private static readonly Color heartColor = new Color(255, 64, 128);
        private float spacing;

        public LifeCounter(Vector2 position, float spacing) : base(position)
        {
            this.spacing = spacing;
            this.sprite = Assets.heartTexture;
        }

        public void Draw(int lives)
        {
            var drawPos = position;
            float widthOffset = sprite.Width;
            drawPos.X -= (spacing + widthOffset) * (lives-1) * 0.5f;
            for (int i = 0; i < lives; i++)
            {
                Renderer.instance.DrawSpriteCentered(sprite, drawPos, heartColor, false);
                drawPos.X += spacing + widthOffset;
            }
        }
    }
}