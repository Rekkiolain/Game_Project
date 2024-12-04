using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PlatformerV3.Entities;
using System.Collections.Generic;

namespace PlatformerV3
{
    internal class Level_1
    {
        private Dictionary<Vector2, int> _collisions;
        private Dictionary<Vector2, int> _mainBlocks;
        private Dictionary<Vector2, int> _details;

        private LevelRenderer _levelRenderer;
        private Player _player;
        private EntityCollisions _entityCollisions;

        public Level_1()
        {
            _levelRenderer = new LevelRenderer();
            _entityCollisions = new EntityCollisions();
        }

        public void LoadContent(ContentManager content, GraphicsDevice graphicsDevice)
        {
            _levelRenderer.LoadContent(content);
            _levelRenderer.getSpriteSheet();

            _collisions = _levelRenderer.LoadMap("../../../Data/Collisions.csv");
            _details = _levelRenderer.LoadMap("../../../Data/Details.csv");
            _mainBlocks = _levelRenderer.LoadMap("../../../Data/MainBlocks.csv");

  
            _player = new Player(content.Load<Texture2D>("Player"), new Rectangle(48, 48, 96, 96));
            _player.LoadContent(content,graphicsDevice);

            _entityCollisions.LoadContent(graphicsDevice);
        }

        public void Update()
        {
            _player.Update(Keyboard.GetState());

            _player._rect.X += (int)_player._velocity.X;
            _entityCollisions.UpdateHorizontalIntersections(_player._rect);
            _entityCollisions.onHorizontalCollisions(_collisions, _player._velocity, _player._rect);


            _player._rect.Y += (int)_player._velocity.Y;
            _entityCollisions.UpdateVerticalIntersections(_player._rect);
            _entityCollisions.onVerticalCollisions(_collisions, _player._velocity, _player._rect);


        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _levelRenderer.getTileMap(_collisions, spriteBatch);
            _levelRenderer.getTileMap(_details, spriteBatch);
            _levelRenderer.getTileMap(_mainBlocks, spriteBatch);

            _player.Draw(spriteBatch);
            _entityCollisions.Draw(spriteBatch);
        }
    }
}
