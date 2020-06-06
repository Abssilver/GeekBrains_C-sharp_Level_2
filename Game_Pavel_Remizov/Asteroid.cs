using System;
using System.Drawing;

namespace Game_Pavel_Remizov
{
    class Asteroid: BaseObject
    {
        //public int Power { get; set; }
        public Asteroid(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            //Power = 1;
        }
        public override void Draw() =>
            Game.Buffer.Graphics.FillEllipse
            (Brushes.White, base.Pos.X, base.Pos.Y, base.Size.Width, base.Size.Height);
        public override void Update()
        {
            base.Pos.X += base.Dir.X;
            base.Pos.Y += base.Dir.Y;
            if (base.Pos.X < 0 || base.Pos.X > Game.Width) base.Dir.X = -base.Dir.X;
            if (base.Pos.Y < 0 || base.Pos.Y > Game.Height) base.Dir.Y = -base.Dir.Y;
        }
        public override void GenerateNewPosition(Random rnd) =>
            base.Pos = new Point
            (Game.Width - base.Size.Width, 
             rnd.Next(Game.Height / 3 + base.Size.Height, Game.Height * 2 / 3 - base.Size.Height));
    }
}
