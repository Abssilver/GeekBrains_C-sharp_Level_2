using System;
using System.Drawing;

namespace Game_Pavel_Remizov
{
    //Переделать виртуальный метод Update в BaseObject в абстрактный и реализовать его в наследниках.
    /*
    *Создать собственное исключение GameObjectException, которое появляется 
    при попытке создать объект с неправильными характеристиками
    (например, отрицательные размеры, слишком большая скорость или позиция).
    */
    abstract class BaseObject : ICollision
    {
        //Можно попробовать закинуть проверку в свойства,
        //но придется поменять структуры на классы, либо менять логику в классах-потомка BaseObject-а
        protected Point Pos;
        protected Point Dir;
        protected Size Size;
        private double _maxSpeed = 100;

        protected BaseObject(Point pos, Point dir, Size size)
        {
            if (pos.X > Game.Width + size.Width * 3 || pos.X < -size.Width * 3 ||
                pos.Y > Game.Height + size.Height * 3 || pos.Y < -size.Height * 3)
                throw new GameObjectPositionException("Invalid object position!", pos);
            else Pos = pos;


            double speed = Math.Sqrt(Math.Pow(dir.X, 2) + Math.Pow(dir.Y, 2));
            if (_maxSpeed < speed)
                throw new GameObjectSpeedException("Invalid object speed!", speed);
            else Dir = dir;


            if (size.Width < 0 || size.Height < 0)
                throw new GameObjectSizeException("Invalid object size!", size);
            else Size = size;
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
