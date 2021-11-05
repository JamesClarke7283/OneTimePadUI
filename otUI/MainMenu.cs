using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;
using otlib;

public partial class MainMenu : Gtk.Window
{
    Builder builder;
    private AppSettings appSettings;

    public static MainMenu Create(AppSettings appSettings)
    {
        Builder builder = new Builder(null, "otUI.interfaces.MainWindow.glade", null);
        return new MainMenu(builder, builder.GetObject("mainmenu").Handle, appSettings);
    }

    protected MainMenu(Builder builder, IntPtr handle, AppSettings appSettings) : base(handle)
    {
        this.builder = builder;
        this.appSettings = appSettings;

        builder.Autoconnect(this);
        DeleteEvent += OnDeleteEvent;
    }

    protected void OnKeygenClicked(object sender, EventArgs e)
    {
        GenerateKeys gk = GenerateKeys.Create(appSettings);
        gk.Run();
        gk.Destroy();
    }

    protected void OnCryptoClicked(object sender, EventArgs e)
    {
        CryptDialog cd = CryptDialog.Create(appSettings);
        cd.Run();
        cd.Destroy();
    }

    protected void On_SettingsButton_clicked(object sender, EventArgs e)
    {
        SettingsDialog sd = SettingsDialog.Create(appSettings);
        sd.Run();
        sd.Destroy();
    }

    protected void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        Application.Quit();
        a.RetVal = true;
    }
}
