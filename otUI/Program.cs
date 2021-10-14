using System;
using Gtk;

namespace otUI
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Application.Init();
            MainWindow win = MainWindow.Create();
            win.Show();
            Application.Run();
        }
    }
}
