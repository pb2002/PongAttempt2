using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Pong
{
    public class InputHandler : Singleton<InputHandler>
    {
        // mapping LUTs
        // mapping indexing is as follows: Up, Down, Confirm, Exit
        // only player 1 has confirm and exit mappings
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
        
        // input player movement. -1 is up, 1 is down, 0 is idle.
        public int[] PlayerMovementInput { get; private set; } = new[] {0, 0};
        
        // Confirm input. confirmDown is used to make sure Confirm is only true for the first frame.
        private bool confirmDown = false;
        public bool Confirm { get; private set; } = false;

        private bool exitDown = false;
        public bool Exit { get; private set; } = false;
        
        public Vector2 MousePosition { get; private set; }

        // Idem
        private bool leftMouseDown = false;
        public bool LeftMouse { get; private set; }
        
        /// <summary>
        /// Polls input and updates input state variables accordingly.
        /// </summary>
        public void UpdateInput()
        {
            // Get input device states
            KeyboardState keyboardState = Keyboard.GetState();
            GamePadState gamePadState = GamePad.GetState(0);
            MouseState mouseState = Mouse.GetState();
            
            // Get Player Movement Input
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
            bool exitRaw = keyboardState.IsKeyDown(KeyboardMapping[0][3]) ||
                           gamePadState.IsButtonDown(ControllerMapping[0][3]);
            Exit = exitRaw && !exitDown;
            exitDown = exitRaw;
            // Mouse position
            MousePosition = mouseState.Position.ToVector2();
            
            // Left Mouse
            bool leftMouseRaw = mouseState.LeftButton == ButtonState.Pressed;
            LeftMouse = leftMouseRaw && !leftMouseDown;
            leftMouseDown = leftMouseRaw;
        }
    }
}