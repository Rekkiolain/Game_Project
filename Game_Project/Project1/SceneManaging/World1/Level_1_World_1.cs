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
using MonoGame.Extended.Tiled;
using Project1.Entities.Player_Character;
using Project1.SceneManaging.Map;

namespace Project1.SceneManaging.World1
{
    internal class Level_1_World_1 : GameScreen
    {
        private new Game1 Game => (Game1)base.Game;
        private Vector2 _position = new Vector2(50, 50);

        private MapLoader _mapLoader;

        //Characters
        private Bird_NPC _bird_NPC;
        private Marine_Hero _marine;


        public Level_1_World_1(Game1 game) : base(game) { }

        public override void Initialize()
        {
            //initalize map
            _mapLoader = new MapLoader(GraphicsDevice);

            //initialize npcs / player
            _marine = new Marine_Hero();
            _marine.Initialize();
            _bird_NPC = new Bird_NPC();
        }
        public override void LoadContent()
        {
            //load map
            _mapLoader.LoadContent(Content);
            //load npcs      
            _bird_NPC.LoadContent(Content);
            //load player
            _marine.LoadContent(Content);
        }
        public override void Update(GameTime gameTime)
        {
            _position = Vector2.Lerp(_position, Mouse.GetState().Position.ToVector2(), 1f * (float)gameTime.ElapsedGameTime.TotalSeconds);
            //update map
            _mapLoader.Update(gameTime);
            //update npcs
            _bird_NPC.Update(gameTime);
            //update player
            _marine.Update(gameTime);
        }


        public override void Draw(GameTime gameTime)
        {
            Game.GraphicsDevice.Clear(Color.Gray); 

            Game._spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            _mapLoader.ScreenCentering(1080, 1920);

            _mapLoader.Draw(_mapLoader.ScreenCentering(1920, 1080));
            _bird_NPC.Draw(gameTime, Game._spriteBatch);
            _marine.Draw(gameTime, Game._spriteBatch);
            Game._spriteBatch.End();
        }


    }
}
