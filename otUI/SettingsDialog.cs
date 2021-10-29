using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;
using otlib;

public class SettingsDialog : Gtk.Dialog
{
    //[UI] Gtk.Button CharsetButton = new Gtk.Button();

    Builder builder;

    public string charset;

    [UI] Gtk.RadioButton UpperLowerNum;
    [UI] Gtk.RadioButton UpperNum;
    [UI] Gtk.RadioButton Numerical;
    [UI] Gtk.Entry Custom = new Gtk.Entry();

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

    protected void On_Save_clicked(object sender, EventArgs e)
    {
        otlib.Settings.charset = charset;
    }

    protected void On_Custom_changed(object sender, EventArgs e)
    {
        charset = Custom.Text;
    }

    protected void On_UpperLowerNum_clicked(object sender, EventArgs e)
    {
        charset = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    }

    protected void On_UpperNum_clicked(object sender, EventArgs e)
    {
        charset = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    }

    protected void On_Numerical_clicked(object sender, EventArgs e)
    {
        charset = "0123456789";
    }


}
