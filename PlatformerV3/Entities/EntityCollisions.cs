using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformerV3.Entities
{
    internal class EntityCollisions
    {
        private int TILESIZE = 48;
        public List<Rectangle> Intersections;
        private Texture2D rectangleTexture;
        public EntityCollisions()
        {
            Intersections = new();
        }

        public void LoadContent(GraphicsDevice graphicsDevice)
        {
            rectangleTexture = new Texture2D(graphicsDevice, 1, 1);
            rectangleTexture.SetData(new Color[] { new(255, 0, 0, 255) });
        }

        public void UpdateHorizontalIntersections(Rectangle playersRectangle)
        {
            Intersections = IntersectingTilesHorizontal(playersRectangle);

        }
        public void UpdateVerticalIntersections(Rectangle playersRectangle)
        {
            Intersections = IntersectingTilesVertical(playersRectangle);

        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            foreach (var rect in Intersections)
            {

                DrawRectHollow(
                    _spriteBatch,
                    new Rectangle(
                        rect.X * TILESIZE,
                        rect.Y * TILESIZE,
                        TILESIZE,
                        TILESIZE
                    ),
                    1
                );

            }
        }
        
        public void onHorizontalCollisions(Dictionary<Vector2, int> _collisions,Vector2 _velocity, Rectangle _rect)
        {
            foreach (var rect in Intersections)
            {

                if (_collisions.TryGetValue(new Vector2(rect.X, rect.Y), out int _val))
                {
                    Rectangle collision = new(
                        rect.X * TILESIZE,
                        rect.Y * TILESIZE,
                        TILESIZE,
                        TILESIZE
                    );

                    if (_velocity.X > 0.0f)
                    {
                        _rect.X = collision.Left - _rect.Width;
                    }
                    else if (_velocity.X < 0.0f)
                    {
                        _rect.X = collision.Right;
                    }
                }

            }
        }

        public void onVerticalCollisions(Dictionary<Vector2, int> _collisions, Vector2 _velocity, Rectangle _rect)
        {
            foreach (var rect in Intersections)
            {

                if (_collisions.TryGetValue(new Vector2(rect.X, rect.Y), out int _val))
                {

                    Rectangle collision = new Rectangle(
                        rect.X * TILESIZE,
                        rect.Y * TILESIZE,
                        TILESIZE,
                        TILESIZE
                    );

                    if (_velocity.Y > 0.0f)
                    {
                        _rect.Y = collision.Top - _rect.Height;
                    }
                    else if (_velocity.Y < 0.0f)
                    {
                        _rect.Y = collision.Bottom;
                    }

                }
            }
        }


        public List<Rectangle> IntersectingTilesHorizontal(Rectangle target)
        {

            List<Rectangle> intersections = new();

            int widthInTiles = (target.Width - (target.Width % TILESIZE)) / TILESIZE;
            int heightInTiles = (target.Height - (target.Height % TILESIZE)) / TILESIZE;

            for (int x = 0; x <= widthInTiles; x++)
            {
                for (int y = 0; y <= heightInTiles; y++)
                {

                    intersections.Add(new Rectangle(

                        (target.X + x * TILESIZE) / TILESIZE,
                        (target.Y + y * (TILESIZE - 1)) / TILESIZE,
                        TILESIZE,
                        TILESIZE

                    ));

                }
            }

            return intersections;
        }

        public List<Rectangle> IntersectingTilesVertical(Rectangle target)
        {

            List<Rectangle> intersections = new();

            int widthInTiles = (target.Width - (target.Width % TILESIZE)) / TILESIZE;
            int heightInTiles = (target.Height - (target.Height % TILESIZE)) / TILESIZE;

            for (int x = 0; x <= widthInTiles; x++)
            {
                for (int y = 0; y <= heightInTiles; y++)
                {

                    intersections.Add(new Rectangle(

                        (target.X + x * (TILESIZE - 1)) / TILESIZE,
                        (target.Y + y * TILESIZE) / TILESIZE,
                        TILESIZE,
                        TILESIZE

                    ));

                }
            }

            return intersections;
        }
        public void DrawRectHollow(SpriteBatch spriteBatch, Rectangle rect, int thickness)
        {
            spriteBatch.Draw(
                rectangleTexture,
                new Rectangle(
                    rect.X,
                    rect.Y,
                    rect.Width,
                    thickness
                ),
                Color.White
            );
            spriteBatch.Draw(
                rectangleTexture,
                new Rectangle(
                    rect.X,
                    rect.Bottom - thickness,
                    rect.Width,
                    thickness
                ),
                Color.White
            );
            spriteBatch.Draw(
                rectangleTexture,
                new Rectangle(
                    rect.X,
                    rect.Y,
                    thickness,
                    rect.Height
                ),
                Color.White
            );
            spriteBatch.Draw(
                rectangleTexture,
                new Rectangle(
                    rect.Right - thickness,
                    rect.Y,
                    thickness,
                    rect.Height
                ),
                Color.White
            );

        }
    }
}
