using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Game_Pavel_Remizov
{
    class UFO : BaseObject
    {
        protected Image _image;
        public UFO(Point pos, Point dir, Size size): base (pos, dir, size)
        { }
        public UFO(Point pos, Point dir, Size aspectRatio, Image image) : base(pos, dir, aspectRatio)
        {
            _image = image;
        }
        public override void Draw()
        {
            if (_image is null)
                Game.Buffer.Graphics.DrawEllipse
                    (Pens.White, base.Pos.X, base.Pos.Y, base.Size.Width, base.Size.Height);
            else
                Game.Buffer.Graphics.DrawImage(_image, new Rectangle(Pos, base.Size));
        }
        public override void Update()
        {
            base.Pos.X += base.Dir.X;
            base.Pos.Y += base.Dir.Y;
            if (base.Pos.X + base.Size.Width < 0) base.Pos.X = Game.Width + base.Size.Width;
            else if (base.Pos.X > Game.Width + base.Size.Width) base.Pos.X = -base.Size.Width;
            if (base.Pos.Y + base.Size.Height < 0) base.Pos.Y = Game.Height + base.Size.Height;
            else if (base.Pos.Y > Game.Height + base.Size.Height) base.Pos.Y = -base.Size.Height;
        }
    }
}
