import gi

gi.require_version("Gtk", "3.0")
from gi.repository import Gtk


def ShowAlert(parent, text):
    dialog = Gtk.MessageDialog(
        transient_for=parent,
        flags=0,
        message_type=Gtk.MessageType.ERROR,
        buttons=Gtk.ButtonsType.OK,
        text="ERROR",
    )
    dialog.format_secondary_text(
        text
    )
    dialog.run()

    dialog.destroy()
