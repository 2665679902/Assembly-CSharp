using System;

namespace YamlDotNet.Core.Events
{
	// Token: 0x02000235 RID: 565
	internal enum EventType
	{
		// Token: 0x04000922 RID: 2338
		None,
		// Token: 0x04000923 RID: 2339
		StreamStart,
		// Token: 0x04000924 RID: 2340
		StreamEnd,
		// Token: 0x04000925 RID: 2341
		DocumentStart,
		// Token: 0x04000926 RID: 2342
		DocumentEnd,
		// Token: 0x04000927 RID: 2343
		Alias,
		// Token: 0x04000928 RID: 2344
		Scalar,
		// Token: 0x04000929 RID: 2345
		SequenceStart,
		// Token: 0x0400092A RID: 2346
		SequenceEnd,
		// Token: 0x0400092B RID: 2347
		MappingStart,
		// Token: 0x0400092C RID: 2348
		MappingEnd,
		// Token: 0x0400092D RID: 2349
		Comment
	}
}
