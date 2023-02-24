using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Klei
{
	// Token: 0x02000517 RID: 1303
	public class MemoryFileDirectory : IFileDirectory
	{
		// Token: 0x0600377A RID: 14202 RVA: 0x0007D598 File Offset: 0x0007B798
		public string GetID()
		{
			return this.id;
		}

		// Token: 0x0600377B RID: 14203 RVA: 0x0007D5A0 File Offset: 0x0007B7A0
		public MemoryFileDirectory(string id, string mount_point = "")
		{
			this.id = id;
			this.mountPoint = FileSystem.Normalize(mount_point);
		}

		// Token: 0x0600377C RID: 14204 RVA: 0x0007D5C6 File Offset: 0x0007B7C6
		public string GetRoot()
		{
			return this.mountPoint;
		}

		// Token: 0x0600377D RID: 14205 RVA: 0x0007D5D0 File Offset: 0x0007B7D0
		public byte[] ReadBytes(string filename)
		{
			byte[] array = null;
			this.dataMap.TryGetValue(filename, out array);
			return array;
		}

		// Token: 0x0600377E RID: 14206 RVA: 0x0007D5F0 File Offset: 0x0007B7F0
		private string GetFullFilename(string filename)
		{
			string text = FileSystem.Normalize(filename);
			return Path.Combine(this.mountPoint, text);
		}

		// Token: 0x0600377F RID: 14207 RVA: 0x0007D614 File Offset: 0x0007B814
		public void Map(string filename, byte[] data)
		{
			string fullFilename = this.GetFullFilename(filename);
			if (this.dataMap.ContainsKey(fullFilename))
			{
				throw new ArgumentException(string.Format("MemoryFileSystem: '{0}' is already mapped.", Array.Empty<object>()));
			}
			this.dataMap[fullFilename] = data;
		}

		// Token: 0x06003780 RID: 14208 RVA: 0x0007D65C File Offset: 0x0007B85C
		public void Unmap(string filename)
		{
			string fullFilename = this.GetFullFilename(filename);
			this.dataMap.Remove(fullFilename);
		}

		// Token: 0x06003781 RID: 14209 RVA: 0x0007D67E File Offset: 0x0007B87E
		public void Clear()
		{
			this.dataMap.Clear();
		}

		// Token: 0x06003782 RID: 14210 RVA: 0x0007D68C File Offset: 0x0007B88C
		public void GetFiles(Regex re, string path, ICollection<string> result)
		{
			foreach (string text in this.dataMap.Keys)
			{
				if (re.IsMatch(text))
				{
					result.Add(text);
				}
			}
		}

		// Token: 0x06003783 RID: 14211 RVA: 0x0007D6F0 File Offset: 0x0007B8F0
		public bool FileExists(string path)
		{
			return this.dataMap.ContainsKey(path);
		}

		// Token: 0x06003784 RID: 14212 RVA: 0x0007D700 File Offset: 0x0007B900
		public FileHandle FindFileHandle(string path)
		{
			if (this.FileExists(path))
			{
				return new FileHandle
				{
					full_path = path,
					source = this
				};
			}
			return default(FileHandle);
		}

		// Token: 0x06003785 RID: 14213 RVA: 0x0007D739 File Offset: 0x0007B939
		public bool IsModded()
		{
			return false;
		}

		// Token: 0x0400140C RID: 5132
		private string id;

		// Token: 0x0400140D RID: 5133
		private string mountPoint;

		// Token: 0x0400140E RID: 5134
		private Dictionary<string, byte[]> dataMap = new Dictionary<string, byte[]>();
	}
}
