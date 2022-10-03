# One Time Pad GTK

A Libre, One Time Pad program written in python3.10 and GTK for UI.

# Functionalities

- Generate Keys

- Encrypt

- Decrypt

- Options (*Optional*) 

# Posible Security issues

- Key randomness (Need to generate random keys) 
- Key storage: it is down to the user to make sure the keys are stored securely and destroyed after use.
- Key reuse: Users should not reuse keys, otherwise all security goes out the window.

# Dependencies

## System

- python3.10

- python3.10-dev

- evince

## Python

- PyGObject>=3.4.2
- PyNaCl>=1.5.0

## Install Instructions

```bash
python3.10 -m venv .venv
source .venv/bin/activate
pip3 install -r requirements.txt
pyinstaller main.py --add-data "onetimepadui.gresource:." --add-data "assets:." --onefile
./dist/bin/main
```

# Screenshots

## Main Menu

![Main Menu](./Docs/Screenshots/UI-Main_Menu.png)

## Generate Keys

![Generate Keys](./Docs/Screenshots/UI-Generate_Keys.png)

## Printing

![Generate Keys_Printing](./Docs/Screenshots/UI-Print_Hover.png)

### Dialog

![Generate Keys_Printing_Dialog](./Docs/Screenshots/UI-Print_Pad_Dialog.png)

### Preview

![Generate Keys_Printing_Preview](./Docs/Screenshots/UI_Print_Preview.png)

## Encrypt

![Encrypt](./Docs/Screenshots/UI-Encrypt.png)

## Decrypt

![Decrypt](./Docs/Screenshots/UI-Decrypt.png)

## Settings

![Settings](./Docs/Screenshots/UI-Settings.png)

### Security

![Settings Security](./Docs/Screenshots/UI_Settings_Security.png)

![Settings Security RNG](./Docs/Screenshots/UI_Settings_RNG_Menu.png)

![Settings Security Changed](./Docs/Screenshots/UI_Settings_Security_Changed.png)

### Appearence

![Settings Appearence](./Docs/Screenshots/UI_Settings_Appearence.png)
