using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uppgift_3._2
{
    class Level
    {
        public const int TileSize = 50;

        public bool[,] vis = new bool[,]
        {
            { false, false, false, false, false, false, false, false, false, false, false, false,},
            { false, false, false, false, false, false, false, false, false, false, false, false,},
            { false, false, false, false, false, false, false, false, false, false, false, false,},
            { false, false, false, false, false, false, false, false, false, false, false, false,},
            { false, false, false, false, false, false, false, false, false, false, false, false,},
            { false, false, false, false, false, false, false, false, false, false, false, false,},
            { false, false, false, false, false, false, false, false, false, false, false, false,},
            { false, false, false, false, false, false, false, false, false, false, false, false,},
            { false, false, false, false, false, false, false, false, false, false, false, false,},
            { false, false, false, false, false, false, false, false, false, false, false, false,},
            { false, false, false, false, false, false, false, false, false, false, false, false,},
            { false, false, false, false, false, false, false, false, false, false, false, false,}
        };

        public int[,] dist = new int[,]
        {
            { 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100,},
            { 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100,},
            { 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100,},
            { 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100,},
            { 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100,},
            { 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100,},
            { 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100,},
            { 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100,},
            { 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100,},
            { 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100,},
            { 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100,},
            { 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100,}
        };

        public int[,] layout = new int[,]
        {
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,},
            { 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1,},
            { 1, 0, 1, 0, 1, 0, 1, 0, 1, 1, 0, 1,},
            { 1, 0, 1, 0, 1, 0, 0, 0, 1, 0, 0, 1,},
            { 1, 0, 1, 0, 0, 0, 1, 0, 1, 0, 1, 1,},
            { 1, 0, 1, 1, 1, 0, 1, 0, 0, 0, 0, 1,},
            { 1, 0, 1, 0, 1, 0, 1, 0, 1, 1, 0, 1,},
            { 1, 0, 1, 0, 0, 0, 1, 0, 1, 1, 0, 1,},
            { 1, 0, 1, 0, 1, 0, 1, 0, 0, 1, 0, 1,},
            { 1, 0, 1, 0, 1, 0, 1, 1, 0, 1, 0, 1,},
            { 1, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 1,},
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,}
        };


        public int Width
        {
            get { return layout.GetLength(1); }
        }

        public int Height
        {
            get { return layout.GetLength(0); }
        }


        public int GetIndex(int cellX, int cellY)
        {
            if (cellX < 0 || cellX > Width - 1 || cellY < 0 || cellY > Height - 1)
                return 0;

            return layout[cellY, cellX];
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    int index = layout[i, j];
                    if (index == 1)
                    {
                        spriteBatch.Draw(Globals.tex, new Vector2(i, j) * TileSize, Color.Black);

                    }
                    else
                    {
                        spriteBatch.Draw(Globals.tex, new Vector2(i, j) * TileSize, Color.White);
                    }
                }
            }
        }
    }
}
