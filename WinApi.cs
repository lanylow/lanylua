using System;
using System.Runtime.InteropServices;

namespace asm_exec {
  public static class WinApi {
    [StructLayout(LayoutKind.Sequential)]
    public struct SECURITY_ATTRIBUTES {
      public int length;
      public IntPtr securityDescriptor;
      public int inheritHandle;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct STARTUPINFO {
      public uint cb;
      public IntPtr reserved0;
      public IntPtr desktop;
      public IntPtr title;
      public uint x;
      public uint y;
      public uint xSize;
      public uint ySize;
      public uint xCountChars;
      public uint yCountChars;
      public uint fillAttributes;
      public uint flags;
      public ushort showWindow;
      public ushort reserved1;
      public IntPtr reserved2;
      public IntPtr stdIn;
      public IntPtr stdOut;
      public IntPtr stdErr;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PROCESS_INFORMATION {
      public IntPtr process;
      public IntPtr thread;
      public int processId;
      public int threadId;
    }

    [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
    public static extern bool CreateProcess(string applicationName, string commandLine, ref SECURITY_ATTRIBUTES processAttributes, ref SECURITY_ATTRIBUTES threadAttributes, bool inheritHandles, uint creationFlags, IntPtr environment, string currentDirectory, ref STARTUPINFO startupInfo, out PROCESS_INFORMATION processInformation);
  }
}
