using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong
{
    public class Entity
    {
        public Vector2 position;
        public Texture2D sprite;
        public Vector2 Size => new Vector2(sprite.Width, sprite.Height);
        
        public Rectangle Bounds
        {
            get
            {
                Rectangle b = sprite.Bounds;
                b.Offset(position-b.Size.ToVector2()/2);
                return b;
            }
        }
        public Entity(Vector2 position)
        {
            this.position = position;
        }
    }
}