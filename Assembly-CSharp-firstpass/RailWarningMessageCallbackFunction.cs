using System;
using System.Runtime.InteropServices;

// Token: 0x02000138 RID: 312
// (Invoke) Token: 0x06000A8C RID: 2700
[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate void RailWarningMessageCallbackFunction(int level, string msg);
