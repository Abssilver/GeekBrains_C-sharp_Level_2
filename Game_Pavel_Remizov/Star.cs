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
        protected Image image;
        public Star(Point pos, Point dir, Size size) : base (pos, dir, size)
        {
        }
        public Star(Point pos, Point dir, Size aspectRatio, Image image) : base(pos, dir, aspectRatio)
        {
            this.image = image;
        }
        public override void Draw()
        {
            if (image is null)
            {
                Game.Buffer.Graphics.DrawLine
                    (Pens.White, Pos.X, Pos.Y, Pos.X + Size.Width, Pos.Y + Size.Height);
                Game.Buffer.Graphics.DrawLine
                    (Pens.White, Pos.X + Size.Width, Pos.Y, Pos.X, Pos.Y + Size.Height);
            }
            else
            {
                Game.Buffer.Graphics.DrawImage(image, new Rectangle(Pos, base.Size));
            }
        }
        public override void Update()
        {
            Pos.X += Dir.X;
            if (Pos.X + Size.Width < 0)
            {
                Random rnd = new Random();
                Pos.X = Game.Width + Size.Width;
                Pos.Y = rnd.Next(Size.Height, Game.Height - Size.Height);
            }
        }
    }
}
