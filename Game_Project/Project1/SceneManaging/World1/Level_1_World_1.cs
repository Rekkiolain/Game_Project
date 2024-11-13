using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Extended.Screens;
using Project1.Entities.NPC;
using Project1.Map;
using MonoGame.Extended.Tiled;

namespace Project1.SceneManaging.World1
{
    internal class Level_1_World_1 : GameScreen
    {
        private new Game1 Game => (Game1)base.Game;
        private Vector2 _position = new Vector2(50, 50);

        private MapLoader _mapLoader;
        private Bird_NPC _bird_NPC;


        public Level_1_World_1(Game1 game) : base(game) { }

        public override void LoadContent()
        {
            //initialize the map
            _mapLoader = new MapLoader(GraphicsDevice);
            _mapLoader.LoadContent(Content);
            //load npcs
            _bird_NPC = new Bird_NPC();
            _bird_NPC.LoadContent(Content);
        }

        public override void Update(GameTime gameTime)
        {
            _position = Vector2.Lerp(_position, Mouse.GetState().Position.ToVector2(), 1f * (float)gameTime.ElapsedGameTime.TotalSeconds);

            //load map
            _mapLoader.Update(gameTime);
            //update npcs
            _bird_NPC.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Game.GraphicsDevice.Clear(Color.Gray); 

            Game._spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            _mapLoader.ScreenCentering(1080, 1920);

            _mapLoader.Draw(_mapLoader.ScreenCentering(1920, 1080));
            _bird_NPC.Draw(gameTime, Game._spriteBatch);
            Game._spriteBatch.End();
        }


    }
}
