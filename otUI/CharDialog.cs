using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;
using otlib;
public class CharDialog : Gtk.Dialog
{
    [UI] Gtk.RadioButton UpperLowerNum;
    [UI] Gtk.RadioButton UpperNum;
    [UI] Gtk.RadioButton Numerical;
    [UI] Gtk.Entry Custom = new Gtk.Entry();

    Builder builder;

    public string charset;

    public static CharDialog Create()
    {
        Builder builder = new Builder(null, "otUI.interfaces.CharsetDialog.glade", null);
        return new CharDialog(builder, builder.GetObject("charsetdialog").Handle);
    }

    protected CharDialog(Builder builder, IntPtr handle) : base(handle)
    {
        this.builder = builder;

        builder.Autoconnect(this);
        AddButton("Close", ResponseType.Close);
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

    protected void On_Save_clicked(object sender, EventArgs e)
    {
        otlib.Settings.charset = charset;
    }
}
