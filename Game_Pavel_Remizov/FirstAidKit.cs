using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Game_Pavel_Remizov
{
    class FirstAidKit : BaseObject
    {
        protected Image image;

        public FirstAidKit(Point pos, Point dir, Size size) : base(pos, dir, size)
        { }
        public FirstAidKit(Point pos, Point dir, Size aspectRatio, Image image) : base(pos, dir, aspectRatio)
        {
            this.image = image;
        }
        public override void Draw()
        {
            if (image is null)
                Game.Buffer.Graphics.FillEllipse
                    (Brushes.Green, base.Pos.X, base.Pos.Y, base.Size.Width, base.Size.Height);
            else
                Game.Buffer.Graphics.DrawImage(image, new Rectangle(base.Pos, base.Size));
        }
        public override void Update()
        {
            if (base.Pos.X > - 2 * base.Size.Width)
                base.Pos.X += base.Dir.X;
        }

    }
}
