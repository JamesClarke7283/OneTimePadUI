﻿using System.Drawing;
using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;
using System.Drawing.Imaging;
using System.IO;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace otUI
{

    public class PrintPreview: Gtk.Dialog
    {
        
        Builder builder;

        [UI] Gtk.Image KeyImage= new Gtk.Image();

        public static PrintPreview Create(object createBitmapImage)
        {
            Builder builder = new Builder(null, "otUI.interfaces.PrintPreview.glade",null);
            return new PrintPreview(builder, builder.GetObject("printdialog").Handle);
        }

        protected PrintPreview(Builder builder, IntPtr handle) : base(handle)
        {

            this.builder = builder;

            builder.Autoconnect(this);
            AddButton("Close", ResponseType.Close);

        }

        public Bitmap CreateBitmapImage(string exampleText)
        {
            exampleText = "Example Text.";

            Bitmap bmp = new Bitmap(1, 1);

            int intWidth = 0;
            int intHeight = 0;

            Font objFont = new Font("Arial", 20, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);

            Graphics objGraphics = Graphics.FromImage(bmp);

            intWidth = (int)objGraphics.MeasureString(exampleText, objFont).Width;
            intHeight = (int)objGraphics.MeasureString(exampleText, objFont).Height;

            bmp = new Bitmap(bmp, new Size(intWidth, intHeight));

            objGraphics = Graphics.FromImage(bmp);

            objGraphics.Clear(Color.White);
            objGraphics.SmoothingMode = SmoothingMode.AntiAlias;
            objGraphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            objGraphics.DrawString(exampleText, objFont, new SolidBrush(Color.FromArgb(102, 102, 102)), 0, 0);
            objGraphics.Flush();


            
            bmp.Save(@"C:\image1.png", ImageFormat.Png);
            
            //KeyImage.Load();

            return (bmp);
        }
       
        

        public static void PrintOperation()
        {
            PrintOperation print = new PrintOperation();
            print.BeginPrint += (obj, args) => { print.NPages = 1; };
            print.DrawPage += (obj, args) =>
            {
                PrintContext context = args.Context;
                Cairo.Context cr = context.CairoContext;

                var imageSurface = new Cairo.ImageSurface("C:\\image1.png");

                int w = imageSurface.Width;
                int h = imageSurface.Height;
                cr.Scale(256.0 / w, 256.0 / h);
                cr.SetSourceSurface(imageSurface, 0, 0);
                cr.Paint();
            };
            print.EndPrint += (obj, args) => { };
            print.Run(PrintOperationAction.Print, null);
        }





    }
}