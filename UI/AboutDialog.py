import gi

gi.require_version('Gtk', '3.0')
gi.require_version('Gio', '2.0')
gi.require_version('Gdk', '3.0')
gi.require_version('GdkPixbuf', '2.0')

from gi.repository import Gtk
from gi.repository import Gio
from gi.repository import Gdk, GdkPixbuf

from main import resource
from Core.Constants import About

Gio.Resource._register(resource)

icon = GdkPixbuf.Pixbuf.new_from_resource("/org/onetimepadui/assets/icons/lock.ico")


class AboutDialog(Gtk.AboutDialog):

    def __init__(self, parent):
        super().__init__(title="About", transient_for=parent, flags=0)
        self.set_default_size(400, 450)
        self.set_program_name(About.PROGRAM_NAME)
        self.set_version(About.VERSION)
        self.set_logo(icon)
        self.set_website(About.WEBSITE)
        self.set_comments(About.COMMENTS)
        self.set_license_type(Gtk.License.GPL_3_0)

        self.add_credit_section("authors", About.AUTHORS)
        self.add_credit_section("artists", About.ARTISTS)
        self.add_credit_section("documenters", About.DOCUMENTERS)

def main(parent):
    dialog = AboutDialog(parent)
    dialog.show()

    Gtk.main()
