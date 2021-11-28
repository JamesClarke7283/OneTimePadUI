import gi

gi.require_version('Gtk', '3.0')

from gi.repository import Gtk


@Gtk.Template(filename="UI/interfaces/AboutDialog.ui")
class AboutDialog(Gtk.AboutDialog):
    __gtype_name__ = "AboutDialog"

def main():
    dialog = AboutDialog()
    dialog.show()

    Gtk.main()
