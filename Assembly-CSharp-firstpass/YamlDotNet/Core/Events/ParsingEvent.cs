using System;

namespace YamlDotNet.Core.Events
{
	// Token: 0x0200023B RID: 571
	public abstract class ParsingEvent
	{
		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06001127 RID: 4391 RVA: 0x000446D1 File Offset: 0x000428D1
		public virtual int NestingIncrease
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06001128 RID: 4392
		internal abstract EventType Type { get; }

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06001129 RID: 4393 RVA: 0x000446D4 File Offset: 0x000428D4
		public Mark Start
		{
			get
			{
				return this.start;
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x0600112A RID: 4394 RVA: 0x000446DC File Offset: 0x000428DC
		public Mark End
		{
			get
			{
				return this.end;
			}
		}

		// Token: 0x0600112B RID: 4395
		public abstract void Accept(IParsingEventVisitor visitor);

		// Token: 0x0600112C RID: 4396 RVA: 0x000446E4 File Offset: 0x000428E4
		internal ParsingEvent(Mark start, Mark end)
		{
			this.start = start;
			this.end = end;
		}

		// Token: 0x04000937 RID: 2359
		private readonly Mark start;

		// Token: 0x04000938 RID: 2360
		private readonly Mark end;
	}
}
