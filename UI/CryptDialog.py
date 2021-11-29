import gi

from UI import HelpDialog
from Core.Constants.Help import CRYPT

gi.require_version('Gtk', '3.0')

from gi.repository import Gtk


@Gtk.Template(filename="UI/interfaces/CryptDialog.ui")
class CryptDialog(Gtk.Dialog):
    __gtype_name__ = "CryptDialog"

    @Gtk.Template.Callback()
    def onEncryptClicked(self, button):
        print("encrypt clicked")

    @Gtk.Template.Callback()
    def onDecryptClicked(self, button):
        print("decrypt clicked")

    @Gtk.Template.Callback()
    def onCopyClicked(self, button):
        print("Copy clicked")

    @Gtk.Template.Callback()
    def onHelpClicked(self, button):
        HelpDialog.main(CRYPT)

    @Gtk.Template.Callback()
    def onCloseClicked(self, button):
        self.destroy()



def main():
    dialog = CryptDialog()
    dialog.show()

    Gtk.main()