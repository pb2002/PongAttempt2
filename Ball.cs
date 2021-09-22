using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PongAttempt2
{
    public class Ball
    {
        public Vector2 position;
        public Texture2D sprite;
        public Vector2 direction;
        public float speed;

        public Ball(Vector2 position, Vector2 direction, float speed)
        {
            this.position = position;
            this.direction = direction;
            this.speed = speed;
            
        }
        public void Draw()
        {
            Renderer.instance.DrawSpriteCentered(sprite,position,Color.White);
        }
    }
}