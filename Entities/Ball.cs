using Microsoft.Xna.Framework;
using System;

namespace Pong
{
    public class Ball : Entity
    {
        private static Random random = new Random();
        private static Color ballColor = new Color(128, 144, 160);
        
        public Vector2 direction;
        public float speed;

        public Ball(Vector2 position, Vector2 direction) : base(position)
        {
            this.direction = direction;
            speed = Prefs.baseSpeed;
            sprite = Assets.ballTexture;
        }

        public void Move(float dt)
        {
            position += direction * speed * dt;
        }

        public void Reset()
        {
            speed = Prefs.baseSpeed;
            
            // randomly select left or right
            float rx = random.Next(0,2) == 1 ? -1 : 1;
            // randomly select a slope
            float ry = (float) random.NextDouble() * 1.5f;
            // construct and normalize vector
            direction = new Vector2(rx, ry);
            direction.Normalize();
            Assets.clickSFX.Play(volume: 0.5f);
            position = Prefs.screenSize / 2f;
        }
        public bool CheckPlayerCollision(Player player)
        {
            Rectangle playerBounds = player.Bounds;
            // check for collision
            if (!Bounds.Intersects(playerBounds)) return false;
            
            var tangent = new Vector2(-player.normal.Y, player.normal.X);
            
            // The difference in height.
            // Equal to the component parallel to the player tangent of vector between the player and ball center.
            float heightDifference = Vector2.Dot(position - player.position, tangent);
            
            direction = player.normal;
            
            // offset the direction along the player tangent based on where the ball hit the paddle.
            direction += heightDifference / playerBounds.Height * tangent;
            
            // make sure to normalize because the length will not be equal to 1
            direction.Normalize();
            return true;
        }

        public bool CheckWallCollision()
        {
            // check for collisions
            if (position.Y < Size.Y / 2f)
            {
                // top wall
                
                // this works better than multiplying by -1 because collisions sink doesn't get resolved
                // which means multiple collisions may occur causing the ball to get stuck in the wall.
                direction.Y = 1 * MathF.Abs(direction.Y);
                return true;
            }
            if (position.Y > Prefs.screenSize.Y - Size.Y / 2f)
            {
                // bottom wall
                direction.Y = -1 * MathF.Abs(direction.Y);
                return true;
            }

            // reverse Y direction
            return false;
        }
        public void Draw()
        {
            Renderer.Instance.DrawSpriteCentered(sprite, position, ballColor);
        }
    }
}