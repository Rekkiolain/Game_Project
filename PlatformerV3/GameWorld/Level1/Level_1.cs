
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Collisions.Layers;
using PlatformerV3.Entities;
using PlatformerV3.Entities.Sprites.NPC_s;
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
        private Toad toad1;
        private Player player1;

        public Level_1()
        {
            _levelRenderer = new LevelRenderer();
            toad1 = new Toad();
            player1 = new Player();
        }

        public void Initialize(GameWindow window, GraphicsDevice graphicsDevice)
        {
            _levelRenderer.Initialize(window, graphicsDevice);
            toad1.Initialize();
            player1.Initialize();

            toad1._toadBase.SetStartPosition(new Vector2(365, 118));
            player1._marineBase.SetStartPosition(new Vector2(100,200));
        }

        public void LoadContent(ContentManager content, GraphicsDevice graphicsDevice)
        {
            player1.LoadContent(content);

            _levelRenderer.LoadContentPart1(content);
            _levelRenderer.getSpriteSheet();

            _mapBounds = _levelRenderer.LoadMap("../../../Data/MapBounds.csv");
            _collisions = _levelRenderer.LoadMap("../../../Data/Collisions.csv");
            _details = _levelRenderer.LoadMap("../../../Data/Details.csv");
            _mainBlocks = _levelRenderer.LoadMap("../../../Data/MainBlocks.csv");

            _levelRenderer.LoadContentPart2(content, _collisions,_mapBounds, graphicsDevice);
            toad1.LoadContent(content);
        }

        public void Update(GameTime gameTime)
        {
            _levelRenderer.Update(gameTime);

            _levelRenderer.collisions._collisionRectangles.Remove(player1._marineBase.HitBox);
            _levelRenderer.collisions._collisionRectangles.Remove(toad1._toadBase.HitBox);

            player1.Update(gameTime);
            toad1.Update(gameTime);

            _levelRenderer.collisions.Collision(player1._marineBase.Bounds.ToRectangle(), ref player1._marineBase.Position, ref player1._marineBase.isFalling, ref player1._marineBase.isIdle, ref player1._marineBase.isOnGround);
            _levelRenderer.collisions.Collision(toad1._toadBase.Bounds.ToRectangle(), ref toad1.ToadBase.Position, ref toad1.isFalling, ref toad1.isIdle, ref toad1.isOnGround);

            //playerpositioning
            player1._marineBase.Bounds.Position = player1._marineBase.Position;
            player1._marineBase.HitBox = player1._marineBase.Bounds.ToRectangle();
            //toad positioning
            toad1._toadBase.Bounds.Position = toad1._toadBase.Position;
            toad1._toadBase.HitBox = toad1._toadBase.Bounds.ToRectangle();

            _levelRenderer.collisions.addEntity(player1._marineBase.HitBox);
            _levelRenderer.collisions.addEntity(toad1._toadBase.HitBox);


        }

        public void Draw(SpriteBatch _spriteBatch)
        {

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: _levelRenderer.getTransformationMatrix());

            _levelRenderer.getTileMap(_mapBounds, _spriteBatch,-16,16);
            _levelRenderer.getTileMap(_collisions, _spriteBatch,0,0);
            _levelRenderer.getTileMap(_mainBlocks, _spriteBatch, 0, 0);
            _levelRenderer.getTileMap(_details, _spriteBatch,0,0);

            toad1.Draw(_spriteBatch);
            player1.Draw(_spriteBatch);
            _spriteBatch.End();


        }

         

    }
}