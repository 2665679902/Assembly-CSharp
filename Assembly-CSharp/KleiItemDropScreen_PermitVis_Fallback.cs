using System;
using Database;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000AD3 RID: 2771
public class KleiItemDropScreen_PermitVis_Fallback : KMonoBehaviour, IKleiItemDropScreen_PermitVis_Target
{
	// Token: 0x060054FF RID: 21759 RVA: 0x001ECD8A File Offset: 0x001EAF8A
	public void ConfigureWith(PermitResource permit, PermitPresentationInfo permitPresInfo)
	{
		this.sprite.sprite = permitPresInfo.sprite;
	}

	// Token: 0x040039C2 RID: 14786
	[SerializeField]
	private Image sprite;
}
