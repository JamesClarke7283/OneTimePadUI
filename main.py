from UI import MainMenu
from Core.Settings import AppSettings, read, write
import os
from pathlib import Path
from platform import system
from Core.Constants.Config import path


file_path = path()
app_settings = AppSettings(file_path)


def __init__():
    global app_settings

    path = Path(file_path)
    if path.is_file() is not True:
        write(file_path, app_settings)
    else:
        app_settings = read(file_path)

    MainMenu.main()


if __name__ == "__main__":
    __init__()
