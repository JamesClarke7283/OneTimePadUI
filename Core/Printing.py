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


