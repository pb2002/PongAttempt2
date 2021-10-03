using Microsoft.Xna.Framework;

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
            // lives should be centered so move position to the left by width / 2
            // width = (spacing + widthOffset) * (lives - 1)
            drawPos.X -= (spacing + widthOffset) * (lives - 1) * 0.5f;
            for (int i = 0; i < lives; i++)
            {
                Renderer.Instance.DrawSpriteCentered(sprite, drawPos, heartColor, false);
                drawPos.X += spacing + widthOffset; // move to the right
            }
        }
    }
}