# ProcessMonitor

This is a simple program that checks every five seconds if a process is still running. If it isn't it lights up in the taskbar and creates a sound. It only creates the sound once. The icon in the taskbar will keep flashing until the program is put on the foreground or the program finds the process again.


## How to use

If provided with a console argument it will use that as the process name. If your process contains spaces you have to put quotes around it.
```
processmonitor "Task Manager"
```
If no console arguments are provided the program will ask which process to look for.

The program will look in both the executable name as the title.