using System;
using YamlDotNet.Core;

namespace YamlDotNet.Serialization
{
	// Token: 0x020001A7 RID: 423
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	public sealed class YamlMemberAttribute : Attribute
	{
		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000D95 RID: 3477 RVA: 0x00038C49 File Offset: 0x00036E49
		// (set) Token: 0x06000D96 RID: 3478 RVA: 0x00038C51 File Offset: 0x00036E51
		public Type SerializeAs { get; set; }

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000D97 RID: 3479 RVA: 0x00038C5A File Offset: 0x00036E5A
		// (set) Token: 0x06000D98 RID: 3480 RVA: 0x00038C62 File Offset: 0x00036E62
		public int Order { get; set; }

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000D99 RID: 3481 RVA: 0x00038C6B File Offset: 0x00036E6B
		// (set) Token: 0x06000D9A RID: 3482 RVA: 0x00038C73 File Offset: 0x00036E73
		public string Alias { get; set; }

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000D9B RID: 3483 RVA: 0x00038C7C File Offset: 0x00036E7C
		// (set) Token: 0x06000D9C RID: 3484 RVA: 0x00038C84 File Offset: 0x00036E84
		public bool ApplyNamingConventions { get; set; }

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000D9D RID: 3485 RVA: 0x00038C8D File Offset: 0x00036E8D
		// (set) Token: 0x06000D9E RID: 3486 RVA: 0x00038C95 File Offset: 0x00036E95
		public ScalarStyle ScalarStyle { get; set; }

		// Token: 0x06000D9F RID: 3487 RVA: 0x00038C9E File Offset: 0x00036E9E
		public YamlMemberAttribute()
		{
			this.ScalarStyle = ScalarStyle.Any;
			this.ApplyNamingConventions = true;
		}

		// Token: 0x06000DA0 RID: 3488 RVA: 0x00038CB4 File Offset: 0x00036EB4
		public YamlMemberAttribute(Type serializeAs)
			: this()
		{
			this.SerializeAs = serializeAs;
		}
	}
}
