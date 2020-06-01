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
        public static void Load()
        {
            //Random rnd = new Random();
            _objs = new BaseObject[30];
            for (int i = 0; i < _objs.Length / 2; i++)
                _objs[i] = new BaseObject(new Point(600, i * 20), new Point(-i, -i), new Size(5, 5));
            for (int i = _objs.Length / 2 ; i < _objs.Length ; i++)
                _objs[i] = new Star(new Point(600, i * 20), new Point(-i, 0), new Size(5,5));
             //   _objs[i] = new BaseObject(new Point(600, i * 20), 
             //                             new Point(rnd.Next(-i, i), rnd.Next(-i, i)), 
             //                             new Size(i*2, i*2));
        }
        public static void Init(Form form)
        {
            Timer timer = new Timer { Interval = 100 };
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
            Buffer.Graphics.Clear(Color.Black);
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
