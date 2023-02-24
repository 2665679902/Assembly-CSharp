using System;

namespace YamlDotNet.Serialization
{
	// Token: 0x0200018F RID: 399
	public interface ITypeResolver
	{
		// Token: 0x06000D15 RID: 3349
		Type Resolve(Type staticType, object actualValue);
	}
}
