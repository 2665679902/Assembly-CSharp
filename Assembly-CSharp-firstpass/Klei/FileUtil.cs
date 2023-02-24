using System;
using System.IO;
using System.Threading;

namespace Klei
{
	// Token: 0x0200051A RID: 1306
	public static class FileUtil
	{
		// Token: 0x14000028 RID: 40
		// (add) Token: 0x06003797 RID: 14231 RVA: 0x0007DBB4 File Offset: 0x0007BDB4
		// (remove) Token: 0x06003798 RID: 14232 RVA: 0x0007DBE8 File Offset: 0x0007BDE8
		public static event System.Action onErrorMessage;

		// Token: 0x06003799 RID: 14233 RVA: 0x0007DC1C File Offset: 0x0007BE1C
		public static void ErrorDialog(FileUtil.ErrorType errorType, string errorSubject, string exceptionMessage, string exceptionStackTrace)
		{
			Debug.Log(string.Format("Error encountered during file access: {0} error: {1}", errorType, errorSubject));
			FileUtil.errorType = errorType;
			FileUtil.errorSubject = errorSubject;
			FileUtil.exceptionMessage = exceptionMessage;
			FileUtil.exceptionStackTrace = exceptionStackTrace;
			if (FileUtil.onErrorMessage != null)
			{
				FileUtil.onErrorMessage();
			}
		}

		// Token: 0x0600379A RID: 14234 RVA: 0x0007DC68 File Offset: 0x0007BE68
		public static T DoIOFunc<T>(Func<T> io_op, int retry_count = 0)
		{
			UnauthorizedAccessException ex = null;
			IOException ex2 = null;
			Exception ex3 = null;
			for (int i = 0; i <= retry_count; i++)
			{
				try
				{
					return io_op();
				}
				catch (UnauthorizedAccessException ex)
				{
				}
				catch (IOException ex2)
				{
				}
				catch (Exception ex3)
				{
				}
				Thread.Sleep(i * 100);
			}
			if (ex != null)
			{
				throw ex;
			}
			if (ex2 != null)
			{
				throw ex2;
			}
			if (ex3 != null)
			{
				throw ex3;
			}
			throw new Exception("Unreachable code path in FileUtil::DoIOFunc()");
		}

		// Token: 0x0600379B RID: 14235 RVA: 0x0007DCE8 File Offset: 0x0007BEE8
		public static void DoIOAction(System.Action io_op, int retry_count = 0)
		{
			UnauthorizedAccessException ex = null;
			IOException ex2 = null;
			Exception ex3 = null;
			for (int i = 0; i <= retry_count; i++)
			{
				try
				{
					io_op();
					return;
				}
				catch (UnauthorizedAccessException ex)
				{
				}
				catch (IOException ex2)
				{
				}
				catch (Exception ex3)
				{
				}
				Thread.Sleep(i * 100);
			}
			if (ex != null)
			{
				throw ex;
			}
			if (ex2 != null)
			{
				throw ex2;
			}
			if (ex3 != null)
			{
				throw ex3;
			}
			throw new Exception("Unreachable code path in FileUtil::DoIOAction()");
		}

		// Token: 0x0600379C RID: 14236 RVA: 0x0007DD64 File Offset: 0x0007BF64
		public static void DoIODialog(System.Action io_op, string io_subject, int retry_count = 0)
		{
			try
			{
				FileUtil.DoIOAction(io_op, retry_count);
			}
			catch (UnauthorizedAccessException ex)
			{
				DebugUtil.LogArgs(new object[] { "UnauthorizedAccessException during IO on ", io_subject, ", squelching. Stack trace was:\n", ex.Message, "\n", ex.StackTrace });
				FileUtil.ErrorDialog(FileUtil.ErrorType.UnauthorizedAccess, io_subject, ex.Message, ex.StackTrace);
			}
			catch (IOException ex2)
			{
				DebugUtil.LogArgs(new object[] { "IOException during IO on ", io_subject, ", squelching. Stack trace was:\n", ex2.Message, "\n", ex2.StackTrace });
				FileUtil.ErrorDialog(FileUtil.ErrorType.IOError, io_subject, ex2.Message, ex2.StackTrace);
			}
			catch
			{
				throw;
			}
		}

		// Token: 0x0600379D RID: 14237 RVA: 0x0007DE48 File Offset: 0x0007C048
		public static T DoIODialog<T>(Func<T> io_op, string io_subject, T fail_result, int retry_count = 0)
		{
			try
			{
				return FileUtil.DoIOFunc<T>(io_op, retry_count);
			}
			catch (UnauthorizedAccessException ex)
			{
				DebugUtil.LogArgs(new object[] { "UnauthorizedAccessException during IO on ", io_subject, ", squelching. Stack trace was:\n", ex.Message, "\n", ex.StackTrace });
				FileUtil.ErrorDialog(FileUtil.ErrorType.IOError, io_subject, ex.Message, ex.StackTrace);
			}
			catch (IOException ex2)
			{
				DebugUtil.LogArgs(new object[] { "IOException during IO on ", io_subject, ", squelching. Stack trace was:\n", ex2.Message, "\n", ex2.StackTrace });
				FileUtil.ErrorDialog(FileUtil.ErrorType.IOError, io_subject, ex2.Message, ex2.StackTrace);
			}
			catch
			{
				throw;
			}
			return fail_result;
		}

		// Token: 0x0600379E RID: 14238 RVA: 0x0007DF30 File Offset: 0x0007C130
		public static FileStream Create(string filename, int retry_count = 0)
		{
			return FileUtil.DoIODialog<FileStream>(() => File.Create(filename), filename, null, retry_count);
		}

		// Token: 0x0600379F RID: 14239 RVA: 0x0007DF64 File Offset: 0x0007C164
		public static bool CreateDirectory(string path, int retry_count = 0)
		{
			return FileUtil.DoIODialog<bool>(delegate
			{
				if (!Directory.Exists(path))
				{
					Directory.CreateDirectory(path);
				}
				return true;
			}, path, false, retry_count);
		}

		// Token: 0x060037A0 RID: 14240 RVA: 0x0007DF98 File Offset: 0x0007C198
		public static bool DeleteDirectory(string path, int retry_count = 0)
		{
			return FileUtil.DoIODialog<bool>(delegate
			{
				if (!Directory.Exists(path))
				{
					return true;
				}
				Directory.Delete(path, true);
				return true;
			}, path, false, retry_count);
		}

		// Token: 0x060037A1 RID: 14241 RVA: 0x0007DFCC File Offset: 0x0007C1CC
		public static bool FileExists(string filename, int retry_count = 0)
		{
			return FileUtil.DoIODialog<bool>(() => File.Exists(filename), filename, false, retry_count);
		}

		// Token: 0x04001412 RID: 5138
		private const FileUtil.Test TEST = FileUtil.Test.NoTesting;

		// Token: 0x04001413 RID: 5139
		private const int DEFAULT_RETRY_COUNT = 0;

		// Token: 0x04001414 RID: 5140
		private const int RETRY_MILLISECONDS = 100;

		// Token: 0x04001415 RID: 5141
		public static FileUtil.ErrorType errorType;

		// Token: 0x04001416 RID: 5142
		public static string errorSubject;

		// Token: 0x04001417 RID: 5143
		public static string exceptionMessage;

		// Token: 0x04001418 RID: 5144
		public static string exceptionStackTrace;

		// Token: 0x02000B22 RID: 2850
		private enum Test
		{
			// Token: 0x0400263F RID: 9791
			NoTesting,
			// Token: 0x04002640 RID: 9792
			RetryOnce
		}

		// Token: 0x02000B23 RID: 2851
		public enum ErrorType
		{
			// Token: 0x04002642 RID: 9794
			None,
			// Token: 0x04002643 RID: 9795
			UnauthorizedAccess,
			// Token: 0x04002644 RID: 9796
			IOError
		}
	}
}
