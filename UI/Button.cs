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
        public Button(Vector2 position, Vector2 size, string text) : base(position, size)
        {
            this.text = text;
        }

        public void Update(float dt)
        {
            Vector2 mousePos = InputHandler.instance.MousePosition;
            IsHover = Bounds.Contains(mousePos);
            IsPressed = InputHandler.instance.LeftMouse && IsHover;
            currentColor = Color.Lerp(currentColor, IsHover ? Renderer.buttonHoverColor : Renderer.buttonColor,
                10 * dt);
        }
        public void Draw()
        {
            
            Renderer.instance.DrawText(font, text, position, currentColor);
        }
    }
}