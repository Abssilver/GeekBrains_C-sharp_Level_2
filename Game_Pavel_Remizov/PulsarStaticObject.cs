using System;
using System.Drawing;

namespace Game_Pavel_Remizov
{
    class PulsarStaticObject: BaseObject
    {
        Random _rnd;
        Size _originalSize;
        float _scaler;
        public PulsarStaticObject(Point pos, Point dir, Size size, Random rnd): base (pos, dir, size)
        {
            _rnd = rnd;
            _originalSize = size;
            _scaler = _originalSize.Width;
        }
        public override void Update()
        {
            _scaler += 0.02f * base.Dir.X;
            base.Size.Width = Convert.ToInt32(_scaler);
            base.Size.Height = Convert.ToInt32(_scaler);
            if (base.Size.Width <= 0)
            {
                base.GenerateNewPosition(_rnd);
                base.Dir.X = -base.Dir.X;
            }
            else if (base.Size.Width > _originalSize.Width)
                base.Dir.X = -base.Dir.X;
        }
        public override void Draw() => 
            Game.Buffer.Graphics.DrawEllipse(Pens.White, base.Pos.X, base.Pos.Y, base.Size.Width, base.Size.Height);
        //protected virtual Point GenerateNewPosition(Random rnd) => new Point(rnd.Next(0, Game.Width), rnd.Next(0, Game.Height));
    }
}
