using Gtk;
using otlib;
public static class ThemeLoader
{
    static CssProviderPatched currentCssProvider = new();
    /// <summary>
    /// Load a gtk theme css file using the theme name provided by the app settings
    /// </summary>
    public static void LoadTheme(AppSettings appSettings)
    {
        if (appSettings.Theme == "Dark")
        {
            currentCssProvider.LoadFromResource("onetimepadgtk.themes.dark.css");
            StyleContext.AddProviderForScreen(Gdk.Screen.Default, currentCssProvider, 800);
        }
        else if (appSettings.Theme == "Light")
        {
            currentCssProvider.LoadFromResource("onetimepadgtk.themes.light.css");
            StyleContext.AddProviderForScreen(Gdk.Screen.Default, currentCssProvider, 800);
        }
        else
        {
            // Anything else use system/default gtk theme
            StyleContext.RemoveProviderForScreen(Gdk.Screen.Default, currentCssProvider);
        }
    }
}
