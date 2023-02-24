using System;

namespace YamlDotNet.Core.Events
{
	// Token: 0x02000240 RID: 576
	public class StreamEnd : ParsingEvent
	{
		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06001149 RID: 4425 RVA: 0x00044945 File Offset: 0x00042B45
		public override int NestingIncrease
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x0600114A RID: 4426 RVA: 0x00044948 File Offset: 0x00042B48
		internal override EventType Type
		{
			get
			{
				return EventType.StreamEnd;
			}
		}

		// Token: 0x0600114B RID: 4427 RVA: 0x0004494B File Offset: 0x00042B4B
		public StreamEnd(Mark start, Mark end)
			: base(start, end)
		{
		}

		// Token: 0x0600114C RID: 4428 RVA: 0x00044955 File Offset: 0x00042B55
		public StreamEnd()
			: this(Mark.Empty, Mark.Empty)
		{
		}

		// Token: 0x0600114D RID: 4429 RVA: 0x00044967 File Offset: 0x00042B67
		public override string ToString()
		{
			return "Stream end";
		}

		// Token: 0x0600114E RID: 4430 RVA: 0x0004496E File Offset: 0x00042B6E
		public override void Accept(IParsingEventVisitor visitor)
		{
			visitor.Visit(this);
		}
	}
}
