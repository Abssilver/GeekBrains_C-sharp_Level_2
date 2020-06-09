using System;
using System.Drawing;

namespace Game_Pavel_Remizov
{
    class Asteroid: BaseObject, ICloneable, IComparable<Asteroid>
    {
        public int Power { get; set; } = 3;
        private Image _image;
        public Point Position => Pos;
        public event DisplayMsg DisplayNotification;
        public Asteroid(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            //Power = 1;
        }
        public Asteroid(Point pos, Point dir, Size size, Image image) : base(pos, dir, size)
        {
            _image = image;
        }
        public object Clone()
        {
            Asteroid asteroid =
                new Asteroid(new Point(Pos.X, Pos.Y), new Point(Dir.X, Dir.Y), new Size(Size.Width, Size.Height))
                { Power = Power };
            return asteroid;
        }
        int IComparable<Asteroid>.CompareTo(Asteroid obj)
        {
            //if (obj is Asteroid temp)
            //    return Power.CompareTo(temp.Power);
            return Power.CompareTo(obj.Power);
            //throw new ArgumentException("Parameter is not an Asteroid!");
        }
        public override void Draw()
        {
            if (_image is null)
            {
                Game.Buffer.Graphics.FillEllipse
                (Brushes.White, base.Pos.X, base.Pos.Y, base.Size.Width, base.Size.Height);
            }
            else
                Game.Buffer.Graphics.DrawImage(_image, new Rectangle(base.Pos, base.Size));
        }
        public override void Update()
        {
            base.Pos.X += base.Dir.X;
            base.Pos.Y += base.Dir.Y;
            if (base.Pos.X < 0 || base.Pos.X > Game.Width) base.Dir.X = -base.Dir.X;
            if (base.Pos.Y < 0 || base.Pos.Y > Game.Height) base.Dir.Y = -base.Dir.Y;
        }
        public override void GenerateNewPosition(Random rnd)
        {
            DestroyAsteroid();
            base.Pos = new Point
            (Game.Width - base.Size.Width,
             rnd.Next(Game.Height / 3 + base.Size.Height, Game.Height * 2 / 3 - base.Size.Height));
        }
        private void DestroyAsteroid() => 
            DisplayNotification?.Invoke($"{DateTime.Now.ToLongTimeString()} - Asteroid has been destroyed!");
        
    }
}
