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
        byte[] keystream = {};
        if (otlib.Settings.rngDevicePath == null)
        {
            keystream = otp.GenerateKeystream((int)keyLength.Value);
        }
        else 
        {
            keystream = otp.GenerateKeystreamRNGDevice(otlib.Settings.rngDevicePath, (int)keyLength.Value); 
        }
        KeyOutputView.Buffer.Text = pp.Prettify(otp.ToString(keystream, otlib.Settings.codeCharset));

    }

    protected void OnHelpClicked(object sender, EventArgs e)
    {
       
        HelpDialog hd = HelpDialog.Create(otUI.HelpConst.GenerateKeysHelp);
        hd.Run();
        hd.Destroy();

    }

}
