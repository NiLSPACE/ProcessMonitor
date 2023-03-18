using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProcessMonitor
{
	internal class Imports
	{
		// To support flashing.
		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		static extern bool FlashWindowEx(ref FLASHWINFO pwfi);

		[StructLayout(LayoutKind.Sequential)]
		public struct FLASHWINFO
		{
			public UInt32 cbSize;
			public IntPtr hwnd;
			public UInt32 dwFlags;
			public UInt32 uCount;
			public UInt32 dwTimeout;
		}

		// stop flashing
		const int FLASHW_STOP = 0;

		// flash the window title
		const int FLASHW_CAPTION = 1;

		// flash the taskbar button
		const int FLASHW_TRAY = 2;

		const int FLASHW_ALL = 3;

		// flash continuously
		const int FLASHW_TIMER = 4;

		// flash until the window comes to the foreground
		const int FLASHW_TIMERNOFG = 12;




		// Do the flashing - this does not involve a raincoat.
		public static bool FlashWindowEx()
		{
			IntPtr hWnd = Process.GetCurrentProcess().MainWindowHandle;
			FLASHWINFO fInfo = new FLASHWINFO();

			fInfo.cbSize = Convert.ToUInt32(Marshal.SizeOf(fInfo));
			fInfo.hwnd = hWnd;
			fInfo.dwFlags = FLASHW_ALL | FLASHW_TIMERNOFG;
			fInfo.uCount = UInt32.MaxValue;
			fInfo.dwTimeout = 0;

			return FlashWindowEx(ref fInfo);
		}

		public static bool StopFlash()
		{
			IntPtr hWnd = Process.GetCurrentProcess().MainWindowHandle;
			FLASHWINFO fInfo = new FLASHWINFO();

			fInfo.cbSize = Convert.ToUInt32(Marshal.SizeOf(fInfo));
			fInfo.hwnd = hWnd;
			fInfo.dwFlags = 0;
			fInfo.uCount = UInt32.MaxValue;
			fInfo.dwTimeout = 0;

			return FlashWindowEx(ref fInfo);
		}
	}
}
