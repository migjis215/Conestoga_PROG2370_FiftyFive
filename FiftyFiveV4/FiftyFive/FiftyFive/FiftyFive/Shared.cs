using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FiftyFive
{
    public class Shared
    {
        public static Vector2 screen;
        public static Vector2 stage;
        public static List<Vector2> positions;
        public static List<Rectangle> numbers;
        public static List<Rectangle> nines;

        public const int STAGE_WITH = 600;
        public const int STAGE_HIEGHT = 440;
        public const int GAP = 20;
    }
}
