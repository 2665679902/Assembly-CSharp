using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Logging
{
	// Token: 0x0200071E RID: 1822
	public static class LoggingInterface
	{
		// Token: 0x06004485 RID: 17541 RVA: 0x0008C89C File Offset: 0x0008AA9C
		public static Result SetCallback(LogMessageFunc callback)
		{
			LogMessageFuncInternal logMessageFuncInternal = new LogMessageFuncInternal(LoggingInterface.LogMessageFunc);
			LoggingInterface.s_LogMessageFunc = callback;
			LoggingInterface.s_LogMessageFuncInternal = logMessageFuncInternal;
			Result result = LoggingInterface.EOS_Logging_SetCallback(logMessageFuncInternal);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06004486 RID: 17542 RVA: 0x0008C8D8 File Offset: 0x0008AAD8
		public static Result SetLogLevel(LogCategory logCategory, LogLevel logLevel)
		{
			Result result = LoggingInterface.EOS_Logging_SetLogLevel(logCategory, logLevel);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06004487 RID: 17543 RVA: 0x0008C8FC File Offset: 0x0008AAFC
		[MonoPInvokeCallback]
		internal static void LogMessageFunc(IntPtr address)
		{
			LogMessage logMessage = null;
			if (Helper.TryMarshalGet<LogMessageInternal, LogMessage>(address, out logMessage))
			{
				LoggingInterface.s_LogMessageFunc(logMessage);
			}
		}

		// Token: 0x06004488 RID: 17544
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_Logging_SetLogLevel(LogCategory logCategory, LogLevel logLevel);

		// Token: 0x06004489 RID: 17545
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_Logging_SetCallback(LogMessageFuncInternal callback);

		// Token: 0x04001A7C RID: 6780
		private static LogMessageFuncInternal s_LogMessageFuncInternal;

		// Token: 0x04001A7D RID: 6781
		private static LogMessageFunc s_LogMessageFunc;
	}
}
