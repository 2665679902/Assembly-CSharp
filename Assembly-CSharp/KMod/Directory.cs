using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Klei;
using UnityEngine;

namespace KMod
{
	// Token: 0x02000D0E RID: 3342
	internal struct Directory : IFileSource
	{
		// Token: 0x06006758 RID: 26456 RVA: 0x0027DEB8 File Offset: 0x0027C0B8
		public Directory(string root)
		{
			this.root = root;
			this.file_system = new AliasDirectory(root, root, Application.streamingAssetsPath, true);
		}

		// Token: 0x06006759 RID: 26457 RVA: 0x0027DED4 File Offset: 0x0027C0D4
		public string GetRoot()
		{
			return this.root;
		}

		// Token: 0x0600675A RID: 26458 RVA: 0x0027DEDC File Offset: 0x0027C0DC
		public bool Exists()
		{
			return Directory.Exists(this.GetRoot());
		}

		// Token: 0x0600675B RID: 26459 RVA: 0x0027DEE9 File Offset: 0x0027C0E9
		public bool Exists(string relative_path)
		{
			return this.Exists() && new DirectoryInfo(FileSystem.Normalize(Path.Combine(this.root, relative_path))).Exists;
		}

		// Token: 0x0600675C RID: 26460 RVA: 0x0027DF10 File Offset: 0x0027C110
		public void GetTopLevelItems(List<FileSystemItem> file_system_items, string relative_root)
		{
			relative_root = relative_root ?? "";
			string text = FileSystem.Normalize(Path.Combine(this.root, relative_root));
			DirectoryInfo directoryInfo = new DirectoryInfo(text);
			if (!directoryInfo.Exists)
			{
				global::Debug.LogError("Cannot iterate over $" + text + ", this directory does not exist");
				return;
			}
			foreach (FileSystemInfo fileSystemInfo in directoryInfo.GetFileSystemInfos())
			{
				file_system_items.Add(new FileSystemItem
				{
					name = fileSystemInfo.Name,
					type = ((fileSystemInfo is DirectoryInfo) ? FileSystemItem.ItemType.Directory : FileSystemItem.ItemType.File)
				});
			}
		}

		// Token: 0x0600675D RID: 26461 RVA: 0x0027DFAC File Offset: 0x0027C1AC
		public IFileDirectory GetFileSystem()
		{
			return this.file_system;
		}

		// Token: 0x0600675E RID: 26462 RVA: 0x0027DFB4 File Offset: 0x0027C1B4
		public void CopyTo(string path, List<string> extensions = null)
		{
			try
			{
				Directory.CopyDirectory(this.root, path, extensions);
			}
			catch (UnauthorizedAccessException)
			{
				FileUtil.ErrorDialog(FileUtil.ErrorType.UnauthorizedAccess, path, null, null);
			}
			catch (IOException)
			{
				FileUtil.ErrorDialog(FileUtil.ErrorType.IOError, path, null, null);
			}
			catch
			{
				throw;
			}
		}

		// Token: 0x0600675F RID: 26463 RVA: 0x0027E014 File Offset: 0x0027C214
		public string Read(string relative_path)
		{
			string text;
			try
			{
				using (FileStream fileStream = File.OpenRead(Path.Combine(this.root, relative_path)))
				{
					byte[] array = new byte[fileStream.Length];
					fileStream.Read(array, 0, (int)fileStream.Length);
					text = Encoding.UTF8.GetString(array);
				}
			}
			catch
			{
				text = string.Empty;
			}
			return text;
		}

		// Token: 0x06006760 RID: 26464 RVA: 0x0027E090 File Offset: 0x0027C290
		private static int CopyDirectory(string sourceDirName, string destDirName, List<string> extensions)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(sourceDirName);
			if (!directoryInfo.Exists)
			{
				return 0;
			}
			if (!FileUtil.CreateDirectory(destDirName, 0))
			{
				return 0;
			}
			FileInfo[] files = directoryInfo.GetFiles();
			int num = 0;
			foreach (FileInfo fileInfo in files)
			{
				bool flag = extensions == null || extensions.Count == 0;
				if (extensions != null)
				{
					using (List<string>.Enumerator enumerator = extensions.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (enumerator.Current == Path.GetExtension(fileInfo.Name).ToLower())
							{
								flag = true;
								break;
							}
						}
					}
				}
				if (flag)
				{
					string text = Path.Combine(destDirName, fileInfo.Name);
					fileInfo.CopyTo(text, false);
					num++;
				}
			}
			foreach (DirectoryInfo directoryInfo2 in directoryInfo.GetDirectories())
			{
				string text2 = Path.Combine(destDirName, directoryInfo2.Name);
				num += Directory.CopyDirectory(directoryInfo2.FullName, text2, extensions);
			}
			if (num == 0)
			{
				FileUtil.DeleteDirectory(destDirName, 0);
			}
			return num;
		}

		// Token: 0x04004BDA RID: 19418
		private AliasDirectory file_system;

		// Token: 0x04004BDB RID: 19419
		private string root;
	}
}
