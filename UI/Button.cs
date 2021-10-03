using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong
{
    public class Button : UIElement
    {
        public string text;
        public SpriteFont font;
        private Color currentColor = Renderer.buttonColor;
        public bool IsPressed { get; private set; }
        public bool IsHover { get; private set; }
        public Button(Vector2 position, Vector2 size, string text, SpriteFont font) : base(position, size)
        {
            this.text = text;
            this.font = font;
        }

        public void Update(float dt)
        {
            Vector2 mousePos = InputHandler.Instance.MousePosition;
            IsHover = Bounds.Contains(mousePos);
            IsPressed = InputHandler.Instance.LeftMouse && IsHover;
            currentColor = Color.Lerp(currentColor, IsHover ? Renderer.buttonHoverColor : Renderer.buttonColor,
                10 * dt);
        }
        public void Draw()
        {
            
            Renderer.Instance.DrawText(font, text, position, currentColor);
        }
    }
}