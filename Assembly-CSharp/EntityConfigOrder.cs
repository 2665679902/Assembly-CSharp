using System;

// Token: 0x02000745 RID: 1861
internal class EntityConfigOrder : Attribute
{
	// Token: 0x06003357 RID: 13143 RVA: 0x001149F8 File Offset: 0x00112BF8
	public EntityConfigOrder(int sort_order)
	{
		this.sortOrder = sort_order;
	}

	// Token: 0x04001F85 RID: 8069
	public int sortOrder;
}
