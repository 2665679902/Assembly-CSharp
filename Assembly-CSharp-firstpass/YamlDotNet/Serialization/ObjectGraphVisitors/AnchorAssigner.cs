using System;
using System.Collections.Generic;
using System.Globalization;

namespace YamlDotNet.Serialization.ObjectGraphVisitors
{
	// Token: 0x020001B7 RID: 439
	public sealed class AnchorAssigner : PreProcessingPhaseObjectGraphVisitorSkeleton, IAliasProvider
	{
		// Token: 0x06000DCD RID: 3533 RVA: 0x00039854 File Offset: 0x00037A54
		public AnchorAssigner(IEnumerable<IYamlTypeConverter> typeConverters)
			: base(typeConverters)
		{
		}

		// Token: 0x06000DCE RID: 3534 RVA: 0x00039868 File Offset: 0x00037A68
		protected override bool Enter(IObjectDescriptor value)
		{
			AnchorAssigner.AnchorAssignment anchorAssignment;
			if (value.Value != null && this.assignments.TryGetValue(value.Value, out anchorAssignment))
			{
				if (anchorAssignment.Anchor == null)
				{
					anchorAssignment.Anchor = "o" + this.nextId.ToString(CultureInfo.InvariantCulture);
					this.nextId += 1U;
				}
				return false;
			}
			return true;
		}

		// Token: 0x06000DCF RID: 3535 RVA: 0x000398CB File Offset: 0x00037ACB
		protected override bool EnterMapping(IObjectDescriptor key, IObjectDescriptor value)
		{
			return true;
		}

		// Token: 0x06000DD0 RID: 3536 RVA: 0x000398CE File Offset: 0x00037ACE
		protected override bool EnterMapping(IPropertyDescriptor key, IObjectDescriptor value)
		{
			return true;
		}

		// Token: 0x06000DD1 RID: 3537 RVA: 0x000398D1 File Offset: 0x00037AD1
		protected override void VisitScalar(IObjectDescriptor scalar)
		{
		}

		// Token: 0x06000DD2 RID: 3538 RVA: 0x000398D3 File Offset: 0x00037AD3
		protected override void VisitMappingStart(IObjectDescriptor mapping, Type keyType, Type valueType)
		{
			this.VisitObject(mapping);
		}

		// Token: 0x06000DD3 RID: 3539 RVA: 0x000398DC File Offset: 0x00037ADC
		protected override void VisitMappingEnd(IObjectDescriptor mapping)
		{
		}

		// Token: 0x06000DD4 RID: 3540 RVA: 0x000398DE File Offset: 0x00037ADE
		protected override void VisitSequenceStart(IObjectDescriptor sequence, Type elementType)
		{
			this.VisitObject(sequence);
		}

		// Token: 0x06000DD5 RID: 3541 RVA: 0x000398E7 File Offset: 0x00037AE7
		protected override void VisitSequenceEnd(IObjectDescriptor sequence)
		{
		}

		// Token: 0x06000DD6 RID: 3542 RVA: 0x000398E9 File Offset: 0x00037AE9
		private void VisitObject(IObjectDescriptor value)
		{
			if (value.Value != null)
			{
				this.assignments.Add(value.Value, new AnchorAssigner.AnchorAssignment());
			}
		}

		// Token: 0x06000DD7 RID: 3543 RVA: 0x0003990C File Offset: 0x00037B0C
		string IAliasProvider.GetAlias(object target)
		{
			AnchorAssigner.AnchorAssignment anchorAssignment;
			if (target != null && this.assignments.TryGetValue(target, out anchorAssignment))
			{
				return anchorAssignment.Anchor;
			}
			return null;
		}

		// Token: 0x04000821 RID: 2081
		private readonly IDictionary<object, AnchorAssigner.AnchorAssignment> assignments = new Dictionary<object, AnchorAssigner.AnchorAssignment>();

		// Token: 0x04000822 RID: 2082
		private uint nextId;

		// Token: 0x02000A4B RID: 2635
		private class AnchorAssignment
		{
			// Token: 0x04002320 RID: 8992
			public string Anchor;
		}
	}
}
