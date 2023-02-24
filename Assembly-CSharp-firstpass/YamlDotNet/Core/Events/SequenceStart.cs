using System;
using System.Globalization;

namespace YamlDotNet.Core.Events
{
	// Token: 0x0200023E RID: 574
	public class SequenceStart : NodeEvent
	{
		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06001140 RID: 4416 RVA: 0x00044896 File Offset: 0x00042A96
		public override int NestingIncrease
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06001141 RID: 4417 RVA: 0x00044899 File Offset: 0x00042A99
		internal override EventType Type
		{
			get
			{
				return EventType.SequenceStart;
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06001142 RID: 4418 RVA: 0x0004489C File Offset: 0x00042A9C
		public bool IsImplicit
		{
			get
			{
				return this.isImplicit;
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06001143 RID: 4419 RVA: 0x000448A4 File Offset: 0x00042AA4
		public override bool IsCanonical
		{
			get
			{
				return !this.isImplicit;
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06001144 RID: 4420 RVA: 0x000448AF File Offset: 0x00042AAF
		public SequenceStyle Style
		{
			get
			{
				return this.style;
			}
		}

		// Token: 0x06001145 RID: 4421 RVA: 0x000448B7 File Offset: 0x00042AB7
		public SequenceStart(string anchor, string tag, bool isImplicit, SequenceStyle style, Mark start, Mark end)
			: base(anchor, tag, start, end)
		{
			this.isImplicit = isImplicit;
			this.style = style;
		}

		// Token: 0x06001146 RID: 4422 RVA: 0x000448D4 File Offset: 0x00042AD4
		public SequenceStart(string anchor, string tag, bool isImplicit, SequenceStyle style)
			: this(anchor, tag, isImplicit, style, Mark.Empty, Mark.Empty)
		{
		}

		// Token: 0x06001147 RID: 4423 RVA: 0x000448EC File Offset: 0x00042AEC
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Sequence start [anchor = {0}, tag = {1}, isImplicit = {2}, style = {3}]", new object[] { base.Anchor, base.Tag, this.isImplicit, this.style });
		}

		// Token: 0x06001148 RID: 4424 RVA: 0x0004493C File Offset: 0x00042B3C
		public override void Accept(IParsingEventVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x0400093D RID: 2365
		private readonly bool isImplicit;

		// Token: 0x0400093E RID: 2366
		private readonly SequenceStyle style;
	}
}
