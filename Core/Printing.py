from math import ceil
import gi

gi.require_version('Gtk', '3.0')
gi.require_version('Pango', '1.0')
gi.require_version('PangoCairo', '1.0')

from gi.repository import Pango, PangoCairo
from gi.repository import Gtk

@staticmethod
def calculate_offset(pad_number, text_width, text_height):
    x = []
    y = []
    offset_dict = dict()

    for pad_index in range(0, pad_number):
        if pad_index % 2 == 0:
            if pad_index > 0:
                x.append(0)
                y.append(y[pad_index - 1] + text_height)
            else:
                x.append(0)
                y.append(0)
        else:
            x.append(text_width + 20)
            y.append(y[pad_index - 1])

    offset_dict["x"] = x
    offset_dict["y"] = y

    return offset_dict


@staticmethod
def calculate_num_pages(text_list, max_per_page):
    return ceil(len(text_list) / max_per_page)

@staticmethod
def print_page(context, text_list, max_per_page, current_page_n):
    d = dict()

    starting_grid = 0

    if len(text_list) > max_per_page:
        starting_grid = max_per_page * current_page_n
        text_list = text_list[starting_grid:max_per_page]
        if len(text_list) > max_per_page:
            remove_count = len(text_list) - max_per_page
            text_list = text_list[max_per_page:remove_count]

    font = Pango.FontDescription("Monospace")
    font.set_weight(Pango.Weight.BOLD)

    for i in range(0, len(text_list)):
        text = text_list[i]
        layout = PangoCairo.create_layout(context)  # context.create_pango_layout(text)
        layout.set_font_description(font)
        text_width, text_height = layout.get_size()
        d = calculate_offset(len(text_list), text_width, text_height)
        context.move_to(d["x"][i], d["y"][i])
        PangoCairo.show_layout(context, layout)


