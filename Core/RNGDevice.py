from pathlib import PurePath
import os


def __posix():
    dev_list = []
    for path, subdirs, files in os.walk("/dev"):
        for name in files:
            pp = PurePath(path, name)
            if os.access(pp, os.R_OK):
                dev_list.append(pp)
    return dev_list


def list():
    if os.name == "posix":
        return __posix()
