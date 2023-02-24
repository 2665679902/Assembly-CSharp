using System;
using System.Collections.Generic;
using System.Linq;

namespace YamlDotNet.Serialization.ObjectGraphVisitors
{
	// Token: 0x020001BD RID: 445
	public abstract class PreProcessingPhaseObjectGraphVisitorSkeleton : IObjectGraphVisitor<Nothing>
	{
		// Token: 0x06000DF6 RID: 3574 RVA: 0x00039CA8 File Offset: 0x00037EA8
		public PreProcessingPhaseObjectGraphVisitorSkeleton(IEnumerable<IYamlTypeConverter> typeConverters)
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
		}

		// Token: 0x06000DF7 RID: 3575 RVA: 0x00039CD4 File Offset: 0x00037ED4
		bool IObjectGraphVisitor<Nothing>.Enter(IObjectDescriptor value, Nothing context)
		{
			return this.typeConverters.FirstOrDefault((IYamlTypeConverter t) => t.Accepts(value.Type)) == null && !(value.Value is IYamlConvertible) && !(value.Value is IYamlSerializable) && this.Enter(value);
		}

		// Token: 0x06000DF8 RID: 3576 RVA: 0x00039D3D File Offset: 0x00037F3D
		bool IObjectGraphVisitor<Nothing>.EnterMapping(IPropertyDescriptor key, IObjectDescriptor value, Nothing context)
		{
			return this.EnterMapping(key, value);
		}

		// Token: 0x06000DF9 RID: 3577 RVA: 0x00039D47 File Offset: 0x00037F47
		bool IObjectGraphVisitor<Nothing>.EnterMapping(IObjectDescriptor key, IObjectDescriptor value, Nothing context)
		{
			return this.EnterMapping(key, value);
		}

		// Token: 0x06000DFA RID: 3578 RVA: 0x00039D51 File Offset: 0x00037F51
		void IObjectGraphVisitor<Nothing>.VisitMappingEnd(IObjectDescriptor mapping, Nothing context)
		{
			this.VisitMappingEnd(mapping);
		}

		// Token: 0x06000DFB RID: 3579 RVA: 0x00039D5A File Offset: 0x00037F5A
		void IObjectGraphVisitor<Nothing>.VisitMappingStart(IObjectDescriptor mapping, Type keyType, Type valueType, Nothing context)
		{
			this.VisitMappingStart(mapping, keyType, valueType);
		}

		// Token: 0x06000DFC RID: 3580 RVA: 0x00039D65 File Offset: 0x00037F65
		void IObjectGraphVisitor<Nothing>.VisitScalar(IObjectDescriptor scalar, Nothing context)
		{
			this.VisitScalar(scalar);
		}

		// Token: 0x06000DFD RID: 3581 RVA: 0x00039D6E File Offset: 0x00037F6E
		void IObjectGraphVisitor<Nothing>.VisitSequenceEnd(IObjectDescriptor sequence, Nothing context)
		{
			this.VisitSequenceEnd(sequence);
		}

		// Token: 0x06000DFE RID: 3582 RVA: 0x00039D77 File Offset: 0x00037F77
		void IObjectGraphVisitor<Nothing>.VisitSequenceStart(IObjectDescriptor sequence, Type elementType, Nothing context)
		{
			this.VisitSequenceStart(sequence, elementType);
		}

		// Token: 0x06000DFF RID: 3583
		protected abstract bool Enter(IObjectDescriptor value);

		// Token: 0x06000E00 RID: 3584
		protected abstract bool EnterMapping(IPropertyDescriptor key, IObjectDescriptor value);

		// Token: 0x06000E01 RID: 3585
		protected abstract bool EnterMapping(IObjectDescriptor key, IObjectDescriptor value);

		// Token: 0x06000E02 RID: 3586
		protected abstract void VisitMappingEnd(IObjectDescriptor mapping);

		// Token: 0x06000E03 RID: 3587
		protected abstract void VisitMappingStart(IObjectDescriptor mapping, Type keyType, Type valueType);

		// Token: 0x06000E04 RID: 3588
		protected abstract void VisitScalar(IObjectDescriptor scalar);

		// Token: 0x06000E05 RID: 3589
		protected abstract void VisitSequenceEnd(IObjectDescriptor sequence);

		// Token: 0x06000E06 RID: 3590
		protected abstract void VisitSequenceStart(IObjectDescriptor sequence, Type elementType);

		// Token: 0x0400082B RID: 2091
		protected readonly IEnumerable<IYamlTypeConverter> typeConverters;
	}
}
