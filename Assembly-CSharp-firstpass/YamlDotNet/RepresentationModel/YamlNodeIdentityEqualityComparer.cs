using System;
using System.Collections.Generic;

namespace YamlDotNet.RepresentationModel
{
	// Token: 0x020001E4 RID: 484
	public sealed class YamlNodeIdentityEqualityComparer : IEqualityComparer<YamlNode>
	{
		// Token: 0x06000EC0 RID: 3776 RVA: 0x0003C52E File Offset: 0x0003A72E
		public bool Equals(YamlNode x, YamlNode y)
		{
			return x == y;
		}

		// Token: 0x06000EC1 RID: 3777 RVA: 0x0003C534 File Offset: 0x0003A734
		public int GetHashCode(YamlNode obj)
		{
			return obj.GetHashCode();
		}
	}
}
