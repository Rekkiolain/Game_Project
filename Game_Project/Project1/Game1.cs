using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using MonoGame.Extended.ViewportAdapters;
using MonoGame.Extended;
using Project1.Entities.NPC;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using Project1.SceneManaging.World1;
using Project1.SceneManaging.StartMenu_UI;

namespace Project1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;

        private readonly ScreenManager _screenManager;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _screenManager = new ScreenManager();
            Components.Add(_screenManager);
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.IsFullScreen = true;
            _graphics.ApplyChanges();
            _screenManager.LoadScreen(new StartMenu(this));



            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Set graphics settings to reduce blurriness
            GraphicsDevice.SamplerStates[0] = SamplerState.PointClamp;
            GraphicsDevice.BlendState = BlendState.AlphaBlend;
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            // Check for key presses to switch screens
            var keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.D1))
            {
                Load_StartMenu();
            }
            else if (keyboardState.IsKeyDown(Keys.D2))
            {
                Load_Level_1_World_1();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            base.Draw(gameTime);
        }

        private void Load_StartMenu()
        {
            _screenManager.LoadScreen(new StartMenu(this), new FadeTransition(GraphicsDevice, Color.Black));
        }
        public void Load_Level_1_World_1()
        {
            _screenManager.LoadScreen(new Level_1_World_1(this), new FadeTransition(GraphicsDevice, Color.Black));
        }
    }
}
