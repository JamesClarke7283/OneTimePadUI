import gi

from Core.Constants.Help import SETTINGS
from Core.Constants import Config
from Core.Settings import CharsetTypes, write
from UI import AboutDialog, HelpDialog
from UI.ThemeLoader import load_theme

import os

gi.require_version('Gtk', '3.0')
gi.require_version('Gio', '2.0')
gi.require_version('Pango', '1.0')

from gi.repository import Gio
from gi.repository import Gtk
from gi.repository import Pango

from main import app_settings, resource
from Core import RNGDevice

Gio.Resource._register(resource)


@Gtk.Template(resource_path="/org/onetimepadui/UI/interfaces/SettingsDialog.ui")
class SettingsDialog(Gtk.Dialog):
    __gtype_name__ = "SettingsDialog"

    text_upper_lower_num_spc_punc = Gtk.Template.Child()
    text_upper_lower_num = Gtk.Template.Child()
    text_upper_num = Gtk.Template.Child()
    text_numerical = Gtk.Template.Child()
    text_custom_btn = Gtk.Template.Child()
    text_custom = Gtk.Template.Child()

    code_upper_lower_num = Gtk.Template.Child()
    code_upper_num = Gtk.Template.Child()
    code_numerical = Gtk.Template.Child()
    code_emoji = Gtk.Template.Child()
    code_custom_btn = Gtk.Template.Child()
    code_custom = Gtk.Template.Child()

    rng_device_combo_box = Gtk.Template.Child()
    has_padding = Gtk.Template.Child()
    theme_combo_box = Gtk.Template.Child()
    has_pretty_print = Gtk.Template.Child()

    def __init__(self):
        super().__init__()
        self.init_template()

        if os.name == "posix":
            dev_list = RNGDevice.list()
            for dev_index in range(1, len(dev_list)):
                self.rng_device_combo_box.append(str(dev_index), str(dev_list[dev_index]))
        else:
            self.rng_device_combo_box.props.button_sensitivity = Gtk.SensitivityType.OFF

        self.load_config()

    def load_config(self):
        self.has_pretty_print.do_state_set(self.has_pretty_print, app_settings.has_pretty_print)
        self.has_padding.do_state_set(self.has_padding, app_settings.has_padding)

        self.code_custom.set_text(",".join(app_settings.code_charset_custom))
        self.text_custom.set_text(",".join(app_settings.text_charset_custom))

        match app_settings.text_charset:
            case CharsetTypes.UPPER_LOWER_NUMERIC_PUNC_SPC:
                self.text_upper_lower_num_spc_punc.do_clicked(self.text_upper_lower_num_spc_punc)
            case CharsetTypes.UPPER_LOWER_NUMERIC:
                self.text_upper_lower_num.do_clicked(self.text_upper_lower_num)
            case CharsetTypes.UPPER_NUMERIC:
                self.text_upper_num.do_clicked(self.text_upper_num)
            case CharsetTypes.NUMERIC:
                self.text_numerical.do_clicked(self.text_numerical)
            case _:
                self.text_custom_btn.do_clicked(self.text_custom_btn)
                self.text_custom_btn.do_clicked(self.text_custom_btn)

        match app_settings.code_charset:
            case CharsetTypes.EMOJI:
                self.code_emoji.do_clicked(self.code_emoji)
            case CharsetTypes.UPPER_LOWER_NUMERIC:
                self.code_upper_lower_num.do_clicked(self.code_upper_lower_num)
            case CharsetTypes.UPPER_NUMERIC:
                self.code_upper_num.do_clicked(self.code_upper_num)
            case CharsetTypes.NUMERIC:
                self.code_numerical.do_clicked(self.code_numerical)
            case _:
                self.code_custom_btn.do_clicked(self.code_custom_btn)
                self.code_custom_btn.do_clicked(self.code_custom_btn)

        if app_settings.theme_id == 2:
            self.theme_combo_box.set_active_id("2")
        elif app_settings.theme_id == 1:
            self.theme_combo_box.set_active_id("1")

        self.rng_device_combo_box.set_active_id(str(app_settings.rng_device_id))
        self.theme_combo_box.set_active_id(str(app_settings.theme_id))
        load_theme()

    @Gtk.Template.Callback()
    def onAboutClicked(self, button):
        AboutDialog.main(self)

    @Gtk.Template.Callback()
    def onSaveClicked(self, button):
        write(str(Config.path()), app_settings)
        self.destroy()

    @Gtk.Template.Callback()
    def onHelpClicked(self, button):
        HelpDialog.main(self, SETTINGS)

    @Gtk.Template.Callback()
    def onRNGDeviceComboboxChanged(self, combobox):
        app_settings.rng_device_path = combobox.get_active_text()
        app_settings.rng_device_id = combobox.get_active_id()

    @Gtk.Template.Callback()
    def onTextUpperLowerNumSPCPuncClicked(self, button):
        app_settings.text_charset = CharsetTypes.UPPER_LOWER_NUMERIC_PUNC_SPC
        self.text_custom.set_editable(False)

    @Gtk.Template.Callback()
    def onTextUpperLowerNumClicked(self, button):
        app_settings.text_charset = CharsetTypes.UPPER_LOWER_NUMERIC
        self.text_custom.set_editable(False)

    @Gtk.Template.Callback()
    def onTextUpperNumClicked(self, button):
        app_settings.text_charset = CharsetTypes.UPPER_NUMERIC
        self.text_custom.set_editable(False)

    @Gtk.Template.Callback()
    def onTextNumericalClicked(self, button):
        app_settings.text_charset = CharsetTypes.NUMERIC
        self.text_custom.set_editable(False)

    @Gtk.Template.Callback()
    def onTextCustomBtnClicked(self, button):
        app_settings.text_charset = CharsetTypes.CUSTOM
        self.text_custom.set_editable(True)

    @Gtk.Template.Callback()
    def onCodeUpperLowerNumClicked(self, button):
        app_settings.code_charset = CharsetTypes.UPPER_LOWER_NUMERIC
        self.code_custom.set_editable(False)

    @Gtk.Template.Callback()
    def onCodeUpperNumClicked(self, button):
        app_settings.code_charset = CharsetTypes.UPPER_NUMERIC
        self.code_custom.set_editable(False)

    @Gtk.Template.Callback()
    def onCodeNumericalClicked(self, button):
        app_settings.code_charset = CharsetTypes.NUMERIC
        self.code_custom.set_editable(False)

    @Gtk.Template.Callback()
    def onCodeEmojiClicked(self, button):
        app_settings.code_charset = CharsetTypes.EMOJI
        self.code_custom.set_editable(False)

    @Gtk.Template.Callback()
    def onCodeCustomBtnClicked(self, button):
        app_settings.code_charset = CharsetTypes.CUSTOM
        self.code_custom.set_editable(True)

    @Gtk.Template.Callback()
    def onHasPaddingStateSet(self, switch, state):
        app_settings.has_padding = state

    @Gtk.Template.Callback()
    def onHasPrettyPrintStateSet(self, switch, state):
        app_settings.has_pretty_print = state

    @Gtk.Template.Callback()
    def onTextCustomChanged(self, editable):
        app_settings.text_charset_custom = self.text_custom.get_text().split(",")

    @Gtk.Template.Callback()
    def onCodeCustomChanged(self, editable):
        app_settings.code_charset_custom = self.code_custom.get_text().split(",")

    @Gtk.Template.Callback()
    def OnThemeComboBoxChanged(self, combo_box):
        app_settings.theme_id = combo_box.get_active_id()
        load_theme()

    @Gtk.Template.Callback()
    def onSetFont(self, font_button):
        font = font_button.get_font()
        print(font)




def main():
    dialog = SettingsDialog()
    dialog.show()

    Gtk.main()
