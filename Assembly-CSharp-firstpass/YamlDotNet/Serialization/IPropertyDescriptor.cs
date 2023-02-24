using System;
using YamlDotNet.Core;

namespace YamlDotNet.Serialization
{
	// Token: 0x0200018B RID: 395
	public interface IPropertyDescriptor
	{
		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000D01 RID: 3329
		string Name { get; }

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000D02 RID: 3330
		bool CanWrite { get; }

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000D03 RID: 3331
		Type Type { get; }

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000D04 RID: 3332
		// (set) Token: 0x06000D05 RID: 3333
		Type TypeOverride { get; set; }

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000D06 RID: 3334
		// (set) Token: 0x06000D07 RID: 3335
		int Order { get; set; }

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000D08 RID: 3336
		// (set) Token: 0x06000D09 RID: 3337
		ScalarStyle ScalarStyle { get; set; }

		// Token: 0x06000D0A RID: 3338
		T GetCustomAttribute<T>() where T : Attribute;

		// Token: 0x06000D0B RID: 3339
		IObjectDescriptor Read(object target);

		// Token: 0x06000D0C RID: 3340
		void Write(object target, object value);
	}
}
