using System;

// Token: 0x02000765 RID: 1893
public class EventBase : Resource
{
	// Token: 0x060033FF RID: 13311 RVA: 0x00117EAB File Offset: 0x001160AB
	public EventBase(string id)
		: base(id, id)
	{
		this.hash = Hash.SDBMLower(id);
	}

	// Token: 0x06003400 RID: 13312 RVA: 0x00117EC1 File Offset: 0x001160C1
	public virtual string GetDescription(EventInstanceBase ev)
	{
		return "";
	}

	// Token: 0x0400201D RID: 8221
	public int hash;
}
