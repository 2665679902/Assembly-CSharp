using System;
using System.Collections.Generic;
using System.ComponentModel;
using YamlDotNet.Core;

namespace YamlDotNet.Serialization.ObjectGraphVisitors
{
	// Token: 0x020001BB RID: 443
	public sealed class DefaultExclusiveObjectGraphVisitor : ChainedObjectGraphVisitor
	{
		// Token: 0x06000DE8 RID: 3560 RVA: 0x00039B91 File Offset: 0x00037D91
		public DefaultExclusiveObjectGraphVisitor(IObjectGraphVisitor<IEmitter> nextVisitor)
			: base(nextVisitor)
		{
		}

		// Token: 0x06000DE9 RID: 3561 RVA: 0x00039B9A File Offset: 0x00037D9A
		private static object GetDefault(Type type)
		{
			if (!type.IsValueType())
			{
				return null;
			}
			return Activator.CreateInstance(type);
		}

		// Token: 0x06000DEA RID: 3562 RVA: 0x00039BAC File Offset: 0x00037DAC
		public override bool EnterMapping(IObjectDescriptor key, IObjectDescriptor value, IEmitter context)
		{
			return !DefaultExclusiveObjectGraphVisitor._objectComparer.Equals(value, DefaultExclusiveObjectGraphVisitor.GetDefault(value.Type)) && base.EnterMapping(key, value, context);
		}

		// Token: 0x06000DEB RID: 3563 RVA: 0x00039BD4 File Offset: 0x00037DD4
		public override bool EnterMapping(IPropertyDescriptor key, IObjectDescriptor value, IEmitter context)
		{
			DefaultValueAttribute customAttribute = key.GetCustomAttribute<DefaultValueAttribute>();
			object obj = ((customAttribute != null) ? customAttribute.Value : DefaultExclusiveObjectGraphVisitor.GetDefault(key.Type));
			return !DefaultExclusiveObjectGraphVisitor._objectComparer.Equals(value.Value, obj) && base.EnterMapping(key, value, context);
		}

		// Token: 0x04000829 RID: 2089
		private static readonly IEqualityComparer<object> _objectComparer = EqualityComparer<object>.Default;
	}
}
