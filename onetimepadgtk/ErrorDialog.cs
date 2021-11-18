using System;
using Gtk;

namespace onetimepadgtk
{
    public static class ErrorDialog
    {
        public static void ShowAlert(Window parent, string text)
        {
            MessageDialog dialog = new MessageDialog(
                parent,
                0,
                Gtk.MessageType.Error,
                ButtonsType.Ok,
                text
            );
            dialog.Run();
            dialog.Destroy();
        }
    }
}
