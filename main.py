import gi

from UI import MainMenu
from Core.Settings import AppSettings, read, write
from Core.Constants.Config import path

import os

gi.require_version('Gio', '2.0')

from gi.repository import Gio

file_path = path()

base_path = os.path.abspath(os.path.dirname(__file__))
resource_path = os.path.join(base_path, 'onetimepadui.gresource')
resource = Gio.Resource.load(resource_path)

Gio.Resource._register(resource)


if file_path.exists() is not True:
    app_settings = AppSettings()
    write(str(file_path), app_settings)
else:
    app_settings = None
    app_settings = read(file_path)


def __init__():
    global app_settings
    global resource

    win = MainMenu
    win.main()


if __name__ == "__main__":
    __init__()
