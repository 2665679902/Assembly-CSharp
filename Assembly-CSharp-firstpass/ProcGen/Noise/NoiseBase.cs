using System;

namespace ProcGen.Noise
{
	// Token: 0x020004F0 RID: 1264
	public class NoiseBase
	{
		// Token: 0x06003692 RID: 13970 RVA: 0x00077BD3 File Offset: 0x00075DD3
		public virtual Type GetObjectType()
		{
			return null;
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06003693 RID: 13971 RVA: 0x00077BD6 File Offset: 0x00075DD6
		// (set) Token: 0x06003694 RID: 13972 RVA: 0x00077BDE File Offset: 0x00075DDE
		public string name { get; set; }

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06003695 RID: 13973 RVA: 0x00077BE7 File Offset: 0x00075DE7
		// (set) Token: 0x06003696 RID: 13974 RVA: 0x00077BEF File Offset: 0x00075DEF
		public Vector2f pos { get; set; }
	}
}
