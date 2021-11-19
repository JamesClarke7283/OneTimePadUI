using System.IO;
using System.Reflection;
using Gtk;
using otlib;

namespace onetimepadgtk
{
    class MainClass
    {
        public static AppSettings appSettings;

        public static void Main(string[] args)
        {
            // Sets current working directory to exe path so icons always load
            string executableDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            Directory.SetCurrentDirectory(executableDir);

            appSettings = AppSettings.Read(Constants.CONFIG_PATH);

            Application.Init();
            MainMenu win = MainMenu.Create(appSettings);
            win.Show();
            Application.Run();
        }
    }
}
