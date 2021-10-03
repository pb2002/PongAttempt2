using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong
{
    // Entity Class
    public class Entity
    {
        public Vector2 position;
        
        protected Texture2D sprite;
        
        public Vector2 Size => new Vector2(sprite.Width, sprite.Height);
        
        public Rectangle Bounds
        {
            get
            {
                Rectangle b = sprite.Bounds;
                // offset the bounds by position to get world-space bounds
                // also subtract Size/2 to center the bounds around the position
                b.Offset(position-Size/2);
                return b;
            }
        }
        
        protected Entity(Vector2 position)
        {
            this.position = position;
        }
    }
}