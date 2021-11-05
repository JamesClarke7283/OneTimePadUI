using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;
using otlib;
using System.IO;
using System.Collections.Generic;

public class SettingsDialog : Gtk.Dialog
{
    Builder builder;
    private AppSettings appSettings;
    [UI] Gtk.RadioButton codeUpperLowerNum;
    [UI] Gtk.RadioButton codeUpperNum;
    [UI] Gtk.RadioButton codeNumerical;
    [UI] Gtk.RadioButton codeCustomBtn;
    [UI] Gtk.Entry codeCustom = new Gtk.Entry();

    [UI] Gtk.RadioButton textUpperLowerNum;
    [UI] Gtk.RadioButton textUpperNum;
    [UI] Gtk.RadioButton textNumerical;
    [UI] Gtk.RadioButton textCustomBtn;
    [UI] Gtk.Entry textCustom = new Gtk.Entry();

    [UI] Gtk.ComboBoxText RNGDeviceComboBox = new Gtk.ComboBoxText();

    [UI] Gtk.ComboBoxText themeComboBox = new Gtk.ComboBoxText();


    [UI] Gtk.Button btn_help = new Gtk.Button();

    public static SettingsDialog Create(AppSettings appSettings)
    {
        Builder builder = new Builder(null, "otUI.interfaces.SettingsDialog.glade", null);
        return new SettingsDialog(builder, builder.GetObject("settingsdialog").Handle, appSettings);
    }

    protected SettingsDialog(Builder builder, IntPtr handle, AppSettings appSettings) : base(handle)
    {
        this.builder = builder;
        this.appSettings = appSettings;

        builder.Autoconnect(this);
        AddButton("Close", ResponseType.Close);

        if (OperatingSystem.IsWindows())
        {
            RNGDeviceComboBox.Sensitive = false;
        }
        else
        {
            IEnumerable<string> l = Directory.EnumerateFiles("/dev", "*", SearchOption.AllDirectories);

            foreach (string item in l)
            {
                RNGDeviceComboBox.AppendText(item);
            }
        }
        LoadConfig();
    }
    private void LoadConfig()
    {
        switch (appSettings.CodeCharSet)
        {
            case CharSetTypes.UPPER_LOWER_NUMERIC:
                codeUpperLowerNum.Click();
                break;
            case CharSetTypes.UPPER_NUMERIC:
                codeUpperNum.Click();
                break;
            case CharSetTypes.NUMERIC:
                codeNumerical.Click();
                break;
            default:
                codeCustomBtn.Click();
                break;
        }
        switch (appSettings.TextCharSet)
        {
            case CharSetTypes.UPPER_LOWER_NUMERIC:
                textUpperLowerNum.Click();
                break;
            case CharSetTypes.UPPER_NUMERIC:
                textUpperNum.Click();
                break;
            case CharSetTypes.NUMERIC:
                textNumerical.Click();
                break;
            default:
                textCustomBtn.Click();
                break;
        }
        codeCustom.Text = appSettings.CodeCharSetCustom;
        textCustom.Text = appSettings.TextCharSetCustom;
    }
    protected void On_Save_clicked(object sender, EventArgs e)
    {
        if (codeCustomBtn.Active)
        {
            appSettings.CodeCharSetCustom = codeCustom.Text;
        }

        if (textCustomBtn.Active)
        {
            appSettings.TextCharSetCustom = textCustom.Text;
        }

        if (appSettings.Theme == "Dark")
        {
            Gtk.CssProvider css_provider = new Gtk.CssProvider();
            css_provider.LoadFromPath("insertcssfilepathhere");                                  //need to find an appropriate theme/ css file to inject
            Gtk.StyleContext.AddProviderForScreen(Gdk.Screen.Default, css_provider, 800);
        }

        if (appSettings.Theme == "Light")
        {
            Gtk.CssProvider css_provider = new Gtk.CssProvider();
            css_provider.LoadFromPath("insertcssfilepathhere");                                  //need to find an appropriate theme/ css file to inject
            Gtk.StyleContext.AddProviderForScreen(Gdk.Screen.Default, css_provider, 800);
        }



        appSettings.Write();
        Destroy();
    }

    // Code charset events
    protected void On_codeCustomBtn_clicked(object sender, EventArgs e)
    {
        appSettings.CodeCharSet = CharSetTypes.CUSTOM;
        codeCustom.IsEditable = true;
    }

    protected void On_codeUpperLowerNum_clicked(object sender, EventArgs e)
    {
        appSettings.CodeCharSet = CharSetTypes.UPPER_LOWER_NUMERIC;
        codeCustom.IsEditable = false;
    }

    protected void On_codeUpperNum_clicked(object sender, EventArgs e)
    {
        appSettings.CodeCharSet = CharSetTypes.UPPER_NUMERIC;
        codeCustom.IsEditable = false;
    }

    protected void On_codeNumerical_clicked(object sender, EventArgs e)
    {
        appSettings.CodeCharSet = CharSetTypes.NUMERIC;
        codeCustom.IsEditable = false;
    }

    // Text charset events

    protected void On_textCustomBtn_clicked(object sender, EventArgs e)
    {
        appSettings.TextCharSet = CharSetTypes.CUSTOM;
        textCustom.IsEditable = true;
    }

    protected void On_textUpperLowerNum_clicked(object sender, EventArgs e)
    {
        appSettings.TextCharSet = CharSetTypes.UPPER_LOWER_NUMERIC;
        textCustom.IsEditable = false;
    }

    protected void On_textUpperNum_clicked(object sender, EventArgs e)
    {
        appSettings.TextCharSet = CharSetTypes.UPPER_NUMERIC;
        textCustom.IsEditable = false;
    }

    protected void On_textNumerical_clicked(object sender, EventArgs e)
    {
        appSettings.TextCharSet = CharSetTypes.NUMERIC;
        textCustom.IsEditable = false;
    }

    protected void On_RNGDeviceComboBox_changed(object sender, EventArgs e)
    {
        if (RNGDeviceComboBox.ActiveText == "Default")
        {
            appSettings.RngDevicePath = null;
        }
        else
        {
            appSettings.RngDevicePath = RNGDeviceComboBox.ActiveText;
        }
    }

    protected void On_btn_help_clicked(object sender, EventArgs e)
    {
        HelpDialog hd = HelpDialog.Create(otUI.HelpConst.SettingsHelp);
        hd.Run();
        hd.Destroy();
    }
    protected void On_themeComboBox_changed(object sender, EventArgs e)
    {
        if (themeComboBox.ActiveText == "Default")
        {
            appSettings.Theme = null;
        }
        if (themeComboBox.ActiveText == "Dark")
        {
            appSettings.Theme = "Dark";
        }
    }
}
