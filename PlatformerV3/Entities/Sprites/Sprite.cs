using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PlatformerV3.Entities.Sprites
{
    public class Sprite
    {
        private Texture2D _texture;
        private Texture2D rectangleTexture;
        private Rectangle _rect;
        private Rectangle _srect;

        public Sprite(Texture2D texture, Rectangle rect, Rectangle srect)
        {
            _texture = texture;
            _rect = rect;
            _srect = srect;
        }

        public void LoadContent(string fileName, ContentManager content,GraphicsDevice graphicsDevice)
        {
            _texture = content.Load<Texture2D>(fileName);

            rectangleTexture = new Texture2D(graphicsDevice, 1, 1);
            rectangleTexture.SetData(new Color[] { new(255, 0, 0, 255) });
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                _texture,
                _rect,
                _srect,
                Color.White
            );
        }

        //public void DrawRectHollow(SpriteBatch spriteBatch, Rectangle rect, int thickness)
        //{
        //    spriteBatch.Draw(
        //        rectangleTexture,
        //        new Rectangle(
        //            rect.X,
        //            rect.Y,
        //            rect.Width,
        //            thickness
        //        ),
        //        Color.White
        //    );
        //    spriteBatch.Draw(
        //        rectangleTexture,
        //        new Rectangle(
        //            rect.X,
        //            rect.Bottom - thickness,
        //            rect.Width,
        //            thickness
        //        ),
        //        Color.White
        //    );
        //    spriteBatch.Draw(
        //        rectangleTexture,
        //        new Rectangle(
        //            rect.X,
        //            rect.Y,
        //            thickness,
        //            rect.Height
        //        ),
        //        Color.White
        //    );
        //    spriteBatch.Draw(
        //        rectangleTexture,
        //        new Rectangle(
        //            rect.Right - thickness,
        //            rect.Y,
        //            thickness,
        //            rect.Height
        //        ),
        //        Color.White
        //    );
        //}
    }
}
