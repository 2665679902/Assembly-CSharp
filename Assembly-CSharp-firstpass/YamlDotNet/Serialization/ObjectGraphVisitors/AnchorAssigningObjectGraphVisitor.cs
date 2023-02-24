using System;
using System.Collections.Generic;
using YamlDotNet.Core;

namespace YamlDotNet.Serialization.ObjectGraphVisitors
{
	// Token: 0x020001B8 RID: 440
	public sealed class AnchorAssigningObjectGraphVisitor : ChainedObjectGraphVisitor
	{
		// Token: 0x06000DD8 RID: 3544 RVA: 0x00039934 File Offset: 0x00037B34
		public AnchorAssigningObjectGraphVisitor(IObjectGraphVisitor<IEmitter> nextVisitor, IEventEmitter eventEmitter, IAliasProvider aliasProvider)
			: base(nextVisitor)
		{
			this.eventEmitter = eventEmitter;
			this.aliasProvider = aliasProvider;
		}

		// Token: 0x06000DD9 RID: 3545 RVA: 0x00039958 File Offset: 0x00037B58
		public override bool Enter(IObjectDescriptor value, IEmitter context)
		{
			string alias = this.aliasProvider.GetAlias(value.Value);
			if (alias != null && !this.emittedAliases.Add(alias))
			{
				this.eventEmitter.Emit(new AliasEventInfo(value)
				{
					Alias = alias
				}, context);
				return false;
			}
			return base.Enter(value, context);
		}

		// Token: 0x06000DDA RID: 3546 RVA: 0x000399AB File Offset: 0x00037BAB
		public override void VisitMappingStart(IObjectDescriptor mapping, Type keyType, Type valueType, IEmitter context)
		{
			this.eventEmitter.Emit(new MappingStartEventInfo(mapping)
			{
				Anchor = this.aliasProvider.GetAlias(mapping.Value)
			}, context);
		}

		// Token: 0x06000DDB RID: 3547 RVA: 0x000399D7 File Offset: 0x00037BD7
		public override void VisitSequenceStart(IObjectDescriptor sequence, Type elementType, IEmitter context)
		{
			this.eventEmitter.Emit(new SequenceStartEventInfo(sequence)
			{
				Anchor = this.aliasProvider.GetAlias(sequence.Value)
			}, context);
		}

		// Token: 0x06000DDC RID: 3548 RVA: 0x00039A02 File Offset: 0x00037C02
		public override void VisitScalar(IObjectDescriptor scalar, IEmitter context)
		{
			this.eventEmitter.Emit(new ScalarEventInfo(scalar)
			{
				Anchor = this.aliasProvider.GetAlias(scalar.Value)
			}, context);
		}

		// Token: 0x04000823 RID: 2083
		private readonly IEventEmitter eventEmitter;

		// Token: 0x04000824 RID: 2084
		private readonly IAliasProvider aliasProvider;

		// Token: 0x04000825 RID: 2085
		private readonly HashSet<string> emittedAliases = new HashSet<string>();
	}
}
