using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using Myra.Graphics2D.UI;
using Project1.Entities.Player_Character;
using Project1.Map;
using static System.Net.Mime.MediaTypeNames;

namespace PlatformerV2.Game_Classes.Level.Level1
{
    internal class Level_1 : GameScreen
    {
        private new Game1 Game => (Game1)base.Game;
        public Level_1(Game1 game) : base(game) { }


        private Level_Generation testworld;
        private Hero hero; 
        public Camera _camera;

        public override void Initialize()
        {
            testworld = new Level_Generation(GraphicsDevice,Game._spriteBatch);

            _camera = new Camera();
            _camera.Initialize(Game.Window, testworld._graphicsDevice);

            hero = new Hero(new RectangleF(43, 760, 48, 75), "HeroLayer");
            hero.Initialize();

            base.Initialize();
        }

        public override void LoadContent()
        {
            testworld.LoadContent(Content, "Test11");
            GraphicsDevice.SamplerStates[0] = SamplerState.PointClamp;
            GraphicsDevice.BlendState = BlendState.AlphaBlend;
            hero.LoadContent(Content);

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            testworld.Update(gameTime);
            hero.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            testworld.Draw(_camera);
            hero.Draw(gameTime, Game._spriteBatch);
        }
    }
}
