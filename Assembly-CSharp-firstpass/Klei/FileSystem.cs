using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Klei
{
	// Token: 0x02000519 RID: 1305
	public static class FileSystem
	{
		// Token: 0x06003786 RID: 14214 RVA: 0x0007D73C File Offset: 0x0007B93C
		public static void Initialize()
		{
			if (FileSystem.file_sources.Count == 0)
			{
				FileSystem.file_sources.Add(new RootDirectory());
			}
		}

		// Token: 0x06003787 RID: 14215 RVA: 0x0007D75C File Offset: 0x0007B95C
		public static byte[] ReadBytes(string filename)
		{
			FileSystem.Initialize();
			foreach (IFileDirectory fileDirectory in FileSystem.file_sources)
			{
				byte[] array = fileDirectory.ReadBytes(filename);
				if (array != null)
				{
					return array;
				}
			}
			return null;
		}

		// Token: 0x06003788 RID: 14216 RVA: 0x0007D7BC File Offset: 0x0007B9BC
		public static FileHandle FindFileHandle(string filename)
		{
			FileSystem.Initialize();
			foreach (IFileDirectory fileDirectory in FileSystem.file_sources)
			{
				if (fileDirectory.FileExists(filename))
				{
					return fileDirectory.FindFileHandle(filename);
				}
			}
			return default(FileHandle);
		}

		// Token: 0x06003789 RID: 14217 RVA: 0x0007D82C File Offset: 0x0007BA2C
		public static void GetFiles(Regex re, string path, ICollection<FileHandle> result)
		{
			FileSystem.Initialize();
			ListPool<string, IFileDirectory>.PooledList pooledList = ListPool<string, IFileDirectory>.Allocate();
			foreach (IFileDirectory fileDirectory in FileSystem.file_sources)
			{
				pooledList.Clear();
				fileDirectory.GetFiles(re, path, pooledList);
				foreach (string text in pooledList)
				{
					result.Add(new FileHandle
					{
						full_path = text,
						source = fileDirectory
					});
				}
			}
			pooledList.Recycle();
		}

		// Token: 0x0600378A RID: 14218 RVA: 0x0007D8F0 File Offset: 0x0007BAF0
		public static void GetFiles(Regex re, string path, ICollection<string> result)
		{
			FileSystem.Initialize();
			foreach (IFileDirectory fileDirectory in FileSystem.file_sources)
			{
				fileDirectory.GetFiles(re, path, result);
			}
		}

		// Token: 0x0600378B RID: 14219 RVA: 0x0007D948 File Offset: 0x0007BB48
		public static void GetFiles(string path, string filename_glob_pattern, ICollection<string> result)
		{
			string text;
			Regex regex;
			FileSystem.GetFilesSearchParams(path, filename_glob_pattern, out text, out regex);
			FileSystem.GetFiles(regex, text, result);
		}

		// Token: 0x0600378C RID: 14220 RVA: 0x0007D968 File Offset: 0x0007BB68
		public static void GetFiles(string path, string filename_glob_pattern, ICollection<FileHandle> result)
		{
			string text;
			Regex regex;
			FileSystem.GetFilesSearchParams(path, filename_glob_pattern, out text, out regex);
			FileSystem.GetFiles(regex, text, result);
		}

		// Token: 0x0600378D RID: 14221 RVA: 0x0007D988 File Offset: 0x0007BB88
		public static void GetFiles(string filename, ICollection<FileHandle> result)
		{
			string text;
			Regex regex;
			FileSystem.GetFilesSearchParams(Path.GetDirectoryName(filename), Path.GetFileName(filename), out text, out regex);
			FileSystem.GetFiles(regex, text, result);
		}

		// Token: 0x0600378E RID: 14222 RVA: 0x0007D9B4 File Offset: 0x0007BBB4
		public static bool FileExists(string path)
		{
			FileSystem.Initialize();
			using (List<IFileDirectory>.Enumerator enumerator = FileSystem.file_sources.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.FileExists(path))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600378F RID: 14223 RVA: 0x0007DA14 File Offset: 0x0007BC14
		public static void ReadFiles(string filename, ICollection<byte[]> result)
		{
			FileSystem.Initialize();
			foreach (IFileDirectory fileDirectory in FileSystem.file_sources)
			{
				byte[] array = fileDirectory.ReadBytes(filename);
				if (array != null)
				{
					result.Add(array);
				}
			}
		}

		// Token: 0x06003790 RID: 14224 RVA: 0x0007DA74 File Offset: 0x0007BC74
		public static bool IsModdedFile(string filename)
		{
			foreach (IFileDirectory fileDirectory in FileSystem.file_sources)
			{
				if (fileDirectory.FileExists(filename))
				{
					return fileDirectory.IsModded();
				}
			}
			return false;
		}

		// Token: 0x06003791 RID: 14225 RVA: 0x0007DAD4 File Offset: 0x0007BCD4
		public static string ConvertToText(byte[] bytes)
		{
			return Encoding.UTF8.GetString(bytes);
		}

		// Token: 0x06003792 RID: 14226 RVA: 0x0007DAE1 File Offset: 0x0007BCE1
		public static string Normalize(string filename)
		{
			return filename.Replace("\\", "/");
		}

		// Token: 0x06003793 RID: 14227 RVA: 0x0007DAF3 File Offset: 0x0007BCF3
		public static string CombineAndNormalize(params string[] paths)
		{
			return FileSystem.Normalize(Path.Combine(paths));
		}

		// Token: 0x06003794 RID: 14228 RVA: 0x0007DB00 File Offset: 0x0007BD00
		private static void GetFilesSearchParams(string path, string filename_glob_pattern, out string normalized_path, out Regex filename_regex)
		{
			normalized_path = null;
			filename_regex = null;
			int num = path.Length - 1;
			while ((num >= 0 && path[num] == '\\') || path[num] == '/')
			{
				num--;
			}
			if (num < 0)
			{
				return;
			}
			if (num < path.Length - 1)
			{
				path = path.Substring(0, num + 1);
			}
			normalized_path = (path = FileSystem.Normalize(path));
			string text = filename_glob_pattern.Replace(".", "\\.").Replace("*", ".*");
			string text2 = Regex.Escape(path);
			text2 = text2 + "/" + text + "$";
			filename_regex = new Regex(text2);
		}

		// Token: 0x06003795 RID: 14229 RVA: 0x0007DBA4 File Offset: 0x0007BDA4
		[Conditional("UNITY_EDITOR_WIN")]
		public static void CheckForCaseSensitiveErrors(string filename)
		{
		}

		// Token: 0x04001411 RID: 5137
		public static List<IFileDirectory> file_sources = new List<IFileDirectory>();
	}
}
