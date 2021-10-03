using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong
{
    public class Label : UIElement
    {
        public SpriteFont font;
        public string text;
        public Color color;
        private readonly Align align;
        public Label(Vector2 position, Vector2 size, string text, Color color, SpriteFont font, Align align = Align.MidCenter) : base(position, size)
        {
            this.text = text;
            this.color = color;
            this.font = font;
            this.align = align;
            this.position += CalculateAlignOffset(align, size);
        }
        public void Draw()
        {
            Renderer.Instance.DrawText(position, text, color, font, align);
        }
    }
}