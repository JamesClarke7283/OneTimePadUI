from platform import system
from os import environ


def path():
    global working_directory
    file_name = "one_time_pad_ui.json"

    if system() == "Linux":
        working_directory = environ['HOME'] + "/.config/onetimepadui/"
    else:
        working_directory = "./"
        
    return working_directory + file_name
