using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PongAttempt2
{
    public class Player
    {
        private static readonly Color[] playerColors = { new Color(255,128,72), new Color(72,128,255) };
        
        public int playerId;
        public Vector2 normal;
        public Vector2 position;
        public Texture2D sprite;
        public float speed;
        public int lives = 1;
        public Rectangle Bounds
        {
            get
            {
                var b = sprite.Bounds;
                b.Offset(position-b.Size.ToVector2()/2);
                return b;
            }
        }
        public Vector2 Size => new Vector2(sprite.Width, sprite.Height);
        public Player(int playerId, Vector2 position, Vector2 normal, float speed)
        {
            this.playerId = playerId;
            this.position = position;
            this.normal = normal;
            this.speed = speed;
        }

        public virtual void Move(float dt)
        {
            position.Y += InputHandler.instance.PlayerMovementInput[playerId] * speed * dt;
            ClampYPosition();
        }

        protected void ClampYPosition()
        {
            position.Y = MathHelper.Clamp(position.Y, sprite.Height / 2f, PongGame.screenSize.Y - sprite.Height / 2f);
        }
        public void Draw()
        {
            Renderer.instance.DrawSpriteCentered(sprite, position, playerColors[playerId], playerId == 1);
        }
    }
}