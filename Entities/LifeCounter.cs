using Microsoft.Xna.Framework;

namespace Pong
{
    public class LifeCounter : Entity
    {
        private Player player;
        private const float spacing = 20f;

        public LifeCounter(Vector2 position, Player player) : base(position)
        {
            sprite = Assets.heartTexture;
            this.player = player;
        }

        public void Draw()
        {
            float heartOffset = sprite.Width + spacing; 
            // lives should be centered, so move the draw position to the left by width / 2
            // where width = heartOffset * (lives - 1)
            Vector2 drawPosition = position - Vector2.UnitX * heartOffset * (player.lives - 1) * 0.5f;
            for (int i = 0; i < player.lives; i++)
            {
                // draw a heart <3
                Renderer.Instance.DrawSpriteCentered(sprite, drawPosition, Renderer.heartColor, false);
                drawPosition.X += heartOffset; // move the draw position one spot to the right
            }
        }
    }
}