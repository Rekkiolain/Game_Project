using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformerV3.GameWorld
{
    internal class LevelRenderer
    {
        private List<Rectangle> _textureStore;
        private Texture2D _textureAtlas;

        public void LoadContent(ContentManager content,string filePath)
        {
            _textureAtlas = content.Load<Texture2D>(filePath);
            _textureStore = new List<Rectangle>();
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

        public void getTileMap(Dictionary<Vector2, int> _tileMap, SpriteBatch _spriteBatch)
        {
            int scale = 16;

            foreach (var item in _tileMap)
            {
                Rectangle dest = new(
                    (int)item.Key.X * scale,
                    (int)item.Key.Y * scale,
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
    }
}
