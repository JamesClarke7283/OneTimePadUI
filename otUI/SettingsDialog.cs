using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;
using otlib;

public class SettingsDialog : Gtk.Dialog
{
    //[UI] Gtk.Button CharsetButton = new Gtk.Button();

    Builder builder;

    public static SettingsDialog Create()
    {
        Builder builder = new Builder(null, "otUI.interfaces.SettingsDialog.glade", null);
        return new SettingsDialog(builder, builder.GetObject("settingsdialog").Handle);
    }

    protected SettingsDialog(Builder builder, IntPtr handle) : base(handle)
    {
        this.builder = builder;

        builder.Autoconnect(this);
        AddButton("Close", ResponseType.Close);
    }

    protected void On_CharsetButton_clicked(object sender, EventArgs e)
    {
        CharDialog cd = CharDialog.Create();
        cd.Run();
        cd.Destroy();
    }


}
