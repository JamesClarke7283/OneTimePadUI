from pathlib import Path


def path():
    file = Path.home() / ".config" / "onetimepadui" / "one_time_pad_ui.json"
    return file
