using System;

namespace YamlDotNet.Core.Events
{
	// Token: 0x02000232 RID: 562
	public class Comment : ParsingEvent
	{
		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x060010EC RID: 4332 RVA: 0x000443D9 File Offset: 0x000425D9
		// (set) Token: 0x060010ED RID: 4333 RVA: 0x000443E1 File Offset: 0x000425E1
		public string Value { get; private set; }

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x060010EE RID: 4334 RVA: 0x000443EA File Offset: 0x000425EA
		// (set) Token: 0x060010EF RID: 4335 RVA: 0x000443F2 File Offset: 0x000425F2
		public bool IsInline { get; private set; }

		// Token: 0x060010F0 RID: 4336 RVA: 0x000443FB File Offset: 0x000425FB
		public Comment(string value, bool isInline)
			: this(value, isInline, Mark.Empty, Mark.Empty)
		{
		}

		// Token: 0x060010F1 RID: 4337 RVA: 0x0004440F File Offset: 0x0004260F
		public Comment(string value, bool isInline, Mark start, Mark end)
			: base(start, end)
		{
			this.Value = value;
			this.IsInline = isInline;
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x060010F2 RID: 4338 RVA: 0x00044428 File Offset: 0x00042628
		internal override EventType Type
		{
			get
			{
				return EventType.Comment;
			}
		}

		// Token: 0x060010F3 RID: 4339 RVA: 0x0004442C File Offset: 0x0004262C
		public override void Accept(IParsingEventVisitor visitor)
		{
			visitor.Visit(this);
		}
	}
}
