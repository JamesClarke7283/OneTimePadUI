using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;


public class HelpDialog : Gtk.Dialog
{
    Builder builder;

    [UI] Gtk.TextView HelpText = new Gtk.TextView();

   

    public static HelpDialog Create(string GenerateKeysHelp)
    {
        
       
        Builder builder = new Builder(null, "otUI.interfaces.HelpDialog.glade", null);
        return new HelpDialog(builder, builder.GetObject("helpdialog").Handle,GenerateKeysHelp);
    }

    protected HelpDialog(Builder builder, IntPtr handle, string text) : base(handle)
    {
        
        this.builder = builder;

        builder.Autoconnect(this);
        AddButton("Close", ResponseType.Close);

        HelpText.Buffer.Text = text;
    }

    
}

