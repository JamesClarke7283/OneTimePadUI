using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;
using Cairo;
using otlib;

namespace otUI
{
    public class PrintPreview : Gtk.Dialog
    {

        Builder builder;

        [UI] Gtk.DrawingArea area = new Gtk.DrawingArea();
        [UI] Gtk.Image KeyImage = new Gtk.Image();
        [UI] Gtk.Button printBtn = new Gtk.Button();
        [UI] Gtk.SpinButton padNumber;

        int keySize;

        public static PrintPreview Create(int keySize=200)
        {
            Builder builder = new Builder(null, "otUI.interfaces.PrintPreview.glade", null);
            return new PrintPreview(builder, builder.GetObject("printdialog").Handle, keySize);
        }
        protected PrintPreview(Builder builder, IntPtr handle, int keySize) : base(handle)
        {

            this.builder = builder;

            builder.Autoconnect(this);
            AddButton("Close", ResponseType.Close);

            //DrawingArea area = new DrawingArea();
            //area.Drawn += OnExpose;

            this.keySize = keySize;


            // Add(area);
            ShowAll();

        }

        protected void On_printBtn_clicked(object sender, EventArgs e) 
        {
            Print(keySize);
        }

        protected void On_padNumber_value_changed(object sender, EventArgs e)
        {
           
        }




        void Print(int keySize=200) 
        {
            var print = new PrintOperation();
            print.BeginPrint += (obj, args) => { print.NPages = 1; };
            print.DrawPage += (obj, args) =>
            {
                using (PrintContext context = args.Context)
                {
                    Cairo.Context cr = context.CairoContext;

                    Pango.FontDescription font = new ()
                    {
                        Family = "Monospace",
                        Weight = Pango.Weight.Bold
                    };

                    byte[] ks = otlib.otp.GenerateKeystream(keySize);
                    string text = otlib.otp.ToString(ks, MainClass.appSettings.CodeCharSetString);

                    PrettyPrint pp = new ();
                    text = pp.Prettify(text);
                    text = pp.GridPrettify(text);

                    Pango.Layout layout = CreatePangoLayout(text);
                    layout.FontDescription = font;
                    layout.GetPixelSize(out int text_width, out int text_height);

                    Pango.CairoHelper.ShowLayout(cr, layout);
                }
            };
            print.EndPrint += (obj, args) => { };
            print.Run(PrintOperationAction.PrintDialog, this);
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
        }
    }
}
