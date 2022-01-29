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
    key_length: int = 200
    code_charset_custom: list = tuple()
    text_charset_custom: list = tuple()
    rng_device_path: str = None
    rng_device_id: str = None
    has_pretty_print: bool = True
    has_padding: bool = True
    theme_id: str = "0"
    code_charset: int = int(CharsetTypes.NUMERIC)
    text_charset: int = int(CharsetTypes.UPPER_LOWER_NUMERIC_PUNC_SPC)

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
    settings = AppSettings(**json.loads(json_str))
    return settings


def write(file_path, app_settings):
    file = Path(file_path)
    Path.mkdir(file.parent, parents=True, exist_ok=True)

    with open(file_path, 'w') as f:
        json_str = json.dumps(asdict(app_settings), indent=4)
        f.writelines(json_str)
