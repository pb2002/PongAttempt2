using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
namespace PongAttempt2
{
    public class Ball
    {
        private static Random random = new Random();
        private static Color ballColor = new Color(128, 144, 160);
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
            float rx = random.Next(0,1) == 1 ? -1 : 1;
            float ry = (float) random.NextDouble() * 1.5f;
            direction = new Vector2(rx, ry);
            direction.Normalize();
            position = PongGame.screenSize / 2f;
        }
        public bool CheckPlayerCollision(Player player)
        {
            Rectangle pBounds = player.Bounds;
            var tangent = new Vector2(-player.normal.Y, player.normal.X);
            
            if (!Bounds.Intersects(pBounds)) return false;
            
            // The difference in height.
            // Equal to the component parallel to the player tangent of vector between the player and ball center.
            float heightDiff = Vector2.Dot(position - player.position, tangent);
            
            // Remove sink effect
            // - The ball position will be the player position with the correct offset.
            // - The offset is equal to half the sum of the body sizes (aka the distance vector if both bodies are touching).
            // - We then take the dot product to get only the offset component parallel to the player normal.
            // - Also, a slight offset (0.01) is added to prevent multiple resolutions of the same collision.
            position = player.position + player.normal * (0.2f + MathF.Abs(Vector2.Dot(Size + player.Size, player.normal))) * 0.5f;
            // finally, offset the ball to have the same 'vertical' position relative to the player as before.
            position += heightDiff * tangent;
            
            direction = player.normal;
            
            // offset the direction along the player tangent based on where the ball hit the paddle.
            direction += heightDiff / pBounds.Height * tangent;
            
            // make sure to normalize because the length will not be equal to 1
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
            Renderer.instance.DrawSpriteCentered(sprite,position, ballColor);
        }
    }
}