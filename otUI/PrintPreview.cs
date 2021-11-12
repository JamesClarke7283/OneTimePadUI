using System.Drawing;
using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;
using System.Drawing.Imaging;
using System.IO;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using Cairo;

namespace otUI
{

    public class PrintPreview : Gtk.Dialog
    {

        Builder builder;

        [UI] Gtk.DrawingArea area = new Gtk.DrawingArea();
        [UI] Gtk.Image KeyImage = new Gtk.Image();

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

            DrawingArea area = new DrawingArea();
            area.Drawn += OnExpose;

            Add(area);
            ShowAll();

        }

        void OnExpose(object o, Gtk.DrawnArgs args)
        {
            //DrawingArea area = (DrawingArea)o;
            using (Cairo.Context g = Gdk.CairoHelper.Create(area.GdkWindow))
            {
                g.LineWidth = 0.1;
                g.SetSourceRGB(250, 0, 0);
                g.Rectangle(0.25, 0.25, 0.5, 0.5);
                g.Stroke();
                
                
                int width, height;
                width = Allocation.Width;
                height = Allocation.Height;

               
                g.Translate(width / 2, height / 2);
                g.Arc(0, 0, 120, 0, 2 * Math.PI);
                

                
                //g.Paint();
                g.Save();

                for (int i = 0; i < 36; i++)
                {
                    g.Rotate(i * Math.PI / 36);
                    g.Scale(0.3, 1);
                    g.Arc(0, 0, 120, 0, 2 * Math.PI);
                    g.Restore();
                    g.Stroke();
                    g.Save();
                }



                //public Bitmap CreateBitmapImage(string keyText)

                //{

                //    Bitmap bmp = new Bitmap(1, 1);

                //    int intWidth = 0;
                //    int intHeight = 0;

                //    Font objFont = new Font("Arial", 20, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);

                //    Graphics objGraphics = Graphics.FromImage(bmp);

                //    intWidth = (int)objGraphics.MeasureString(keyText, objFont).Width;
                //    intHeight = (int)objGraphics.MeasureString(keyText, objFont).Height;

                //    bmp = new Bitmap(bmp, new Size(intWidth, intHeight));

                //    objGraphics = Graphics.FromImage(bmp);

                //    objGraphics.Clear(Color.White);
                //    objGraphics.SmoothingMode = SmoothingMode.AntiAlias;
                //    objGraphics.TextRenderingHint = TextRenderingHint.AntiAlias;
                //    objGraphics.DrawString(keyText, objFont, new SolidBrush(Color.FromArgb(102, 102, 102)), 0, 0);
                //    objGraphics.Flush();



                //    bmp.Save(@"image1.png", ImageFormat.Png);


                //    return (bmp);
                //}





                //public static void PrintOperation()
                //{
                //    PrintOperation print = new PrintOperation();
                //    print.BeginPrint += (obj, args) => { print.NPages = 1; };
                //    print.DrawPage += (obj, args) =>
                //    {
                //        PrintContext context = args.Context;
                //        Cairo.Context cr = context.CairoContext;

                //        var imageSurface = new Cairo.ImageSurface("C:\\image1.png");

                //        int w = imageSurface.Width;
                //        int h = imageSurface.Height;
                //        cr.Scale(256.0 / w, 256.0 / h);
                //        cr.SetSourceSurface(imageSurface, 0, 0);
                //        cr.Paint();
                //    };
                //    print.EndPrint += (obj, args) => { };
                //    print.Run(PrintOperationAction.Print, null);
                //}





            }
        }
    }
}
