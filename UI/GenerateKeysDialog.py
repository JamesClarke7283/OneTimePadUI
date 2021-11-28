import gi
from UI import PrintDialog, HelpDialog
from Core.Constants.Help import GENERATE_KEYS

gi.require_version('Gtk', '3.0')

from gi.repository import Gtk


@Gtk.Template(filename="UI/interfaces/GenerateKeysDialog.ui")
class GenerateKeysDialog(Gtk.Dialog):
    __gtype_name__ = "GenerateKeysDialog"

    @Gtk.Template.Callback()
    def onGenerateClicked(self, button):
        print("Generate clicked")

    @Gtk.Template.Callback()
    def onCopyClicked(self, button):
        print("Copy clicked")

    @Gtk.Template.Callback()
    def onPrintClicked(self, button):
        PrintDialog.main()

    @Gtk.Template.Callback()
    def onHelpClicked(self, button):
        HelpDialog.main(GENERATE_KEYS)

    @Gtk.Template.Callback()
    def onCloseClicked(self, button):
        self.destroy()


def main():
    dialog = GenerateKeysDialog()
    dialog.show()

    Gtk.main()
