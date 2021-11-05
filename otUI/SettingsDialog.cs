using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;
using otlib;

public class SettingsDialog : Gtk.Dialog
{
    Builder builder;
    public string codeCharset = "0123456789";
    public string textCharset = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
    public string rngDevicePath = null;

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

    public static SettingsDialog Create()
    {
        Builder builder = new Builder(null, "otUI.interfaces.SettingsDialog.glade", null);
        return new SettingsDialog(builder, builder.GetObject("settingsdialog").Handle);
    }

    protected SettingsDialog(Builder builder, IntPtr handle) : base(handle)
    {
        this.builder = builder;

        builder.Autoconnect(this);
        AddButton("Close", ResponseType.Close);
        RNGDeviceComboBox.AppendText("test");
    }

    protected void On_Save_clicked(object sender, EventArgs e)
    {
        if (codeCustomBtn.Active) 
        {
            codeCharset = codeCustom.Text;
        }

        if (textCustomBtn.Active) 
        {
            textCharset = textCustom.Text;
        }


        otlib.Settings.codeCharset = codeCharset;
        otlib.Settings.textCharset = textCharset;
        otlib.Settings.rngDevicePath = rngDevicePath;
        Destroy();
    }

    // Code charset events
    protected void On_codeCustomBtn_clicked(object sender, EventArgs e)
    {
       codeCustom.IsEditable = true;
    }

    protected void On_codeUpperLowerNum_clicked(object sender, EventArgs e)
    {
        codeCharset = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        codeCustom.IsEditable = false;
    }

    protected void On_codeUpperNum_clicked(object sender, EventArgs e)
    {
        codeCharset = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        codeCustom.IsEditable = false;
    }

    protected void On_codeNumerical_clicked(object sender, EventArgs e)
    {
        codeCharset = "0123456789";
        codeCustom.IsEditable = false;
    }

    // Text charset events

    // Code charset events
    protected void On_textCustomBtn_clicked(object sender, EventArgs e)
    {
        textCustom.IsEditable = true;
    }

    protected void On_textUpperLowerNum_clicked(object sender, EventArgs e)
    {
        textCharset = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        textCustom.IsEditable = false;
    }

    protected void On_textUpperNum_clicked(object sender, EventArgs e)
    {
        textCharset = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        textCustom.IsEditable = false;
    }

    protected void On_textNumerical_clicked(object sender, EventArgs e)
    {
        textCharset = "0123456789";
        textCustom.IsEditable = false;
    }

    protected void On_RNGDeviceComboBox_changed(object sender, EventArgs e)
    {
        if (RNGDeviceComboBox.ActiveText == "Default") 
        {
            rngDevicePath = null;
        }
        else 
        {
            rngDevicePath = RNGDeviceComboBox.ActiveText; 
        }
    }



}
