using System;
using System.Globalization;
using YamlDotNet.Core.Tokens;

namespace YamlDotNet.Core.Events
{
	// Token: 0x02000234 RID: 564
	public class DocumentStart : ParsingEvent
	{
		// Token: 0x170001DE RID: 478
		// (get) Token: 0x060010FB RID: 4347 RVA: 0x0004448C File Offset: 0x0004268C
		public override int NestingIncrease
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x060010FC RID: 4348 RVA: 0x0004448F File Offset: 0x0004268F
		internal override EventType Type
		{
			get
			{
				return EventType.DocumentStart;
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x060010FD RID: 4349 RVA: 0x00044492 File Offset: 0x00042692
		public TagDirectiveCollection Tags
		{
			get
			{
				return this.tags;
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x060010FE RID: 4350 RVA: 0x0004449A File Offset: 0x0004269A
		public VersionDirective Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x060010FF RID: 4351 RVA: 0x000444A2 File Offset: 0x000426A2
		public bool IsImplicit
		{
			get
			{
				return this.isImplicit;
			}
		}

		// Token: 0x06001100 RID: 4352 RVA: 0x000444AA File Offset: 0x000426AA
		public DocumentStart(VersionDirective version, TagDirectiveCollection tags, bool isImplicit, Mark start, Mark end)
			: base(start, end)
		{
			this.version = version;
			this.tags = tags;
			this.isImplicit = isImplicit;
		}

		// Token: 0x06001101 RID: 4353 RVA: 0x000444CB File Offset: 0x000426CB
		public DocumentStart(VersionDirective version, TagDirectiveCollection tags, bool isImplicit)
			: this(version, tags, isImplicit, Mark.Empty, Mark.Empty)
		{
		}

		// Token: 0x06001102 RID: 4354 RVA: 0x000444E0 File Offset: 0x000426E0
		public DocumentStart(Mark start, Mark end)
			: this(null, null, true, start, end)
		{
		}

		// Token: 0x06001103 RID: 4355 RVA: 0x000444ED File Offset: 0x000426ED
		public DocumentStart()
			: this(null, null, true, Mark.Empty, Mark.Empty)
		{
		}

		// Token: 0x06001104 RID: 4356 RVA: 0x00044502 File Offset: 0x00042702
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Document start [isImplicit = {0}]", this.isImplicit);
		}

		// Token: 0x06001105 RID: 4357 RVA: 0x0004451E File Offset: 0x0004271E
		public override void Accept(IParsingEventVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x0400091E RID: 2334
		private readonly TagDirectiveCollection tags;

		// Token: 0x0400091F RID: 2335
		private readonly VersionDirective version;

		// Token: 0x04000920 RID: 2336
		private readonly bool isImplicit;
	}
}
