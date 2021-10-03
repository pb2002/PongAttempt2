using System;
using Microsoft.Xna.Framework;

namespace Pong.States
{
    public class MenuState : State
    {
        private readonly Label titleLabel;
        private readonly Label subtitleLabel;

        // we did not want to use an array here, because it would make things way to complicated and would be beyond the
        // scope of a simple pong project.

        private readonly Button onePlayerButton;
        private readonly Button twoPlayerButton;
        private readonly Button aiBattleButton;
        private readonly Button difficultyButton;
        private readonly Button darkModeButton;
        private readonly Button howToPlayButton;

        private readonly string[] difficultyNames = { "EASY", "NORMAL", "HARD", "IMPOSSIBLE" };

        public MenuState(PongGame game) : base(game)
        {
            Vector2 screenCenter = Prefs.screenSize / 2;
            titleLabel = new Label(screenCenter + new Vector2(0, -50), Vector2.Zero, "PONG", Renderer.titleColor,
                Assets.titleFont);
            subtitleLabel = new Label(screenCenter + new Vector2(0, 50), Vector2.Zero, "By Pepijn & Tigo",
                Renderer.subtitleColor, Assets.subtitleFont);

            onePlayerButton = new Button(
                screenCenter + new Vector2(0, 130),
                new Vector2(360, 48),
                "PLAYER VS AI",
                Assets.subtitleFont);

            twoPlayerButton = new Button(
                screenCenter + new Vector2(0, 190),
                new Vector2(380, 48),
                "PLAYER VS PLAYER",
                Assets.subtitleFont);

            aiBattleButton = new Button(
                screenCenter + new Vector2(0, 250),
                new Vector2(300, 48),
                "AI VS AI",
                Assets.subtitleFont);

            difficultyButton = new Button(
                new Vector2(30, 30),
                new Vector2(435, 32),
                $"AI DIFFICULTY : {difficultyNames[Prefs.difficulty]}",
                Assets.smallFont, Align.TopLeft, Align.TopLeft);

            darkModeButton = new Button(
                new Vector2(30, 80),
                new Vector2(280, 32),
                $"DARK MODE : {(Renderer.Instance.darkMode ? "ON" : "OFF")}",
                Assets.smallFont, Align.TopLeft, Align.TopLeft);
            
            howToPlayButton = new Button(
                new Vector2(Prefs.screenSize.X - 30, 30),
                new Vector2(270, 32),
                $"HOW TO PLAY",
                Assets.smallFont, Align.TopRight, Align.TopRight);
            
            onePlayerButton.OnButtonPressed += () => StartGame(GameMode.OnePlayer);
            twoPlayerButton.OnButtonPressed += () => StartGame(GameMode.TwoPlayer);
            aiBattleButton.OnButtonPressed += () => StartGame(GameMode.AIBattle);
            howToPlayButton.OnButtonPressed += () => game.SwitchState(new HowToPlayState(game));
            
            difficultyButton.OnButtonPressed += () =>
            {
                Prefs.difficulty = (Prefs.difficulty + 1) % 4;
                difficultyButton.label.text = $"AI DIFFICULTY : {difficultyNames[Prefs.difficulty]}";
            };

            darkModeButton.OnButtonPressed += () =>
            {
                Renderer.Instance.darkMode = !Renderer.Instance.darkMode;
                darkModeButton.label.text = $"DARK MODE : {(Renderer.Instance.darkMode ? "ON" : "OFF")}";
            };
            
            Assets.musicBaseLayer.FadeIn(2f);
        }

        private void StartGame(GameMode gameMode)
        {
            game.SwitchState(new InGameState(game, gameMode));
            Assets.musicBaseLayer.FadeOut(3f);
        }

        public override void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            onePlayerButton.Update(dt);
            twoPlayerButton.Update(dt);
            aiBattleButton.Update(dt);
            difficultyButton.Update(dt);
            darkModeButton.Update(dt);
            howToPlayButton.Update(dt);
            // title wobble
            titleLabel.position = Prefs.screenSize / 2
                                  + MathF.Sin((float)gameTime.TotalGameTime.TotalSeconds) * Vector2.UnitY * 9
                                  - 70 * Vector2.UnitY;
        }

        public override void Draw(GameTime gameTime)
        {
            titleLabel.Draw();
            subtitleLabel.Draw();

            onePlayerButton.Draw();
            twoPlayerButton.Draw();
            aiBattleButton.Draw();
            difficultyButton.Draw();
            darkModeButton.Draw();
            howToPlayButton.Draw();
            
        }
    }
}