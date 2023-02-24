using System;
using YamlDotNet.Serialization.Utilities;

namespace YamlDotNet.Serialization.NamingConventions
{
	// Token: 0x020001D2 RID: 466
	public sealed class HyphenatedNamingConvention : INamingConvention
	{
		// Token: 0x06000E3D RID: 3645 RVA: 0x0003B113 File Offset: 0x00039313
		public string Apply(string value)
		{
			return value.FromCamelCase("-");
		}
	}
}
