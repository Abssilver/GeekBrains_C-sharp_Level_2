using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Game_Pavel_Remizov
{
    static class Game
    {
        private static BufferedGraphicsContext _context;

        public static BufferedGraphics Buffer;

        private static BaseObject[] _objs;
        public static int Width { get; set; }
        public static int Height { get; set; }

        static Game()
        { 
        }
        private static void Load()
        {
            Random rnd = new Random();
            _objs = new BaseObject[30];
            for (int i = 0; i < _objs.Length; i+=10)
            {
                int imageSize = rnd.Next(4, 6);
                _objs[i] =
                    new CustomPulsarStaticObject(new Point(rnd.Next(0, Width), rnd.Next(0, Height)),
                                                 new Point(-1, 0),
                                                 new Size(imageSize, imageSize), rnd,
                                                 Brushes.Yellow);
                _objs[i + 1] =
                    new CustomPulsarStaticObject(new Point(rnd.Next(0, Width), rnd.Next(0, Height)),
                                                 new Point(-1, 0),
                                                 new Size(imageSize, imageSize), rnd,
                                                 Brushes.Gray);
                _objs[i + 2] =
                    new CustomPulsarStaticObject(new Point(rnd.Next(0, Width), rnd.Next(0, Height)),
                                                 new Point(-1, 0),
                                                 new Size(imageSize, imageSize), rnd,
                                                 Brushes.LightBlue);
                _objs[i + 3] =
                    new Star(new Point(rnd.Next(Width / 5, Width), rnd.Next(0, Height)),
                             new Point(-2, 0),
                             new Size(imageSize * 2, imageSize * 2),
                             Image.FromFile($"..//..//Resourses//box_asteroid_{rnd.Next(1, 5)}.png"));
                _objs[i + 4] =
                    new Star(new Point(rnd.Next(Width / 5, Width), rnd.Next(0, Height)),
                             new Point(-3, 0),
                             new Size(imageSize * 3, imageSize * 3),
                             Image.FromFile($"..//..//Resourses//box_asteroid_{rnd.Next(1, 5)}.png"));
                _objs[i + 5] =
                    new AdvancedMovingStar(new Point(rnd.Next(0, Width), rnd.Next(0, Height / 3)),//класс сыроват
                                           new Point(1, 1), //работает только с (1,1) и (-1,-1)
                                           new Size(imageSize * 5, imageSize * 2),
                                           Image.FromFile($"..//..//Resourses//rect52_comet_{rnd.Next(1, 6)}.png"));
                _objs[i + 6] =
                    new UFO(new Point(rnd.Next(Width / 5, Width), rnd.Next(0, Height)),
                                           new Point(-1, -1),
                                           new Size(imageSize * 5, imageSize * 5),
                                           Image.FromFile($"..//..//Resourses//box_ufo_{rnd.Next(1, 6)}.png"));
                _objs[i + 7] =
                    new Star(new Point(rnd.Next(Width / 5, Width), rnd.Next(0, Height)),
                             new Point(-2, 0),
                             new Size(imageSize * 5, imageSize * 5),
                             Image.FromFile($"..//..//Resourses//box_object_{rnd.Next(1, 5)}.png"));
                _objs[i + 8] =
                    new Star(new Point(rnd.Next(0, Width / 2), rnd.Next(imageSize * 15, Height - imageSize * 15)),
                             new Point(-1, 0),
                             new Size(imageSize * 15, imageSize * 15),
                             Image.FromFile($"..//..//Resourses//box_planet_{rnd.Next(1, 14)}.png"));
                _objs[i + 9] =
                    new Star(new Point(rnd.Next(Width / 2, Width), rnd.Next(imageSize * 10, Height - imageSize * 10)),
                             new Point(-1, 0),
                             new Size(imageSize * 15, imageSize * 10),
                             Image.FromFile($"..//..//Resourses//rect32_planet_{rnd.Next(1, 8)}.png"));
            }
        }
        public static void Init(Form form)
        {
            Timer timer = new Timer { Interval = 25 };
            timer.Start();
            timer.Tick += Timer_Tick;
            Graphics g;
            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();
            Width = form.ClientSize.Width;
            Height = form.ClientSize.Height;
            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));
            Load();
            FormSetup(form);
        }
        private static void FormSetup(Form form)
        {
            form.Icon = new Icon($"..//..//Resourses//gameIcon.ico");
            form.Text = "Spaceship Shooter v0.01";
        }
        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }
        public static void Draw()
        {
            /*
            Buffer.Graphics.Clear(Color.Black);
            Buffer.Graphics.DrawRectangle(Pens.White, new Rectangle(100, 100, 200, 200));
            Buffer.Graphics.FillRectangle(Brushes.Wheat, new Rectangle(100, 100, 200, 200));
            Buffer.Render();
            */
            Buffer.Graphics.Clear(Color.FromArgb(19, 21, 36));
            foreach (BaseObject obj in _objs)
                obj.Draw();
            Buffer.Render();
        }
        public static void Update()
        {
            foreach (BaseObject obj in _objs)
                obj.Update();
            {

            }
        }
    }
}
