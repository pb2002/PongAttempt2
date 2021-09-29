using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Pong.States
{
    public class MenuState : State
    {

        private SpriteFont titleFont;
        private SpriteFont subtitleFont;

        private Label titleLabel;
        private Label subtitleLabel;

        private Button onePlayerButton;
        private Button twoPlayerButton;
        private Button cpuBattleButton;
        private Button coopButton;
        private Button difficultyButton;

        private readonly string[] difficultyNames = {"easy", "normal", "hard", "impossible"};
        public MenuState(PongGame game, ContentManager content) : base(game, content)
        {
            titleLabel = new Label(PongGame.screenSize / 2 + new Vector2(0, -50), Vector2.Zero, "PONG", Renderer.titleColor);
            subtitleLabel = new Label(PongGame.screenSize / 2 + new Vector2(0, 50), Vector2.Zero, "By Pepijn & Tigo", Renderer.subtitleColor);
            
            onePlayerButton = new Button(PongGame.screenSize / 2 + new Vector2(0, 130), new Vector2(360, 48), "PLAYER VS CPU");
            twoPlayerButton = new Button(PongGame.screenSize / 2 + new Vector2(0, 190), new Vector2(380, 48), "PLAYER VS PLAYER");
            coopButton = new Button(PongGame.screenSize / 2 + new Vector2(0, 250), new Vector2(260, 48), "COOP MODE");
            cpuBattleButton = new Button(PongGame.screenSize / 2 + new Vector2(0, 310), new Vector2(300, 48), "CPU VS CPU");
            
            difficultyButton = new Button(PongGame.screenSize / 2 + new Vector2(0, 400), new Vector2(500, 48),
                $"AI difficulty: {difficultyNames[Prefs.difficulty]}");
        }
        public override void LoadContent()
        {
            titleFont = content.Load<SpriteFont>("titleFont");
            subtitleFont = content.Load<SpriteFont>("subtitleFont");
            
            titleLabel.font = titleFont;
            subtitleLabel.font = subtitleFont;
            
            onePlayerButton.font = subtitleFont;
            twoPlayerButton.font = subtitleFont;
            coopButton.font = subtitleFont;
            cpuBattleButton.font = subtitleFont;
            
            difficultyButton.font = subtitleFont;
        }

        public override void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            onePlayerButton.Update(dt);
            twoPlayerButton.Update(dt);
            cpuBattleButton.Update(dt);
            coopButton.Update(dt);
            difficultyButton.Update(dt);
            
            if (onePlayerButton.IsPressed)
                game.SwitchState(new CompetitiveGameState(game, content, GameMode.OnePlayer));
            
            if (twoPlayerButton.IsPressed)
                game.SwitchState(new CompetitiveGameState(game, content, GameMode.TwoPlayer));
            
            if (cpuBattleButton.IsPressed)
                game.SwitchState(new CompetitiveGameState(game, content, GameMode.CPUBattle));
            
            if (coopButton.IsPressed)
                game.SwitchState(new CoopGameState(game, content));

            if (difficultyButton.IsPressed)
            {
                Prefs.difficulty = (Prefs.difficulty + 1) % 4;
                difficultyButton.text = $"AI difficulty: {difficultyNames[Prefs.difficulty]}";
            }
            
            titleLabel.position = PongGame.screenSize / 2 
                                  + MathF.Sin((float)gameTime.TotalGameTime.TotalSeconds) * Vector2.UnitY * 9
                                  - 70 * Vector2.UnitY;
            
        }

        public override void Draw(GameTime gameTime)
        {
            Vector2 screenCenter = PongGame.screenSize / 2;
            
            titleLabel.Draw();
            subtitleLabel.Draw();
            
            onePlayerButton.Draw();
            twoPlayerButton.Draw();
            coopButton.Draw();
            cpuBattleButton.Draw();
            difficultyButton.Draw();
        }
    }
}