using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;
using Myra;
using Myra.Graphics2D.UI;
using FontStashSharp;
using System.IO;

namespace Project1.SceneManaging.StartMenu_UI
{
    internal class StartMenu : GameScreen
    {
        private new Game1 Game => (Game1)base.Game;
        private Texture2D _background;
        private Desktop _desktop;
        private SpriteFontBase _titleFont;
        private FontSystem fontSystem;
        public StartMenu(Game1 game) : base(game) { }

        public override void LoadContent()
        {
            _background = Game.Content.Load<Texture2D>("background_main_menu");
            MyraEnvironment.Game = Game;
            _desktop = new Desktop();

            fontSystem = new FontSystem();
            fontSystem.AddFont(File.ReadAllBytes("Content/Font.ttf")); 
            _titleFont = fontSystem.GetFont(38); 


            BuildUI();

        }
        
        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(GameTime gameTime)
        {
            Game.GraphicsDevice.Clear(Color.Black);
            Game._spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            Game._spriteBatch.Draw(_background, Vector2.Zero, Color.White);
            Game._spriteBatch.End();

            _desktop.Render();
        }
        private void BuildUI()
        {
            var container = new VerticalStackPanel
            {
                Spacing = 20, 
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center 
            };

            var titleFontLarge = fontSystem.GetFont(128);  
            var buttonFontSmall = fontSystem.GetFont(32); 

            var titleLabel = new Label
            {
                Text = "New Game",
                HorizontalAlignment = HorizontalAlignment.Center,
                Font = titleFontLarge 
            };
            container.Widgets.Add(titleLabel);

            var startGameButton = new TextButton
            {
                Text = "Start New Game",
                HorizontalAlignment = HorizontalAlignment.Center,
                Font = buttonFontSmall, 
                Background = null, 
                Border = null 
            };
            startGameButton.Click += (s, a) => Game.Load_Level_1_World_1();
            container.Widgets.Add(startGameButton);

            var quitButton = new TextButton
            {
                Text = "Quit",
                HorizontalAlignment = HorizontalAlignment.Center,
                Font = buttonFontSmall, // Use smaller font for buttons
                Background = null, // Remove background
                Border = null // Remove border
            };
            quitButton.Click += (s, a) => Game.Exit();
            container.Widgets.Add(quitButton);

            // Attach the container to the desktop
            _desktop.Root = container;
        }


    }
}
