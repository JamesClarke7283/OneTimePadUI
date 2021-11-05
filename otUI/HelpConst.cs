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
                                                "\nOutput Box\n" +
                                                "Will Display the Finished text, whether Encypted or Decrypted.\n";

        public static string SettingsHelp = "Settings Help\n"+
                                            "Welcome to the Preferences help box!\n"+
                                            "\nChange Charset\n"+
                                            "\nCode Charset\n"+
                                            "Input the Character Set that you would like to be used in the Key, in the 'Generate Keys' Feature. If Custom is selected, then input the characters you would like to be used in the 'Custom' Box.\n"+
                                            "\nText Charset\n"+
                                            "Input the Character Set needed to -\n"+
                                            "\nRNG Device\n";
    }
}
