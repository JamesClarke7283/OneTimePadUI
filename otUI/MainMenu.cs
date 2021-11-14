using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;
using otlib;
using Gdk;

public partial class MainMenu : Gtk.Window
{
    Builder builder;
    private AppSettings appSettings;


    public static Gdk.Pixbuf SharedAppIcon = new Gdk.Pixbuf(null,"otUI.assets.lock.ico");
    public static Gdk.Pixbuf GenKeyIcon = new Gdk.Pixbuf(null, "otUI.assets.key.ico");



    public static Gtk.Image GenKeyImg = new Gtk.Image(pixbuf: GenKeyIcon);

    [UI] Gtk.Button keygen = new Gtk.Button();
    [UI] Gtk.Button cryptodecrypt = new Gtk.Button();
    [UI] Gtk.Button SettingsButton = new Gtk.Button();



    public static MainMenu Create(AppSettings appSettings)
    {
        Builder builder = new Builder(null, "otUI.interfaces.MainWindow.glade", null);
        return new MainMenu(builder, builder.GetObject("mainmenu").Handle, appSettings);
        
    }

    protected MainMenu(Builder builder, IntPtr handle, AppSettings appSettings) : base(handle)
    {
        this.builder = builder;
        this.appSettings = appSettings;
        Icon = MainMenu.SharedAppIcon;

        keygen.Image = GenKeyImg;

        builder.Autoconnect(this);
        DeleteEvent += OnDeleteEvent;
        ThemeLoader.LoadTheme(appSettings);
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
