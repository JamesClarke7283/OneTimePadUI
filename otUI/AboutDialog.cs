using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;


public class AboutDialog : Gtk.AboutDialog
{
    Builder builder;

    public static AboutDialog Create()
    {

        Builder builder = new Builder(null, "otUI.interfaces.AboutDialog.glade", null);
        return new AboutDialog(builder, builder.GetObject("aboutdialog").Handle);
    }

    protected AboutDialog(Builder builder, IntPtr handle) : base(handle)
    {
        
        this.builder = builder;

        builder.Autoconnect(this);
        AddButton("Close", ResponseType.Close);

    }

    
}

