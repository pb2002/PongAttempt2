using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PongAttempt2
{
    public class Player
    {
        public Vector2 position;
        public Texture2D sprite;
        public int playerId;s
        public void Draw()
        {
            Renderer.instance.DrawSpriteCentered(sprite, Color.White, playerId == 1);
        }
    }
}