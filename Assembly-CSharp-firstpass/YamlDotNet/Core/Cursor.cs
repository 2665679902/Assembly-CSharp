using System;

namespace YamlDotNet.Core
{
	// Token: 0x020001FE RID: 510
	[Serializable]
	internal class Cursor
	{
		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000F93 RID: 3987 RVA: 0x0003DF88 File Offset: 0x0003C188
		// (set) Token: 0x06000F94 RID: 3988 RVA: 0x0003DF90 File Offset: 0x0003C190
		public int Index { get; set; }

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000F95 RID: 3989 RVA: 0x0003DF99 File Offset: 0x0003C199
		// (set) Token: 0x06000F96 RID: 3990 RVA: 0x0003DFA1 File Offset: 0x0003C1A1
		public int Line { get; set; }

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000F97 RID: 3991 RVA: 0x0003DFAA File Offset: 0x0003C1AA
		// (set) Token: 0x06000F98 RID: 3992 RVA: 0x0003DFB2 File Offset: 0x0003C1B2
		public int LineOffset { get; set; }

		// Token: 0x06000F99 RID: 3993 RVA: 0x0003DFBB File Offset: 0x0003C1BB
		public Cursor()
		{
			this.Line = 1;
		}

		// Token: 0x06000F9A RID: 3994 RVA: 0x0003DFCA File Offset: 0x0003C1CA
		public Cursor(Cursor cursor)
		{
			this.Index = cursor.Index;
			this.Line = cursor.Line;
			this.LineOffset = cursor.LineOffset;
		}

		// Token: 0x06000F9B RID: 3995 RVA: 0x0003DFF6 File Offset: 0x0003C1F6
		public Mark Mark()
		{
			return new Mark(this.Index, this.Line, this.LineOffset + 1);
		}

		// Token: 0x06000F9C RID: 3996 RVA: 0x0003E014 File Offset: 0x0003C214
		public void Skip()
		{
			int num = this.Index;
			this.Index = num + 1;
			num = this.LineOffset;
			this.LineOffset = num + 1;
		}

		// Token: 0x06000F9D RID: 3997 RVA: 0x0003E044 File Offset: 0x0003C244
		public void SkipLineByOffset(int offset)
		{
			this.Index += offset;
			int line = this.Line;
			this.Line = line + 1;
			this.LineOffset = 0;
		}

		// Token: 0x06000F9E RID: 3998 RVA: 0x0003E078 File Offset: 0x0003C278
		public void ForceSkipLineAfterNonBreak()
		{
			if (this.LineOffset != 0)
			{
				int line = this.Line;
				this.Line = line + 1;
				this.LineOffset = 0;
			}
		}
	}
}
