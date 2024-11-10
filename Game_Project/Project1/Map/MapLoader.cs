using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;
using TiledSharp;

namespace Project1.Map
{
    public class MapLoader
    {
        private SpriteBatch spriteBatch;
        private TmxMap map;
        private Texture2D tileset;
        private int tilesetTilesWide;
        private int tileWidth;
        private int tileHeight;
        private float scale;

        public MapLoader(SpriteBatch _spriteBatch, ContentManager content, string mapFilePath, string tilesetAssetName, int screenWidth, int screenHeight)
        {
            spriteBatch = _spriteBatch;

            // Load the map
            map = new TmxMap(mapFilePath);

            // Load the tileset texture using the content manager
            tileset = content.Load<Texture2D>(tilesetAssetName + map.Tilesets[0].Name);

            // Get tile dimensions
            tileWidth = map.Tilesets[0].TileWidth;
            tileHeight = map.Tilesets[0].TileHeight;
            tilesetTilesWide = tileset.Width / tileWidth;

            // Calculate scale to fit map to screen
            int mapPixelWidth = map.Width * tileWidth;
            int mapPixelHeight = map.Height * tileHeight;
            float scaleX = (float)screenWidth / mapPixelWidth;
            float scaleY = (float)screenHeight / mapPixelHeight;
            scale = Math.Min(scaleX, scaleY); // Use the smaller scale factor to fit the map
        }

        public void Draw()
        {
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            for (var i = 0; i < map.Layers.Count; i++)
            {
                for (var j = 0; j < map.Layers[i].Tiles.Count; j++)
                {
                    int gid = map.Layers[i].Tiles[j].Gid;
                    if (gid == 0)
                    {
                        continue; 
                    }

                    int tileFrame = gid - 1;
                    int column = tileFrame % tilesetTilesWide;
                    int row = tileFrame / tilesetTilesWide;

                    float x = (j % map.Width) * map.TileWidth * scale;
                    float y = (float)Math.Floor(j / (double)map.Width) * map.TileHeight * scale;

                    Rectangle tilesetRec = new Rectangle(tileWidth * column, tileHeight * row, tileWidth, tileHeight);
                    Rectangle destRec = new Rectangle((int)x, (int)y, (int)(tileWidth * scale), (int)(tileHeight * scale));

                    spriteBatch.Draw(tileset, destRec, tilesetRec, Color.White);
                }
            } 
            spriteBatch.End();
        }

    }
}