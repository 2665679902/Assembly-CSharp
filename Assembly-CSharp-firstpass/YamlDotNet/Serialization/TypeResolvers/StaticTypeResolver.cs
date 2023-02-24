using System;

namespace YamlDotNet.Serialization.TypeResolvers
{
	// Token: 0x020001B1 RID: 433
	public sealed class StaticTypeResolver : ITypeResolver
	{
		// Token: 0x06000DBD RID: 3517 RVA: 0x000395B5 File Offset: 0x000377B5
		public Type Resolve(Type staticType, object actualValue)
		{
			return staticType;
		}
	}
}
