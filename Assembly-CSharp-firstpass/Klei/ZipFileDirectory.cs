using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Ionic.Zip;

namespace Klei
{
	// Token: 0x02000514 RID: 1300
	public class ZipFileDirectory : IFileDirectory
	{
		// Token: 0x0600375D RID: 14173 RVA: 0x0007D0DA File Offset: 0x0007B2DA
		public string GetID()
		{
			return this.id;
		}

		// Token: 0x0600375E RID: 14174 RVA: 0x0007D0E2 File Offset: 0x0007B2E2
		public ZipFileDirectory(string id, ZipFile zipfile, string mount_point = "", bool isModded = false)
		{
			this.id = id;
			this.isModded = isModded;
			this.mountPoint = FileSystem.Normalize(mount_point);
			this.zipfile = zipfile;
		}

		// Token: 0x0600375F RID: 14175 RVA: 0x0007D10C File Offset: 0x0007B30C
		public ZipFileDirectory(string id, Stream zip_data_stream, string mount_point = "", bool isModded = false)
			: this(id, ZipFile.Read(zip_data_stream), mount_point, isModded)
		{
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06003760 RID: 14176 RVA: 0x0007D11E File Offset: 0x0007B31E
		public string MountPoint
		{
			get
			{
				return this.mountPoint;
			}
		}

		// Token: 0x06003761 RID: 14177 RVA: 0x0007D126 File Offset: 0x0007B326
		public string GetRoot()
		{
			return this.MountPoint;
		}

		// Token: 0x06003762 RID: 14178 RVA: 0x0007D130 File Offset: 0x0007B330
		public byte[] ReadBytes(string filename)
		{
			if (this.mountPoint.Length > 0)
			{
				filename = filename.Substring(this.mountPoint.Length);
			}
			ZipEntry zipEntry = this.zipfile[filename];
			if (zipEntry == null)
			{
				return null;
			}
			MemoryStream memoryStream = new MemoryStream();
			zipEntry.Extract(memoryStream);
			return memoryStream.ToArray();
		}

		// Token: 0x06003763 RID: 14179 RVA: 0x0007D184 File Offset: 0x0007B384
		public void GetFiles(Regex re, string path, ICollection<string> result)
		{
			if (this.zipfile.Count <= 0)
			{
				return;
			}
			foreach (ZipEntry zipEntry in this.zipfile.Entries)
			{
				if (!zipEntry.IsDirectory)
				{
					string text = FileSystem.Normalize(Path.Combine(this.mountPoint, zipEntry.FileName));
					if (re.IsMatch(text))
					{
						result.Add(text);
					}
				}
			}
		}

		// Token: 0x06003764 RID: 14180 RVA: 0x0007D210 File Offset: 0x0007B410
		public bool FileExists(string path)
		{
			if (this.mountPoint.Length > 0)
			{
				if (this.mountPoint.Length > path.Length)
				{
					Debug.LogError("Tried finding an invalid path inside a matching mount point!\n" + path + "\n" + this.mountPoint);
				}
				path = path.Substring(this.mountPoint.Length);
			}
			return this.zipfile.ContainsEntry(path);
		}

		// Token: 0x06003765 RID: 14181 RVA: 0x0007D278 File Offset: 0x0007B478
		public FileHandle FindFileHandle(string path)
		{
			if (this.FileExists(path))
			{
				if (this.mountPoint.Length > 0)
				{
					if (this.mountPoint.Length > path.Length)
					{
						Debug.LogError("Tried finding an invalid path inside a matching mount point!\n" + path + "\n" + this.mountPoint);
					}
					path = path.Substring(this.mountPoint.Length);
				}
				return new FileHandle
				{
					full_path = FileSystem.Normalize(Path.Combine(this.mountPoint, path)),
					source = this
				};
			}
			return default(FileHandle);
		}

		// Token: 0x06003766 RID: 14182 RVA: 0x0007D310 File Offset: 0x0007B510
		public bool IsModded()
		{
			return this.isModded;
		}

		// Token: 0x04001403 RID: 5123
		private string id;

		// Token: 0x04001404 RID: 5124
		private string mountPoint;

		// Token: 0x04001405 RID: 5125
		private ZipFile zipfile;

		// Token: 0x04001406 RID: 5126
		private bool isModded;
	}
}
