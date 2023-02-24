using System;
using UnityEngine;

// Token: 0x02000870 RID: 2160
public class ObjectLayerListItem
{
	// Token: 0x17000454 RID: 1108
	// (get) Token: 0x06003E07 RID: 15879 RVA: 0x0015A0CE File Offset: 0x001582CE
	// (set) Token: 0x06003E08 RID: 15880 RVA: 0x0015A0D6 File Offset: 0x001582D6
	public ObjectLayerListItem previousItem { get; private set; }

	// Token: 0x17000455 RID: 1109
	// (get) Token: 0x06003E09 RID: 15881 RVA: 0x0015A0DF File Offset: 0x001582DF
	// (set) Token: 0x06003E0A RID: 15882 RVA: 0x0015A0E7 File Offset: 0x001582E7
	public ObjectLayerListItem nextItem { get; private set; }

	// Token: 0x17000456 RID: 1110
	// (get) Token: 0x06003E0B RID: 15883 RVA: 0x0015A0F0 File Offset: 0x001582F0
	// (set) Token: 0x06003E0C RID: 15884 RVA: 0x0015A0F8 File Offset: 0x001582F8
	public GameObject gameObject { get; private set; }

	// Token: 0x06003E0D RID: 15885 RVA: 0x0015A101 File Offset: 0x00158301
	public ObjectLayerListItem(GameObject gameObject, ObjectLayer layer, int new_cell)
	{
		this.gameObject = gameObject;
		this.layer = layer;
		this.Refresh(new_cell);
	}

	// Token: 0x06003E0E RID: 15886 RVA: 0x0015A12A File Offset: 0x0015832A
	public void Clear()
	{
		this.Refresh(Grid.InvalidCell);
	}

	// Token: 0x06003E0F RID: 15887 RVA: 0x0015A138 File Offset: 0x00158338
	public bool Refresh(int new_cell)
	{
		if (this.cell != new_cell)
		{
			if (this.cell != Grid.InvalidCell && Grid.Objects[this.cell, (int)this.layer] == this.gameObject)
			{
				GameObject gameObject = null;
				if (this.nextItem != null && this.nextItem.gameObject != null)
				{
					gameObject = this.nextItem.gameObject;
				}
				Grid.Objects[this.cell, (int)this.layer] = gameObject;
			}
			if (this.previousItem != null)
			{
				this.previousItem.nextItem = this.nextItem;
			}
			if (this.nextItem != null)
			{
				this.nextItem.previousItem = this.previousItem;
			}
			this.previousItem = null;
			this.nextItem = null;
			this.cell = new_cell;
			if (this.cell != Grid.InvalidCell)
			{
				GameObject gameObject2 = Grid.Objects[this.cell, (int)this.layer];
				if (gameObject2 != null && gameObject2 != this.gameObject)
				{
					ObjectLayerListItem objectLayerListItem = gameObject2.GetComponent<Pickupable>().objectLayerListItem;
					this.nextItem = objectLayerListItem;
					objectLayerListItem.previousItem = this;
				}
				Grid.Objects[this.cell, (int)this.layer] = this.gameObject;
			}
			return true;
		}
		return false;
	}

	// Token: 0x06003E10 RID: 15888 RVA: 0x0015A27C File Offset: 0x0015847C
	public bool Update(int cell)
	{
		return this.Refresh(cell);
	}

	// Token: 0x0400289F RID: 10399
	private int cell = Grid.InvalidCell;

	// Token: 0x040028A0 RID: 10400
	private ObjectLayer layer;
}
