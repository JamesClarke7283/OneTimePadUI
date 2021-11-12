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

            //DrawingArea area = new DrawingArea();
            //area.Drawn += OnExpose;

            var print = new PrintOperation();
            print.BeginPrint += (obj, args) => { print.NPages = 1; };
            print.DrawPage += (obj, args) =>
            {
                using (PrintContext context = args.Context)
                {
                Cairo.Context cr = context.CairoContext;

                Pango.FontDescription font = new Pango.FontDescription();

                string text = "8873 2357 3753 1118 2547 3653 2752 4692 5408 1538 3552 1509 2354 2299 2432 7893 8170 9799 5717 3368 5077 5537 5976 8135 5591 2546 4122 5812 7273 6762 6071 3255 5459 8214 9358 2200 7359 4564 1437 6425 9498 6275 4479 4384 1644 8145 3128 3081 3011 0339\n" +
                                          "2396 8070 6813 7860 6087 3272 8516 9280 6189 4636 6923 4691 8895 1846 7437 0351 5017 3770 0525 3905 1635 6809 9951 3411 0658 7322 0619 7350 7260 6687 1141 7445 6372 2291 1430 2784 3374 9040 3490 0623 2077 8122 2179 7817 0949 0979 9732 3449 7850 0601";
                int rectangle_width = 300;
                int rectangle_height = 300;

                font.Family = "Monospace";
                font.Weight = Pango.Weight.Bold;

                // http://developer.gnome.org/pangomm/unstable/classPango_1_1Layout.html
                Pango.Layout layout = CreatePangoLayout(text);

                layout.FontDescription = font;

                int text_width;
                int text_height;

                //get the text dimensions (it updates the variables -- by reference)
                layout.GetPixelSize(out text_width, out text_height);

                // Position the text in the middle
                cr.MoveTo((rectangle_width - text_width) / 2d, (rectangle_height - text_height) / 2d);

                //Pango.CairoHelper.ShowLayout(cr, layout);


                var imageSurface = new Cairo.ImageSurface(Format.RGB24, text_width, text_height);
                cr.SetSourceSurface(imageSurface, 0, 0);
                //cr.Scale(256.0 / text_width, 256.0 / text_height);
                //cr.SetSourceSurface(imageSurface, 0, 0);
                cr.Paint();
                }

            };
            print.EndPrint += (obj, args) => { };

            // print.Run(PrintOperationAction.Print, null);
            print.Run(PrintOperationAction.PrintDialog, this);

            // Add(area);
            ShowAll();

        }

        void OnExpose(object o, Gtk.DrawnArgs args)
        {
            string text = "8873 2357 3753 1118 2547 3653 2752 4692 5408 1538 3552 1509 2354 2299 2432 7893 8170 9799 5717 3368 5077 5537 5976 8135 5591 2546 4122 5812 7273 6762 6071 3255 5459 8214 9358 2200 7359 4564 1437 6425 9498 6275 4479 4384 1644 8145 3128 3081 3011 0339\n" +
                          "2396 8070 6813 7860 6087 3272 8516 9280 6189 4636 6923 4691 8895 1846 7437 0351 5017 3770 0525 3905 1635 6809 9951 3411 0658 7322 0619 7350 7260 6687 1141 7445 6372 2291 1430 2784 3374 9040 3490 0623 2077 8122 2179 7817 0949 0979 9732 3449 7850 0601";
            int rectangle_width = 300;
            int rectangle_height = 300;
            Context cr = args.Cr;

            Pango.FontDescription font = new Pango.FontDescription();

            font.Family = "Monospace";
            font.Weight = Pango.Weight.Bold;

            // http://developer.gnome.org/pangomm/unstable/classPango_1_1Layout.html
            Pango.Layout layout = CreatePangoLayout(text);

            layout.FontDescription = font;

            int text_width;
            int text_height;

            //get the text dimensions (it updates the variables -- by reference)
            layout.GetPixelSize(out text_width, out text_height);

            // Position the text in the middle
            cr.MoveTo((rectangle_width - text_width) / 2d, (rectangle_height - text_height) / 2d);

            Pango.CairoHelper.ShowLayout(cr, layout);


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
