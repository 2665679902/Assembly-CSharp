using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Klei
{
	// Token: 0x02000513 RID: 1299
	public interface IFileDirectory
	{
		// Token: 0x06003756 RID: 14166
		string GetRoot();

		// Token: 0x06003757 RID: 14167
		byte[] ReadBytes(string filename);

		// Token: 0x06003758 RID: 14168
		void GetFiles(Regex re, string path, ICollection<string> result);

		// Token: 0x06003759 RID: 14169
		string GetID();

		// Token: 0x0600375A RID: 14170
		bool FileExists(string path);

		// Token: 0x0600375B RID: 14171
		FileHandle FindFileHandle(string filename);

		// Token: 0x0600375C RID: 14172
		bool IsModded();
	}
}
