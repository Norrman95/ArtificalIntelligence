using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Uppgift_3._2
{
    static class Globals
    {
        public static Texture2D tex { get; private set; }
        public static Texture2D tex2 { get; private set; }

        public static void LoadContent(ContentManager Content)
        {
            tex = Content.Load<Texture2D>("tex");
            tex2 = Content.Load<Texture2D>("tex2");
        }
    }
}