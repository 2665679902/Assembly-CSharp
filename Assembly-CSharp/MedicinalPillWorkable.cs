using System;
using Klei.AI;
using UnityEngine;

// Token: 0x02000810 RID: 2064
[AddComponentMenu("KMonoBehaviour/Workable/MedicinalPillWorkable")]
public class MedicinalPillWorkable : Workable, IConsumableUIItem
{
	// Token: 0x06003BD4 RID: 15316 RVA: 0x0014B9C0 File Offset: 0x00149BC0
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.SetWorkTime(10f);
		this.showProgressBar = false;
		this.synchronizeAnims = false;
		base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Main, Db.Get().BuildingStatusItems.Normal, null);
		this.CreateChore();
	}

	// Token: 0x06003BD5 RID: 15317 RVA: 0x0014BA20 File Offset: 0x00149C20
	protected override void OnCompleteWork(Worker worker)
	{
		if (!string.IsNullOrEmpty(this.pill.info.effect))
		{
			Effects component = worker.GetComponent<Effects>();
			EffectInstance effectInstance = component.Get(this.pill.info.effect);
			if (effectInstance != null)
			{
				effectInstance.timeRemaining = effectInstance.effect.duration;
			}
			else
			{
				component.Add(this.pill.info.effect, true);
			}
		}
		Sicknesses sicknesses = worker.GetSicknesses();
		foreach (string text in this.pill.info.curedSicknesses)
		{
			SicknessInstance sicknessInstance = sicknesses.Get(text);
			if (sicknessInstance != null)
			{
				Game.Instance.savedInfo.curedDisease = true;
				sicknessInstance.Cure();
			}
		}
		base.gameObject.DeleteObject();
	}

	// Token: 0x06003BD6 RID: 15318 RVA: 0x0014BB14 File Offset: 0x00149D14
	private void CreateChore()
	{
		new TakeMedicineChore(this);
	}

	// Token: 0x06003BD7 RID: 15319 RVA: 0x0014BB20 File Offset: 0x00149D20
	public bool CanBeTakenBy(GameObject consumer)
	{
		if (!string.IsNullOrEmpty(this.pill.info.effect))
		{
			Effects component = consumer.GetComponent<Effects>();
			if (component == null || component.HasEffect(this.pill.info.effect))
			{
				return false;
			}
		}
		if (this.pill.info.medicineType == MedicineInfo.MedicineType.Booster)
		{
			return true;
		}
		Sicknesses sicknesses = consumer.GetSicknesses();
		if (this.pill.info.medicineType == MedicineInfo.MedicineType.CureAny && sicknesses.Count > 0)
		{
			return true;
		}
		foreach (SicknessInstance sicknessInstance in sicknesses)
		{
			if (this.pill.info.curedSicknesses.Contains(sicknessInstance.modifier.Id))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x1700043E RID: 1086
	// (get) Token: 0x06003BD8 RID: 15320 RVA: 0x0014BC08 File Offset: 0x00149E08
	public string ConsumableId
	{
		get
		{
			return this.PrefabID().Name;
		}
	}

	// Token: 0x1700043F RID: 1087
	// (get) Token: 0x06003BD9 RID: 15321 RVA: 0x0014BC23 File Offset: 0x00149E23
	public string ConsumableName
	{
		get
		{
			return this.GetProperName();
		}
	}

	// Token: 0x17000440 RID: 1088
	// (get) Token: 0x06003BDA RID: 15322 RVA: 0x0014BC2B File Offset: 0x00149E2B
	public int MajorOrder
	{
		get
		{
			return (int)(this.pill.info.medicineType + 1000);
		}
	}

	// Token: 0x17000441 RID: 1089
	// (get) Token: 0x06003BDB RID: 15323 RVA: 0x0014BC43 File Offset: 0x00149E43
	public int MinorOrder
	{
		get
		{
			return 0;
		}
	}

	// Token: 0x17000442 RID: 1090
	// (get) Token: 0x06003BDC RID: 15324 RVA: 0x0014BC46 File Offset: 0x00149E46
	public bool Display
	{
		get
		{
			return true;
		}
	}

	// Token: 0x040026FD RID: 9981
	public MedicinalPill pill;
}
