using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

//[UI] Gtk.TextView tv = new Gtk.TextView();

public class CryptDialog : Gtk.Dialog
{
    Builder builder;

    public static CryptDialog Create()
    {
        Builder builder = new Builder(null, "otUI.interfaces.Crypt.ui", null);
        return new CryptDialog(builder, builder.GetObject("cryptdialog").Handle);
    }


    protected CryptDialog(Builder builder, IntPtr handle) : base(handle)
    {
        this.builder = builder;

        builder.Autoconnect(this);
        AddButton("Close", ResponseType.Close);

    }

    protected void OnEncryptClicked(object sender, EventArgs e)
    {

    }

    protected void OnDecryptClicked(object sender, EventArgs e)
    {

    }

    protected void OnCharsetClicked(object sender, EventArgs e)
    {

    }

    protected void OnHelpClicked(object sender, EventArgs e)
    {

    }

}
