using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PongAttempt2
{
    public class Player
    {
        public Vector2 position;
        public Texture2D sprite;
        public int playerId;

        public Player(Vector2 position, int playerId)
        {
            this.position = position;
            this.playerId = playerId;
        }
        public void Draw()
        {
            Renderer.instance.DrawSpriteCentered(sprite, position, Color.White, playerId == 1);
        }
    }
}