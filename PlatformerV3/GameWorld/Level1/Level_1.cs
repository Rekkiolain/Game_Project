
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using PlatformerV3.Entities;
using PlatformerV3.World;
using System.Collections.Generic;
using System.Diagnostics;

namespace PlatformerV3.GameWorld.Level1
{
    internal class Level_1
    {
        private Dictionary<Vector2, int> _collisions;
        private Dictionary<Vector2, int> _mainBlocks;
        private Dictionary<Vector2, int> _details;

        private LevelRenderer _levelRenderer;
        private Player _player;
        private EntityCollisionsV2 collisions;
        private Camera _camera;

        public Level_1()
        {
            _levelRenderer = new LevelRenderer();
            collisions = new EntityCollisionsV2();
            _camera = new Camera();
            _player = new Player(new RectangleF(new Vector2(250, 250), new SizeF(12, 18)));
        }

        public void Initialize(GameWindow window, GraphicsDevice graphicsDevice)
        {
            _camera.Initialize(window, graphicsDevice);

            _player.Initialize();
        }

        public void LoadContent(ContentManager content, GraphicsDevice graphicsDevice)
        {
            _levelRenderer.LoadContent(content, "blocks");
            _levelRenderer.getSpriteSheet();

            _collisions = _levelRenderer.LoadMap("../../../Data/Collisions.csv");
            _details = _levelRenderer.LoadMap("../../../Data/Details.csv");
            _mainBlocks = _levelRenderer.LoadMap("../../../Data/MainBlocks.csv");

            collisions.GenerateCollisionRectangles(_collisions);
            collisions.CreateRedTexture(graphicsDevice);

            _player.LoadContent(content);
        }

        public void Update(GameTime gameTime)
        {
            collisions._collisionRectangles.Remove(_player._hitBox);

            _player.Update(gameTime);

            collisions.Collision(_player._bounds.ToRectangle(), ref _player._position, ref _player._isFalling, ref _player._isIdle, ref _player._isOnGround);

            _player._bounds.Position = _player._position;
            _player._hitBox = _player._bounds.ToRectangle();

            collisions.addEntity(_player._hitBox);
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            var transformationMatrix = _camera._camera.GetViewMatrix();

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: transformationMatrix);


            _levelRenderer.getTileMap(_collisions, _spriteBatch);
            _levelRenderer.getTileMap(_details, _spriteBatch);
            _levelRenderer.getTileMap(_mainBlocks, _spriteBatch);

            _player.Draw(_spriteBatch);

            _spriteBatch.End();


        }

    }
}