using Microsoft.Xna.Framework;

namespace Pong
{
    public class UIElement
    {
        
        public Vector2 position;
        public Vector2 size { get; set; }

        public Rectangle Bounds => new Rectangle((position-size/2).ToPoint(), size.ToPoint());
        public UIElement(Vector2 position, Vector2 size)
        {

            this.position = position;
            this.size = size;
        }

        public static Vector2 CalculateAlignOffset(Align align, Vector2 size)
        {
            (float width, float height) = size;
            int hAlign = (int)align % 3 - 1;
            int vAlign = ((int)align - hAlign)/3 - 1;
            return new Vector2(hAlign * width/2, vAlign * height/2);
        }
    }
}