using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000662 RID: 1634
	public class UpdateSessionOptions
	{
		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x06003FA2 RID: 16290 RVA: 0x00087A1B File Offset: 0x00085C1B
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x06003FA3 RID: 16291 RVA: 0x00087A1E File Offset: 0x00085C1E
		// (set) Token: 0x06003FA4 RID: 16292 RVA: 0x00087A26 File Offset: 0x00085C26
		public SessionModification SessionModificationHandle { get; set; }
	}
}
