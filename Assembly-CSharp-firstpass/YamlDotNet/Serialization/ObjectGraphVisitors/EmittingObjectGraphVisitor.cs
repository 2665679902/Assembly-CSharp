using System;
using YamlDotNet.Core;

namespace YamlDotNet.Serialization.ObjectGraphVisitors
{
	// Token: 0x020001BC RID: 444
	public sealed class EmittingObjectGraphVisitor : IObjectGraphVisitor<IEmitter>
	{
		// Token: 0x06000DED RID: 3565 RVA: 0x00039C29 File Offset: 0x00037E29
		public EmittingObjectGraphVisitor(IEventEmitter eventEmitter)
		{
			this.eventEmitter = eventEmitter;
		}

		// Token: 0x06000DEE RID: 3566 RVA: 0x00039C38 File Offset: 0x00037E38
		bool IObjectGraphVisitor<IEmitter>.Enter(IObjectDescriptor value, IEmitter context)
		{
			return true;
		}

		// Token: 0x06000DEF RID: 3567 RVA: 0x00039C3B File Offset: 0x00037E3B
		bool IObjectGraphVisitor<IEmitter>.EnterMapping(IObjectDescriptor key, IObjectDescriptor value, IEmitter context)
		{
			return true;
		}

		// Token: 0x06000DF0 RID: 3568 RVA: 0x00039C3E File Offset: 0x00037E3E
		bool IObjectGraphVisitor<IEmitter>.EnterMapping(IPropertyDescriptor key, IObjectDescriptor value, IEmitter context)
		{
			return true;
		}

		// Token: 0x06000DF1 RID: 3569 RVA: 0x00039C41 File Offset: 0x00037E41
		void IObjectGraphVisitor<IEmitter>.VisitScalar(IObjectDescriptor scalar, IEmitter context)
		{
			this.eventEmitter.Emit(new ScalarEventInfo(scalar), context);
		}

		// Token: 0x06000DF2 RID: 3570 RVA: 0x00039C55 File Offset: 0x00037E55
		void IObjectGraphVisitor<IEmitter>.VisitMappingStart(IObjectDescriptor mapping, Type keyType, Type valueType, IEmitter context)
		{
			this.eventEmitter.Emit(new MappingStartEventInfo(mapping), context);
		}

		// Token: 0x06000DF3 RID: 3571 RVA: 0x00039C6A File Offset: 0x00037E6A
		void IObjectGraphVisitor<IEmitter>.VisitMappingEnd(IObjectDescriptor mapping, IEmitter context)
		{
			this.eventEmitter.Emit(new MappingEndEventInfo(mapping), context);
		}

		// Token: 0x06000DF4 RID: 3572 RVA: 0x00039C7E File Offset: 0x00037E7E
		void IObjectGraphVisitor<IEmitter>.VisitSequenceStart(IObjectDescriptor sequence, Type elementType, IEmitter context)
		{
			this.eventEmitter.Emit(new SequenceStartEventInfo(sequence), context);
		}

		// Token: 0x06000DF5 RID: 3573 RVA: 0x00039C92 File Offset: 0x00037E92
		void IObjectGraphVisitor<IEmitter>.VisitSequenceEnd(IObjectDescriptor sequence, IEmitter context)
		{
			this.eventEmitter.Emit(new SequenceEndEventInfo(sequence), context);
		}

		// Token: 0x0400082A RID: 2090
		private readonly IEventEmitter eventEmitter;
	}
}
