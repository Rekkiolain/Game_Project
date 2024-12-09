
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
        private Dictionary<Vector2, int> _mapBounds;
        private Dictionary<Vector2, int> _collisions;
        private Dictionary<Vector2, int> _mainBlocks;
        private Dictionary<Vector2, int> _details;

        private LevelRenderer _levelRenderer;

        public Level_1()
        {
            _levelRenderer = new LevelRenderer();
        }

        public void Initialize(GameWindow window, GraphicsDevice graphicsDevice)
        {
            _levelRenderer.Initialize(window,graphicsDevice);
        }

        public void LoadContent(ContentManager content, GraphicsDevice graphicsDevice)
        {
            _levelRenderer.LoadContentPart1(content);
            _levelRenderer.getSpriteSheet();

            _mapBounds = _levelRenderer.LoadMap("../../../Data/MapBounds.csv");
            _collisions = _levelRenderer.LoadMap("../../../Data/Collisions.csv");
            _details = _levelRenderer.LoadMap("../../../Data/Details.csv");
            _mainBlocks = _levelRenderer.LoadMap("../../../Data/MainBlocks.csv");

            _levelRenderer.LoadContentPart2(content, _collisions,_mapBounds, graphicsDevice);

        }

        public void Update(GameTime gameTime)
        {
            _levelRenderer.Update(gameTime);
        }

        public void Draw(SpriteBatch _spriteBatch)
        {

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: _levelRenderer.getTransformationMatrix());

            _levelRenderer.getTileMap(_mapBounds, _spriteBatch,-16,16);
            _levelRenderer.getTileMap(_collisions, _spriteBatch,0,0);
            _levelRenderer.getTileMap(_mainBlocks, _spriteBatch, 0, 0);
            _levelRenderer.getTileMap(_details, _spriteBatch,0,0);

            _levelRenderer.Draw(_spriteBatch);
            _spriteBatch.End();


        }

    }
}