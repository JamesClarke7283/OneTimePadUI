# One Time Pad GTK

A Libre, One Time Pad program written in python3.10 and GTK for UI.

# !!Attention Assignment Marker!!
Use the csharp branch or tag 1.0. As this is what was completed as part of the assignment.
The application has since been rewritten in python in the 2.0.0 version.

[Csharp branch](https://gitlab.com/jc1234/onetimepadgtk/-/tree/csharp)

# !!Attention Users!!
The main branch is now stable for general usage, use tag releases for more stability

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

## Python

- PyGObject>=3.4.2
