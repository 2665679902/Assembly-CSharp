using System;

namespace YamlDotNet.Core
{
	// Token: 0x02000200 RID: 512
	internal enum EmitterState
	{
		// Token: 0x040008A5 RID: 2213
		StreamStart,
		// Token: 0x040008A6 RID: 2214
		StreamEnd,
		// Token: 0x040008A7 RID: 2215
		FirstDocumentStart,
		// Token: 0x040008A8 RID: 2216
		DocumentStart,
		// Token: 0x040008A9 RID: 2217
		DocumentContent,
		// Token: 0x040008AA RID: 2218
		DocumentEnd,
		// Token: 0x040008AB RID: 2219
		FlowSequenceFirstItem,
		// Token: 0x040008AC RID: 2220
		FlowSequenceItem,
		// Token: 0x040008AD RID: 2221
		FlowMappingFirstKey,
		// Token: 0x040008AE RID: 2222
		FlowMappingKey,
		// Token: 0x040008AF RID: 2223
		FlowMappingSimpleValue,
		// Token: 0x040008B0 RID: 2224
		FlowMappingValue,
		// Token: 0x040008B1 RID: 2225
		BlockSequenceFirstItem,
		// Token: 0x040008B2 RID: 2226
		BlockSequenceItem,
		// Token: 0x040008B3 RID: 2227
		BlockMappingFirstKey,
		// Token: 0x040008B4 RID: 2228
		BlockMappingKey,
		// Token: 0x040008B5 RID: 2229
		BlockMappingSimpleValue,
		// Token: 0x040008B6 RID: 2230
		BlockMappingValue
	}
}
