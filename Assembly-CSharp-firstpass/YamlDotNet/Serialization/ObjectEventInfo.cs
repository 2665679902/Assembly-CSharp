using System;

namespace YamlDotNet.Serialization
{
	// Token: 0x0200017C RID: 380
	public class ObjectEventInfo : EventInfo
	{
		// Token: 0x06000CCF RID: 3279 RVA: 0x000377A4 File Offset: 0x000359A4
		protected ObjectEventInfo(IObjectDescriptor source)
			: base(source)
		{
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000CD0 RID: 3280 RVA: 0x000377AD File Offset: 0x000359AD
		// (set) Token: 0x06000CD1 RID: 3281 RVA: 0x000377B5 File Offset: 0x000359B5
		public string Anchor { get; set; }

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000CD2 RID: 3282 RVA: 0x000377BE File Offset: 0x000359BE
		// (set) Token: 0x06000CD3 RID: 3283 RVA: 0x000377C6 File Offset: 0x000359C6
		public string Tag { get; set; }
	}
}
