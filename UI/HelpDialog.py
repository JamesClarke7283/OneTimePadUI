import gi

gi.require_version('Gtk', '3.0')

from gi.repository import Gtk


@Gtk.Template(filename="UI/interfaces/HelpDialog.ui")
class HelpDialog(Gtk.Dialog):
    helptext = "TEST"

    __gtype_name__ = "HelpDialog"

    @Gtk.Template.Callback()
    def onCloseClicked(self, button):
        self.destroy()

    @Gtk.Template.Callback()
    def onHelpTextMoveCursor(self, textview, step, count, extend_selection):
        textbuffer = textview.get_buffer()
        textbuffer.set_text(self.helptext)


def main(helpText):
    dialog = HelpDialog()
    dialog.show()
    Gtk.main()
