import gi


from UI import PrintDialog, HelpDialog
from Core.Constants.Help import GENERATE_KEYS
import os

gi.require_version('Gdk', '3.0')
gi.require_version('Gtk', '3.0')
gi.require_version('Gio', '2.0')

from gi.repository import Gtk
from gi.overrides import Gdk
from gi.repository import Gio

from Core.Crypto.Keys import Generate
from Core.PrettyPrint import prettyfy
from main import app_settings, resource
from Core.Constants import Config
from Core.Settings import write


Gio.Resource._register(resource)


@Gtk.Template(resource_path="/org/onetimepadui/UI/interfaces/GenerateKeysDialog.ui")
class GenerateKeysDialog(Gtk.Dialog):
    __gtype_name__ = "GenerateKeysDialog"

    key_output_view = Gtk.Template.Child()
    key_length = Gtk.Template.Child()
    print_btn = Gtk.Template.Child()

    def __init__(self):
        super().__init__()
        self.init_template()

        self.key_length.set_value(app_settings.key_length)

        if os.name != "posix":
            self.print_btn.set_sensitive(False)

    @Gtk.Template.Callback()
    def onGenerateClicked(self, button):
        key = ""

        if app_settings.rng_device_path is None or app_settings.rng_device_path == "None":
            key = Generate.key_stream(app_settings.key_length, app_settings.get_code_charset())
        else:
            key = Generate.key_stream_device(app_settings.key_length, app_settings.rng_device_path,
                                             app_settings.get_code_charset())

        if app_settings.has_pretty_print:
            key = prettyfy(key)

        self.key_output_view.get_buffer().set_text(key)

    @Gtk.Template.Callback()
    def onCopyClicked(self, button):
        clipboard = Gtk.Clipboard.get(Gdk.SELECTION_CLIPBOARD)
        textbuffer = self.key_output_view.get_buffer()
        start_iter, end_iter = textbuffer.get_start_iter(), textbuffer.get_end_iter()
        text = textbuffer.get_text(start_iter, end_iter, True)
        clipboard.set_text(text, -1)

    @Gtk.Template.Callback()
    def onPrintClicked(self, button):
        PrintDialog.main()

    @Gtk.Template.Callback()
    def onHelpClicked(self, button):
        HelpDialog.main(self, GENERATE_KEYS)

    @Gtk.Template.Callback()
    def onCloseClicked(self, button):
        self.destroy()

    @Gtk.Template.Callback()
    def onKeyLengthValueChanged(self, spin_button, scroll):
        app_settings.key_length = spin_button.get_value_as_int()
        write(str(Config.path()), app_settings)


def main():
    dialog = GenerateKeysDialog()
    dialog.show()

    Gtk.main()
