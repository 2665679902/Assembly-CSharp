using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Ionic.Zip;
using Klei;
using UnityEngine;

namespace KMod
{
	// Token: 0x02000D0F RID: 3343
	internal struct ZipFile : IFileSource
	{
		// Token: 0x06006761 RID: 26465 RVA: 0x0027E1B4 File Offset: 0x0027C3B4
		public ZipFile(string filename)
		{
			this.filename = filename;
			this.zipfile = ZipFile.Read(filename);
			this.file_system = new ZipFileDirectory(this.zipfile.Name, this.zipfile, Application.streamingAssetsPath, true);
		}

		// Token: 0x06006762 RID: 26466 RVA: 0x0027E1EB File Offset: 0x0027C3EB
		public string GetRoot()
		{
			return this.filename;
		}

		// Token: 0x06006763 RID: 26467 RVA: 0x0027E1F3 File Offset: 0x0027C3F3
		public bool Exists()
		{
			return File.Exists(this.GetRoot());
		}

		// Token: 0x06006764 RID: 26468 RVA: 0x0027E200 File Offset: 0x0027C400
		public bool Exists(string relative_path)
		{
			if (!this.Exists())
			{
				return false;
			}
			using (IEnumerator<ZipEntry> enumerator = this.zipfile.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (FileSystem.Normalize(enumerator.Current.FileName).StartsWith(relative_path))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06006765 RID: 26469 RVA: 0x0027E268 File Offset: 0x0027C468
		public void GetTopLevelItems(List<FileSystemItem> file_system_items, string relative_root)
		{
			HashSetPool<string, ZipFile>.PooledHashSet pooledHashSet = HashSetPool<string, ZipFile>.Allocate();
			string[] array;
			if (!string.IsNullOrEmpty(relative_root))
			{
				relative_root = relative_root ?? "";
				relative_root = FileSystem.Normalize(relative_root);
				array = relative_root.Split(new char[] { '/' });
			}
			else
			{
				array = new string[0];
			}
			foreach (ZipEntry zipEntry in this.zipfile)
			{
				List<string> list = (from part in FileSystem.Normalize(zipEntry.FileName).Split(new char[] { '/' })
					where !string.IsNullOrEmpty(part)
					select part).ToList<string>();
				if (this.IsSharedRoot(array, list))
				{
					list = list.GetRange(array.Length, list.Count - array.Length);
					if (list.Count != 0)
					{
						string text = list[0];
						if (pooledHashSet.Add(text))
						{
							file_system_items.Add(new FileSystemItem
							{
								name = text,
								type = ((1 < list.Count) ? FileSystemItem.ItemType.Directory : FileSystemItem.ItemType.File)
							});
						}
					}
				}
			}
			pooledHashSet.Recycle();
		}

		// Token: 0x06006766 RID: 26470 RVA: 0x0027E3A0 File Offset: 0x0027C5A0
		private bool IsSharedRoot(string[] root_path, List<string> check_path)
		{
			for (int i = 0; i < root_path.Length; i++)
			{
				if (i >= check_path.Count || root_path[i] != check_path[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06006767 RID: 26471 RVA: 0x0027E3D8 File Offset: 0x0027C5D8
		public IFileDirectory GetFileSystem()
		{
			return this.file_system;
		}

		// Token: 0x06006768 RID: 26472 RVA: 0x0027E3E0 File Offset: 0x0027C5E0
		public void CopyTo(string path, List<string> extensions = null)
		{
			foreach (ZipEntry zipEntry in this.zipfile.Entries)
			{
				bool flag = extensions == null || extensions.Count == 0;
				if (extensions != null)
				{
					foreach (string text in extensions)
					{
						if (zipEntry.FileName.ToLower().EndsWith(text))
						{
							flag = true;
							break;
						}
					}
				}
				if (flag)
				{
					string text2 = FileSystem.Normalize(Path.Combine(path, zipEntry.FileName));
					string directoryName = Path.GetDirectoryName(text2);
					if (string.IsNullOrEmpty(directoryName) || FileUtil.CreateDirectory(directoryName, 0))
					{
						using (MemoryStream memoryStream = new MemoryStream((int)zipEntry.UncompressedSize))
						{
							zipEntry.Extract(memoryStream);
							using (FileStream fileStream = FileUtil.Create(text2, 0))
							{
								fileStream.Write(memoryStream.GetBuffer(), 0, memoryStream.GetBuffer().Length);
							}
						}
					}
				}
			}
		}

		// Token: 0x06006769 RID: 26473 RVA: 0x0027E530 File Offset: 0x0027C730
		public string Read(string relative_path)
		{
			ICollection<ZipEntry> collection = this.zipfile.SelectEntries(relative_path);
			if (collection.Count == 0)
			{
				return string.Empty;
			}
			foreach (ZipEntry zipEntry in collection)
			{
				using (MemoryStream memoryStream = new MemoryStream((int)zipEntry.UncompressedSize))
				{
					zipEntry.Extract(memoryStream);
					return Encoding.UTF8.GetString(memoryStream.GetBuffer());
				}
			}
			return string.Empty;
		}

		// Token: 0x04004BDC RID: 19420
		private string filename;

		// Token: 0x04004BDD RID: 19421
		private ZipFile zipfile;

		// Token: 0x04004BDE RID: 19422
		private ZipFileDirectory file_system;
	}
}
