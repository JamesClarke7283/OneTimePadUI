using System;
using Gtk;
using otlib;
using UI = Gtk.Builder.ObjectAttribute;
using otUI;

//[UI] Gtk.TextView tv = new Gtk.TextView();

public class CryptDialog : Gtk.Dialog
{
    Builder builder;
    private AppSettings appSettings;

    [UI] Gtk.TextView keyInput = new Gtk.TextView();
    [UI] Gtk.TextView msgCipherInput = new Gtk.TextView();
    [UI] Gtk.TextView output = new Gtk.TextView();


    public static CryptDialog Create(AppSettings appSettings)
    {
        Builder builder = new Builder(null, "otUI.interfaces.Crypt.ui", null);
        return new CryptDialog(builder, builder.GetObject("cryptdialog").Handle, appSettings);
    }


    protected CryptDialog(Builder builder, IntPtr handle, AppSettings appSettings) : base(handle)
    {
        this.builder = builder;
        this.appSettings = appSettings;

        builder.Autoconnect(this);
        AddButton("Close", ResponseType.Close);
    }

    protected void OnEncryptClicked(object sender, EventArgs e)
    {
        try
        {
            PrettyPrint pp = new PrettyPrint();
            byte[] ks = otp.ToBytes(pp.UnPrettify(keyInput.Buffer.Text), appSettings.CodeCharSetString);
            string msg = msgCipherInput.Buffer.Text;

            byte[] ciphertext = otp.Encrypt(msg, ks, appSettings.CodeCharSetString, appSettings.TextCharSetString);

            if (appSettings.hasPrettyPrint)
            {
                output.Buffer.Text = pp.Prettify(otp.ToString(ciphertext, appSettings.CodeCharSetString));
            }
            else 
            {
                output.Buffer.Text = otp.ToString(ciphertext, appSettings.CodeCharSetString);
            }
        }
        catch (System.Collections.Generic.KeyNotFoundException error)
        {
            ErrorDialog.ShowAlert(this, "Message contains character that is not in textCode characterset");
        }
        catch (Exception error2) 
        {
            ErrorDialog.ShowAlert(this, error2.Message);
        }
    }

    protected void OnDecryptClicked(object sender, EventArgs e)
    {
        PrettyPrint pp = new PrettyPrint();
        byte[] ks = otp.ToBytes(pp.UnPrettify(keyInput.Buffer.Text), appSettings.CodeCharSetString);
        string ciphertext = pp.UnPrettify(msgCipherInput.Buffer.Text);

        byte[] msg = otp.Decrypt(ciphertext, ks, appSettings.CodeCharSetString, appSettings.TextCharSetString);
        output.Buffer.Text = otp.ToString(msg, appSettings.TextCharSetString);
    }

    protected void OnHelpClicked(object sender, EventArgs e)
    {
        HelpDialog hd = HelpDialog.Create(otUI.HelpConst.CryptKeysHelp);
        hd.Run();
        hd.Destroy();
    }

    protected void OnCopyClicked(object sender, EventArgs e)
    {
        Gtk.Clipboard clipboard = Gtk.Clipboard.Get(Gdk.Atom.Intern("CLIPBOARD", false));
        clipboard.Text = output.Buffer.Text;
    }
}
