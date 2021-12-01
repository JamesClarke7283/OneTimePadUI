import gi

gi.require_version('Gtk', '3.0')

from gi.repository import Gtk


class HelpDialog(Gtk.Dialog):

    def __init__(self, parent, help_text):
        super().__init__(title="Help", transient_for=parent, flags=0)

        button = Gtk.Button.new_with_mnemonic("_Close")
        button.connect("clicked", self.on_close_clicked)

        label = Gtk.Label(label=help_text)
        box = self.get_content_area()
        box.add(label)
        box.add(button)

        self.show_all()

    def on_close_clicked(self, button):
        self.destroy()


def main(parent, help_text):
    dialog = HelpDialog(parent, help_text)
    dialog.show()
    Gtk.main()
