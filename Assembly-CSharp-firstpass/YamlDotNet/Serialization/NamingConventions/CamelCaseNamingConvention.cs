using System;
using YamlDotNet.Serialization.Utilities;

namespace YamlDotNet.Serialization.NamingConventions
{
	// Token: 0x020001D1 RID: 465
	public sealed class CamelCaseNamingConvention : INamingConvention
	{
		// Token: 0x06000E3B RID: 3643 RVA: 0x0003B103 File Offset: 0x00039303
		public string Apply(string value)
		{
			return value.ToCamelCase();
		}
	}
}
