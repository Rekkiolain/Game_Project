using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.Tiled;
using System;

namespace Project1.SceneManaging.Map
{
    public class MapLoader
    {
        public TiledMap _tiledMap;
        public TiledMapRenderer _tiledMapRenderer;
        private GraphicsDevice _graphicsDevice;

        public MapLoader(GraphicsDevice graphicsDevice)
        {
            _graphicsDevice = graphicsDevice;
        }

        public void LoadContent(ContentManager content)
        {
            _tiledMap = content.Load<TiledMap>("Test7");
            _tiledMapRenderer = new TiledMapRenderer(_graphicsDevice, _tiledMap);
            _tiledMap.GetLayer("Collisions");
        }

        public void Update(GameTime gameTime)
        {
            _tiledMapRenderer?.Update(gameTime);
        }

        public void Draw(Matrix scaleMatrix)
        {
            _tiledMapRenderer?.Draw(scaleMatrix);
        }

        public Matrix ScreenCentering(int Height, int Width)
        {
            int _screenWidth = Height;
            int _screenHeight = Width;

            int mapWidth = _tiledMap.Width * _tiledMap.TileWidth;
            int mapHeight = _tiledMap.Height * _tiledMap.TileHeight;

            float scaleX = (float)_screenWidth / mapWidth;
            float scaleY = (float)_screenHeight / mapHeight;

            float scale = Math.Min(scaleX, scaleY);

            float offsetX = (_screenWidth - mapWidth * scale) / 2f;
            float offsetY = (_screenHeight - mapHeight * scale) / 2f;

            Matrix scaleMatrix = Matrix.CreateScale(scale) * Matrix.CreateTranslation(offsetX, offsetY, 0);
            return scaleMatrix;

        }
    }
}
