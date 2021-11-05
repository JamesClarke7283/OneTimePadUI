using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;
using otlib;
using otUI;

public class GenerateKeys : Gtk.Dialog
{
    private AppSettings appSettings;
    [UI] Gtk.TextView KeyOutputView = new Gtk.TextView();
    [UI] Gtk.SpinButton keyLength;

    Builder builder;

    public static GenerateKeys Create(AppSettings appSettings)
    {
        Builder builder = new Builder(null, "otUI.interfaces.GenerateKeys.glade", null);
        return new GenerateKeys(builder, builder.GetObject("generatekeysdialog").Handle, appSettings);
    }

    protected GenerateKeys(Builder builder, IntPtr handle, AppSettings appSettings) : base(handle)
    {
        this.builder = builder;
        this.appSettings = appSettings;

        builder.Autoconnect(this);
        AddButton("Close", ResponseType.Close);
    }

    protected void OnGenerateClicked(object sender, EventArgs e)
    {
        try
        {
            PrettyPrint pp = new PrettyPrint();
            byte[] keystream = { };
            if (appSettings.RngDevicePath == null)
            {
                keystream = otp.GenerateKeystream((int)keyLength.Value);
            }
            else
            {
                keystream = otp.GenerateKeystreamRNGDevice(appSettings.RngDevicePath, (int)keyLength.Value);
            }
            KeyOutputView.Buffer.Text = pp.Prettify(otp.ToString(keystream, appSettings.CodeCharSetString));
        }
        catch (System.UnauthorizedAccessException)
        {
            ErrorDialog.ShowAlert(this, "Error: Permission Denied, cannot access this device");
        }
    }

    protected void OnHelpClicked(object sender, EventArgs e)
    {
        HelpDialog hd = HelpDialog.Create(otUI.HelpConst.GenerateKeysHelp);
        hd.Run();
        hd.Destroy();
    }
}
