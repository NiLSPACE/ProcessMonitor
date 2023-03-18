//using Microsoft.WindowsAPICodePack.Taskbar;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProcessMonitor
{
	public class Program
	{
		/// <summary>
		/// Logs the provided text with a timestamp as a prefix.
		/// </summary>
		/// <param name="text"></param>
		static void Log(string text)
		{
			var timestamp = DateTime.Now.AddHours(-16).ToString("HH:mm");
			Console.WriteLine($"[{timestamp}]: {text}");
		}

		public static void Main(string[] args)
		{
			var processName = args.ElementAtOrDefault(0);
			while (string.IsNullOrEmpty(processName))
			{
				Log("No process name was provided.");
				Log("What is the name of the process?");
				processName = Console.ReadLine();
			}

			Log($"Monitoring {processName}");

			bool processFound = false;
			while (true)
			{
				var process = FindProcess(processName);
				if (processFound && (process == null))
				{
					Log("Process not found.\a");
					Imports.FlashWindowEx();
					processFound = false;
				}
				else if (!processFound && (process != null))
				{
					Log("Process found.");
					Imports.StopFlash();
					processFound = true;
				}
				Thread.Sleep(5000);
			}
		}

		/// <summary>
		/// Finds a process by executable name or window title.
		/// </summary>
		/// <param name="processName"></param>
		/// <returns></returns>
		private static Process FindProcess(string processName)
		{
			return Process.GetProcesses().FirstOrDefault(x =>
				x.ProcessName.ToLower().Contains(processName.ToLower()) ||
				x.MainWindowTitle.ToLower().Contains(processName.ToLower())
			);
		}
	}
}
