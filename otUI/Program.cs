using Gtk;
using otlib;

namespace otUI
{
    class MainClass
    {
        public static AppSettings appSettings;

        public static void Main(string[] args)
        {
            appSettings = AppSettings.Read(Constants.CONFIG_PATH);

            Application.Init();
            MainMenu win = MainMenu.Create(appSettings);
            win.Show();
            Application.Run();
        }
    }
}
