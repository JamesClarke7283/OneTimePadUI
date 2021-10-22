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
        otp o = new otp();
        byte[] ks =  otp.ToBytes(keyInput.Buffer.Text);
        string msg = msgCipherInput.Buffer.Text;

        byte[] ciphertext = o.Encrypt(msg, ks);
        output.Buffer.Text = otp.ToString(ciphertext);
    }

    protected void OnDecryptClicked(object sender, EventArgs e)
    {
        otp o = new otp();
        byte[] ks = otp.ToBytes(keyInput.Buffer.Text);
        string ciphertext = msgCipherInput.Buffer.Text;

        byte[] msg = o.Decrypt(ciphertext, ks);
        output.Buffer.Text = otp.ToString(msg);
    }

    protected void OnCharsetClicked(object sender, EventArgs e)
    {

    }

    protected void OnHelpClicked(object sender, EventArgs e)
    {

    }

}
