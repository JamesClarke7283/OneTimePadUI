from UI import MainMenu
from Core.Settings import AppSettings, read, write
from Core.Constants.Config import path

file_path = path()
app_settings = AppSettings(file_path)


def __init__():
    global app_settings
    if file_path.exists() is not True:
        write(str(file_path), app_settings)
    else:
        app_settings = read(file_path)

    MainMenu.main()


if __name__ == "__main__":
    __init__()
