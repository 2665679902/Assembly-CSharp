using System;
using YamlDotNet.Core;

namespace YamlDotNet.Serialization.ObjectGraphVisitors
{
	// Token: 0x020001B9 RID: 441
	public abstract class ChainedObjectGraphVisitor : IObjectGraphVisitor<IEmitter>
	{
		// Token: 0x06000DDD RID: 3549 RVA: 0x00039A2D File Offset: 0x00037C2D
		protected ChainedObjectGraphVisitor(IObjectGraphVisitor<IEmitter> nextVisitor)
		{
			this.nextVisitor = nextVisitor;
		}

		// Token: 0x06000DDE RID: 3550 RVA: 0x00039A3C File Offset: 0x00037C3C
		public virtual bool Enter(IObjectDescriptor value, IEmitter context)
		{
			return this.nextVisitor.Enter(value, context);
		}

		// Token: 0x06000DDF RID: 3551 RVA: 0x00039A4B File Offset: 0x00037C4B
		public virtual bool EnterMapping(IObjectDescriptor key, IObjectDescriptor value, IEmitter context)
		{
			return this.nextVisitor.EnterMapping(key, value, context);
		}

		// Token: 0x06000DE0 RID: 3552 RVA: 0x00039A5B File Offset: 0x00037C5B
		public virtual bool EnterMapping(IPropertyDescriptor key, IObjectDescriptor value, IEmitter context)
		{
			return this.nextVisitor.EnterMapping(key, value, context);
		}

		// Token: 0x06000DE1 RID: 3553 RVA: 0x00039A6B File Offset: 0x00037C6B
		public virtual void VisitScalar(IObjectDescriptor scalar, IEmitter context)
		{
			this.nextVisitor.VisitScalar(scalar, context);
		}

		// Token: 0x06000DE2 RID: 3554 RVA: 0x00039A7A File Offset: 0x00037C7A
		public virtual void VisitMappingStart(IObjectDescriptor mapping, Type keyType, Type valueType, IEmitter context)
		{
			this.nextVisitor.VisitMappingStart(mapping, keyType, valueType, context);
		}

		// Token: 0x06000DE3 RID: 3555 RVA: 0x00039A8C File Offset: 0x00037C8C
		public virtual void VisitMappingEnd(IObjectDescriptor mapping, IEmitter context)
		{
			this.nextVisitor.VisitMappingEnd(mapping, context);
		}

		// Token: 0x06000DE4 RID: 3556 RVA: 0x00039A9B File Offset: 0x00037C9B
		public virtual void VisitSequenceStart(IObjectDescriptor sequence, Type elementType, IEmitter context)
		{
			this.nextVisitor.VisitSequenceStart(sequence, elementType, context);
		}

		// Token: 0x06000DE5 RID: 3557 RVA: 0x00039AAB File Offset: 0x00037CAB
		public virtual void VisitSequenceEnd(IObjectDescriptor sequence, IEmitter context)
		{
			this.nextVisitor.VisitSequenceEnd(sequence, context);
		}

		// Token: 0x04000826 RID: 2086
		private readonly IObjectGraphVisitor<IEmitter> nextVisitor;
	}
}
