using System;
using System.Threading;

namespace Power_Wallpaper
{
    /// <summary>
    /// Code from https://github.com/NeuroWhAI/YoutubeWallpaper/blob/master/YoutubeWallpaper/BehindDesktopIcon.cs
    /// </summary>
    public class BehindDesktopIcon
    {
        public static bool FixBehindDesktopIcon(IntPtr formHandle)
        {
            IntPtr progman = WinAPI.FindWindow("Progman", null);

            if (WinAPI.GetParent(formHandle) == progman)
                return true;

            if (progman == IntPtr.Zero)
                return false;


            IntPtr workerw = IntPtr.Zero;

            // 여러번 시도함.
            for (int step = 0; step < 8; ++step)
            {
                // 한번씩은 건너뜀.
                if (step % 2 == 0)
                {
                    IntPtr result = IntPtr.Zero;
                    WinAPI.SendMessageTimeout(progman,
                        0x052C,
                        new IntPtr(0),
                        IntPtr.Zero,
                        WinAPI.SendMessageTimeoutFlags.SMTO_NORMAL,
                        10000,
                        out result);
                }


                WinAPI.EnumWindows(new WinAPI.EnumWindowsProc((tophandle, topparamhandle) =>
                {
                    IntPtr p = WinAPI.FindWindowEx(tophandle,
                        IntPtr.Zero,
                        "SHELLDLL_DefView",
                        IntPtr.Zero);

                    if (p != IntPtr.Zero)
                    {
                        workerw = WinAPI.FindWindowEx(IntPtr.Zero,
                            tophandle,
                            "WorkerW",
                            IntPtr.Zero);
                    }

                    return true;
                }), IntPtr.Zero);


                if (workerw == IntPtr.Zero)
                {
                    Thread.Sleep(1000);
                }
                else
                {
                    break;
                }
            }

            if (workerw == IntPtr.Zero)
                return false;


            WinAPI.ShowWindow(workerw, 0/*HIDE*/);
            WinAPI.SetParent(formHandle, progman);


            return true;
        }
    }
}
