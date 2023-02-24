using System;

namespace KMod
{
	// Token: 0x02000D0C RID: 3340
	public struct FileSystemItem
	{
		// Token: 0x04004BD8 RID: 19416
		public string name;

		// Token: 0x04004BD9 RID: 19417
		public FileSystemItem.ItemType type;

		// Token: 0x02001B43 RID: 6979
		public enum ItemType
		{
			// Token: 0x04007B09 RID: 31497
			Directory,
			// Token: 0x04007B0A RID: 31498
			File
		}
	}
}
