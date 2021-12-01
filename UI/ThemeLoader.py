from main import app_settings, resource
import gi

gi.require_version('Gdk', '3.0')
gi.require_version('Gtk', '3.0')
gi.require_version('Gio', '2.0')

from gi.repository import Gdk
from gi.repository import Gtk
from gi.repository import Gio

Gio.Resource._register(resource)


def load_theme():
    css_provider = Gtk.CssProvider().new()
    context = Gtk.StyleContext()
    screen = Gdk.Screen.get_default()
    is_default = False

    match app_settings.theme_id:
        case "1":
            css_provider.load_from_resource("/org/onetimepadui/assets/themes/light.css")
        case "2":
            css_provider.load_from_resource("/org/onetimepadui/assets/themes/dark.css")
        case _:
            context.remove_provider_for_screen(screen, css_provider)
            is_default = True

    if is_default is False:
        context.add_provider_for_screen(screen, css_provider, Gtk.STYLE_PROVIDER_PRIORITY_USER)



