using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace Game_Pavel_Remizov
{
    class PulsarStaticObject: BaseObject
    {
        Random rnd;
        Size _originalSize;
        float _scaler;
        public PulsarStaticObject(Point pos, Point dir, Size size, Random rnd): base (pos, dir, size)
        {
            this.rnd = rnd;
            _originalSize = size;
            _scaler = _originalSize.Width;
        }
        public override void Update()
        {
            _scaler += 0.05f * Dir.X;
            Size.Width = Convert.ToInt32(_scaler);
            Size.Height = Convert.ToInt32(_scaler);
            if (Size.Width <= 0)
            {
                Pos = GenerateNewPosition(rnd);
                Dir.X = -Dir.X;
            }
            else if (Size.Width > _originalSize.Width)
            {
                Dir.X = -Dir.X;
            }
        }
        protected virtual Point GenerateNewPosition(Random rnd) => new Point(rnd.Next(0, Game.Width), rnd.Next(0, Game.Height));
    }
}
