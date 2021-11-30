import gi

from UI import GenerateKeysDialog, CryptDialog, SettingsDialog
from UI.ErrorDialog import ShowAlert
from UI.ThemeLoader import load_theme

gi.require_version('Gtk', '3.0')

from gi.repository import Gtk


@Gtk.Template(filename="UI/interfaces/MainMenu.ui")
class MainMenu(Gtk.Window):
    __gtype_name__ = "MainMenu"

    def __init__(self):
        super().__init__()
        self.init_template()
        load_theme()

    @Gtk.Template.Callback()
    def onKeygenClicked(self, button):
        GenerateKeysDialog.main()

    @Gtk.Template.Callback()
    def onCryptClicked(self, button):
        CryptDialog.main()

    @Gtk.Template.Callback()
    def onSettingsClicked(self, button):
        SettingsDialog.main()


def main():
    window = MainMenu()
    window.show()

    Gtk.main()
