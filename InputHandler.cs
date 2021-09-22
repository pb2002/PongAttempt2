using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace PongAttempt2
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
            new[] {Keys.W, Keys.S, Keys.Space, Keys.Escape},
            new[] {Keys.Up, Keys.Down}
        };

        public int[] PlayerMovementInput { get; private set; } = new[] {0, 0};
        public bool Confirm { get; private set; } = false;
        public bool Exit { get; private set; } = false;
        
        public void UpdateInput()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            GamePadState gamePadState = GamePad.GetState(0);
            
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
            Confirm = keyboardState.IsKeyDown(KeyboardMapping[0][2]) ||
                      gamePadState.IsButtonDown(ControllerMapping[0][2]);
            // Exit
            Exit = keyboardState.IsKeyDown(KeyboardMapping[0][3]) ||
                      gamePadState.IsButtonDown(ControllerMapping[0][3]);
        }
    }
}