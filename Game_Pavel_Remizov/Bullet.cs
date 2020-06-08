using System;
using System.Drawing;

namespace Game_Pavel_Remizov
{
    class Bullet : BaseObject
    {
        public Bullet(Point pos, Point dir, Size size) : base(pos, dir, size)
        { 
        }
        public override void Draw() =>
            Game.Buffer.Graphics.DrawRectangle
            (Pens.OrangeRed, base.Pos.X, base.Pos.Y, base.Size.Width, base.Size.Height);
        public override void Update() => base.Pos.X += 3;
        public override void GenerateNewPosition(Random rnd) =>
            base.Pos = new Point
            (0, rnd.Next(Game.Height / 3 + base.Size.Height, Game.Height * 2 / 3 - base.Size.Height));
    }
}
