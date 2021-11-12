using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;
using Cairo;
using otlib;
using System.Collections.Generic;

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
        List<string> textArr = new List<string>() { };

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
            PrettyPrint pp = new ();

            byte[] ks = otlib.otp.GenerateKeystream(keySize);
            string text = otlib.otp.ToString(ks, MainClass.appSettings.CodeCharSetString);

            text = pp.Prettify(text);
            text = pp.GridPrettify(text);
            textArr.Add(text);


            // Add(area);
            ShowAll();

        }

        protected void On_printBtn_clicked(object sender, EventArgs e) 
        {
            Print(textArr,keySize);
        }

        protected void On_padNumber_value_changed(object sender, EventArgs e)
        {
           
        }




        void Print(List<string> textArr,int keySize=200) 
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

                    string text = textArr[0];

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
            string text = textArr[0];
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
