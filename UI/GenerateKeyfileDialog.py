import gi

from UI import GenerateKeysDialog, CryptDialog, SettingsDialog
from UI.ThemeLoader import load_theme
from Core.KeyFile import core as keyfile
from Core.Crypto.Keys.Generate import key_stream, key_stream_device
from main import resource, app_settings

gi.require_version('Gtk', '3.0')
gi.require_version('Gio', '2.0')

from gi.repository import Gtk
from gi.repository import Gio


Gio.Resource._register(resource)


def BulkGenKeys(length, quantity):
    keys = []
    for i in range(quantity):
        if app_settings.rng_device_path is None or app_settings.rng_device_path == "None":
            key = key_stream(length, app_settings.get_code_charset())
        else:
            key = key_stream_device(length, app_settings.rng_device_path,
                                             app_settings.get_code_charset())
        keys.append(key)
    return keys


@Gtk.Template(resource_path="/org/onetimepadui/UI/interfaces/GenerateKeyfileDialog.ui")
class GenerateKeyfile(Gtk.Dialog):
    __gtype_name__ = "GenerateKeyfile"

    key_length = Gtk.Template.Child()
    key_quantity = Gtk.Template.Child()
    key_password_buffer = Gtk.Template.Child()

    def __init__(self):
        super().__init__()
        self.init_template()
        self.path = ""
        self.length = self.key_length.get_value_as_int()
        self.quantity = self.key_quantity.get_value_as_int()
        self.password = self.key_password_buffer.get_text()

        self.add_buttons(
            Gtk.STOCK_CANCEL, Gtk.ResponseType.CANCEL, Gtk.STOCK_OK, Gtk.ResponseType.OK
        )

    @Gtk.Template.Callback()
    def onFileClicked(self, widget):
        dialog = Gtk.FileChooserDialog(
            title="Please choose a file to save", parent=self, action=Gtk.FileChooserAction.SAVE
        )
        dialog.add_buttons(
            Gtk.STOCK_CANCEL,
            Gtk.ResponseType.CANCEL,
            Gtk.STOCK_OPEN,
            Gtk.ResponseType.OK,
        )

        response = dialog.run()
        if response == Gtk.ResponseType.OK:
            self.path = dialog.get_filename()

        dialog.destroy()

    @Gtk.Template.Callback()
    def onLengthChanged(self, length_spin_button):
        self.length = length_spin_button.get_value_as_int()

    @Gtk.Template.Callback()
    def onQuantityChanged(self, quantity_spin_button):
        self.quantity = quantity_spin_button.get_value_as_int()

    @Gtk.Template.Callback()
    def onPasswordChanged(self, editable):
        self.password = self.key_password_buffer.get_text()

def main():
    dialog = GenerateKeyfile()
    response = dialog.run()
    if response == Gtk.ResponseType.OK:
        keys = BulkGenKeys(dialog.length, dialog.quantity)
        keyfile.save_keys(dialog.password, keys, dialog.path)
        dialog.destroy()

    Gtk.main()
