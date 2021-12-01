import math

import gi

from Core.Constants.Help import PRINT
from Core import Printing
from Core.Crypto.Keys import Generate

from UI import HelpDialog
from Core.PrettyPrint import grid_prettyfy, prettyfy, unprettyfy
from main import app_settings

gi.require_version('Gtk', '3.0')
gi.require_version('Pango', '1.0')
gi.require_version('PangoCairo', '1.0')

from gi.repository import Gtk
from gi.repository import Pango, PangoCairo

import cairo


@Gtk.Template(filename="UI/interfaces/PrintDialog.ui")
class PrintDialog(Gtk.Dialog):
    __gtype_name__ = "PrintDialog"

    pad_number = Gtk.Template.Child()
    GRID_GROUP_N = 5

    def populate_text_list(self, n_pads):
        text_list = []
        for i in range(0, n_pads):
            if app_settings.rng_device_path is None or app_settings.rng_device_path == "None":
                key = Generate.key_stream(app_settings.key_length, app_settings.get_code_charset())
            else:
                key = Generate.key_stream_device(app_settings.key_length, app_settings.rng_device_path,
                                                 app_settings.get_code_charset())
            text = grid_prettyfy(key, self.GRID_GROUP_N)
            text_list.append(text)
        return text_list

    def __init__(self):
        super().__init__()
        self.init_template()
        self.text_list = self.populate_text_list(self.pad_number.get_value_as_int())
        self.max_per_page = 8
        self.layout = []

    def otp_print(self):
        p = Gtk.PrintOperation()
        p.connect("begin_print", self.begin_print)
        p.connect("draw_page", self.draw_page)

        return p

    def begin_print(self, print_operation, context):
        context = context.get_cairo_context()

        font = Pango.FontDescription("Monospace 12")
        font.set_weight(Pango.Weight.BOLD)

        self.layout = PangoCairo.create_layout(context)
        self.layout.set_font_description(font)

        n_pages = Printing.calculate_num_pages(self.text_list, self.max_per_page)
        print_operation.set_n_pages(n_pages)

    def draw_page(self, print_operation, print_context, page_number):

        context = print_context.get_cairo_context()
        self.print_page(context, self.text_list, self.max_per_page, page_number)
        print_operation.draw_page_finish()

    def print_page(self,context, text_list, max_per_page, current_page_n):
        d = dict()
        starting_grid = 0

        if len(text_list) > max_per_page:
            starting_grid = max_per_page * current_page_n
            text_list = text_list[starting_grid:starting_grid+max_per_page]
            """
            if len(text_list) > max_per_page:
                remove_count = len(text_list) - max_per_page
                text_list = text_list[max_per_page:max_per_page+remove_count]
            """

        for i in range(0, len(text_list)):
            text = text_list[i]
            self.layout.set_text(text)
            text_width, text_height = self.layout.get_pixel_size()
            d = Printing.calculate_offset(len(text_list), text_width, text_height)
            context.move_to(d["x"][i], d["y"][i])
            PangoCairo.show_layout(context, self.layout)

    @Gtk.Template.Callback()
    def onHelpClicked(self, button):
        HelpDialog.main(PRINT)

    @Gtk.Template.Callback()
    def onPrintClicked(self, button):
        p = self.otp_print()
        p.run(Gtk.PrintOperationAction.PRINT_DIALOG, self)

    @Gtk.Template.Callback()
    def onCloseClicked(self, button):
        self.destroy()

    @Gtk.Template.Callback()
    def onPadNumberChanged(self, spin_button):
        value = spin_button.get_value_as_int()
        self.text_list = self.populate_text_list(value)


def main():
    dialog = PrintDialog()
    dialog.show()

    Gtk.main()
