using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game_Pavel_Remizov
{
    //Тестовый класс, он достаточно сыроват. Хотел попробовать сделать рисунок наклоненным внутренними средствами
    class AdvancedMovingStar : Star
    {
        private float _angle;
        private float _distance;
        //private float _step;
        private List<float> defaultAngles = new List<float>(){ 0f, 90f, -90f, 180f };
        public AdvancedMovingStar(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }
        public AdvancedMovingStar(Point pos, Point dir, Size aspectRatio, Image image) : base(pos, dir, aspectRatio)
        {
            base.image = image;
            _angle = dir.X == 0 ? dir.Y > 0 ? 90f : 
                                  dir.Y == 0 ? 0f: -90f : 
                     dir.Y == 0 ? dir.X > 0 ? 0f : 180f:
                     Convert.ToSingle(Math.Atan((double)dir.Y / dir.X) * 180 / Math.PI);
            //_step = Convert.ToInt32(pos.X * Math.Cos(_angle * Math.PI / 180));
            if (!defaultAngles.Contains(_angle))
            {
                Pos.X = Convert.ToInt32(Pos.X / Math.Cos(_angle * Math.PI / 180));
                _distance = Convert.ToInt32(Game.Height / Math.Sin(_angle * Math.PI / 180));
            }
            //_objectPosition = pos;
        }
        public override void Draw()
        {
            if (image is null)
            {
                base.Draw();
            }
            else
            {
                Game.Buffer.Graphics.RotateTransform(_angle);
                Game.Buffer.Graphics.DrawImage(image, new Rectangle(Pos, base.Size));
                Game.Buffer.Graphics.RotateTransform(-_angle);
            }
        }
        public override void Update()
        {
            if (!defaultAngles.Contains(_angle))
            {
                Pos.X += Dir.X;
                if (Pos.X + Size.Width < 0)
                {
                    Pos.X = (int)_distance + Size.Width;
                }
                else if (Pos.X > (int)_distance + Size.Width)
                {
                    Pos.X = -Size.Width;
                }
            }
            else
                base.Update();
        }

    }
}
