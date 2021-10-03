using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong
{
    public class Button : UIElement
    {
        public event Action OnButtonPressed;
        public const float buttonFadeLerpTime = 10f;
        public Label label;
        public bool IsPressed { get; private set; }
        public bool IsHover { get; private set; }
        
        public Button(Vector2 position, Vector2 size, string text, SpriteFont font, Align buttonAlign = Align.MidCenter, Align textAlign = Align.MidCenter) : base(position, size)
        {
            this.position -= CalculateAlignOffset(buttonAlign, size);
            label = new Label(this.position, size, text, Renderer.buttonColor, font, textAlign);
        }
        public Button(Vector2 position, Vector2 size, Label label, Align align = Align.MidCenter) : base(position, size)
        {
            this.position -= CalculateAlignOffset(align, size);
            this.label = label;
        }

        public void Update(float dt)
        {
            Vector2 mousePos = InputHandler.Instance.MousePosition;
            IsHover = Bounds.Contains(mousePos);
            IsPressed = InputHandler.Instance.LeftMouse && IsHover;
            if (IsPressed)
            {
                OnButtonPressed?.Invoke();
                Assets.clickSFX.Play(0.5f);
            }
            label.color = Color.Lerp(label.color, IsHover ? Renderer.buttonHoverColor : Renderer.buttonColor,
                buttonFadeLerpTime * dt);
        }
        
        public void Draw()
        {
            label.Draw();
        }

        protected virtual void OnOnButtonPressed()
        {
            OnButtonPressed?.Invoke();
        }
    }
}