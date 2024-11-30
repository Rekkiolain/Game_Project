using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using PlatformerV2.Game_Classes.Level;
using PlatformerV2.Game_Classes.Level.Level1;
using Project1.Entities.Player_Character;
using Project1.Map;
using static System.Net.Mime.MediaTypeNames;

namespace PlatformerV2
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private readonly ScreenManager _screenManager;
        //private Vector2 _worldPosition;

        public SpriteBatch _spriteBatch;
        

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
            _graphics.PreferredBackBufferWidth = 1920; //NEEDS TO BE PLACED IN OTHER CLASS LATER
            _graphics.PreferredBackBufferHeight = 1080; //NEEDS TO BE PLACED IN OTHER CLASS LATER
            _graphics.IsFullScreen = true; //NEEDS TO BE PLACED IN OTHER CLASS LATER
            _graphics.ApplyChanges(); //NEEDS TO BE PLACED IN OTHER CLASS LATER



            base.Initialize();
            LoadLevel_1();

        }

        protected override void LoadContent()
        {

            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            base.Update(gameTime);

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            _spriteBatch.End();

            base.Draw(gameTime);

        }

        private void LoadLevel_1()
        {
            _screenManager.LoadScreen(new Level_1(this), new FadeTransition(GraphicsDevice, Color.Black));
        }
    }
}
