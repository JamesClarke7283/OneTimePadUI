using System;
using Gtk;
using otlib;
using UI = Gtk.Builder.ObjectAttribute;

//[UI] Gtk.TextView tv = new Gtk.TextView();

public class CryptDialog : Gtk.Dialog
{
    Builder builder;

    [UI] Gtk.TextView keyInput = new Gtk.TextView();
    [UI] Gtk.TextView msgCipherInput = new Gtk.TextView();
    [UI] Gtk.TextView output = new Gtk.TextView();


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
        otlib.PrettyPrint pp = new otlib.PrettyPrint();
        byte[] ks = otp.ToBytes(pp.UnPrettify(keyInput.Buffer.Text),otlib.Settings.charset);
        string msg = msgCipherInput.Buffer.Text;

        byte[] ciphertext = otp.Encrypt(msg, ks, otlib.Settings.charset);
        output.Buffer.Text = pp.Prettify(otp.ToString(ciphertext,otlib.Settings.charset));
    }

    protected void OnDecryptClicked(object sender, EventArgs e)
    {
        otlib.PrettyPrint pp = new otlib.PrettyPrint();
        byte[] ks = otp.ToBytes(pp.UnPrettify(keyInput.Buffer.Text), otlib.Settings.charset);
        string ciphertext = pp.UnPrettify(msgCipherInput.Buffer.Text);

        byte[] msg = otp.Decrypt(ciphertext, ks, otlib.Settings.charset);
        output.Buffer.Text = otp.ToString(msg, otlib.Settings.charset);
    }

    protected void OnHelpClicked(object sender, EventArgs e)
    {
        HelpDialog hd = HelpDialog.Create("hello");
        hd.Run();
        hd.Destroy();
    }

}
