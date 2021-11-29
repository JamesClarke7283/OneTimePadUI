import gi

from Core.Constants.Help import SETTINGS
from Core.Settings import CharsetTypes
from UI import AboutDialog, HelpDialog

gi.require_version('Gtk', '3.0')

from gi.repository import Gtk
from main import app_settings


@Gtk.Template(filename="UI/interfaces/SettingsDialog.ui")
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
        self.load_config()

    def load_config(self):
        self.has_pretty_print.do_state_set(self.has_pretty_print, app_settings.has_pretty_print)
        self.has_padding.do_state_set(self.has_padding, app_settings.has_padding)

        self.code_custom.set_text("".join(app_settings.code_charset_custom))
        self.text_custom.set_text("".join(app_settings.text_charset_custom))

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

        if app_settings.theme == "Dark":
            self.theme_combo_box.set_active_id(2)
        elif app_settings.theme == "Light":
            self.theme_combo_box.set_active_id(1)


    @Gtk.Template.Callback()
    def onAboutClicked(self, button):
        AboutDialog.main()

    @Gtk.Template.Callback()
    def onSaveClicked(self, button):
        print("save clicked")

    @Gtk.Template.Callback()
    def onHelpClicked(self, button):
        HelpDialog.main(SETTINGS)


def main():
    dialog = SettingsDialog()
    dialog.show()

    Gtk.main()
