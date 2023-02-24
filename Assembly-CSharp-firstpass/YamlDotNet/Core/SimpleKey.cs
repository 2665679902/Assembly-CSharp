using System;

namespace YamlDotNet.Core
{
	// Token: 0x02000214 RID: 532
	[Serializable]
	internal class SimpleKey
	{
		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06001077 RID: 4215 RVA: 0x00043BFE File Offset: 0x00041DFE
		// (set) Token: 0x06001078 RID: 4216 RVA: 0x00043C06 File Offset: 0x00041E06
		public bool IsPossible { get; set; }

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06001079 RID: 4217 RVA: 0x00043C0F File Offset: 0x00041E0F
		// (set) Token: 0x0600107A RID: 4218 RVA: 0x00043C17 File Offset: 0x00041E17
		public bool IsRequired { get; private set; }

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x0600107B RID: 4219 RVA: 0x00043C20 File Offset: 0x00041E20
		// (set) Token: 0x0600107C RID: 4220 RVA: 0x00043C28 File Offset: 0x00041E28
		public int TokenNumber { get; private set; }

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x0600107D RID: 4221 RVA: 0x00043C31 File Offset: 0x00041E31
		public int Index
		{
			get
			{
				return this.cursor.Index;
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x0600107E RID: 4222 RVA: 0x00043C3E File Offset: 0x00041E3E
		public int Line
		{
			get
			{
				return this.cursor.Line;
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x0600107F RID: 4223 RVA: 0x00043C4B File Offset: 0x00041E4B
		public int LineOffset
		{
			get
			{
				return this.cursor.LineOffset;
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06001080 RID: 4224 RVA: 0x00043C58 File Offset: 0x00041E58
		public Mark Mark
		{
			get
			{
				return this.cursor.Mark();
			}
		}

		// Token: 0x06001081 RID: 4225 RVA: 0x00043C65 File Offset: 0x00041E65
		public SimpleKey()
		{
			this.cursor = new Cursor();
		}

		// Token: 0x06001082 RID: 4226 RVA: 0x00043C78 File Offset: 0x00041E78
		public SimpleKey(bool isPossible, bool isRequired, int tokenNumber, Cursor cursor)
		{
			this.IsPossible = isPossible;
			this.IsRequired = isRequired;
			this.TokenNumber = tokenNumber;
			this.cursor = new Cursor(cursor);
		}

		// Token: 0x04000902 RID: 2306
		private readonly Cursor cursor;
	}
}
