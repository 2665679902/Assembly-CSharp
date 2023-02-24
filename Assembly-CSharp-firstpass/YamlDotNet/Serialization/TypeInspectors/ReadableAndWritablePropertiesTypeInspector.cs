using System;
using System.Collections.Generic;
using System.Linq;

namespace YamlDotNet.Serialization.TypeInspectors
{
	// Token: 0x020001B4 RID: 436
	public sealed class ReadableAndWritablePropertiesTypeInspector : TypeInspectorSkeleton
	{
		// Token: 0x06000DC4 RID: 3524 RVA: 0x000396AA File Offset: 0x000378AA
		public ReadableAndWritablePropertiesTypeInspector(ITypeInspector innerTypeDescriptor)
		{
			this._innerTypeDescriptor = innerTypeDescriptor;
		}

		// Token: 0x06000DC5 RID: 3525 RVA: 0x000396B9 File Offset: 0x000378B9
		public override IEnumerable<IPropertyDescriptor> GetProperties(Type type, object container)
		{
			return from p in this._innerTypeDescriptor.GetProperties(type, container)
				where p.CanWrite
				select p;
		}

		// Token: 0x0400081F RID: 2079
		private readonly ITypeInspector _innerTypeDescriptor;
	}
}
