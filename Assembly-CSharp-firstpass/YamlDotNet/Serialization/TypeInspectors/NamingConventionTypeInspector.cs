using System;
using System.Collections.Generic;
using System.Linq;

namespace YamlDotNet.Serialization.TypeInspectors
{
	// Token: 0x020001B3 RID: 435
	public sealed class NamingConventionTypeInspector : TypeInspectorSkeleton
	{
		// Token: 0x06000DC1 RID: 3521 RVA: 0x00039619 File Offset: 0x00037819
		public NamingConventionTypeInspector(ITypeInspector innerTypeDescriptor, INamingConvention namingConvention)
		{
			if (innerTypeDescriptor == null)
			{
				throw new ArgumentNullException("innerTypeDescriptor");
			}
			this.innerTypeDescriptor = innerTypeDescriptor;
			if (namingConvention == null)
			{
				throw new ArgumentNullException("namingConvention");
			}
			this.namingConvention = namingConvention;
		}

		// Token: 0x06000DC2 RID: 3522 RVA: 0x0003964B File Offset: 0x0003784B
		public override IEnumerable<IPropertyDescriptor> GetProperties(Type type, object container)
		{
			return this.innerTypeDescriptor.GetProperties(type, container).Select(delegate(IPropertyDescriptor p)
			{
				YamlMemberAttribute customAttribute = p.GetCustomAttribute<YamlMemberAttribute>();
				if (customAttribute != null && !customAttribute.ApplyNamingConventions)
				{
					return p;
				}
				return new PropertyDescriptor(p)
				{
					Name = this.namingConvention.Apply(p.Name)
				};
			});
		}

		// Token: 0x0400081D RID: 2077
		private readonly ITypeInspector innerTypeDescriptor;

		// Token: 0x0400081E RID: 2078
		private readonly INamingConvention namingConvention;
	}
}
