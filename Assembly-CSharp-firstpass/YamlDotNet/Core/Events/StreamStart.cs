using System;

namespace YamlDotNet.Core.Events
{
	// Token: 0x02000241 RID: 577
	public class StreamStart : ParsingEvent
	{
		// Token: 0x17000200 RID: 512
		// (get) Token: 0x0600114F RID: 4431 RVA: 0x00044977 File Offset: 0x00042B77
		public override int NestingIncrease
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06001150 RID: 4432 RVA: 0x0004497A File Offset: 0x00042B7A
		internal override EventType Type
		{
			get
			{
				return EventType.StreamStart;
			}
		}

		// Token: 0x06001151 RID: 4433 RVA: 0x0004497D File Offset: 0x00042B7D
		public StreamStart()
			: this(Mark.Empty, Mark.Empty)
		{
		}

		// Token: 0x06001152 RID: 4434 RVA: 0x0004498F File Offset: 0x00042B8F
		public StreamStart(Mark start, Mark end)
			: base(start, end)
		{
		}

		// Token: 0x06001153 RID: 4435 RVA: 0x00044999 File Offset: 0x00042B99
		public override string ToString()
		{
			return "Stream start";
		}

		// Token: 0x06001154 RID: 4436 RVA: 0x000449A0 File Offset: 0x00042BA0
		public override void Accept(IParsingEventVisitor visitor)
		{
			visitor.Visit(this);
		}
	}
}
