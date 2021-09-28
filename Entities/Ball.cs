using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
namespace Pong
{
    public class Ball : Entity
    {
        private static Random random = new Random();
        private static Color ballColor = new Color(128, 144, 160);
        public const float baseSpeed = 300f;
        
        public Vector2 direction;
        public float speed;

        public Ball(Vector2 position, Vector2 direction) : base(position)
        {
            this.direction = direction;
            this.speed = baseSpeed;
        }

        public void Move(float dt)
        {
            position += direction * speed * dt;
        }

        public void Reset()
        {
            speed = baseSpeed;
            
            float rx = random.Next(0,2) == 1 ? -1 : 1;
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