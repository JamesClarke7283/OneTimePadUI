using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;


public class GenHelpDialog : Gtk.Dialog
{
    Builder builder;

    [UI] Gtk.TextView KeyGenHelpText = new Gtk.TextView();

    public static GenHelpDialog Create()
    {
        Builder builder = new Builder(null, "otUI.interfaces.GenHelp.glade", null);
        return new GenHelpDialog(builder, builder.GetObject("genhelpdialog").Handle);
    }

    protected GenHelpDialog(Builder builder, IntPtr handle) : base(handle)
    {
        this.builder = builder;

        builder.Autoconnect(this);
        AddButton("Close", ResponseType.Close);
    }

    
}

