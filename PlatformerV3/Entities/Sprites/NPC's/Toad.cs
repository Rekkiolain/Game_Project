using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Collisions;
using MonoGame.Extended.Graphics;
using MonoGame.Extended.Input.InputListeners;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformerV3.Entities.Sprites.NPC_s
{
    public class Toad : BaseSprite,IEntity
    {
        public BaseSprite _toadBase;
        private AnimatedSprite _toad;

        private Vector2 _velocity;
        private bool _movingRight = true;
        private float _changeDirectionTimer = 0f;
        private float _changeDirectionInterval = 2f;


        public BaseSprite ToadBase => _toadBase;

        public Toad() : base(0, 0, 12, 18, new RectangleF(new Vector2(0, 0), new SizeF(12, 18))) 
        {
            _speed = 1f;
        }
        

        

        public new IShapeF Bounds => throw new NotImplementedException();

        public void Initialize()
        {
            _toadBase = new BaseSprite(0, 0, 12, 18, new RectangleF(new Vector2(0, 0), new SizeF(12, 18)));
            _velocity = Vector2.Zero;
        }

        public void LoadContent(ContentManager content)
        {
            _toadBase.LoadContent(content, "SpritesheetToad", width: 80, height: 64);

            _toadBase.SpriteSheet.DefineAnimation("Attack", builder =>
            {
                builder.IsLooping(true)
                    .AddFrame(0, TimeSpan.FromSeconds(0.1))
                    .AddFrame(1, TimeSpan.FromSeconds(0.1))
                    .AddFrame(2, TimeSpan.FromSeconds(0.1));
            });
            _toadBase.SpriteSheet.DefineAnimation("Jump", builder =>
            {
                builder.IsLooping(true)
                    .AddFrame(4, TimeSpan.FromSeconds(0.1))
                    .AddFrame(5, TimeSpan.FromSeconds(0.1))
                    .AddFrame(6, TimeSpan.FromSeconds(0.1))
                    .AddFrame(7, TimeSpan.FromSeconds(0.1));
            });
            _toadBase.SpriteSheet.DefineAnimation("Idle", builder =>
            {
                builder.IsLooping(true)
                    .AddFrame(8, TimeSpan.FromSeconds(0.2))
                    .AddFrame(9, TimeSpan.FromSeconds(0.2))
                    .AddFrame(10, TimeSpan.FromSeconds(0.2))
                    .AddFrame(11, TimeSpan.FromSeconds(0.2));
            });

            _toad = new AnimatedSprite(_toadBase.SpriteSheet, "Jump");
          
        }

        public void Update(GameTime gameTime)
        {
            _toadBase.DeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _toad.Update(gameTime);

            _changeDirectionTimer += _toadBase.DeltaTime;
            if (_changeDirectionTimer >= _changeDirectionInterval)
            {
                _movingRight = !_movingRight;
                _changeDirectionTimer = 0f;
            }

            if (_movingRight)
            {
                _velocity.X = _speed;
                _toadBase.SpriteEffects = SpriteEffects.None;
            }
            else
            {
                _velocity.X = -_speed;
                _toadBase.SpriteEffects = SpriteEffects.FlipHorizontally;
            }

            if (!_isOnGround)
            {
                _velocity.Y += _gravity;
                if (_velocity.Y > _maxFallSpeed) _velocity.Y = _maxFallSpeed;
            }

            _toadBase.Position += _velocity;

            _toadBase.Position.Y += _velocity.Y;
        }

        public void OnCollision(CollisionEventArgs collisionInfo) { }

        public void Draw(SpriteBatch spriteBatch)
        {
            _toadBase.Draw(spriteBatch, 75, 48, new Vector2(0, -11), _toad);
        }

        private bool _isOnGround = false;  // You will need a way to determine if the toad is on the ground
    }

}
