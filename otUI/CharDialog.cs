using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;
using otlib;
public class CharDialog : Gtk.Dialog
{

    Builder builder;

    public static CharDialog Create()
    {
        Builder builder = new Builder(null, "otUI.interfaces.CharSet.glade", null);
        return new CharDialog(builder, builder.GetObject("chardialog").Handle);
    }

    protected CharDialog(Builder builder, IntPtr handle) : base(handle)
    {
        this.builder = builder;

        builder.Autoconnect(this);
        //AddButton("Close", ResponseType.Close);
    }

    protected void on_Save_clicked(object sender, EventArgs e)
    {

    }

    protected void on_UpperLowerNum_clicked(object sender, EventArgs e)
    {

    }
    protected void on_UpperNum_clicked(object sender, EventArgs e)
    {

    }
    protected void on_Numerical_clicked(object sender, EventArgs e)
    {

    }
}
