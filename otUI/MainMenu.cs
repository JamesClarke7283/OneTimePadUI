﻿using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

//[UI] Gtk.TextView tv = new Gtk.TextView();

public partial class MainMenu : Gtk.Window
{
    Builder builder;

    public static MainMenu Create()
    {
        Builder builder = new Builder(null, "otUI.interfaces.MainWindow.glade", null);
        return new MainMenu(builder, builder.GetObject("mainmenu").Handle);
    }

    protected MainMenu(Builder builder, IntPtr handle) : base(handle)
    {
        this.builder = builder;

        builder.Autoconnect(this);
        DeleteEvent += OnDeleteEvent;
    }

    protected void OnKeygenClicked(object sender, EventArgs e)
    {
        //Hide();
        GenerateKeys gk = GenerateKeys.Create();
        gk.Run();
        gk.Destroy();
    }

    protected void OnCryptoClicked(object sender, EventArgs e)
    {
        //Hide();
        CryptDialog cd = CryptDialog.Create();
        cd.Run();
        cd.Destroy();
    }



    protected void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        Application.Quit();
        a.RetVal = true;
    }
}
