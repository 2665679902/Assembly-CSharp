using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Klei
{
	// Token: 0x02000515 RID: 1301
	public class AliasDirectory : IFileDirectory
	{
		// Token: 0x06003767 RID: 14183 RVA: 0x0007D318 File Offset: 0x0007B518
		public string GetID()
		{
			return this.id;
		}

		// Token: 0x06003768 RID: 14184 RVA: 0x0007D320 File Offset: 0x0007B520
		public AliasDirectory(string id, string actual_location, string path_prefix, bool isModded = false)
		{
			this.id = id;
			actual_location = FileSystem.Normalize(actual_location);
			path_prefix = FileSystem.Normalize(path_prefix);
			this.isModded = isModded;
			this.root = actual_location;
			this.prefix = path_prefix;
		}

		// Token: 0x06003769 RID: 14185 RVA: 0x0007D358 File Offset: 0x0007B558
		private string GetActualPath(string filename)
		{
			if (filename.StartsWith(this.prefix))
			{
				string text = filename.Substring(this.prefix.Length);
				return FileSystem.Normalize(this.root + text);
			}
			return filename;
		}

		// Token: 0x0600376A RID: 14186 RVA: 0x0007D398 File Offset: 0x0007B598
		private string GetVirtualPath(string filename)
		{
			if (filename.StartsWith(this.root))
			{
				string text = filename.Substring(this.root.Length);
				return FileSystem.Normalize(this.prefix + text);
			}
			return filename;
		}

		// Token: 0x0600376B RID: 14187 RVA: 0x0007D3D8 File Offset: 0x0007B5D8
		public string GetRoot()
		{
			return this.root;
		}

		// Token: 0x0600376C RID: 14188 RVA: 0x0007D3E0 File Offset: 0x0007B5E0
		public byte[] ReadBytes(string src_filename)
		{
			string actualPath = this.GetActualPath(src_filename);
			if (!File.Exists(actualPath))
			{
				return null;
			}
			return File.ReadAllBytes(actualPath);
		}

		// Token: 0x0600376D RID: 14189 RVA: 0x0007D408 File Offset: 0x0007B608
		public void GetFiles(Regex re, string src_path, ICollection<string> result)
		{
			string actualPath = this.GetActualPath(src_path);
			if (!Directory.Exists(actualPath))
			{
				return;
			}
			string[] files = Directory.GetFiles(actualPath);
			for (int i = 0; i < files.Length; i++)
			{
				string text = FileSystem.Normalize(files[i]);
				string virtualPath = this.GetVirtualPath(text);
				if (re.IsMatch(virtualPath))
				{
					result.Add(virtualPath);
				}
			}
		}

		// Token: 0x0600376E RID: 14190 RVA: 0x0007D45F File Offset: 0x0007B65F
		public bool FileExists(string path)
		{
			return File.Exists(this.GetActualPath(path));
		}

		// Token: 0x0600376F RID: 14191 RVA: 0x0007D470 File Offset: 0x0007B670
		public FileHandle FindFileHandle(string path)
		{
			if (this.FileExists(path))
			{
				path = this.GetVirtualPath(FileSystem.Normalize(path));
				return new FileHandle
				{
					full_path = path,
					source = this
				};
			}
			return default(FileHandle);
		}

		// Token: 0x06003770 RID: 14192 RVA: 0x0007D4B7 File Offset: 0x0007B6B7
		public bool IsModded()
		{
			return this.isModded;
		}

		// Token: 0x04001407 RID: 5127
		private string id;

		// Token: 0x04001408 RID: 5128
		private string root;

		// Token: 0x04001409 RID: 5129
		private string prefix;

		// Token: 0x0400140A RID: 5130
		private bool isModded;
	}
}
