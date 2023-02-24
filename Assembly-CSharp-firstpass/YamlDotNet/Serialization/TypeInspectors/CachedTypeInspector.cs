using System;
using System.Collections.Generic;

namespace YamlDotNet.Serialization.TypeInspectors
{
	// Token: 0x020001B2 RID: 434
	public sealed class CachedTypeInspector : TypeInspectorSkeleton
	{
		// Token: 0x06000DBF RID: 3519 RVA: 0x000395C0 File Offset: 0x000377C0
		public CachedTypeInspector(ITypeInspector innerTypeDescriptor)
		{
			if (innerTypeDescriptor == null)
			{
				throw new ArgumentNullException("innerTypeDescriptor");
			}
			this.innerTypeDescriptor = innerTypeDescriptor;
		}

		// Token: 0x06000DC0 RID: 3520 RVA: 0x000395E8 File Offset: 0x000377E8
		public override IEnumerable<IPropertyDescriptor> GetProperties(Type type, object container)
		{
			List<IPropertyDescriptor> list;
			if (!this.cache.TryGetValue(type, out list))
			{
				list = new List<IPropertyDescriptor>(this.innerTypeDescriptor.GetProperties(type, container));
			}
			return list;
		}

		// Token: 0x0400081B RID: 2075
		private readonly ITypeInspector innerTypeDescriptor;

		// Token: 0x0400081C RID: 2076
		private readonly Dictionary<Type, List<IPropertyDescriptor>> cache = new Dictionary<Type, List<IPropertyDescriptor>>();
	}
}
