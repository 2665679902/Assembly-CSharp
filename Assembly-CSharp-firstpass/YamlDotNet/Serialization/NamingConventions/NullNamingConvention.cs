using System;

namespace YamlDotNet.Serialization.NamingConventions
{
	// Token: 0x020001D3 RID: 467
	public sealed class NullNamingConvention : INamingConvention
	{
		// Token: 0x06000E3F RID: 3647 RVA: 0x0003B128 File Offset: 0x00039328
		public string Apply(string value)
		{
			return value;
		}
	}
}
