using System;
using System.Runtime.InteropServices;

namespace ServiceGenius.App.Interop;

internal static partial class Win32
{  
    [LibraryImport("user32.dll")]
    internal static partial IntPtr GetActiveWindow();
}
