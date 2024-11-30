using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.ViewportAdapters;
using Myra.Graphics2D.UI;
using System;


namespace Project1.Map
{
    public class Camera
    {
        public OrthographicCamera _camera;

        public void Initialize(GameWindow window, GraphicsDevice graphicsDevice)
        {
            var viewportAdapter = new BoxingViewportAdapter(window, graphicsDevice, 1920, 1080);
            _camera = new OrthographicCamera(viewportAdapter);
            _camera.Position = new Vector2(0, 0); 
        }

        public void Draw(GameTime gameTime, SpriteBatch _spriteBatch)
        {
            var transformMatrix = _camera.GetViewMatrix();
            _spriteBatch.Begin(transformMatrix: transformMatrix);
            _spriteBatch.End();
        }

    }
}