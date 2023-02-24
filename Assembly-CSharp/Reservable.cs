using System;
using UnityEngine;

// Token: 0x020008E3 RID: 2275
[AddComponentMenu("KMonoBehaviour/scripts/Reservable")]
public class Reservable : KMonoBehaviour
{
	// Token: 0x1700049D RID: 1181
	// (get) Token: 0x06004189 RID: 16777 RVA: 0x0016F649 File Offset: 0x0016D849
	public GameObject ReservedBy
	{
		get
		{
			return this.reservedBy;
		}
	}

	// Token: 0x1700049E RID: 1182
	// (get) Token: 0x0600418A RID: 16778 RVA: 0x0016F651 File Offset: 0x0016D851
	public bool isReserved
	{
		get
		{
			return !(this.reservedBy == null);
		}
	}

	// Token: 0x0600418B RID: 16779 RVA: 0x0016F662 File Offset: 0x0016D862
	public bool Reserve(GameObject reserver)
	{
		if (this.reservedBy == null)
		{
			this.reservedBy = reserver;
			return true;
		}
		return false;
	}

	// Token: 0x0600418C RID: 16780 RVA: 0x0016F67C File Offset: 0x0016D87C
	public void ClearReservation(GameObject reserver)
	{
		if (this.reservedBy == reserver)
		{
			this.reservedBy = null;
		}
	}

	// Token: 0x04002BB5 RID: 11189
	private GameObject reservedBy;
}
