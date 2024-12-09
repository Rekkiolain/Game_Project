using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using PlatformerV3.Entities;
using PlatformerV3.World;
using System.Collections.Generic;
using System.IO;

namespace PlatformerV3.GameWorld
{
    internal class LevelRenderer
    {
        private List<Rectangle> _textureStore;
        private Texture2D _textureAtlas;
        private Player _player;
        private EntityCollisionsV2 collisions;
        private Camera _camera;

        public LevelRenderer()
        {
            collisions = new EntityCollisionsV2();
            _camera = new Camera();
            _player = new Player();
        }


        public void Initialize(GameWindow window, GraphicsDevice graphicsDevice)
        {
            _camera.Initialize(window, graphicsDevice);
            _player.Initialize();
        }

        public void LoadContentPart1(ContentManager content)
        {
            _textureAtlas = content.Load<Texture2D>("blocks");
            _textureStore = new List<Rectangle>();
        }

        public void LoadContentPart2(ContentManager content, Dictionary<Vector2, int> _collisionsList, Dictionary<Vector2, int> _mapBounds, GraphicsDevice graphicsDevice)
        {
            collisions.GenerateCollisionRectangles(_collisionsList,_mapBounds);


            collisions.CreateRedTexture(graphicsDevice);

            _player.LoadContent(content);
        }

        public void Update(GameTime gameTime)
        {
            collisions._collisionRectangles.Remove(_player._marineBase.HitBox);

            _player.Update(gameTime);

            collisions.Collision(_player._marineBase.Bounds.ToRectangle(), ref _player._marineBase.Position, ref _player._isFalling, ref _player._isIdle, ref _player._isOnGround);

            _player._marineBase.Bounds.Position = _player._marineBase.Position;
            _player._marineBase.HitBox = _player._marineBase.Bounds.ToRectangle();

            collisions.addEntity(_player._marineBase.HitBox);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            collisions.Draw(spriteBatch);
            _player.Draw(spriteBatch);
        }

        public void getSpriteSheet()
        {
            int x = 0, y = 0;

            for (int row = 0; row < 128; row++)
            {
                for (int col = 0; col < 128; col++)
                {
                    _textureStore.Add(new Rectangle(x, y, 8, 8));
                    x += 8;
                }
                x = 0;
                y += 8;
            }
        }

        public void getTileMap(Dictionary<Vector2, int> _tileMap, SpriteBatch _spriteBatch, int X, int Y)
        {
            int scale = 16;

            foreach (var item in _tileMap)
            {
                Vector2 offset = new Vector2(X, Y);
                Rectangle dest = new(
                    (int)item.Key.X * scale + (int)offset.X,
                    (int)item.Key.Y * scale - (int)offset.Y,
                    scale,
                    scale
                );

                Rectangle src = _textureStore[item.Value - 0];
                _spriteBatch.Draw(_textureAtlas, dest, src, Color.White);
            }
        }

        public Dictionary<Vector2, int> LoadMap(string filepath)
        {
            Dictionary<Vector2, int> result = new();

            StreamReader reader = new(filepath);

            int y = 0;
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] items = line.Split(',');

                for (int x = 0; x < items.Length; x++)
                {
                    if (int.TryParse(items[x], out int value))
                    {
                        if (value > 0)
                        {
                            result[new Vector2(x, y)] = value;
                        }
                    }
                }
                y++;


            }
            return result;
        }

        public Matrix getTransformationMatrix() { return _camera._camera.GetViewMatrix(); }

    }
}
