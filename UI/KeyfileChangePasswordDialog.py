import gi

from Core.KeyFile import core as keyfile
from main import resource

gi.require_version('Gtk', '3.0')
gi.require_version('Gio', '2.0')

from gi.repository import Gtk
from gi.repository import Gio


Gio.Resource._register(resource)


@Gtk.Template(resource_path="/org/onetimepadui/UI/interfaces/KeyfileChangePasswordDialog.ui")
class GenerateKeyfile(Gtk.Dialog):
    __gtype_name__ = "ChangeKeyfilePassword"

    old_password_buffer = Gtk.Template.Child()
    new_password_buffer = Gtk.Template.Child()

    def __init__(self):
        super().__init__()
        self.init_template()
        self.path = ""
        self.old_password = ""
        self.new_password = ""

        self.add_buttons(
            Gtk.STOCK_CANCEL, Gtk.ResponseType.CANCEL, Gtk.STOCK_OK, Gtk.ResponseType.OK
        )

    @Gtk.Template.Callback()
    def onFileSet(self, file_chooser_button):
        self.path = file_chooser_button.get_filename()

    @Gtk.Template.Callback()
    def onOldPasswordChanged(self, editable):
        self.old_password = self.old_password_buffer.get_text()

    @Gtk.Template.Callback()
    def onNewPasswordChanged(self, editable):
        self.new_password = self.new_password_buffer.get_text()


def main():
    dialog = GenerateKeyfile()
    response = dialog.run()
    if response == Gtk.ResponseType.OK:
        keyfile.change_keys_password(dialog.old_password, dialog.new_password, dialog.path)
        dialog.destroy()

    Gtk.main()
