using System.IO;
using System.Xml.Serialization;

namespace otlib
{
    public enum CharSetTypes
    {
        UPPER_LOWER_NUMERIC,
        UPPER_NUMERIC,
        NUMERIC,
        CUSTOM
    }
    [XmlRoot("Settings", IsNullable = true)]
    public class AppSettings
    {
        [XmlIgnore]
        public string Filename;
        [XmlElement(IsNullable = true)]
        public string RngDevicePath = null;
        public CharSetTypes CodeCharSet = CharSetTypes.NUMERIC;
        public CharSetTypes TextCharSet = CharSetTypes.UPPER_LOWER_NUMERIC;
        public string CodeCharSetCustom = "";
        public string TextCharSetCustom = "";
        [XmlIgnore]
        public string CodeCharSetString
        {
            get
            {
                switch (CodeCharSet)
                {
                    case CharSetTypes.UPPER_LOWER_NUMERIC:
                        return Constants.UPPER_LOWER_NUMERIC;
                    case CharSetTypes.UPPER_NUMERIC:
                        return Constants.UPPER_NUMERIC;
                    case CharSetTypes.NUMERIC:
                        return Constants.NUMERIC;
                    default:
                        return CodeCharSetCustom;
                }
            }
        }
        [XmlIgnore]
        public string TextCharSetString
        {
            get
            {
                switch (TextCharSet)
                {
                    case CharSetTypes.UPPER_LOWER_NUMERIC:
                        return Constants.UPPER_LOWER_NUMERIC;
                    case CharSetTypes.UPPER_NUMERIC:
                        return Constants.UPPER_NUMERIC;
                    case CharSetTypes.NUMERIC:
                        return Constants.NUMERIC;
                    default:
                        return TextCharSetCustom;
                }
            }
        }
        public AppSettings(string filename)
        {
            Filename = filename;
        }
        public AppSettings()
        {
        }
        public static AppSettings Read(string filename)
        {
            if (File.Exists(filename))
            {
                AppSettings appSettings;
                XmlSerializer serializer = new(typeof(AppSettings));
                using (var stream = File.OpenRead(filename))
                    appSettings = (AppSettings)serializer.Deserialize(stream);
                appSettings.Filename = filename;
                return appSettings;
            }
            return new(filename);
        }
        public void Write()
        {
            XmlSerializer serializer = new(typeof(AppSettings));
            Directory.CreateDirectory(Directory.GetParent(Filename).FullName);
            using (var stream = File.Open(Filename, FileMode.Create))
                serializer.Serialize(stream, this);
        }
    }
}
