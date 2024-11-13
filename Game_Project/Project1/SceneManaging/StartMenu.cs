using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.StartMenu
{
    internal class StartMenu : GameScreen
    {
        private new Game1 Game => (Game1)base.Game;
        private Texture2D _logo;
        private SpriteFont _font;
        private Vector2 _position = new Vector2(50, 50);

        public StartMenu(Game1 game) : base(game) { }

        public override void LoadContent()
        {
            _logo = Game.Content.Load<Texture2D>("background");
        }

        public override void Update(GameTime gameTime)
        {
            _position = Vector2.Lerp(_position, Mouse.GetState().Position.ToVector2(), 1f * (float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        public override void Draw(GameTime gameTime)
        {
            Game.GraphicsDevice.Clear(Color.CornflowerBlue);
            Game._spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            Game._spriteBatch.Draw(_logo, _position, Color.White);
            Game._spriteBatch.End();
        }


    }
}
