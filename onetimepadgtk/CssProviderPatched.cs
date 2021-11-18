/*
patching from commit by zii-dmg:
    https://github.com/GtkSharp/GtkSharp/commit/aa1095a02de2dc90c5fc59855bc21d23a0646e79
    https://github.com/GtkSharp/GtkSharp/issues/212
    https://github.com/GtkSharp/GtkSharp/pull/238
    // TODO remove patch when nuget package is updated!
*/
using System;
using System.IO;
using System.Reflection;

namespace Gtk
{
    public class CssProviderPatched : CssProvider
    {
        public new bool LoadFromResource(string resource) => LoadFromResource(Assembly.GetCallingAssembly(), resource);

        public bool LoadFromResource(Assembly assembly, string resource)
        {
            if (assembly == null)
                assembly = Assembly.GetCallingAssembly();

            Stream stream = assembly.GetManifestResourceStream(resource);
            if (stream == null)
                throw new ArgumentException("'" + resource + "' is not a valid resource name of assembly '" + assembly + "'.", nameof(resource));

            using (var reader = new StreamReader(stream))
            {
                string data = reader.ReadToEnd();
                return LoadFromData(data);
            }
        }
    }
}
