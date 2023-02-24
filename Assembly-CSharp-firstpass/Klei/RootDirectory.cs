using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Klei
{
	// Token: 0x02000516 RID: 1302
	public class RootDirectory : IFileDirectory
	{
		// Token: 0x06003771 RID: 14193 RVA: 0x0007D4BF File Offset: 0x0007B6BF
		public string GetID()
		{
			return this.id;
		}

		// Token: 0x06003772 RID: 14194 RVA: 0x0007D4C7 File Offset: 0x0007B6C7
		public string GetRoot()
		{
			return "";
		}

		// Token: 0x06003773 RID: 14195 RVA: 0x0007D4CE File Offset: 0x0007B6CE
		public byte[] ReadBytes(string filename)
		{
			return File.ReadAllBytes(filename);
		}

		// Token: 0x06003774 RID: 14196 RVA: 0x0007D4D8 File Offset: 0x0007B6D8
		public string ReadText(string filename)
		{
			byte[] array = this.ReadBytes(filename);
			return Encoding.UTF8.GetString(array);
		}

		// Token: 0x06003775 RID: 14197 RVA: 0x0007D4F8 File Offset: 0x0007B6F8
		public void GetFiles(Regex re, string path, ICollection<string> result)
		{
			if (!Directory.Exists(path))
			{
				return;
			}
			string[] files = Directory.GetFiles(path);
			for (int i = 0; i < files.Length; i++)
			{
				string text = FileSystem.Normalize(files[i]);
				if (re.IsMatch(text))
				{
					result.Add(text);
				}
			}
		}

		// Token: 0x06003776 RID: 14198 RVA: 0x0007D53C File Offset: 0x0007B73C
		public bool FileExists(string path)
		{
			return File.Exists(path);
		}

		// Token: 0x06003777 RID: 14199 RVA: 0x0007D544 File Offset: 0x0007B744
		public FileHandle FindFileHandle(string path)
		{
			if (this.FileExists(path))
			{
				return new FileHandle
				{
					full_path = FileSystem.Normalize(path),
					source = this
				};
			}
			return default(FileHandle);
		}

		// Token: 0x06003778 RID: 14200 RVA: 0x0007D582 File Offset: 0x0007B782
		public bool IsModded()
		{
			return false;
		}

		// Token: 0x0400140B RID: 5131
		private string id = "StandardFS";
	}
}
