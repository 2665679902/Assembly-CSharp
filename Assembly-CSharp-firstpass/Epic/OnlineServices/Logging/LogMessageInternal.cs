using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Logging
{
	// Token: 0x0200071B RID: 1819
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LogMessageInternal : IDisposable
	{
		// Token: 0x170008AB RID: 2219
		// (get) Token: 0x06004479 RID: 17529 RVA: 0x0008C830 File Offset: 0x0008AA30
		public string Category
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_Category, out @default);
				return @default;
			}
		}

		// Token: 0x170008AC RID: 2220
		// (get) Token: 0x0600447A RID: 17530 RVA: 0x0008C854 File Offset: 0x0008AA54
		public string Message
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_Message, out @default);
				return @default;
			}
		}

		// Token: 0x170008AD RID: 2221
		// (get) Token: 0x0600447B RID: 17531 RVA: 0x0008C878 File Offset: 0x0008AA78
		public LogLevel Level
		{
			get
			{
				LogLevel @default = Helper.GetDefault<LogLevel>();
				Helper.TryMarshalGet<LogLevel>(this.m_Level, out @default);
				return @default;
			}
		}

		// Token: 0x0600447C RID: 17532 RVA: 0x0008C89A File Offset: 0x0008AA9A
		public void Dispose()
		{
		}

		// Token: 0x04001A79 RID: 6777
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_Category;

		// Token: 0x04001A7A RID: 6778
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_Message;

		// Token: 0x04001A7B RID: 6779
		private LogLevel m_Level;
	}
}
