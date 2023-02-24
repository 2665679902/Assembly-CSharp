using System;
using YamlDotNet.Serialization.Utilities;

namespace YamlDotNet.Serialization.NamingConventions
{
	// Token: 0x020001D4 RID: 468
	public sealed class PascalCaseNamingConvention : INamingConvention
	{
		// Token: 0x06000E41 RID: 3649 RVA: 0x0003B133 File Offset: 0x00039333
		public string Apply(string value)
		{
			return value.ToPascalCase();
		}
	}
}
