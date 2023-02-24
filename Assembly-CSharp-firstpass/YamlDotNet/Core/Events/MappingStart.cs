using System;
using System.Globalization;

namespace YamlDotNet.Core.Events
{
	// Token: 0x02000238 RID: 568
	public class MappingStart : NodeEvent
	{
		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06001117 RID: 4375 RVA: 0x0004455A File Offset: 0x0004275A
		public override int NestingIncrease
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06001118 RID: 4376 RVA: 0x0004455D File Offset: 0x0004275D
		internal override EventType Type
		{
			get
			{
				return EventType.MappingStart;
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06001119 RID: 4377 RVA: 0x00044561 File Offset: 0x00042761
		public bool IsImplicit
		{
			get
			{
				return this.isImplicit;
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x0600111A RID: 4378 RVA: 0x00044569 File Offset: 0x00042769
		public override bool IsCanonical
		{
			get
			{
				return !this.isImplicit;
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x0600111B RID: 4379 RVA: 0x00044574 File Offset: 0x00042774
		public MappingStyle Style
		{
			get
			{
				return this.style;
			}
		}

		// Token: 0x0600111C RID: 4380 RVA: 0x0004457C File Offset: 0x0004277C
		public MappingStart(string anchor, string tag, bool isImplicit, MappingStyle style, Mark start, Mark end)
			: base(anchor, tag, start, end)
		{
			this.isImplicit = isImplicit;
			this.style = style;
		}

		// Token: 0x0600111D RID: 4381 RVA: 0x00044599 File Offset: 0x00042799
		public MappingStart(string anchor, string tag, bool isImplicit, MappingStyle style)
			: this(anchor, tag, isImplicit, style, Mark.Empty, Mark.Empty)
		{
		}

		// Token: 0x0600111E RID: 4382 RVA: 0x000445B0 File Offset: 0x000427B0
		public MappingStart()
			: this(null, null, true, MappingStyle.Any, Mark.Empty, Mark.Empty)
		{
		}

		// Token: 0x0600111F RID: 4383 RVA: 0x000445C8 File Offset: 0x000427C8
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Mapping start [anchor = {0}, tag = {1}, isImplicit = {2}, style = {3}]", new object[] { base.Anchor, base.Tag, this.isImplicit, this.style });
		}

		// Token: 0x06001120 RID: 4384 RVA: 0x00044618 File Offset: 0x00042818
		public override void Accept(IParsingEventVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x0400092E RID: 2350
		private readonly bool isImplicit;

		// Token: 0x0400092F RID: 2351
		private readonly MappingStyle style;
	}
}
