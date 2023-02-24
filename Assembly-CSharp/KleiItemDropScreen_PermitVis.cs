using System;
using System.Collections;
using Database;
using UnityEngine;

// Token: 0x02000AD1 RID: 2769
public class KleiItemDropScreen_PermitVis : KMonoBehaviour
{
	// Token: 0x060054F4 RID: 21748 RVA: 0x001ECB08 File Offset: 0x001EAD08
	public void ConfigureWith(PermitResource permit)
	{
		PermitPresentationInfo permitPresentationInfo = permit.GetPermitPresentationInfo();
		bool flag = permit != null;
		this.ResetState();
		this.equipmentVis.gameObject.SetActive(false);
		this.fallbackVis.gameObject.SetActive(false);
		if (!flag)
		{
			this.fallbackVis.gameObject.SetActive(true);
			this.fallbackVis.ConfigureWith(permit, permitPresentationInfo);
			return;
		}
		if (permit.Category == PermitCategory.Equipment)
		{
			this.equipmentVis.gameObject.SetActive(true);
			this.equipmentVis.ConfigureWith(permit, permitPresentationInfo);
			return;
		}
		this.fallbackVis.gameObject.SetActive(true);
		this.fallbackVis.ConfigureWith(permit, permitPresentationInfo);
	}

	// Token: 0x060054F5 RID: 21749 RVA: 0x001ECBAE File Offset: 0x001EADAE
	public Promise AnimateIn()
	{
		return Updater.RunRoutine(this, this.AnimateInRoutine());
	}

	// Token: 0x060054F6 RID: 21750 RVA: 0x001ECBBC File Offset: 0x001EADBC
	public Promise AnimateOut()
	{
		return Updater.RunRoutine(this, this.AnimateOutRoutine());
	}

	// Token: 0x060054F7 RID: 21751 RVA: 0x001ECBCA File Offset: 0x001EADCA
	private IEnumerator AnimateInRoutine()
	{
		this.root.gameObject.SetActive(true);
		yield return Updater.Ease(delegate(Vector3 v3)
		{
			this.root.transform.localScale = v3;
		}, this.root.transform.localScale, Vector3.one, 0.5f, null);
		yield break;
	}

	// Token: 0x060054F8 RID: 21752 RVA: 0x001ECBD9 File Offset: 0x001EADD9
	private IEnumerator AnimateOutRoutine()
	{
		yield return Updater.Ease(delegate(Vector3 v3)
		{
			this.root.transform.localScale = v3;
		}, this.root.transform.localScale, Vector3.zero, 0.25f, null);
		this.root.gameObject.SetActive(true);
		yield break;
	}

	// Token: 0x060054F9 RID: 21753 RVA: 0x001ECBE8 File Offset: 0x001EADE8
	public void ResetState()
	{
		this.root.transform.localScale = Vector3.zero;
	}

	// Token: 0x040039BD RID: 14781
	[SerializeField]
	private RectTransform root;

	// Token: 0x040039BE RID: 14782
	[Header("Different Permit Visualizers")]
	[SerializeField]
	private KleiItemDropScreen_PermitVis_Fallback fallbackVis;

	// Token: 0x040039BF RID: 14783
	[SerializeField]
	private KleiItemDropScreen_PermitVis_DupeEquipment equipmentVis;
}
