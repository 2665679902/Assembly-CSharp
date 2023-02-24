using System;

namespace KSerialization
{
	// Token: 0x02000500 RID: 1280
	public sealed class SerializationConfig : Attribute
	{
		// Token: 0x060036FB RID: 14075 RVA: 0x0007940B File Offset: 0x0007760B
		public SerializationConfig(MemberSerialization memberSerialization)
		{
			this.MemberSerialization = memberSerialization;
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x060036FC RID: 14076 RVA: 0x0007941A File Offset: 0x0007761A
		// (set) Token: 0x060036FD RID: 14077 RVA: 0x00079422 File Offset: 0x00077622
		public MemberSerialization MemberSerialization { get; set; }
	}
}
