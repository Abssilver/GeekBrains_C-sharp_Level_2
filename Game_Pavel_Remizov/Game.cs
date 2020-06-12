using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Game_Pavel_Remizov
{
    //Сделать проверку на задание размера экрана в классе Game.
    //Если высота или ширина(Width, Height) больше 1000 или принимает отрицательное значение,
    //выбросить исключение ArgumentOutOfRangeException().
    static class Game
    {
        private static string filepath = "gameLog.txt";
        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;
        private static Timer _timer = new Timer { Interval = 25 };
        private static Random _rnd = new Random();

        private static BaseObject[] _objs;
        //private static Bullet _bullet;
        private static List<Bullet> _bullets = new List<Bullet>();
        private static FirstAidKit _firstAidKit;
        private static List<Asteroid> _asteroids;
        private static int _width;
        private static int _height;
        private static int _level = 3;
        private static int _score = 0;

        private static Ship _ship;
        public static int Width 
        {
            get => _width;
            set
            {
                if (value > 1000 || value < 0)
                    throw new ArgumentOutOfRangeException("Width is invalid value!");
                else _width = value;
            }
        }
        public static int Height 
        {
            get => _height;
            set
            {
                if (value > 1000 || value < 0)
                    throw new ArgumentOutOfRangeException("Height is invalid value!");
                else _height = value;
            }
        }

        static Game()
        { 
        }
        private static void Load()
        {
            try
            {
                _ship = new Ship(new Point(10, 10), 
                                 new Point(5, 5), 
                                 new Size(50, 20), 
                                 Image.FromFile($"..//..//Resourses//rect52_spaceship_{_rnd.Next(1, 3)}.png"));

                _ship.DisplayNotification += DisplayMessage;
                _ship.DisplayNotification += SaveLogToFile;

                _objs = new BaseObject[90];
                //_bullet = new Bullet(new Point(0, 200), new Point(5, 0), new Size(4, 1));
                //_asteroids = new Asteroid[3];

                //Звезды - 45шт.
                for (int i = 0; i < _objs.Length / 2; i++) 
                {
                    int r = _rnd.Next(5, 50);
                    _objs[i] = new Star(new Point(100, _rnd.Next(0, Height)), new Point(-r, r), new Size(3, 3));
                }
                //Пульсирующие звезды. Жетлый цвет - 30шт
                for (int i = _objs.Length / 2; i < 15 * _objs.Length / 18; i++) 
                {
                    int pulsarSize = _rnd.Next(2, 5);
                    _objs[i] = new CustomPulsarStaticObject(new Point(_rnd.Next(0, Width), _rnd.Next(0, Height)),
                                                            new Point(_rnd.Next(-5, -2), 0),
                                                            new Size(pulsarSize, pulsarSize), _rnd,
                                                            Brushes.Yellow);
                }
                //НЛО - 3 шт.
                for (int i = 15 * _objs.Length / 18; i < 13 * _objs.Length / 15; i++)
                {
                    int speed = 0;
                    while (speed.Equals(0))
                        speed = _rnd.Next(-7, 7);
                    _objs[i] = new UFO(new Point(_rnd.Next(Width / 5, Width), _rnd.Next(0, Height)),
                                       new Point(speed, speed),
                                       new Size(25, 25),
                                       Image.FromFile($"..//..//Resourses//box_ufo_{_rnd.Next(1, 6)}.png"));
                }
                //Объекты - 2шт.
                for (int i = 13 * _objs.Length / 15; i < 8 * _objs.Length / 9; i++)
                {
                    int speed = _rnd.Next(-7, -3);
                    _objs[i] = new Star(new Point(_rnd.Next(Width / 5, Width), _rnd.Next(0, Height)),
                             new Point(speed, 0),
                             new Size(30, 30),
                             Image.FromFile($"..//..//Resourses//box_object_{_rnd.Next(1, 5)}.png"));
                }
                //Планеты - 3шт.
                for (int i = 8 * _objs.Length / 9; i < 83 * _objs.Length / 90; i++)
                {
                    int imageSize = _rnd.Next(35, 50);
                    _objs[i] = 
                        new Star(new Point(_rnd.Next(0, Width / 2), _rnd.Next(imageSize, Height - imageSize)),
                        new Point(-1, 0),
                        new Size(imageSize, imageSize),
                        Image.FromFile($"..//..//Resourses//box_planet_{_rnd.Next(1, 14)}.png"));
                }
                //Прямоугольные планеты - 2шт.
                for (int i = 83 * _objs.Length / 90; i < 17 * _objs.Length / 18; i++)
                {
                    int imageSize = _rnd.Next(35, 50);
                    _objs[i] =
                        new Star(new Point(_rnd.Next(Width / 2, Width), _rnd.Next(imageSize, Height - imageSize)),
                                 new Point(-1, 0),
                                 new Size(3 * imageSize, 2 * imageSize),
                                 Image.FromFile($"..//..//Resourses//rect32_planet_{_rnd.Next(1, 8)}.png"));
                }
                //Кометы - 5 шт.
                for (int i = 17 * _objs.Length / 18; i < _objs.Length; i++)
                {
                    int speed = _rnd.Next(2, 9);
                    _objs[i] =
                    new Star(new Point(_rnd.Next(0, Width), _rnd.Next(Height / 4, 3 * Height / 4)),//класс сыроват
                             new Point(-speed, 0),
                             new Size(30, 12),
                             Image.FromFile($"..//..//Resourses//rect52_comet_{_rnd.Next(1, 6)}.png"));
                }

                //Астероиды - 3 шт.
                GenerateAsteroids(_level++);
            }
            catch (GameObjectPositionException ex)
            {
                MessageBox.Show($"Invalid value: {ex.Position}", $"Error: {ex.Message}"); 
            }
            catch (GameObjectSpeedException ex)
            {
                MessageBox.Show($"Invalid value: {ex.Speed}", $"Error: {ex.Message}");
            }
            catch (GameObjectSizeException ex)
            {
                MessageBox.Show($"Invalid value: {ex.Size}", $"Error: {ex.Message}");
            }
        }
        private static void GenerateAsteroids(int quantity)
        {
            _asteroids = new List<Asteroid>(quantity);
            for (int i = 0; i < quantity; i++)
            {
                int r = _rnd.Next(29, 30);
                _asteroids.Add
                    (new Asteroid(new Point(_rnd.Next(Width / 2, Width), _rnd.Next(0, Height)), new Point(-r / 5, r), new Size(r, r),
                     Image.FromFile($"..//..//Resourses//box_asteroid_{_rnd.Next(1, 5)}.png")));

                _asteroids[i].DisplayNotification += DisplayMessage;
                _asteroids[i].DisplayNotification += SaveLogToFile;
            }
        }
        public static void Init(Form form)
        {
            try
            {
                Graphics g;
                g = form.CreateGraphics();
                Width = form.ClientSize.Width;
                Height = form.ClientSize.Height;

                _context = BufferedGraphicsManager.Current;
                Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));
                Load();
                FormSetup(form);

                using (FileStream stream = File.Create(filepath))
                {
                    byte[] gameInfo = new UTF8Encoding(true).GetBytes($"This is game log of {DateTime.Now}\n");
                    stream.Write(gameInfo, 0, gameInfo.Length);
                }

                _timer.Start();
                _timer.Tick += Timer_Tick;

                form.KeyDown += Form_KeyDown;
                Ship.MessageDie += Finish;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show(ex.Message, "Error!");
            }
        }
        public static void Finish()
        {
            _timer.Stop();
            Buffer.Graphics.DrawString
                ("The End", new Font(FontFamily.GenericSansSerif, 60, FontStyle.Underline), Brushes.White, 200, 100);
            Buffer.Render();
        }
        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey)
                _bullets.Add(new Bullet
                    (new Point(_ship.Rect.X + _ship.shipSizeX / 2, _ship.Rect.Y + _ship.shipSizeY / 2),
                     new Point(10, 0), new Size(4, 1)));
            if (e.KeyCode == Keys.Up) _ship.Up();
            if (e.KeyCode == Keys.Down) _ship.Down();
        }
        private static void FormSetup(Form form)
        {
            form.Icon = new Icon($"..//..//Resourses//gameIcon.ico");
            form.Text = "Spaceship Shooter v0.01";
        }
        private static void DisplayMessage(string message) => Console.WriteLine(message);
        private static void SaveLogToFile(string message)
        {
            if (File.Exists(filepath))
            {
                using (StreamWriter writer = File.AppendText(filepath))
                    writer.WriteLine(message);
            }
        }
        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }
        private static void Draw()
        {
            Buffer.Graphics.Clear(Color.FromArgb(19, 21, 36));
            foreach (BaseObject obj in _objs)
                obj.Draw();
            foreach (Asteroid obj in _asteroids)
                obj?.Draw();
            foreach (Bullet bullet in _bullets)
                bullet?.Draw();
            _firstAidKit?.Draw();
            _ship?.Draw();
            if (_ship != null)
            {
                Buffer.Graphics.DrawString
                    ("Energy: " + _ship.Energy, 
                     new Font(FontFamily.GenericMonospace , 15), Brushes.Yellow, Width - 160, 5);
                Buffer.Graphics.DrawString
                    ("Score: " + _score, 
                     new Font(FontFamily.GenericMonospace, 15), Brushes.Yellow, Width - 160, 50);
            }
            Buffer.Render();
        }
        public static void Update()
        {
            foreach (BaseObject obj in _objs)
                obj?.Update();
            foreach (Bullet bullet in _bullets)
                bullet?.Update();
            _firstAidKit?.Update();
            for (int i = 0; i < _asteroids.Count; i++)
            {
                //if (_asteroids[i] == null) continue;
                _asteroids[i].Update();
                for (int j = 0; j < _bullets.Count; j++)
                {
                    if (_bullets[j].OutOfBounds)
                    {
                        _bullets.RemoveAt(j);
                        j--;
                        continue;
                    }
                    if (i >= 0 && _bullets[j].Collision(_asteroids[i]))
                    {
                        System.Media.SystemSounds.Hand.Play();
                        _firstAidKit =
                            new FirstAidKit(new Point(_asteroids[i].Position.X, _asteroids[i].Position.Y),
                                            new Point(-3, 0),
                                            new Size(20, 20),
                                            Image.FromFile($"..//..//Resourses//box_energy_1.png"));
                        //_asteroids[i] = null;
                        _asteroids.RemoveAt(i--);
                        AddScoreForAsteroid();
                        //_asteroids[i].GenerateNewPosition(_rnd);
                        _bullets.RemoveAt(j--);
                    }
                }
                if (i < 0 || !_ship.Collision(_asteroids[i])) continue;
                _ship?.EnergyLow(_rnd.Next(5, 20));
                System.Media.SystemSounds.Asterisk.Play();
                //_asteroids[i].GenerateNewPosition(_rnd);
                if (_ship.Energy <= 0) _ship?.Die();
            }
            if (_firstAidKit != null && _ship.Collision(_firstAidKit))
            {
                _firstAidKit = null;
                _ship?.EnergyUp(_rnd.Next(5, 20));
            }
            if (_asteroids.Count == 0) GenerateAsteroids(_level++);
        }
        private static void AddScoreForAsteroid()
        {
            int scoreForAsteroid = _rnd.Next(5, 11);
            _score += scoreForAsteroid;
            string msg = string.Format
                ("{0} - Player received {1} points for the destroyed asteroid! Total score: {2}",
                 DateTime.Now.ToLongTimeString(), scoreForAsteroid, _score);
            DisplayMessage(msg);
            SaveLogToFile(msg);
        }
    }
}
