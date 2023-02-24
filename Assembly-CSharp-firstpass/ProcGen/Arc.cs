using System;
using KSerialization;
using Satsuma;

namespace ProcGen
{
	// Token: 0x020004BA RID: 1210
	[SerializationConfig(MemberSerialization.OptIn)]
	public class Arc
	{
		// Token: 0x170002FE RID: 766
		// (get) Token: 0x060033C7 RID: 13255 RVA: 0x00070C00 File Offset: 0x0006EE00
		// (set) Token: 0x060033C8 RID: 13256 RVA: 0x00070C08 File Offset: 0x0006EE08
		public Arc arc { get; private set; }

		// Token: 0x060033C9 RID: 13257 RVA: 0x00070C11 File Offset: 0x0006EE11
		public void SetArc(Arc arc)
		{
			Debug.Assert(!this.arcSet, "Tried setting up an Arc twice, no go.");
			this.arc = arc;
			this.arcSet = true;
		}

		// Token: 0x060033CA RID: 13258 RVA: 0x00070C34 File Offset: 0x0006EE34
		public void SetType(string type)
		{
			this.type = type;
		}

		// Token: 0x060033CB RID: 13259 RVA: 0x00070C3D File Offset: 0x0006EE3D
		public Arc()
		{
		}

		// Token: 0x060033CC RID: 13260 RVA: 0x00070C50 File Offset: 0x0006EE50
		public Arc(string type)
		{
			this.type = type;
		}

		// Token: 0x060033CD RID: 13261 RVA: 0x00070C6A File Offset: 0x0006EE6A
		public Arc(Arc arc, string type)
		{
			this.arc = arc;
			this.type = type;
		}

		// Token: 0x04001229 RID: 4649
		private bool arcSet;

		// Token: 0x0400122B RID: 4651
		[Serialize]
		public string type = "";

		// Token: 0x0400122C RID: 4652
		[Serialize]
		public TagSet tags;
	}
}
