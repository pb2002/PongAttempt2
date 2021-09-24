using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
namespace PongAttempt2
{
    public class Ball
    {
        private static Random random = new Random();
        
        public Vector2 position;
        public Texture2D sprite;
        public Vector2 direction;
        public float speed;
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

        public Ball(Vector2 position, Vector2 direction, float speed)
        {
            this.position = position;
            this.direction = direction;
            this.speed = speed;
        }

        public void Move(float dt)
        {
            position += direction * speed * dt;
        }

        public void Reset()
        {
            direction = new Vector2((float) random.NextDouble(), (float) random.NextDouble());
            direction.Normalize();
            position = PongGame.screenSize / 2f;
        }
        public bool CheckPlayerCollision(Player player)
        {
            var pBounds = player.Bounds;
            var playerDir = player.playerId == 0 ? 1 : -1;
            
            if (!Bounds.Intersects(pBounds)) return false;
            
            position.X = player.position.X + playerDir * (0.2f + Size.X + pBounds.Width) * 0.5f;

            direction.X = playerDir;
            var heightDiff = position.Y - player.position.Y;
            direction.Y = 1.5f * heightDiff / pBounds.Height;
  
            direction.Normalize();
            return true;
        }

        public bool CheckWallCollision()
        {
            if (!(position.Y < Size.Y / 2f) && !(position.Y > PongGame.screenSize.Y - Size.Y / 2f)) return false;
            
            direction.Y *= -1;
            return true;
        }
        public void Draw()
        {
            Renderer.instance.DrawSpriteCentered(sprite,position,Color.White);
        }
    }
}