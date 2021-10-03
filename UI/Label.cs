using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong
{
    public class Label : UIElement
    {
        public SpriteFont font;
        public string text;
        public Color color;
        public Label(Vector2 position, Vector2 size, string text, Color color, SpriteFont font) : base(position, size)
        {
            this.text = text;
            this.color = color;
            this.font = font;
        }
        public void Draw()
        {
            Renderer.Instance.DrawText(font, text, position, color);
        }
    }
}