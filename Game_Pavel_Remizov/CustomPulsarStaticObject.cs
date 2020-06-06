using System;
using System.Drawing;

namespace Game_Pavel_Remizov
{
    class CustomPulsarStaticObject : PulsarStaticObject
    {
        private Brush _brush;
        public CustomPulsarStaticObject(Point pos, Point dir, Size size, Random rnd, Brush brush) : 
            base(pos, dir, size, rnd)
        {
            _brush = brush;
        }
        public override void Draw() =>
            Game.Buffer.Graphics.FillEllipse(_brush, new Rectangle(base.Pos, base.Size));
    }
}
