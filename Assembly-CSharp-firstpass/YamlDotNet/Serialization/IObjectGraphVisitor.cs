using System;

namespace YamlDotNet.Serialization
{
	// Token: 0x0200018A RID: 394
	public interface IObjectGraphVisitor<TContext>
	{
		// Token: 0x06000CF9 RID: 3321
		bool Enter(IObjectDescriptor value, TContext context);

		// Token: 0x06000CFA RID: 3322
		bool EnterMapping(IObjectDescriptor key, IObjectDescriptor value, TContext context);

		// Token: 0x06000CFB RID: 3323
		bool EnterMapping(IPropertyDescriptor key, IObjectDescriptor value, TContext context);

		// Token: 0x06000CFC RID: 3324
		void VisitScalar(IObjectDescriptor scalar, TContext context);

		// Token: 0x06000CFD RID: 3325
		void VisitMappingStart(IObjectDescriptor mapping, Type keyType, Type valueType, TContext context);

		// Token: 0x06000CFE RID: 3326
		void VisitMappingEnd(IObjectDescriptor mapping, TContext context);

		// Token: 0x06000CFF RID: 3327
		void VisitSequenceStart(IObjectDescriptor sequence, Type elementType, TContext context);

		// Token: 0x06000D00 RID: 3328
		void VisitSequenceEnd(IObjectDescriptor sequence, TContext context);
	}
}
