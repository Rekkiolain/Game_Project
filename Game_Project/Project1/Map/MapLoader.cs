using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.Tiled;

namespace Project1.Map
{
    public class MapLoader
    {
        private TiledMap _tiledMap;
        private TiledMapRenderer _tiledMapRenderer;
        private GraphicsDevice _graphicsDevice;

        public MapLoader(GraphicsDevice graphicsDevice)
        {
            _graphicsDevice = graphicsDevice;
        }

        public void LoadContent(ContentManager content)
        {
            _tiledMap = content.Load<TiledMap>("Test2");
            _tiledMapRenderer = new TiledMapRenderer(_graphicsDevice, _tiledMap);
        }

        public void Update(GameTime gameTime)
        {
            _tiledMapRenderer?.Update(gameTime);
        }

        public void Draw(Matrix scaleMatrix)
        {
            _tiledMapRenderer?.Draw(scaleMatrix);
        }
    }
}
