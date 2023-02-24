using System;
using System.Runtime.InteropServices;
using rail;

// Token: 0x0200013A RID: 314
// (Invoke) Token: 0x06000A94 RID: 2708
[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate void RailCaptureVoiceCallback(EnumRailVoiceCaptureFormat fmt, bool is_last_package, IntPtr encoded_buffer, uint encoded_length);
