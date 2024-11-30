using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.Tiled;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Collisions;
using MonoGame.Extended;
using MonoGame.Extended.Timers;

namespace PlatformerV2.Game_Classes
{
    internal class EnviromentCollisions : ICollisionActor
    {
        private List<RectangleF> _collisions;
        private TiledMap _tiledMap;

        private Texture2D _debugTexture;
        private GraphicsDevice _graphicsDevice;
        private SpriteBatch _spriteBatch;
        private RectangleF _rectangleF;

        public Vector2 Velocity;
        public IShapeF Bounds { get; }

        public EnviromentCollisions(TiledMap tiledmap, GraphicsDevice graphicsDevice, SpriteBatch spritebatch)
        {
            _tiledMap = tiledmap;
            _graphicsDevice = graphicsDevice;
            _spriteBatch = spritebatch;
            Bounds = _rectangleF;
        }
        public void LoadContent()
        {
            _debugTexture = new Texture2D(_graphicsDevice, 1, 1);
            _debugTexture.SetData(new Color[] { Color.White });

            var CollisionsHard = _tiledMap.GetLayer<TiledMapObjectLayer>("CollisionHard");
            _collisions = new List<RectangleF>();

            foreach (var HardCollision in CollisionsHard.Objects)
            {
                _rectangleF = new RectangleF(
                    (int)HardCollision.Position.X,
                    (int)HardCollision.Position.Y,
                    (int)HardCollision.Size.Width,
                    (int)HardCollision.Size.Height
                );

                _collisions.Add(_rectangleF);
            }
        }
        public void Draw()
        {
            foreach (var collision in _collisions)
            {
                // Use the X and Y of the RectangleF to create a Vector2 for the position
                Vector2 position = new Vector2(collision.X, collision.Y);

                // Pass the width and height of the collision rectangle to the Draw method
                _spriteBatch.Draw(_debugTexture, position, null, Color.Red, 0f, Vector2.Zero, new Vector2(collision.Width, collision.Height), SpriteEffects.None, 0f);
            }
        }
        public void OnCollision(CollisionEventArgs collisionInfo)
        {
            Bounds.Position -= collisionInfo.PenetrationVector;
        }
    }
}
