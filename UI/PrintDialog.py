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

    pad_number = Gtk.SpinButton()

    def populate_text_list(self, n_pads):
        self.text_list = []
        for i in range(0, n_pads):
            if app_settings.rng_device_path is None or app_settings.rng_device_path == "None":
                key = Generate.key_stream(app_settings.key_length, app_settings.get_code_charset())
            else:
                key = Generate.key_stream_device(app_settings.key_length, app_settings.rng_device_path,
                                                 app_settings.get_code_charset())
            text = prettyfy(key)
            text = grid_prettyfy(text)
            self.text_list.append(text)

    def __init__(self):
        super().__init__()
        self.init_template()
        self.text_list = self.populate_text_list(self.pad_number.get_value_as_int())
        self.max_per_page = 8

    def otp_print(self):
        p = Gtk.PrintOperation()
        p.connect("begin_print", self.begin_print)
        p.connect("draw_page", self.draw_page)

        return p

    def begin_print(self, print_operation, context):
        n_pages = Printing.calculate_num_pages(self.text_list, self.max_per_page)
        print_operation.set_n_pages(n_pages)

    def draw_page(self, print_operation, print_context, page_number):
        context = print_context.get_cairo_context()
        Printing.print_page(context, self.text_list, self.max_per_page, page_number)


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
        self.populate_text_list(value)


def main():
    dialog = PrintDialog()
    dialog.show()

    Gtk.main()
