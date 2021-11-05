using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace otUI
{
    public class HelpConst
    {
        public static string GenerateKeysHelp = "Generate Keys Help \n" +
                                                "Welcome to the Generate Keys feature of OneTimePad.\n" +
                                                "\nNumber Selection.\n" +
                                                "Selects the amount of characters in the produced key. Use the + and - sign to add or subtract the amount of characters, or enter manually.\n" +
                                                "\nCharSet Button.\n" +
                                                "Allows users to pick what Character Set to use. Choose between Uppercase, lowercase, numerical or custom.\n" +
                                                "\nGenerate Button\n " +
                                                "Generates the key and displays it in the box below.\n" +
                                                "\nClose Button\n" +
                                                "Closes the window back to the main menu.\n";

        public static string CryptKeysHelp = "Crypt Help\n" +
                                                "Welcome to the Crypt Feature of OneTimePad.\n" +
                                                "\nKey Input Box\n" +
                                                "Input the key generated using the Generate Key Feature.\n" +
                                                "\nMessage/ Ciphertext Input Box\n" +
                                                "Input the text needed to Encrypt/Decrypt\n" +
                                                "\nEncrypt Button\n" +
                                                "Will Encrypt the text inputted into the Message/ Ciphertext Input Box.\n" +
                                                "\nDecrypt Button\n" +
                                                "Will Decrypt the text inputted into the Message/ Ciphertext Input Box.\n" +
                                                "\nCharSet Button\n" +
                                                "Allows users to pick what Character Set to use. Choices available are uppercase, lowercase, numerical or custom.\n" +
                                                "\nOutput Box\n" +
                                                "Will Display the Finished text, whether Encypted or Decrypted.\n";

        public static string SettingsHelp = "Test\n";
    }
}
