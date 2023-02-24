using System;
using YamlDotNet.Serialization.Utilities;

namespace YamlDotNet.Serialization.NamingConventions
{
	// Token: 0x020001D5 RID: 469
	public sealed class UnderscoredNamingConvention : INamingConvention
	{
		// Token: 0x06000E43 RID: 3651 RVA: 0x0003B143 File Offset: 0x00039343
		public string Apply(string value)
		{
			return value.FromCamelCase("_");
		}
	}
}
