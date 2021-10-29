using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;


public class HelpDialog : Gtk.Dialog
{
    Builder builder;

    [UI] Gtk.TextView HelpText = new Gtk.TextView();

    public static HelpDialog Create()
    {
        Builder builder = new Builder(null, "otUI.interfaces.HelpDialog.glade", null);
        return new HelpDialog(builder, builder.GetObject("helpdialog").Handle);
    }

    protected HelpDialog(Builder builder, IntPtr handle) : base(handle)
    {
        this.builder = builder;

        builder.Autoconnect(this);
        AddButton("Close", ResponseType.Close);
    }

    
}

