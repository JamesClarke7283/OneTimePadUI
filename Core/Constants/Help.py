GENERATE_KEYS = """Generate Keys Help

Welcome to the Generate Keys feature of OneTimePad.

Number Selection.
Selects the amount of characters in the produced key. Use the + and - sign to add or subtract the amount of characters, or enter manually.

Generate Button
Generates the key and displays it in the box below.

Print
"Prints the key into a booklet that can be printed as a hardcopy.

Close Button
"Closes the window back to the main menu.

"""

CRYPT = """Crypt Help

Welcome to the Crypt Feature of OneTimePad.

Key Input Box
Input the key generated using the Generate Key Feature.

Message/Ciphertext Input Box
Input the text needed to Encrypt/Decrypt

Encrypt Button
Will Encrypt the text inputted into the Message/ Ciphertext Input Box.

Decrypt Button
Will Decrypt the text inputted into the Message/ Ciphertext Input Box.

Output Box
Will Display the Finished text, whether Encypted or Decrypted.
"""

SETTINGS = """Settings Help

Welcome to the Preferences help box!

Change Charset

Code Charset
Input the Character Set that is required to be used in the Key, in the 'Generate Keys' Feature. If Custom is selected, then input the characters required to be used in the 'Custom' Box.
For custom charset, Seperate every character with a ',' for example A,B,C

Text Charset
Input the Character Set that is going to be used in the message that needs to be encrypted or Decrypted. If Custom is selected, then input the characters required to be used in the 'Custom' Box.
For custom charset, Seperate every character with a ',' for example A,B,C

RNG Device
Select Random Number Device to get random key material.

Message Padding
Message padding is where the size of the message is made to be always the same size as the key
random bytes get added on the end, this obscures details about the message to an adversary.

Change Theme

Dark Theme
Dark theme alters the UI to display mostly dark colours. This is displayed as light text on a dark background. Dark theme is used to relieve eye strain and match the current lighting conditions of the user.

Light Theme
Light Theme is the standard format of the UI. Dark text on a light background. Good for use in bright surroundings.

Pretty print
Pretty print adds spaces between every 4 characters in the generate keys and crypt dialog.
"""

PRINT = """Print Dialog Help

Number of pads spin button
Lets you select how many one time pads you want to print out

Print
Button brings up print dialog with print settings
"""
