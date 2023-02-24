using System;

namespace YamlDotNet.Serialization.TypeResolvers
{
	// Token: 0x020001B0 RID: 432
	public sealed class DynamicTypeResolver : ITypeResolver
	{
		// Token: 0x06000DBB RID: 3515 RVA: 0x000395A0 File Offset: 0x000377A0
		public Type Resolve(Type staticType, object actualValue)
		{
			if (actualValue == null)
			{
				return staticType;
			}
			return actualValue.GetType();
		}
	}
}
