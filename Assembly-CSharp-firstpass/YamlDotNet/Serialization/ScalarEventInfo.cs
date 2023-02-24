using System;
using YamlDotNet.Core;

namespace YamlDotNet.Serialization
{
	// Token: 0x0200017D RID: 381
	public sealed class ScalarEventInfo : ObjectEventInfo
	{
		// Token: 0x06000CD4 RID: 3284 RVA: 0x000377CF File Offset: 0x000359CF
		public ScalarEventInfo(IObjectDescriptor source)
			: base(source)
		{
			this.Style = source.ScalarStyle;
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000CD5 RID: 3285 RVA: 0x000377E4 File Offset: 0x000359E4
		// (set) Token: 0x06000CD6 RID: 3286 RVA: 0x000377EC File Offset: 0x000359EC
		public string RenderedValue { get; set; }

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000CD7 RID: 3287 RVA: 0x000377F5 File Offset: 0x000359F5
		// (set) Token: 0x06000CD8 RID: 3288 RVA: 0x000377FD File Offset: 0x000359FD
		public ScalarStyle Style { get; set; }

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000CD9 RID: 3289 RVA: 0x00037806 File Offset: 0x00035A06
		// (set) Token: 0x06000CDA RID: 3290 RVA: 0x0003780E File Offset: 0x00035A0E
		public bool IsPlainImplicit { get; set; }

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000CDB RID: 3291 RVA: 0x00037817 File Offset: 0x00035A17
		// (set) Token: 0x06000CDC RID: 3292 RVA: 0x0003781F File Offset: 0x00035A1F
		public bool IsQuotedImplicit { get; set; }
	}
}
