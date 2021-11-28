import gi

gi.require_version("Gtk", "3.0")

from gi.repository import Gtk


@Gtk.Template(filename="UI/interfaces/PrintDialog.ui")
class PrintDialog(Gtk.Dialog):
    __gtype_name__ = "PrintDialog"

    @Gtk.Template.Callback()
    def onHelpClicked(self, button):
        print("help clicked")

    @Gtk.Template.Callback()
    def onPrintClicked(self, button):
        print("print clicked")

    @Gtk.Template.Callback()
    def onCloseClicked(self, button):
        self.destroy()

def main():
    dialog = PrintDialog()
    dialog.show()

    Gtk.main()
