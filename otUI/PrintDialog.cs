using System.Drawing;
using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace otUI
{

    public class PrintPreview: Gtk.Dialog
    {
        Builder builder;

        [UI] Gtk.Image KeyImage= new Gtk.Image();

        public static PrintPreview Create()
        {
            Builder builder = new Builder(null, "otUI.interfaces.PrintPreview.glade", null);
            return new PrintPreview(builder, builder.GetObject("printdialog").Handle);
        }

        protected PrintPreview(Builder builder, IntPtr handle) : base(handle)
        {

            this.builder = builder;

            builder.Autoconnect(this);
            AddButton("Close", ResponseType.Close);

        }


        public Bitmap ConvertTextToImage(string txt, string fontname, int fontsize, Color bgcolor, Color fcolor, int width, int Height)
        {
            Bitmap bmp = new Bitmap(width, Height);
            using (Graphics graphics = Graphics.FromImage(bmp))
            {

                Font font = new Font(fontname, fontsize);
                graphics.FillRectangle(new SolidBrush(bgcolor), 0, 0, bmp.Width, bmp.Height);
                graphics.DrawString(txt, font, new SolidBrush(fcolor), 0, 0);
                graphics.Flush();
                font.Dispose();
                graphics.Dispose();


            }
            return bmp;

        }


        //protected void OnPrintClicked(object sender, EventArgs e)
        //{
        //    KeyImage = this.ConvertTextToImage(x.Text, "Bookman Old Style", 10, Color.Yellow, Color.Red, x.Width, x.Height);

        //}

        



    }
}
