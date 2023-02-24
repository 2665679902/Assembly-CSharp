using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using YamlDotNet.Core;
using YamlDotNet.Helpers;

namespace YamlDotNet.Serialization
{
	// Token: 0x020001A2 RID: 418
	public sealed class YamlAttributeOverrides
	{
		// Token: 0x06000D84 RID: 3460 RVA: 0x000387F4 File Offset: 0x000369F4
		public T GetAttribute<T>(Type type, string member) where T : Attribute
		{
			List<YamlAttributeOverrides.AttributeMapping> list;
			if (this.overrides.TryGetValue(new YamlAttributeOverrides.AttributeKey(typeof(T), member), out list))
			{
				int num = 0;
				YamlAttributeOverrides.AttributeMapping attributeMapping = null;
				foreach (YamlAttributeOverrides.AttributeMapping attributeMapping2 in list)
				{
					int num2 = attributeMapping2.Matches(type);
					if (num2 > num)
					{
						num = num2;
						attributeMapping = attributeMapping2;
					}
				}
				if (num > 0)
				{
					return (T)((object)attributeMapping.Attribute);
				}
			}
			return default(T);
		}

		// Token: 0x06000D85 RID: 3461 RVA: 0x00038890 File Offset: 0x00036A90
		public void Add(Type type, string member, Attribute attribute)
		{
			YamlAttributeOverrides.AttributeMapping attributeMapping = new YamlAttributeOverrides.AttributeMapping(type, attribute);
			YamlAttributeOverrides.AttributeKey attributeKey = new YamlAttributeOverrides.AttributeKey(attribute.GetType(), member);
			List<YamlAttributeOverrides.AttributeMapping> list;
			if (!this.overrides.TryGetValue(attributeKey, out list))
			{
				list = new List<YamlAttributeOverrides.AttributeMapping>();
				this.overrides.Add(attributeKey, list);
			}
			else if (list.Contains(attributeMapping))
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Attribute ({2}) already set for Type {0}, Member {1}", type.FullName, member, attribute));
			}
			list.Add(attributeMapping);
		}

		// Token: 0x06000D86 RID: 3462 RVA: 0x00038908 File Offset: 0x00036B08
		public void Add<TClass>(Expression<Func<TClass, object>> propertyAccessor, Attribute attribute)
		{
			PropertyInfo propertyInfo = propertyAccessor.AsProperty();
			this.Add(typeof(TClass), propertyInfo.Name, attribute);
		}

		// Token: 0x06000D87 RID: 3463 RVA: 0x00038934 File Offset: 0x00036B34
		public YamlAttributeOverrides Clone()
		{
			YamlAttributeOverrides yamlAttributeOverrides = new YamlAttributeOverrides();
			foreach (KeyValuePair<YamlAttributeOverrides.AttributeKey, List<YamlAttributeOverrides.AttributeMapping>> keyValuePair in this.overrides)
			{
				foreach (YamlAttributeOverrides.AttributeMapping attributeMapping in keyValuePair.Value)
				{
					yamlAttributeOverrides.Add(attributeMapping.RegisteredType, keyValuePair.Key.PropertyName, attributeMapping.Attribute);
				}
			}
			return yamlAttributeOverrides;
		}

		// Token: 0x0400080B RID: 2059
		private readonly Dictionary<YamlAttributeOverrides.AttributeKey, List<YamlAttributeOverrides.AttributeMapping>> overrides = new Dictionary<YamlAttributeOverrides.AttributeKey, List<YamlAttributeOverrides.AttributeMapping>>();

		// Token: 0x02000A3C RID: 2620
		private struct AttributeKey
		{
			// Token: 0x060054DC RID: 21724 RVA: 0x0009DE5F File Offset: 0x0009C05F
			public AttributeKey(Type attributeType, string propertyName)
			{
				this.AttributeType = attributeType;
				this.PropertyName = propertyName;
			}

			// Token: 0x060054DD RID: 21725 RVA: 0x0009DE70 File Offset: 0x0009C070
			public override bool Equals(object obj)
			{
				YamlAttributeOverrides.AttributeKey attributeKey = (YamlAttributeOverrides.AttributeKey)obj;
				return this.AttributeType.Equals(attributeKey.AttributeType) && this.PropertyName.Equals(attributeKey.PropertyName);
			}

			// Token: 0x060054DE RID: 21726 RVA: 0x0009DEAA File Offset: 0x0009C0AA
			public override int GetHashCode()
			{
				return HashCode.CombineHashCodes(this.AttributeType.GetHashCode(), this.PropertyName.GetHashCode());
			}

			// Token: 0x040022F8 RID: 8952
			public readonly Type AttributeType;

			// Token: 0x040022F9 RID: 8953
			public readonly string PropertyName;
		}

		// Token: 0x02000A3D RID: 2621
		private sealed class AttributeMapping
		{
			// Token: 0x060054DF RID: 21727 RVA: 0x0009DEC7 File Offset: 0x0009C0C7
			public AttributeMapping(Type registeredType, Attribute attribute)
			{
				this.RegisteredType = registeredType;
				this.Attribute = attribute;
			}

			// Token: 0x060054E0 RID: 21728 RVA: 0x0009DEE0 File Offset: 0x0009C0E0
			public override bool Equals(object obj)
			{
				YamlAttributeOverrides.AttributeMapping attributeMapping = obj as YamlAttributeOverrides.AttributeMapping;
				return attributeMapping != null && this.RegisteredType.Equals(attributeMapping.RegisteredType) && this.Attribute.Equals(attributeMapping.Attribute);
			}

			// Token: 0x060054E1 RID: 21729 RVA: 0x0009DF1D File Offset: 0x0009C11D
			public override int GetHashCode()
			{
				return HashCode.CombineHashCodes(this.RegisteredType.GetHashCode(), this.Attribute.GetHashCode());
			}

			// Token: 0x060054E2 RID: 21730 RVA: 0x0009DF3C File Offset: 0x0009C13C
			public int Matches(Type matchType)
			{
				int num = 0;
				Type type = matchType;
				while (type != null)
				{
					num++;
					if (type == this.RegisteredType)
					{
						return num;
					}
					type = type.BaseType();
				}
				if (matchType.GetInterfaces().Contains(this.RegisteredType))
				{
					return num;
				}
				return 0;
			}

			// Token: 0x040022FA RID: 8954
			public readonly Type RegisteredType;

			// Token: 0x040022FB RID: 8955
			public readonly Attribute Attribute;
		}
	}
}
