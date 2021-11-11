using System;
using System.Collections.Generic;
using System.IO;
using Gtk;
using otlib;
using UI = Gtk.Builder.ObjectAttribute;

public class SettingsDialog : Gtk.Dialog
{
    Builder builder;
    private AppSettings appSettings;
    [UI] Gtk.RadioButton codeUpperLowerNum;
    [UI] Gtk.RadioButton codeUpperNum;
    [UI] Gtk.RadioButton codeNumerical;
    [UI] Gtk.RadioButton codeCustomBtn;
    [UI] Gtk.RadioButton codeEmoji;

    [UI] Gtk.Entry codeCustom = new Gtk.Entry();

    [UI] Gtk.RadioButton textUpperLowerNum;
    [UI] Gtk.RadioButton textUpperNum;
    [UI] Gtk.RadioButton textNumerical;
    [UI] Gtk.RadioButton textUpperLowerNumSPCPunc;

    [UI] Gtk.RadioButton textCustomBtn;

    [UI] Gtk.Entry textCustom = new Gtk.Entry();

    [UI] Gtk.ComboBoxText RNGDeviceComboBox = new Gtk.ComboBoxText();

    [UI] Gtk.ComboBoxText themeComboBox = new Gtk.ComboBoxText();


    [UI] Gtk.Button btn_help = new Gtk.Button();
    [UI] Gtk.Switch hasPrettyPrint;
    [UI] Gtk.Switch hasPadding;

    public bool stateHasPrettyPrint = true;
    public bool stateHasPadding = true;

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

            try
            {
                foreach (string item in l)
                {
                    RNGDeviceComboBox.AppendText(item);
                }
            }
            catch (UnauthorizedAccessException) { }
        }
        LoadConfig();
    }
    private void LoadConfig()
    {

        hasPrettyPrint.Active = appSettings.HasPrettyPrint;
        hasPadding.Active = appSettings.HasPadding;

        switch (appSettings.CodeCharSet)
        {
            case CharSetTypes.EMOJI:
                codeEmoji.Click();
                break;
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
            case CharSetTypes.UPPER_LOWER_NUMERIC_PUNC_SPC:
                textUpperLowerNumSPCPunc.Click();
                break;
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

        if (appSettings.Theme == "Dark")
        {
            themeComboBox.ActiveId = "2";
        }
        else if (appSettings.Theme == "Light")
        {
            themeComboBox.ActiveId = "1";
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

        if (themeComboBox.ActiveText == "System")
        {
            appSettings.Theme = null;
        }
        else if (themeComboBox.ActiveText == "Dark")
        {
            appSettings.Theme = "Dark";
        }
        else if (themeComboBox.ActiveText == "Light")
        {
            appSettings.Theme = "Light";
        }

        appSettings.HasPrettyPrint = hasPrettyPrint.Active;
        appSettings.HasPadding = hasPadding.Active;

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

    protected void On_codeEmoji_clicked(object sender, EventArgs e)
    {
        appSettings.CodeCharSet = CharSetTypes.EMOJI;
        textCustom.IsEditable = false;
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

    protected void On_textUpperLowerNumSPCPunc_clicked(object sender, EventArgs e)
    {
        appSettings.TextCharSet = CharSetTypes.UPPER_LOWER_NUMERIC_PUNC_SPC;
        textCustom.IsEditable = false;
    }






    protected void On_RNGDeviceComboBox_changed(object sender, EventArgs e)
    {
        if (RNGDeviceComboBox.ActiveText == "None")
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



}
