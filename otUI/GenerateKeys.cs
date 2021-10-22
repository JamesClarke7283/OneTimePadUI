using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;
using otlib;

public class GenerateKeys : Gtk.Dialog
{
    [UI] Gtk.TextView KeyOutputView = new Gtk.TextView();
    [UI] Gtk.SpinButton keyLength;

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
        AddButton("Close", ResponseType.Close);
    }

    protected void OnGenerateClicked(object sender, EventArgs e)
    {

        otlib.PrettyPrint pp = new otlib.PrettyPrint();
        byte[] keystream = otp.GenerateKeystream((int)keyLength.Value);
        KeyOutputView.Buffer.Text = pp.Prettify(otp.ToString(keystream, otlib.Settings.charset));

    }

    protected void OnCharsetClicked(object sender, EventArgs e)
    {

    }

    protected void OnHelpClicked(object sender, EventArgs e)
    {

    }

}
