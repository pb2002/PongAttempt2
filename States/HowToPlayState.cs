using Microsoft.Xna.Framework;

namespace Pong.States
{
    public class HowToPlayState : State
    {
        private Label titleLabel;
        private Label controlsLabel;
        private Label mappingLabel;
        private Label bodyLabel;
        private Button backButton;
        
        public HowToPlayState(PongGame game) : base(game)
        {
            Vector2 screenCenter = Prefs.screenSize / 2;
            
            titleLabel = new Label(
                new Vector2(screenCenter.X,50),
                Vector2.Zero,
                "HOW TO PLAY", 
                Renderer.titleColor,
                Assets.subtitleFont,
                Align.TopCenter);
            
            controlsLabel = new Label(
                new Vector2(screenCenter.X - 50,200),
                Vector2.Zero, 
                "Up (Player 1)\nDown (Player 1)\nUp (Player 2)\nDown (Player 2)\nPause\n", 
                Renderer.buttonHoverColor,
                Assets.smallFont,
                Align.TopRight);
            
            mappingLabel = new Label(
                new Vector2(screenCenter.X + 50,200),
                Vector2.Zero,
                "W\nS\nUP ARROW\nDOWN ARROW\nESCAPE\n",
                Renderer.buttonColor,
                Assets.smallFont,
                Align.TopLeft);
            
            bodyLabel = new Label(
                screenCenter + new Vector2(0, 100),
                Vector2.Zero, 
                @"
AI difficulty can be selected from the main menu.

Every player has 3 lives. Last player standing wins.", 
                Renderer.buttonColor,
                Assets.smallFont);
            
            backButton = new Button(
                new Vector2(screenCenter.X, Prefs.screenSize.Y - 100),
                new Vector2(120, 48),
                "BACK",
                Assets.subtitleFont);
            
            // go back to the main menu
            backButton.OnButtonPressed += () => game.SwitchState(new MenuState(game));
        }

        public override void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            backButton.Update(dt);
        }

        public override void Draw(GameTime gameTime)
        {
            titleLabel.Draw();
            controlsLabel.Draw();
            mappingLabel.Draw();
            bodyLabel.Draw();
            backButton.Draw();
        }
    }
}