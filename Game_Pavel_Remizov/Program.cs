﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game_Pavel_Remizov
{
    class Program
    {
        static void Main(string[] args)
        {

            Form form = new Form()
            {
                Width = 1000, //Screen.PrimaryScreen.Bounds.Width,
                Height = Screen.PrimaryScreen.Bounds.Height
            };
            Game.Init(form);
            form.Show();
            Application.Run(form);

            //form.Width = 800;
            //form.Height = 600; 
        }
    }
}
