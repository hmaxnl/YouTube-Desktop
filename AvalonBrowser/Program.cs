using System;
using System.Diagnostics;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;

namespace AvalonBrowser
{
    class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Trace.WriteLine("This action is not permitted!");
        }
    }
}