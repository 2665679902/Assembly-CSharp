using System;
using System.Collections.Generic;
using System.IO;
using YamlDotNet.Serialization;

namespace Klei
{
	// Token: 0x0200051D RID: 1309
	public static class YamlIO
	{
		// Token: 0x060037D6 RID: 14294 RVA: 0x0007E3A0 File Offset: 0x0007C5A0
		public static void Save<T>(T some_object, string filename, List<global::Tuple<string, Type>> tagMappings = null)
		{
			using (StreamWriter streamWriter = new StreamWriter(filename))
			{
				SerializerBuilder serializerBuilder = new SerializerBuilder();
				if (tagMappings != null)
				{
					foreach (global::Tuple<string, Type> tuple in tagMappings)
					{
						serializerBuilder = serializerBuilder.WithTagMapping(tuple.first, tuple.second);
					}
				}
				serializerBuilder.Build().Serialize(streamWriter, some_object);
			}
		}

		// Token: 0x060037D7 RID: 14295 RVA: 0x0007E434 File Offset: 0x0007C634
		public static void SaveOrWarnUser<T>(T some_object, string filename, List<global::Tuple<string, Type>> tagMappings = null)
		{
			FileUtil.DoIODialog(delegate
			{
				YamlIO.Save<T>(some_object, filename, tagMappings);
			}, filename, 0);
		}

		// Token: 0x060037D8 RID: 14296 RVA: 0x0007E474 File Offset: 0x0007C674
		public static T LoadFile<T>(FileHandle filehandle, YamlIO.ErrorHandler handle_error = null, List<global::Tuple<string, Type>> tagMappings = null)
		{
			return YamlIO.Parse<T>(FileSystem.ConvertToText(filehandle.source.ReadBytes(filehandle.full_path)), filehandle, handle_error, tagMappings);
		}

		// Token: 0x060037D9 RID: 14297 RVA: 0x0007E494 File Offset: 0x0007C694
		public static T LoadFile<T>(string filename, YamlIO.ErrorHandler handle_error = null, List<global::Tuple<string, Type>> tagMappings = null)
		{
			FileHandle fileHandle = FileSystem.FindFileHandle(filename);
			if (fileHandle.source == null)
			{
				throw new FileNotFoundException("YamlIO tried loading a file that doesn't exist: " + filename);
			}
			return YamlIO.LoadFile<T>(fileHandle, handle_error, tagMappings);
		}

		// Token: 0x060037DA RID: 14298 RVA: 0x0007E4BC File Offset: 0x0007C6BC
		public static void LogError(YamlIO.Error error, bool force_log_as_warning)
		{
			YamlIO.ErrorLogger errorLogger = ((force_log_as_warning || error.severity == YamlIO.Error.Severity.Recoverable) ? new YamlIO.ErrorLogger(Debug.LogWarningFormat) : new YamlIO.ErrorLogger(Debug.LogErrorFormat));
			if (error.inner_exception == null)
			{
				errorLogger("{0} parse error in {1}\n{2}", new object[]
				{
					error.severity,
					error.file.full_path,
					error.message
				});
				return;
			}
			errorLogger("{0} parse error in {1}\n{2}\n{3}", new object[]
			{
				error.severity,
				error.file.full_path,
				error.message,
				error.inner_exception.Message
			});
		}

		// Token: 0x060037DB RID: 14299 RVA: 0x0007E574 File Offset: 0x0007C774
		public static T Parse<T>(string readText, FileHandle debugFileHandle, YamlIO.ErrorHandler handle_error = null, List<global::Tuple<string, Type>> tagMappings = null)
		{
			try
			{
				if (handle_error == null)
				{
					handle_error = new YamlIO.ErrorHandler(YamlIO.LogError);
				}
				readText = readText.Replace("\t", "    ");
				Action<string> action = delegate(string error)
				{
					handle_error(new YamlIO.Error
					{
						file = debugFileHandle,
						text = readText,
						message = error,
						severity = YamlIO.Error.Severity.Recoverable
					}, false);
				};
				DeserializerBuilder deserializerBuilder = new DeserializerBuilder();
				deserializerBuilder.IgnoreUnmatchedProperties(action);
				if (tagMappings != null)
				{
					foreach (global::Tuple<string, Type> tuple in tagMappings)
					{
						deserializerBuilder = deserializerBuilder.WithTagMapping(tuple.first, tuple.second);
					}
				}
				Deserializer deserializer = deserializerBuilder.Build();
				StringReader stringReader = new StringReader(readText);
				return deserializer.Deserialize<T>(stringReader);
			}
			catch (Exception ex)
			{
				handle_error(new YamlIO.Error
				{
					file = debugFileHandle,
					text = readText,
					message = ex.Message,
					inner_exception = ex.InnerException,
					severity = YamlIO.Error.Severity.Fatal
				}, false);
			}
			return default(T);
		}

		// Token: 0x04001432 RID: 5170
		private const bool verbose_errors = false;

		// Token: 0x02000B29 RID: 2857
		public struct Error
		{
			// Token: 0x0400264C RID: 9804
			public FileHandle file;

			// Token: 0x0400264D RID: 9805
			public string message;

			// Token: 0x0400264E RID: 9806
			public Exception inner_exception;

			// Token: 0x0400264F RID: 9807
			public string text;

			// Token: 0x04002650 RID: 9808
			public YamlIO.Error.Severity severity;

			// Token: 0x02000B57 RID: 2903
			public enum Severity
			{
				// Token: 0x040026C7 RID: 9927
				Fatal,
				// Token: 0x040026C8 RID: 9928
				Recoverable
			}
		}

		// Token: 0x02000B2A RID: 2858
		// (Invoke) Token: 0x06005874 RID: 22644
		public delegate void ErrorHandler(YamlIO.Error error, bool force_log_as_warning);

		// Token: 0x02000B2B RID: 2859
		// (Invoke) Token: 0x06005878 RID: 22648
		private delegate void ErrorLogger(string format, params object[] args);
	}
}
