using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Pong.States
{
        
    public class CoopGameOverState : State
    {

        private Label titleLabel;
        private Label resultLabel;
        private Button continueButton;

        private int score;
        
        public CoopGameOverState(PongGame game, int score) : base(game)
        {
            this.score = score;
            titleLabel = new Label(Prefs.screenSize / 2, Vector2.Zero, "GAME OVER", Renderer.titleColor, Assets.titleFont);
            resultLabel = new Label(Prefs.screenSize / 2 + new Vector2(0, 120), Vector2.Zero, $"score: {score}", Renderer.subtitleColor, Assets.subtitleFont);
            continueButton = new Button(Prefs.screenSize / 2 + new Vector2(0, 184), new Vector2(200, 48), "CONTINUE", Assets.subtitleFont);
        }

        public override void Update(GameTime gameTime)
        {
            float dt = (float) gameTime.ElapsedGameTime.TotalSeconds;
            continueButton.Update(dt);
            if(continueButton.IsPressed)
                game.SwitchState(new MenuState(game));
        }

        public override void Draw(GameTime gameTime)
        {
            Vector2 screenCenter = Prefs.screenSize / 2;
            titleLabel.Draw();
            resultLabel.Draw();
            continueButton.Draw();

        }
    }
}
