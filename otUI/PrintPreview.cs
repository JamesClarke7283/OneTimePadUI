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

        public static PrintPreview Create(int keySize = 200)
        {
            Builder builder = new Builder(null, "otUI.interfaces.PrintPreview.glade", null);
            return new PrintPreview(builder, builder.GetObject("printdialog").Handle, keySize);
        }
        protected PrintPreview(Builder builder, IntPtr handle, int keySize) : base(handle)
        {
            this.builder = builder;

            builder.Autoconnect(this);
            AddButton("Close", ResponseType.Close);

            this.keySize = keySize;
                       
            ShowAll();

        }

        protected void On_printBtn_clicked(object sender, EventArgs e)
        {
            Print(textArr, keySize);
        }


        protected void onHelpClicked(object sender, EventArgs e)
        {
            HelpDialog hd = HelpDialog.Create(otUI.HelpConst.PrintDialogHelp);
            hd.Run();
            hd.Destroy();
        }



        protected void On_padNumber_value_changed(object sender, EventArgs e)
        {
            textArr = new List<string>() { };
            PrettyPrint pp = new();

            for (int i = 0; i < padNumber.Value; i++)
            {
                byte[] ks = otlib.otp.GenerateKeystream(keySize);
                string text = otlib.otp.ToString(ks, MainClass.appSettings.CodeCharSetString);

                text = pp.Prettify(text);
                text = pp.GridPrettify(text);
                textArr.Add(text);
            }
        }
        public static Dictionary<char, List<int>> CalculateOffset(int padnumber, int text_width, int text_height)
        {
            List<int> x = new List<int>() { };
            List<int> y = new List<int>() { };
            Dictionary<char, List<int>> dict = new Dictionary<char, List<int>>() { };

            for (int padIndex = 0; padIndex < padnumber; padIndex++)
            {
                if (padIndex % 2 == 0)
                {
                    if (padIndex > 0)
                    {
                        x.Add(0);
                        y.Add(y[padIndex - 1] + text_height);
                    }
                    else
                    {
                        x.Add(0);
                        y.Add(0);
                    }
                }
                else
                {
                    y.Add(y[padIndex - 1]);
                    x.Add(text_width + 20);
                }
            }

            dict.Add('x', x);
            dict.Add('y', y);
            return dict;
        }
        int CalculateNumberOfPages(List<string> textArr, int maxPerPage)
        {
            return (int)Math.Ceiling((double)textArr.Count / maxPerPage);
        }
        void PrintPage(Cairo.Context context, List<string> grids, int maxPerPage, int currPage)
        {
            Dictionary<char, List<int>> dict = new() { };

            int startingGrid = 0;
            if (grids.Count > maxPerPage)
            {
                startingGrid = maxPerPage * currPage;
                grids = textArr.GetRange(startingGrid, maxPerPage);
                if (grids.Count > maxPerPage)
                {
                    int removeCount = grids.Count - maxPerPage;
                    grids.RemoveRange(maxPerPage, removeCount);
                }
            }

            for (int i = 0; i < grids.Count; i++)
            {
                Pango.FontDescription font = new()
                {
                    Family = "Monospace",
                    Weight = Pango.Weight.Bold
                };

                string text = grids[i];

                Pango.Layout layout = CreatePangoLayout(text);
                layout.FontDescription = font;
                layout.GetPixelSize(out int text_width, out int text_height);

                dict = CalculateOffset(grids.Count, text_width, text_height);

                context.MoveTo(dict['x'][i], dict['y'][i]);

                Pango.CairoHelper.ShowLayout(context, layout);
            }
        }
        void PrintPage(PrintContext printContext, List<string> grids, int maxPerPage, int currPage)
        {
            using (printContext)
            {
                Cairo.Context context = printContext.CairoContext;
                PrintPage(context, grids, maxPerPage, currPage);
            }
        }
        void Print(List<string> textArr, int keySize = 200)
        {
            int maxPerPage = 10;
            var print = new PrintOperation();
            print.BeginPrint += (obj, args) => { print.NPages = CalculateNumberOfPages(textArr, maxPerPage); };
            print.DrawPage += (obj, args) =>
            {
                PrintPage(args.Context, textArr, maxPerPage, args.PageNr);
            };
            print.EndPrint += (obj, args) => { };
            print.Run(PrintOperationAction.PrintDialog, this);
        }
        void OnExpose(object o, Gtk.DrawnArgs args)
        {
            Context cr = args.Cr;
            PrintPage(cr, textArr, 10, 0);
        }
    }
}
