using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDefence
{
    class TextureManager
    {
        public static Texture2D missile { get; private set; }
        public static Texture2D enemy { get; private set; }
        public static Texture2D player { get; private set; }

        public static SpriteFont font { get; private set; }

        public static void LoadContent(ContentManager Content)
        {
            missile = Content.Load<Texture2D>("missile");
            player = Content.Load<Texture2D>("player");
            enemy = Content.Load<Texture2D>("enemy");

            font = Content.Load<SpriteFont>("Font"); 
        }
    }

    static class Settings
    {
        public const int ScreenWidth = 920;
        public const int ScreenHeight = 800;
    }
}
