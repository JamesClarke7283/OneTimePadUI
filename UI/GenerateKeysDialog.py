import gi

gi.require_version("Gtk", "3.0")

from gi.repository import Gtk


@Gtk.Template(filename="UI/interfaces/CryptDialog.ui")
class MainMenu(Gtk.Window):
    __gtype_name__ = "MainMenu"

    @Gtk.Template.Callback()
    def onKeygenClicked(self, button):
        print("Keygen clicked")

    @Gtk.Template.Callback()
    def onCryptClicked(self, button):
        print("Crypt clicked")

    @Gtk.Template.Callback()
    def onSettingsClicked(self, button):
        print("Settings clicked")



"""
    @Gtk.Template.Callback()
    def onCodeBtnClicked(self, button):
        CD.main()
"""

def main():
    window = MainMenu()
    window.show()

    Gtk.main()
