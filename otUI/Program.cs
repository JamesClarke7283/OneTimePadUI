using System;
using Gtk;
using otlib;

namespace otUI
{
    class MainClass
    {

        public static void Main(string[] args)
        {
            Application.Init();
            MainMenu win = MainMenu.Create();
            win.Show();
            Application.Run();
        }
    }
}
