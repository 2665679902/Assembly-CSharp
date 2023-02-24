using System;
using KSerialization;
using UnityEngine;

// Token: 0x02000757 RID: 1879
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/Workable/EquippableWorkable")]
public class EquippableWorkable : Workable, ISaveLoadable
{
	// Token: 0x060033BB RID: 13243 RVA: 0x00116874 File Offset: 0x00114A74
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.workerStatusItem = Db.Get().DuplicantStatusItems.Equipping;
		this.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_equip_clothing_kanim") };
		this.synchronizeAnims = false;
	}

	// Token: 0x060033BC RID: 13244 RVA: 0x001168C1 File Offset: 0x00114AC1
	public global::QualityLevel GetQuality()
	{
		return this.quality;
	}

	// Token: 0x060033BD RID: 13245 RVA: 0x001168C9 File Offset: 0x00114AC9
	public void SetQuality(global::QualityLevel level)
	{
		this.quality = level;
	}

	// Token: 0x060033BE RID: 13246 RVA: 0x001168D2 File Offset: 0x00114AD2
	protected override void OnSpawn()
	{
		base.SetWorkTime(1.5f);
		this.equippable.OnAssign += this.RefreshChore;
	}

	// Token: 0x060033BF RID: 13247 RVA: 0x001168F6 File Offset: 0x00114AF6
	private void CreateChore()
	{
		global::Debug.Assert(this.chore == null, "chore should be null");
		this.chore = new EquipChore(this);
	}

	// Token: 0x060033C0 RID: 13248 RVA: 0x00116917 File Offset: 0x00114B17
	public void CancelChore(string reason = "")
	{
		if (this.chore != null)
		{
			this.chore.Cancel(reason);
			Prioritizable.RemoveRef(this.equippable.gameObject);
			this.chore = null;
		}
	}

	// Token: 0x060033C1 RID: 13249 RVA: 0x00116944 File Offset: 0x00114B44
	private void RefreshChore(IAssignableIdentity target)
	{
		if (this.chore != null)
		{
			this.CancelChore("Equipment Reassigned");
		}
		if (target != null && !target.GetSoleOwner().GetComponent<Equipment>().IsEquipped(this.equippable))
		{
			this.CreateChore();
		}
	}

	// Token: 0x060033C2 RID: 13250 RVA: 0x0011697C File Offset: 0x00114B7C
	protected override void OnCompleteWork(Worker worker)
	{
		if (this.equippable.assignee != null)
		{
			Ownables soleOwner = this.equippable.assignee.GetSoleOwner();
			if (soleOwner)
			{
				soleOwner.GetComponent<Equipment>().Equip(this.equippable);
			}
		}
	}

	// Token: 0x060033C3 RID: 13251 RVA: 0x001169C0 File Offset: 0x00114BC0
	protected override void OnStopWork(Worker worker)
	{
		this.workTimeRemaining = this.GetWorkTime();
		base.OnStopWork(worker);
	}

	// Token: 0x04001FC1 RID: 8129
	[MyCmpReq]
	private Equippable equippable;

	// Token: 0x04001FC2 RID: 8130
	private Chore chore;

	// Token: 0x04001FC3 RID: 8131
	private global::QualityLevel quality;
}
