from UI import MainMenu
from Core.Settings import AppSettings, read, write
from Core.Constants.Config import path
from UI.ErrorDialog import ShowAlert

file_path = path()
if file_path.exists() is not True:
    app_settings = AppSettings()
    write(str(file_path), app_settings)
else:
    app_settings = None
    app_settings = read(file_path)


def __init__():
    global app_settings

    win = MainMenu
    win.main()


if __name__ == "__main__":
    __init__()
