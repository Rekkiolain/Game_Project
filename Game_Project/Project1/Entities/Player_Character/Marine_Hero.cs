using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Animations;
using MonoGame.Extended.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Extended.Input.InputListeners;
using Microsoft.Xna.Framework.Input;

namespace Project1.Entities.Player_Character
{
    internal class Marine_Hero
    {
        private SpriteSheet _spriteSheet;
        private AnimatedSprite _marine;
        private KeyboardListener _keyboardListener;
        private Vector2 _position = Vector2.Zero; 

        public void LoadContent(ContentManager content)
        {
            Texture2D MarineTexture = content.Load<Texture2D>("Player");
            Texture2DAtlas atlas = Texture2DAtlas.Create("Assets/Player/Player", MarineTexture, 75, 48);
            _spriteSheet = new SpriteSheet("SpriteSheet/Player", atlas);

            _spriteSheet.DefineAnimation("Idle", builder =>
            {
                builder.IsLooping(true)
                        .AddFrame(regionIndex: 30, duration: TimeSpan.FromSeconds(0.1))
                        .AddFrame(31, TimeSpan.FromSeconds(0.1))
                        .AddFrame(32, TimeSpan.FromSeconds(0.1))
                        .AddFrame(33, TimeSpan.FromSeconds(0.1));
            });
            _spriteSheet.DefineAnimation("Jump", builder =>
            {
                builder.IsLooping(false)
                        .AddFrame(130, TimeSpan.FromSeconds(0.1))
                        .AddFrame(131, TimeSpan.FromSeconds(0.1))
                        .AddFrame(132, TimeSpan.FromSeconds(0.1))
                        .AddFrame(133, TimeSpan.FromSeconds(0.1))
                        .AddFrame(134, TimeSpan.FromSeconds(0.1))
                        .AddFrame(135, TimeSpan.FromSeconds(0.1));

            });
            _spriteSheet.DefineAnimation("Run", builder =>
            {
                builder.IsLooping(true)
                        .AddFrame(60, TimeSpan.FromSeconds(0.1))
                        .AddFrame(61, TimeSpan.FromSeconds(0.1))
                        .AddFrame(62, TimeSpan.FromSeconds(0.1))
                        .AddFrame(63, TimeSpan.FromSeconds(0.1))
                        .AddFrame(64, TimeSpan.FromSeconds(0.1))
                        .AddFrame(65, TimeSpan.FromSeconds(0.1))
                        .AddFrame(66, TimeSpan.FromSeconds(0.1))
                        .AddFrame(67, TimeSpan.FromSeconds(0.1))
                        .AddFrame(68, TimeSpan.FromSeconds(0.1))
                        .AddFrame(69, TimeSpan.FromSeconds(0.1));

            });

            _marine = new AnimatedSprite(_spriteSheet, "Idle");
        }
        public void Initialize()
        {
            _keyboardListener = new KeyboardListener();

            
            bool isRunning = false;

           
            _keyboardListener.KeyPressed += (sender, eventArgs) =>
            {
                
                if (eventArgs.Key == Keys.Space && _marine.CurrentAnimation == "Idle")
                {
                    _marine.SetAnimation("Jump").OnAnimationEvent += (sender, trigger) =>
                    {
                        if (trigger == AnimationEventTrigger.AnimationCompleted)
                        {
                            _marine.SetAnimation("Idle");
                        }
                    };
                }

                if ((eventArgs.Key == Keys.Q || eventArgs.Key == Keys.D) && !isRunning)
                {
                    isRunning = true; 
                    _marine.SetAnimation("Run");
                }
            };

            
            _keyboardListener.KeyReleased += (sender, eventArgs) =>
            {
                if ((eventArgs.Key == Keys.Q || eventArgs.Key == Keys.D) && isRunning)
                {
                    isRunning = false; 
                    _marine.SetAnimation("Idle");
                }
            };
        }

        public void Update(GameTime gameTime)
        {
            _keyboardListener.Update(gameTime);
            _marine.Update(gameTime);

            if (_marine.CurrentAnimation == "Run")
            {
                float speed = 100f; 
                float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

                
                if (Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    _position += new Vector2(speed * deltaTime, 0); 
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Q))
                {
                    _position += new Vector2(-speed * deltaTime, 0);
                }
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            int scale = 2;
            spriteBatch.Draw(_marine, _position, 0, new Vector2(scale)); 
        }
    }
}
