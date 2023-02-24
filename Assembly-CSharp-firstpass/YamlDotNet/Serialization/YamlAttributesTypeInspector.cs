using System;
using System.Collections.Generic;
using System.Linq;
using YamlDotNet.Serialization.TypeInspectors;

namespace YamlDotNet.Serialization
{
	// Token: 0x020001A4 RID: 420
	public sealed class YamlAttributesTypeInspector : TypeInspectorSkeleton
	{
		// Token: 0x06000D8B RID: 3467 RVA: 0x00038A6F File Offset: 0x00036C6F
		public YamlAttributesTypeInspector(ITypeInspector innerTypeDescriptor)
		{
			this.innerTypeDescriptor = innerTypeDescriptor;
		}

		// Token: 0x06000D8C RID: 3468 RVA: 0x00038A80 File Offset: 0x00036C80
		public override IEnumerable<IPropertyDescriptor> GetProperties(Type type, object container)
		{
			return from p in (from p in this.innerTypeDescriptor.GetProperties(type, container)
					where p.GetCustomAttribute<YamlIgnoreAttribute>() == null
					select p).Select(delegate(IPropertyDescriptor p)
				{
					PropertyDescriptor propertyDescriptor = new PropertyDescriptor(p);
					YamlMemberAttribute customAttribute = p.GetCustomAttribute<YamlMemberAttribute>();
					if (customAttribute != null)
					{
						if (customAttribute.SerializeAs != null)
						{
							propertyDescriptor.TypeOverride = customAttribute.SerializeAs;
						}
						propertyDescriptor.Order = customAttribute.Order;
						propertyDescriptor.ScalarStyle = customAttribute.ScalarStyle;
						if (customAttribute.Alias != null)
						{
							propertyDescriptor.Name = customAttribute.Alias;
						}
					}
					return propertyDescriptor;
				})
				orderby p.Order
				select p;
		}

		// Token: 0x0400080E RID: 2062
		private readonly ITypeInspector innerTypeDescriptor;
	}
}
