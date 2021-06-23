using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using CefNet;

namespace YouTubeGUI.Cef
{
    public class CefApplication : CefNetApplication
    {
        protected override void OnBeforeCommandLineProcessing(string processType, CefCommandLine commandLine)
        {
            base.OnBeforeCommandLineProcessing(processType, commandLine);

            Trace.WriteLine("ChromiumWebBrowser_OnBeforeCommandLineProcessing");
            Trace.WriteLine(commandLine.CommandLineString);

            #if DEBUG
            commandLine.AppendSwitch("show-fps-counter");
            #endif
            
            commandLine.AppendSwitchWithValue("remote-debugging-port", "9222");
            commandLine.AppendSwitch("off-screen-rendering-enabled");
            commandLine.AppendSwitchWithValue("off-screen-frame-rate", "30");
			
            //enable-devtools-experiments
            commandLine.AppendSwitch("enable-devtools-experiments");

            //("force-device-scale-factor", "1");

            commandLine.AppendSwitch("enable-begin-frame-scheduling");
            commandLine.AppendSwitch("enable-media-stream");

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                commandLine.AppendSwitch("no-zygote");
                commandLine.AppendSwitch("no-sandbox");
            }
            Trace.WriteLine($"Updated commandline: {commandLine.CommandLineString}");
        }

        protected override void OnContextCreated(CefBrowser browser, CefFrame frame, CefV8Context context)
        {
            base.OnContextCreated(browser, frame, context);
            frame.ExecuteJavaScript(@"
                {
                const newProto = navigator.__proto__;
                delete newProto.webdriver;
                navigator.__proto__ = newProto;
                }", frame.Url, 0);
        }

        public Action<long> ScheduleMessagePumpWorkCallback { get; set; } = null!;

        protected override void OnScheduleMessagePumpWork(long delayMs)
        {
            ScheduleMessagePumpWorkCallback(delayMs);
        }
    }
}