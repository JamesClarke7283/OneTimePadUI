import gi

gi.require_version('Gtk', '3.0')

from gi.repository import Gtk


@Gtk.Template(filename="UI/interfaces/HelpDialog.ui")
class HelpDialog(Gtk.Dialog):
    __gtype_name__ = "HelpDialog"

    HelpText = Gtk.Template.Child()

    def __init__(self, help_text):
        super().__init__()
        self.init_template()

        self.HelpText.get_buffer().set_text(help_text)

    @Gtk.Template.Callback()
    def onCloseClicked(self, button):
        self.destroy()


def main(helpText):
    dialog = HelpDialog(helpText)
    dialog.show()
    Gtk.main()
