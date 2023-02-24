using System;
using Klei;
using Klei.AI;
using TUNING;
using UnityEngine;

// Token: 0x0200080D RID: 2061
[AddComponentMenu("KMonoBehaviour/Workable/MechanicalSurfboardWorkable")]
public class MechanicalSurfboardWorkable : Workable, IWorkerPrioritizable
{
	// Token: 0x06003BC5 RID: 15301 RVA: 0x0014B35D File Offset: 0x0014955D
	private MechanicalSurfboardWorkable()
	{
		base.SetReportType(ReportManager.ReportType.PersonalTime);
	}

	// Token: 0x06003BC6 RID: 15302 RVA: 0x0014B36D File Offset: 0x0014956D
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.showProgressBar = true;
		this.resetProgressOnStop = true;
		this.synchronizeAnims = true;
		base.SetWorkTime(30f);
		this.surfboard = base.GetComponent<MechanicalSurfboard>();
	}

	// Token: 0x06003BC7 RID: 15303 RVA: 0x0014B3A1 File Offset: 0x001495A1
	protected override void OnStartWork(Worker worker)
	{
		this.operational.SetActive(true, false);
		worker.GetComponent<Effects>().Add("MechanicalSurfing", false);
	}

	// Token: 0x06003BC8 RID: 15304 RVA: 0x0014B3C4 File Offset: 0x001495C4
	public override Workable.AnimInfo GetAnim(Worker worker)
	{
		Workable.AnimInfo animInfo = default(Workable.AnimInfo);
		AttributeInstance attributeInstance = worker.GetAttributes().Get(Db.Get().Attributes.Athletics);
		if (attributeInstance.GetTotalValue() <= 7f)
		{
			animInfo.overrideAnims = new KAnimFile[] { Assets.GetAnim(this.surfboard.interactAnims[0]) };
		}
		else if (attributeInstance.GetTotalValue() <= 15f)
		{
			animInfo.overrideAnims = new KAnimFile[] { Assets.GetAnim(this.surfboard.interactAnims[1]) };
		}
		else
		{
			animInfo.overrideAnims = new KAnimFile[] { Assets.GetAnim(this.surfboard.interactAnims[2]) };
		}
		return animInfo;
	}

	// Token: 0x06003BC9 RID: 15305 RVA: 0x0014B488 File Offset: 0x00149688
	protected override bool OnWorkTick(Worker worker, float dt)
	{
		Building component = base.GetComponent<Building>();
		MechanicalSurfboard component2 = base.GetComponent<MechanicalSurfboard>();
		int widthInCells = component.Def.WidthInCells;
		int num = -(widthInCells - 1) / 2;
		int num2 = widthInCells / 2;
		int num3 = UnityEngine.Random.Range(num, num2);
		float num4 = component2.waterSpillRateKG * dt;
		float num5;
		SimUtil.DiseaseInfo diseaseInfo;
		float num6;
		base.GetComponent<Storage>().ConsumeAndGetDisease(SimHashes.Water.CreateTag(), num4, out num5, out diseaseInfo, out num6);
		int num7 = Grid.OffsetCell(Grid.PosToCell(base.gameObject), new CellOffset(num3, 0));
		ushort elementIndex = ElementLoader.GetElementIndex(SimHashes.Water);
		FallingWater.instance.AddParticle(num7, elementIndex, num5, num6, diseaseInfo.idx, diseaseInfo.count, true, false, false, false);
		return false;
	}

	// Token: 0x06003BCA RID: 15306 RVA: 0x0014B530 File Offset: 0x00149730
	protected override void OnCompleteWork(Worker worker)
	{
		Effects component = worker.GetComponent<Effects>();
		if (!string.IsNullOrEmpty(this.surfboard.specificEffect))
		{
			component.Add(this.surfboard.specificEffect, true);
		}
		if (!string.IsNullOrEmpty(this.surfboard.trackingEffect))
		{
			component.Add(this.surfboard.trackingEffect, true);
		}
	}

	// Token: 0x06003BCB RID: 15307 RVA: 0x0014B58E File Offset: 0x0014978E
	protected override void OnStopWork(Worker worker)
	{
		this.operational.SetActive(false, false);
		worker.GetComponent<Effects>().Remove("MechanicalSurfing");
	}

	// Token: 0x06003BCC RID: 15308 RVA: 0x0014B5B0 File Offset: 0x001497B0
	public bool GetWorkerPriority(Worker worker, out int priority)
	{
		priority = this.basePriority;
		Effects component = worker.GetComponent<Effects>();
		if (!string.IsNullOrEmpty(this.surfboard.trackingEffect) && component.HasEffect(this.surfboard.trackingEffect))
		{
			priority = 0;
			return false;
		}
		if (!string.IsNullOrEmpty(this.surfboard.specificEffect) && component.HasEffect(this.surfboard.specificEffect))
		{
			priority = RELAXATION.PRIORITY.RECENTLY_USED;
		}
		return true;
	}

	// Token: 0x040026F4 RID: 9972
	[MyCmpReq]
	private Operational operational;

	// Token: 0x040026F5 RID: 9973
	public int basePriority;

	// Token: 0x040026F6 RID: 9974
	private MechanicalSurfboard surfboard;
}
