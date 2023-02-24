using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using YamlDotNet.Core;

namespace YamlDotNet.Serialization.TypeInspectors
{
	// Token: 0x020001B5 RID: 437
	public sealed class ReadablePropertiesTypeInspector : TypeInspectorSkeleton
	{
		// Token: 0x06000DC6 RID: 3526 RVA: 0x000396EC File Offset: 0x000378EC
		public ReadablePropertiesTypeInspector(ITypeResolver typeResolver)
		{
			if (typeResolver == null)
			{
				throw new ArgumentNullException("typeResolver");
			}
			this._typeResolver = typeResolver;
		}

		// Token: 0x06000DC7 RID: 3527 RVA: 0x00039709 File Offset: 0x00037909
		private static bool IsValidProperty(PropertyInfo property)
		{
			return property.CanRead && property.GetGetMethod().GetParameters().Length == 0;
		}

		// Token: 0x06000DC8 RID: 3528 RVA: 0x00039724 File Offset: 0x00037924
		public override IEnumerable<IPropertyDescriptor> GetProperties(Type type, object container)
		{
			return from p in type.GetPublicProperties().Where(new Func<PropertyInfo, bool>(ReadablePropertiesTypeInspector.IsValidProperty))
				select new ReadablePropertiesTypeInspector.ReflectionPropertyDescriptor(p, this._typeResolver);
		}

		// Token: 0x04000820 RID: 2080
		private readonly ITypeResolver _typeResolver;

		// Token: 0x02000A48 RID: 2632
		private sealed class ReflectionPropertyDescriptor : IPropertyDescriptor
		{
			// Token: 0x06005513 RID: 21779 RVA: 0x0009E476 File Offset: 0x0009C676
			public ReflectionPropertyDescriptor(PropertyInfo propertyInfo, ITypeResolver typeResolver)
			{
				this._propertyInfo = propertyInfo;
				this._typeResolver = typeResolver;
				this.ScalarStyle = ScalarStyle.Any;
			}

			// Token: 0x17000E60 RID: 3680
			// (get) Token: 0x06005514 RID: 21780 RVA: 0x0009E493 File Offset: 0x0009C693
			public string Name
			{
				get
				{
					return this._propertyInfo.Name;
				}
			}

			// Token: 0x17000E61 RID: 3681
			// (get) Token: 0x06005515 RID: 21781 RVA: 0x0009E4A0 File Offset: 0x0009C6A0
			public Type Type
			{
				get
				{
					return this._propertyInfo.PropertyType;
				}
			}

			// Token: 0x17000E62 RID: 3682
			// (get) Token: 0x06005516 RID: 21782 RVA: 0x0009E4AD File Offset: 0x0009C6AD
			// (set) Token: 0x06005517 RID: 21783 RVA: 0x0009E4B5 File Offset: 0x0009C6B5
			public Type TypeOverride { get; set; }

			// Token: 0x17000E63 RID: 3683
			// (get) Token: 0x06005518 RID: 21784 RVA: 0x0009E4BE File Offset: 0x0009C6BE
			// (set) Token: 0x06005519 RID: 21785 RVA: 0x0009E4C6 File Offset: 0x0009C6C6
			public int Order { get; set; }

			// Token: 0x17000E64 RID: 3684
			// (get) Token: 0x0600551A RID: 21786 RVA: 0x0009E4CF File Offset: 0x0009C6CF
			public bool CanWrite
			{
				get
				{
					return this._propertyInfo.CanWrite;
				}
			}

			// Token: 0x17000E65 RID: 3685
			// (get) Token: 0x0600551B RID: 21787 RVA: 0x0009E4DC File Offset: 0x0009C6DC
			// (set) Token: 0x0600551C RID: 21788 RVA: 0x0009E4E4 File Offset: 0x0009C6E4
			public ScalarStyle ScalarStyle { get; set; }

			// Token: 0x0600551D RID: 21789 RVA: 0x0009E4ED File Offset: 0x0009C6ED
			public void Write(object target, object value)
			{
				this._propertyInfo.SetValue(target, value, null);
			}

			// Token: 0x0600551E RID: 21790 RVA: 0x0009E4FD File Offset: 0x0009C6FD
			public T GetCustomAttribute<T>() where T : Attribute
			{
				return (T)((object)this._propertyInfo.GetCustomAttributes(typeof(T), true).FirstOrDefault<object>());
			}

			// Token: 0x0600551F RID: 21791 RVA: 0x0009E520 File Offset: 0x0009C720
			public IObjectDescriptor Read(object target)
			{
				object obj = this._propertyInfo.ReadValue(target);
				Type type = this.TypeOverride ?? this._typeResolver.Resolve(this.Type, obj);
				return new ObjectDescriptor(obj, type, this.Type, this.ScalarStyle);
			}

			// Token: 0x04002318 RID: 8984
			private readonly PropertyInfo _propertyInfo;

			// Token: 0x04002319 RID: 8985
			private readonly ITypeResolver _typeResolver;
		}
	}
}
