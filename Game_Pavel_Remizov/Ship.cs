using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Pavel_Remizov
{
    class Ship : BaseObject
    {
        private int _energy = 100;
        private int _maxEnergy = 150;
        public int Energy => _energy;

        private Image _image;

        public static event Message MessageDie;

        public event DisplayMsg DisplayNotification;
        public int shipSizeX => base.Size.Width;
        public int shipSizeY => base.Size.Height;
        public Ship(Point pos, Point dir, Size size) : base (pos, dir, size)
        { }
        public Ship(Point pos, Point dir, Size size, Image image) : base(pos, dir, size)
        {
            _image = image;
        }
        public override void Draw()
        {
            if (_image is null)
                Game.Buffer.Graphics.FillEllipse
                    (Brushes.Wheat, base.Pos.X, base.Pos.Y, base.Size.Width, base.Size.Height);
            else
                Game.Buffer.Graphics.DrawImage(_image, new Rectangle(base.Pos, base.Size));
        }
        public override void Update()
        {
        }
        public void Up()
        {
            if (base.Pos.Y > 0) base.Pos.Y -= base.Dir.Y;
        }
        public void Down()
        {
            if (base.Pos.Y < Game.Height) base.Pos.Y += base.Dir.Y;
        }
        public void EnergyLow(int amount)
        {
            _energy -= amount;
            DisplayNotification?.Invoke
                ($"{DateTime.Now.ToLongTimeString()} - Spaceship got {amount} damage! Remaining energy: {Energy}");
        }
        public void Die()
        {
            DisplayNotification?.Invoke($"{DateTime.Now.ToLongTimeString()} - Spaceship has been destroyed!");
            MessageDie?.Invoke();
        }
        public void EnergyUp(int amount)
        {
            if (_energy < _maxEnergy)
            {
                _energy = _energy + amount > _maxEnergy ? _maxEnergy : _energy + amount;
                DisplayNotification?.Invoke
                    ($"{DateTime.Now.ToLongTimeString()} - Spaceship has been repaired for {amount}! Remaining energy: {Energy}");
            }
        }
    }
}
