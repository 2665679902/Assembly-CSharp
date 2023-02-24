using System;

namespace YamlDotNet.Core.Events
{
	// Token: 0x02000237 RID: 567
	public class MappingEnd : ParsingEvent
	{
		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06001111 RID: 4369 RVA: 0x00044527 File Offset: 0x00042727
		public override int NestingIncrease
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06001112 RID: 4370 RVA: 0x0004452A File Offset: 0x0004272A
		internal override EventType Type
		{
			get
			{
				return EventType.MappingEnd;
			}
		}

		// Token: 0x06001113 RID: 4371 RVA: 0x0004452E File Offset: 0x0004272E
		public MappingEnd(Mark start, Mark end)
			: base(start, end)
		{
		}

		// Token: 0x06001114 RID: 4372 RVA: 0x00044538 File Offset: 0x00042738
		public MappingEnd()
			: this(Mark.Empty, Mark.Empty)
		{
		}

		// Token: 0x06001115 RID: 4373 RVA: 0x0004454A File Offset: 0x0004274A
		public override string ToString()
		{
			return "Mapping end";
		}

		// Token: 0x06001116 RID: 4374 RVA: 0x00044551 File Offset: 0x00042751
		public override void Accept(IParsingEventVisitor visitor)
		{
			visitor.Visit(this);
		}
	}
}
