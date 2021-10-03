using Microsoft.Xna.Framework;

namespace Pong
{
    public class UIElement
    {
        
        public Vector2 position;
        public Vector2 size { get; set; }

        public Rectangle Bounds => new Rectangle((position-size/2).ToPoint(), size.ToPoint());

        protected UIElement(Vector2 position, Vector2 size)
        {

            this.position = position;
            this.size = size;
        }
        
        /// <summary>
        /// Calculates the offset vector to be added to/subtracted from the position for it to be aligned accordingly.
        /// </summary>
        /// <param name="align">The alignment setting</param>
        /// <param name="size">The size of the object to align</param>
        /// <returns>The offset to add to/subtract from the position</returns>
        public static Vector2 CalculateAlignOffset(Align align, Vector2 size)
        {
            (float width, float height) = size;
            // remainder of align no. = horizontal alignment
            // floored division of align no. = vertical alignment
            // offset both by -1 to get numbers in the range [-1, 1]
            int hAlign = (int)align % 3 - 1;
            int vAlign = ((int)align - hAlign)/3 - 1;
            return new Vector2(hAlign * width/2, vAlign * height/2);
        }
    }
}