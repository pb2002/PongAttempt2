using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PongAttempt2
{
    public class Player
    {
        public int playerId;
        public Vector2 position;
        public Texture2D sprite;
        public float speed;

        public int lives = 3;
        public Rectangle Bounds
        {
            get
            {
                var b = sprite.Bounds;
                b.Offset(position-b.Size.ToVector2()/2);
                return b;
            }
        }

        public Player(int playerId, Vector2 position, float speed)
        {
            this.position = position;
            this.playerId = playerId;
            this.speed = speed;
        }

        public void Move(float dt)
        {
            position.Y += InputHandler.instance.PlayerMovementInput[playerId] * speed * dt;
            position.Y = MathHelper.Clamp(position.Y, sprite.Height / 2f, speed - sprite.Height / 2f);
        }
        public void Draw()
        {
            Renderer.instance.DrawSpriteCentered(sprite, position, Color.White, playerId == 1);
        }
    }
}