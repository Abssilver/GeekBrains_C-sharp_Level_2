using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Pavel_Remizov
{
    class Star: BaseObject
    {
        protected Image _image;
        public Star(Point pos, Point dir, Size size) : base (pos, dir, size)
        {
        }
        public Star(Point pos, Point dir, Size aspectRatio, Image image) : base(pos, dir, aspectRatio)
        {
            _image = image;
        }
        public override void Draw()
        {
            if (_image is null)
            {
                Game.Buffer.Graphics.DrawLine
                    (Pens.White, base.Pos.X, base.Pos.Y, base.Pos.X + base.Size.Width, base.Pos.Y + base.Size.Height);
                Game.Buffer.Graphics.DrawLine
                    (Pens.White, base.Pos.X + base.Size.Width, base.Pos.Y, base.Pos.X, base.Pos.Y + base.Size.Height);
            }
            else
            {
                Game.Buffer.Graphics.DrawImage(_image, new Rectangle(base.Pos, base.Size));
            }
        }
        public override void Update()
        {
            base.Pos.X += base.Dir.X;
            if (base.Pos.X + base.Size.Width < 0)
            {
                Random rnd = new Random();
                base.Pos.X = Game.Width + base.Size.Width;
                base.Pos.Y = rnd.Next(base.Size.Height, Game.Height - base.Size.Height);
            }
        }
    }
}
