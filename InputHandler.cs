using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Pong
{
    public class InputHandler : Singleton<InputHandler>
    {
        public Buttons[][] ControllerMapping { get; private set; } =
        {
            new[]{Buttons.LeftThumbstickUp, Buttons.LeftThumbstickDown, Buttons.A, Buttons.Back},
            new[]{Buttons.RightThumbstickUp, Buttons.RightThumbstickDown}
        };

        public Keys[][] KeyboardMapping { get; private set; } =
        {
            new[] {Keys.W, Keys.S, Keys.Enter, Keys.Escape},
            new[] {Keys.Up, Keys.Down}
        };

        public int[] PlayerMovementInput { get; private set; } = new[] {0, 0};
        
        private bool confirmDown = false;
        public bool Confirm { get; private set; } = false;
        public bool Exit { get; private set; } = false;

        public Vector2 MousePosition { get; private set; }

        private bool leftMouseDown = false;
        public bool LeftMouse { get; private set; }
        
        public void UpdateInput()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            GamePadState gamePadState = GamePad.GetState(0);
            MouseState mouseState = Mouse.GetState();
            // Player Movement Input
            for (int i = 0; i < PlayerMovementInput.Length; i++)
            {
                var moveInput = 0;
                if (keyboardState.IsKeyDown(KeyboardMapping[i][0]) ||
                    gamePadState.IsButtonDown(ControllerMapping[i][0])) moveInput -= 1;
                if (keyboardState.IsKeyDown(KeyboardMapping[i][1]) ||
                    gamePadState.IsButtonDown(ControllerMapping[i][1])) moveInput += 1;
                PlayerMovementInput[i] = moveInput;
            }
            // Confirm
            bool confirmRaw = keyboardState.IsKeyDown(KeyboardMapping[0][2]) ||
                      gamePadState.IsButtonDown(ControllerMapping[0][2]);
            Confirm = confirmRaw && !confirmDown;
            confirmDown = confirmRaw;
            // Exit
            Exit = keyboardState.IsKeyDown(KeyboardMapping[0][3]) ||
                      gamePadState.IsButtonDown(ControllerMapping[0][3]);
            
            MousePosition = mouseState.Position.ToVector2();
            bool leftMouseRaw = mouseState.LeftButton == ButtonState.Pressed;
            LeftMouse = leftMouseRaw && !leftMouseDown;
            leftMouseDown = leftMouseRaw;

        }
    }
}