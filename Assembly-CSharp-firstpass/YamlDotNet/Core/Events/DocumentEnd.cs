using System;
using System.Globalization;

namespace YamlDotNet.Core.Events
{
	// Token: 0x02000233 RID: 563
	public class DocumentEnd : ParsingEvent
	{
		// Token: 0x170001DB RID: 475
		// (get) Token: 0x060010F4 RID: 4340 RVA: 0x00044435 File Offset: 0x00042635
		public override int NestingIncrease
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x060010F5 RID: 4341 RVA: 0x00044438 File Offset: 0x00042638
		internal override EventType Type
		{
			get
			{
				return EventType.DocumentEnd;
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x060010F6 RID: 4342 RVA: 0x0004443B File Offset: 0x0004263B
		public bool IsImplicit
		{
			get
			{
				return this.isImplicit;
			}
		}

		// Token: 0x060010F7 RID: 4343 RVA: 0x00044443 File Offset: 0x00042643
		public DocumentEnd(bool isImplicit, Mark start, Mark end)
			: base(start, end)
		{
			this.isImplicit = isImplicit;
		}

		// Token: 0x060010F8 RID: 4344 RVA: 0x00044454 File Offset: 0x00042654
		public DocumentEnd(bool isImplicit)
			: this(isImplicit, Mark.Empty, Mark.Empty)
		{
		}

		// Token: 0x060010F9 RID: 4345 RVA: 0x00044467 File Offset: 0x00042667
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Document end [isImplicit = {0}]", this.isImplicit);
		}

		// Token: 0x060010FA RID: 4346 RVA: 0x00044483 File Offset: 0x00042683
		public override void Accept(IParsingEventVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x0400091D RID: 2333
		private readonly bool isImplicit;
	}
}
