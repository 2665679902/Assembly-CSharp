using System;
using System.Collections.Generic;
using Klei;

namespace KMod
{
	// Token: 0x02000D0D RID: 3341
	public interface IFileSource
	{
		// Token: 0x06006751 RID: 26449
		string GetRoot();

		// Token: 0x06006752 RID: 26450
		bool Exists();

		// Token: 0x06006753 RID: 26451
		bool Exists(string relative_path);

		// Token: 0x06006754 RID: 26452
		void GetTopLevelItems(List<FileSystemItem> file_system_items, string relative_root = "");

		// Token: 0x06006755 RID: 26453
		IFileDirectory GetFileSystem();

		// Token: 0x06006756 RID: 26454
		void CopyTo(string path, List<string> extensions = null);

		// Token: 0x06006757 RID: 26455
		string Read(string relative_path);
	}
}
