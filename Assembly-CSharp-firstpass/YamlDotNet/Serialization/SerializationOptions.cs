using System;

namespace YamlDotNet.Serialization
{
	// Token: 0x0200019D RID: 413
	[Flags]
	public enum SerializationOptions
	{
		// Token: 0x040007FC RID: 2044
		None = 0,
		// Token: 0x040007FD RID: 2045
		Roundtrip = 1,
		// Token: 0x040007FE RID: 2046
		DisableAliases = 2,
		// Token: 0x040007FF RID: 2047
		EmitDefaults = 4,
		// Token: 0x04000800 RID: 2048
		JsonCompatible = 8,
		// Token: 0x04000801 RID: 2049
		DefaultToStaticType = 16
	}
}
