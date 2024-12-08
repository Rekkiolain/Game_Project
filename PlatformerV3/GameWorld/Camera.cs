using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformerV3.World
{
    internal class Camera
    {
        public OrthographicCamera _camera;

        public void Initialize(GameWindow Window, GraphicsDevice graphicsDevice)
        {
            var viewportAdapter = new BoxingViewportAdapter(Window, graphicsDevice, 1920, 1080);
            _camera = new OrthographicCamera(viewportAdapter);
            _camera.Origin = new Vector2(0, 0);
            _camera.Zoom = 03;

        }
    }
}