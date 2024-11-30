using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.Tiled;
using Project1.Map;
using System;
using System.Collections.Generic;

namespace PlatformerV2.Game_Classes.Level
{
    internal class Level_Generation
    {
        private List<Rectangle> _collisions;
        private TiledMap _tiledMap;
        private TiledMapRenderer _tiledMapRenderer;
        private SpriteBatch _spriteBatch;
        private EnviromentCollisions _enviromentCollisions;

        public GraphicsDevice _graphicsDevice;

        public Level_Generation(GraphicsDevice graphicsDevice, SpriteBatch spritebatch)
        {
            _graphicsDevice = graphicsDevice;
            _spriteBatch = spritebatch;
        }
        public void LoadContent(ContentManager content, string level_name)
        {
            _tiledMap = content.Load<TiledMap>(level_name);
            _tiledMapRenderer = new TiledMapRenderer(_graphicsDevice, _tiledMap);

            _enviromentCollisions = new EnviromentCollisions(_tiledMap, _graphicsDevice, _spriteBatch);
            _enviromentCollisions.LoadContent();

        }

        public void Update(GameTime gameTime)
        {
            _tiledMapRenderer.Update(gameTime);
        }

        public void Draw(Camera camera)
        {
            var transformMatrix = camera._camera.GetViewMatrix();

            var mapWidth = _tiledMap.Width * _tiledMap.TileWidth;
            float scaleX = 1920f / mapWidth;
            float scale = scaleX;

            var scaledTransformMatrix = transformMatrix * Matrix.CreateScale(scale);
            _tiledMapRenderer.Draw(scaledTransformMatrix);

            _spriteBatch.Begin(transformMatrix: scaledTransformMatrix);
            _enviromentCollisions.Draw();
            _spriteBatch.End();
        }


    }
}

