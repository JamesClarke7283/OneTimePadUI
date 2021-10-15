using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

//[UI] Gtk.TextView tv = new Gtk.TextView();

public class GenerateKeys : Gtk.Dialog
{
    Builder builder;

    public static GenerateKeys Create()
    {
        Builder builder = new Builder(null, "otUI.interfaces.GenerateKeys.glade", null);
        return new GenerateKeys(builder, builder.GetObject("generatekeysdialog").Handle);
    }

    protected GenerateKeys(Builder builder, IntPtr handle) : base(handle)
    {
        this.builder = builder;

        builder.Autoconnect(this);
    }

}
