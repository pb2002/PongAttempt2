using Microsoft.Xna.Framework;

namespace Pong
{
    public class UIElement
    {
        public Vector2 position;
        public Vector2 size;

        public Rectangle Bounds => new Rectangle((position-size/2).ToPoint(), size.ToPoint());
        public UIElement(Vector2 position, Vector2 size)
        {
            this.position = position;
            this.size = size;
        }
    }
}