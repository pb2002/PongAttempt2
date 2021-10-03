using Microsoft.Xna.Framework;

namespace Pong.States
{
    
    public class GameOverState : State
    {
        private readonly Label titleLabel;
        private readonly Label resultLabel;
        private readonly Button continueButton;
        
        private int winningPlayer;
        public GameOverState(PongGame game, int winningPlayer) : base(game)
        {
            this.winningPlayer = winningPlayer;
            titleLabel = new Label(Prefs.screenSize / 2, Vector2.Zero, "GAME OVER", Renderer.titleColor, 
                Assets.titleFont);
            resultLabel = new Label(Prefs.screenSize / 2 + new Vector2(0, 120), Vector2.Zero, 
                $"player {winningPlayer+1} wins!", Renderer.subtitleColor, Assets.subtitleFont);
            continueButton = new Button(Prefs.screenSize / 2 + new Vector2(0, 184), new Vector2(200, 48), 
                "CONTINUE", Assets.subtitleFont);
            Assets.musicBaseLayer.FadeIn(2f);
        }

        public override void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            continueButton.Update(dt);
            if(continueButton.IsPressed)
                game.SwitchState(new MenuState(game));
            
        }

        public override void Draw(GameTime gameTime)
        {
            titleLabel.Draw();
            resultLabel.Draw();
            continueButton.Draw();
        }
    }
}
