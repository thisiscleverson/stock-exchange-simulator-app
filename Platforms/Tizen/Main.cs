using System;
using Microsoft.Maui;
using Microsoft.Maui.Hosting;

namespace Stock_Exchange_Simulator;

class Program : MauiApplication
{
    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

    static void Main(string[] args)
    {
        var app = new Program();
        app.Run(args);
    }
}