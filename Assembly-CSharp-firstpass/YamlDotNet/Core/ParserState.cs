using System;

namespace YamlDotNet.Core
{
	// Token: 0x0200020F RID: 527
	internal enum ParserState
	{
		// Token: 0x040008CF RID: 2255
		StreamStart,
		// Token: 0x040008D0 RID: 2256
		StreamEnd,
		// Token: 0x040008D1 RID: 2257
		ImplicitDocumentStart,
		// Token: 0x040008D2 RID: 2258
		DocumentStart,
		// Token: 0x040008D3 RID: 2259
		DocumentContent,
		// Token: 0x040008D4 RID: 2260
		DocumentEnd,
		// Token: 0x040008D5 RID: 2261
		BlockNode,
		// Token: 0x040008D6 RID: 2262
		BlockNodeOrIndentlessSequence,
		// Token: 0x040008D7 RID: 2263
		FlowNode,
		// Token: 0x040008D8 RID: 2264
		BlockSequenceFirstEntry,
		// Token: 0x040008D9 RID: 2265
		BlockSequenceEntry,
		// Token: 0x040008DA RID: 2266
		IndentlessSequenceEntry,
		// Token: 0x040008DB RID: 2267
		BlockMappingFirstKey,
		// Token: 0x040008DC RID: 2268
		BlockMappingKey,
		// Token: 0x040008DD RID: 2269
		BlockMappingValue,
		// Token: 0x040008DE RID: 2270
		FlowSequenceFirstEntry,
		// Token: 0x040008DF RID: 2271
		FlowSequenceEntry,
		// Token: 0x040008E0 RID: 2272
		FlowSequenceEntryMappingKey,
		// Token: 0x040008E1 RID: 2273
		FlowSequenceEntryMappingValue,
		// Token: 0x040008E2 RID: 2274
		FlowSequenceEntryMappingEnd,
		// Token: 0x040008E3 RID: 2275
		FlowMappingFirstKey,
		// Token: 0x040008E4 RID: 2276
		FlowMappingKey,
		// Token: 0x040008E5 RID: 2277
		FlowMappingValue,
		// Token: 0x040008E6 RID: 2278
		FlowMappingEmptyValue
	}
}
