using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Animations;
using MonoGame.Extended.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Project1.Entities.NPC
{
    internal class Bird_NPC
    {
        private SpriteSheet _spriteSheet;
        private AnimationController _attackAnimationController;

        public void LoadContent(ContentManager content)
        {
            Texture2D BirdTexture = content.Load<Texture2D>("Bird_NPC");
            Texture2DAtlas atlas = Texture2DAtlas.Create("Assets/NPC/Bird_NPC", BirdTexture, 32, 32);
            _spriteSheet = new SpriteSheet("SpriteSheet/Bird_NPC", atlas);

            _spriteSheet.DefineAnimation("Fly", builder =>
            {
                builder.IsLooping(true)
                        .AddFrame(regionIndex: 0, duration: TimeSpan.FromSeconds(0.1))
                        .AddFrame(1, TimeSpan.FromSeconds(0.1))
                        .AddFrame(2, TimeSpan.FromSeconds(0.1))
                        .AddFrame(3, TimeSpan.FromSeconds(0.1))
                        .AddFrame(4, TimeSpan.FromSeconds(0.1))
                        .AddFrame(5, TimeSpan.FromSeconds(0.1))
                        .AddFrame(6, TimeSpan.FromSeconds(0.1));

            });
            SpriteSheetAnimation attackAnimation = _spriteSheet.GetAnimation("Fly");
            _attackAnimationController = new AnimationController(attackAnimation);


        }
        public void Update(GameTime gameTime)
        {
            _attackAnimationController.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch _spriteBatch)
        {
            Texture2DRegion currentFrameTexture = _spriteSheet.TextureAtlas[_attackAnimationController.CurrentFrame];
            _spriteBatch.Draw(currentFrameTexture, Vector2.Zero, Color.White, 0.0f, Vector2.Zero, new Vector2(3, 3), SpriteEffects.None, 0.0f);
        }

    }
}
