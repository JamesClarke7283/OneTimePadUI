from enum import IntEnum
from dataclasses import dataclass, asdict, fields
from Core.Constants import Charset
from pathlib import Path
import json
import os


class CharsetTypes(IntEnum):
    UPPER_LOWER_NUMERIC_PUNC_SPC = 0
    UPPER_LOWER_NUMERIC = 1
    UPPER_NUMERIC = 2
    NUMERIC = 3
    EMOJI = 4
    CUSTOM = 5


@dataclass
class AppSettings:
    code_charset_custom: list = tuple("1")
    text_charset_custom: list = tuple("a")
    rng_device_path: str = None
    has_pretty_print: bool = True
    has_padding: bool = True
    theme_path: str = "./themes"
    code_charset: int = int(CharsetTypes.NUMERIC)
    text_charset: int = int(CharsetTypes.UPPER_LOWER_NUMERIC_PUNC_SPC)

    def __init__(self, filepath):
        self.filepath = filepath

    def get_code_charset(self):
        match self.code_charset:
            case CharsetTypes.EMOJI:
                return Charset.EMOJI
            case CharsetTypes.UPPER_LOWER_NUMERIC:
                return Charset.UPPER_LOWER_NUMERIC
            case CharsetTypes.UPPER_NUMERIC:
                return Charset.UPPER_NUMERIC
            case CharsetTypes.NUMERIC:
                return Charset.NUMERIC
            case _:
                return self.code_charset_custom

    def get_text_charset(self):
        match self.text_charset:
            case CharsetTypes.UPPER_LOWER_NUMERIC_PUNC_SPC:
                return Charset.UPPER_LOWER_NUMERIC_PUNC_SPC
            case CharsetTypes.UPPER_LOWER_NUMERIC:
                return Charset.UPPER_LOWER_NUMERIC
            case CharsetTypes.UPPER_NUMERIC:
                return Charset.UPPER_NUMERIC
            case CharsetTypes.NUMERIC:
                return Charset.NUMERIC
            case _:
                return self.text_charset_custom


def read(file_path):
    path = Path(file_path)
    json_str = ""
    if path.is_file():
        with open(file_path, 'r') as f:
            json_str = f.readlines()
    json_str = "".join(json_str)
    app_settings = AppSettings(json.loads(json_str))
    return app_settings


def write(file_path, app_settings):
    dir_path = os.path.dirname(file_path)
    wd_path = Path(dir_path)
    if wd_path.is_dir() is not True:
        os.mkdir(dir_path)

    with open(file_path, 'w') as f:
        json_str = json.dumps(asdict(app_settings))
        f.writelines(json_str)




