using System;
using System.Collections.Generic;
using System.Linq;
using YamlDotNet.Core;

namespace YamlDotNet.Serialization.ObjectGraphVisitors
{
	// Token: 0x020001BA RID: 442
	public sealed class CustomSerializationObjectGraphVisitor : ChainedObjectGraphVisitor
	{
		// Token: 0x06000DE6 RID: 3558 RVA: 0x00039ABC File Offset: 0x00037CBC
		public CustomSerializationObjectGraphVisitor(IObjectGraphVisitor<IEmitter> nextVisitor, IEnumerable<IYamlTypeConverter> typeConverters, ObjectSerializer nestedObjectSerializer)
			: base(nextVisitor)
		{
			IEnumerable<IYamlTypeConverter> enumerable;
			if (typeConverters == null)
			{
				enumerable = Enumerable.Empty<IYamlTypeConverter>();
			}
			else
			{
				IEnumerable<IYamlTypeConverter> enumerable2 = typeConverters.ToList<IYamlTypeConverter>();
				enumerable = enumerable2;
			}
			this.typeConverters = enumerable;
			this.nestedObjectSerializer = nestedObjectSerializer;
		}

		// Token: 0x06000DE7 RID: 3559 RVA: 0x00039AF0 File Offset: 0x00037CF0
		public override bool Enter(IObjectDescriptor value, IEmitter context)
		{
			IYamlTypeConverter yamlTypeConverter = this.typeConverters.FirstOrDefault((IYamlTypeConverter t) => t.Accepts(value.Type));
			if (yamlTypeConverter != null)
			{
				yamlTypeConverter.WriteYaml(context, value.Value, value.Type);
				return false;
			}
			IYamlConvertible yamlConvertible = value.Value as IYamlConvertible;
			if (yamlConvertible != null)
			{
				yamlConvertible.Write(context, this.nestedObjectSerializer);
				return false;
			}
			IYamlSerializable yamlSerializable = value.Value as IYamlSerializable;
			if (yamlSerializable != null)
			{
				yamlSerializable.WriteYaml(context);
				return false;
			}
			return base.Enter(value, context);
		}

		// Token: 0x04000827 RID: 2087
		private readonly IEnumerable<IYamlTypeConverter> typeConverters;

		// Token: 0x04000828 RID: 2088
		private readonly ObjectSerializer nestedObjectSerializer;
	}
}
