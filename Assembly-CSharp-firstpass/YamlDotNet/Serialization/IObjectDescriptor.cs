using System;
using YamlDotNet.Core;

namespace YamlDotNet.Serialization
{
	// Token: 0x02000187 RID: 391
	public interface IObjectDescriptor
	{
		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000CF3 RID: 3315
		object Value { get; }

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000CF4 RID: 3316
		Type Type { get; }

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000CF5 RID: 3317
		Type StaticType { get; }

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000CF6 RID: 3318
		ScalarStyle ScalarStyle { get; }
	}
}
