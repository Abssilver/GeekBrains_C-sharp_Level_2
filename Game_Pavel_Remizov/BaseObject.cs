using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Game_Pavel_Remizov
{
    //Переделать виртуальный метод Update в BaseObject в абстрактный и реализовать его в наследниках.
    abstract class BaseObject: ICollision
    {
        protected Point Pos;
        protected Point Dir;
        protected Size Size;

        protected BaseObject(Point pos, Point dir, Size size)
        {
            Pos = pos;
            Dir = dir;
            Size = size;
        }
        public abstract void Draw();
        //Game.Buffer.Graphics.DrawEllipse(Pens.White, Pos.X, Pos.Y, Size.Width, Size.Height);
        public abstract void Update();
        /*
            Pos.X += Dir.X;
            Pos.Y += Dir.Y;
            if (Pos.X < 0 || Pos.X > Game.Width) Dir.X = -Dir.X;
            if (Pos.Y < 0 || Pos.Y > Game.Height) Dir.Y = -Dir.Y;
        */
        public bool Collision(ICollision col) => col.Rect.IntersectsWith(this.Rect);
        public Rectangle Rect => new Rectangle(Pos, Size);
        public virtual void GenerateNewPosition(Random rnd)
        {
            Pos = new Point()
            {
                X = rnd.Next(0 + Size.Width, Game.Width - Size.Width),
                Y = rnd.Next(0 + Size.Height, Game.Height - Size.Height)
            };
        }
    }
}
