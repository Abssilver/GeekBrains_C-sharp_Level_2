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
        protected Image image;
        public UFO(Point pos, Point dir, Size size): base (pos, dir, size)
        {
            Pos = pos;
            Dir = dir;
            Size = size;
        }
        public UFO(Point pos, Point dir, Size aspectRatio, Image image) : base(pos, dir, aspectRatio)
        {
            this.image = image;
        }
        public override void Draw()
        {
            if (image is null)
                base.Draw();
            else
                Game.Buffer.Graphics.DrawImage(image, new Rectangle(Pos, base.Size));
        }
        public override void Update()
        {
            Pos.X += Dir.X;
            Pos.Y += Dir.Y;
            if (Pos.X + Size.Width < 0) Pos.X = Game.Width + Size.Width;
            else if (Pos.X > Game.Width + Size.Width) Pos.X = -Size.Width;
            if (Pos.Y + Size.Height < 0) Pos.Y = Game.Height + Size.Height;
            else if (Pos.Y > Game.Height + Size.Height) Pos.Y = -Size.Height;
        }
    }
}
