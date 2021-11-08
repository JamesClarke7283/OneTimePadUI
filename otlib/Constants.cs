using System;
using System.IO;
namespace otlib
{
    public static class Constants
    {
        public const string UPPER_LOWER_NUMERIC_PUNC_SPC = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789.,?! ";
        public const string UPPER_LOWER_NUMERIC = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        public const string UPPER_NUMERIC = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        public const string NUMERIC = "0123456789";
        public const string EMOJI = "😀️😥️👍️👎️";

        public static readonly string CONFIG_PATH = Path.Join(
            Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
            ".config",
            "one_time_pad_gtk",
            "config.xml"
        );
    }
}
