using System;

namespace YamlDotNet.Core
{
	// Token: 0x02000203 RID: 515
	internal static class HashCode
	{
		// Token: 0x06000FE6 RID: 4070 RVA: 0x000402F2 File Offset: 0x0003E4F2
		public static int CombineHashCodes(int h1, int h2)
		{
			return ((h1 << 5) + h1) ^ h2;
		}
	}
}
