import gi

from UI import HelpDialog
from Core.Constants.Help import CRYPT
from Core.PrettyPrint import prettyfy, unprettyfy

gi.require_version('Gdk', '3.0')
gi.require_version('Gtk', '3.0')
gi.require_version('Gio', '2.0')

from gi.overrides import Gdk
from gi.repository import Gtk
from gi.repository import Gio

from Core.Crypto import OTP
from UI.ErrorDialog import ShowAlert
from UI import KeyfileChangePasswordDialog, KeyfileUseKeyDialog
from main import app_settings, resource


Gio.Resource._register(resource)


@Gtk.Template(resource_path="/org/onetimepadui/UI/interfaces/CryptDialog.ui")
class CryptDialog(Gtk.Dialog):
    __gtype_name__ = "CryptDialog"

    output = Gtk.Template.Child()
    key_input = Gtk.Template.Child()
    msg_cipher_input = Gtk.Template.Child()

    def __init__(self):
        super().__init__()
        self.init_template()
        self.otp = OTP.Create(app_settings.get_code_charset(), app_settings.get_text_charset(), app_settings.has_padding)

    def get_text(self, text_view):
        text_buffer = text_view.get_buffer()
        start_iter, end_iter = text_buffer.get_start_iter(), text_buffer.get_end_iter()
        text = text_buffer.get_text(start_iter, end_iter, True)
        return text

    @Gtk.Template.Callback()
    def onEncryptClicked(self, button):
        try:
            msg, key = self.get_text(self.msg_cipher_input), self.get_text(self.key_input)

            cipher_text = self.otp.encrypt(msg, unprettyfy(key))

            if app_settings.has_padding:
                cipher_text = prettyfy(cipher_text)

            self.output.get_buffer().set_text(cipher_text)
        except KeyError:
            ShowAlert(self, "Message contains character that is not in textCode character set")
        except Exception as e:
            ShowAlert(self, str(e))

    @Gtk.Template.Callback()
    def onDecryptClicked(self, button):
        cipher_text, key = self.get_text(self.msg_cipher_input), self.get_text(self.key_input)

        plain_text = self.otp.decrypt(unprettyfy(cipher_text), unprettyfy(key))
        self.output.get_buffer().set_text(plain_text)

    @Gtk.Template.Callback()
    def onCopyClicked(self, button):
        clipboard = Gtk.Clipboard.get(Gdk.SELECTION_CLIPBOARD)
        textbuffer = self.output.get_buffer()
        start_iter, end_iter = textbuffer.get_start_iter(), textbuffer.get_end_iter()
        text = textbuffer.get_text(start_iter, end_iter, True)
        clipboard.set_text(text, -1)

    @Gtk.Template.Callback()
    def onHelpClicked(self, button):
        HelpDialog.main(self, CRYPT)

    @Gtk.Template.Callback()
    def onCloseClicked(self, button):
        self.destroy()

    @Gtk.Template.Callback()
    def onChangePasswordActivate(self, menu_item):
        KeyfileChangePasswordDialog.main()

    @Gtk.Template.Callback()
    def onLoadKeyActivate(self, menu_item):
        key = KeyfileUseKeyDialog.main()
        self.key_input.get_buffer().set_text(key)


def main():
    dialog = CryptDialog()
    dialog.show()

    Gtk.main()
