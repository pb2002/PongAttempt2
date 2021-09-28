using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong
{
    public class Label : UIElement
    {
        public SpriteFont font;
        public string text;
        public Color color;
        public Label(Vector2 position, Vector2 size, string text, Color color) : base(position, size)
        {
            this.text = text;
            this.color = color;
        }
        public void Draw()
        {
            Renderer.instance.DrawText(font, text, position, color);
        }
    }
}