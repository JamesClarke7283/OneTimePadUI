import gi

from Core.Constants.Help import SETTINGS
from UI import AboutDialog, HelpDialog

gi.require_version('Gtk', '3.0')

from gi.repository import Gtk


@Gtk.Template(filename="UI/interfaces/SettingsDialog.ui")
class SettingsDialog(Gtk.Dialog):
    __gtype_name__ = "SettingsDialog"

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
