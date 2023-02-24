using System;
using System.Collections.Generic;
using System.Linq;
using YamlDotNet.Core;
using YamlDotNet.Serialization.TypeInspectors;

namespace YamlDotNet.Serialization
{
	// Token: 0x020001A3 RID: 419
	public sealed class YamlAttributeOverridesInspector : TypeInspectorSkeleton
	{
		// Token: 0x06000D89 RID: 3465 RVA: 0x000389F7 File Offset: 0x00036BF7
		public YamlAttributeOverridesInspector(ITypeInspector innerTypeDescriptor, YamlAttributeOverrides overrides)
		{
			this.innerTypeDescriptor = innerTypeDescriptor;
			this.overrides = overrides;
		}

		// Token: 0x06000D8A RID: 3466 RVA: 0x00038A10 File Offset: 0x00036C10
		public override IEnumerable<IPropertyDescriptor> GetProperties(Type type, object container)
		{
			if (this.overrides == null)
			{
				return this.innerTypeDescriptor.GetProperties(type, container);
			}
			return from p in this.innerTypeDescriptor.GetProperties(type, container)
				select new YamlAttributeOverridesInspector.OverridePropertyDescriptor(p, this.overrides, type);
		}

		// Token: 0x0400080C RID: 2060
		private readonly ITypeInspector innerTypeDescriptor;

		// Token: 0x0400080D RID: 2061
		private readonly YamlAttributeOverrides overrides;

		// Token: 0x02000A3E RID: 2622
		public sealed class OverridePropertyDescriptor : IPropertyDescriptor
		{
			// Token: 0x060054E3 RID: 21731 RVA: 0x0009DF89 File Offset: 0x0009C189
			public OverridePropertyDescriptor(IPropertyDescriptor baseDescriptor, YamlAttributeOverrides overrides, Type classType)
			{
				this.baseDescriptor = baseDescriptor;
				this.overrides = overrides;
				this.classType = classType;
			}

			// Token: 0x17000E56 RID: 3670
			// (get) Token: 0x060054E4 RID: 21732 RVA: 0x0009DFA6 File Offset: 0x0009C1A6
			public string Name
			{
				get
				{
					return this.baseDescriptor.Name;
				}
			}

			// Token: 0x17000E57 RID: 3671
			// (get) Token: 0x060054E5 RID: 21733 RVA: 0x0009DFB3 File Offset: 0x0009C1B3
			public bool CanWrite
			{
				get
				{
					return this.baseDescriptor.CanWrite;
				}
			}

			// Token: 0x17000E58 RID: 3672
			// (get) Token: 0x060054E6 RID: 21734 RVA: 0x0009DFC0 File Offset: 0x0009C1C0
			public Type Type
			{
				get
				{
					return this.baseDescriptor.Type;
				}
			}

			// Token: 0x17000E59 RID: 3673
			// (get) Token: 0x060054E7 RID: 21735 RVA: 0x0009DFCD File Offset: 0x0009C1CD
			// (set) Token: 0x060054E8 RID: 21736 RVA: 0x0009DFDA File Offset: 0x0009C1DA
			public Type TypeOverride
			{
				get
				{
					return this.baseDescriptor.TypeOverride;
				}
				set
				{
					this.baseDescriptor.TypeOverride = value;
				}
			}

			// Token: 0x17000E5A RID: 3674
			// (get) Token: 0x060054E9 RID: 21737 RVA: 0x0009DFE8 File Offset: 0x0009C1E8
			// (set) Token: 0x060054EA RID: 21738 RVA: 0x0009DFF5 File Offset: 0x0009C1F5
			public int Order
			{
				get
				{
					return this.baseDescriptor.Order;
				}
				set
				{
					this.baseDescriptor.Order = value;
				}
			}

			// Token: 0x17000E5B RID: 3675
			// (get) Token: 0x060054EB RID: 21739 RVA: 0x0009E003 File Offset: 0x0009C203
			// (set) Token: 0x060054EC RID: 21740 RVA: 0x0009E010 File Offset: 0x0009C210
			public ScalarStyle ScalarStyle
			{
				get
				{
					return this.baseDescriptor.ScalarStyle;
				}
				set
				{
					this.baseDescriptor.ScalarStyle = value;
				}
			}

			// Token: 0x060054ED RID: 21741 RVA: 0x0009E01E File Offset: 0x0009C21E
			public void Write(object target, object value)
			{
				this.baseDescriptor.Write(target, value);
			}

			// Token: 0x060054EE RID: 21742 RVA: 0x0009E02D File Offset: 0x0009C22D
			public T GetCustomAttribute<T>() where T : Attribute
			{
				T t;
				if ((t = this.overrides.GetAttribute<T>(this.classType, this.Name)) == null)
				{
					t = this.baseDescriptor.GetCustomAttribute<T>();
				}
				return t;
			}

			// Token: 0x060054EF RID: 21743 RVA: 0x0009E05A File Offset: 0x0009C25A
			public IObjectDescriptor Read(object target)
			{
				return this.baseDescriptor.Read(target);
			}

			// Token: 0x040022FC RID: 8956
			private readonly IPropertyDescriptor baseDescriptor;

			// Token: 0x040022FD RID: 8957
			private readonly YamlAttributeOverrides overrides;

			// Token: 0x040022FE RID: 8958
			private readonly Type classType;
		}
	}
}
