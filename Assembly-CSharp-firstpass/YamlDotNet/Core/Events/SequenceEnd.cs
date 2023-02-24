using System;

namespace YamlDotNet.Core.Events
{
	// Token: 0x0200023D RID: 573
	public class SequenceEnd : ParsingEvent
	{
		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x0600113A RID: 4410 RVA: 0x00044864 File Offset: 0x00042A64
		public override int NestingIncrease
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x0600113B RID: 4411 RVA: 0x00044867 File Offset: 0x00042A67
		internal override EventType Type
		{
			get
			{
				return EventType.SequenceEnd;
			}
		}

		// Token: 0x0600113C RID: 4412 RVA: 0x0004486A File Offset: 0x00042A6A
		public SequenceEnd(Mark start, Mark end)
			: base(start, end)
		{
		}

		// Token: 0x0600113D RID: 4413 RVA: 0x00044874 File Offset: 0x00042A74
		public SequenceEnd()
			: this(Mark.Empty, Mark.Empty)
		{
		}

		// Token: 0x0600113E RID: 4414 RVA: 0x00044886 File Offset: 0x00042A86
		public override string ToString()
		{
			return "Sequence end";
		}

		// Token: 0x0600113F RID: 4415 RVA: 0x0004488D File Offset: 0x00042A8D
		public override void Accept(IParsingEventVisitor visitor)
		{
			visitor.Visit(this);
		}
	}
}
