using System;
using Klei.AI;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x0200065F RID: 1631
[AddComponentMenu("KMonoBehaviour/Workable/ToiletWorkableUse")]
public class ToiletWorkableUse : Workable, IGameObjectEffectDescriptor
{
	// Token: 0x06002BCD RID: 11213 RVA: 0x000E6172 File Offset: 0x000E4372
	private ToiletWorkableUse()
	{
		base.SetReportType(ReportManager.ReportType.PersonalTime);
	}

	// Token: 0x06002BCE RID: 11214 RVA: 0x000E6182 File Offset: 0x000E4382
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.showProgressBar = true;
		this.resetProgressOnStop = true;
		this.attributeConverter = Db.Get().AttributeConverters.ToiletSpeed;
		base.SetWorkTime(8.5f);
	}

	// Token: 0x06002BCF RID: 11215 RVA: 0x000E61B8 File Offset: 0x000E43B8
	protected override void OnStartWork(Worker worker)
	{
		base.OnStartWork(worker);
		if (Sim.IsRadiationEnabled() && worker.GetAmounts().Get(Db.Get().Amounts.RadiationBalance).value > 0f)
		{
			worker.gameObject.GetComponent<KSelectable>().AddStatusItem(Db.Get().DuplicantStatusItems.ExpellingRads, null);
		}
		Room roomOfGameObject = Game.Instance.roomProber.GetRoomOfGameObject(base.gameObject);
		if (roomOfGameObject != null)
		{
			roomOfGameObject.roomType.TriggerRoomEffects(base.GetComponent<KPrefabID>(), worker.GetComponent<Effects>());
		}
	}

	// Token: 0x06002BD0 RID: 11216 RVA: 0x000E624A File Offset: 0x000E444A
	protected override void OnStopWork(Worker worker)
	{
		if (Sim.IsRadiationEnabled())
		{
			worker.gameObject.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().DuplicantStatusItems.ExpellingRads, false);
		}
		base.OnStopWork(worker);
	}

	// Token: 0x06002BD1 RID: 11217 RVA: 0x000E627B File Offset: 0x000E447B
	protected override void OnAbortWork(Worker worker)
	{
		if (Sim.IsRadiationEnabled())
		{
			worker.gameObject.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().DuplicantStatusItems.ExpellingRads, false);
		}
		base.OnAbortWork(worker);
	}

	// Token: 0x06002BD2 RID: 11218 RVA: 0x000E62AC File Offset: 0x000E44AC
	protected override void OnCompleteWork(Worker worker)
	{
		Db.Get().Amounts.Bladder.Lookup(worker).SetValue(0f);
		if (Sim.IsRadiationEnabled())
		{
			worker.gameObject.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().DuplicantStatusItems.ExpellingRads, false);
			AmountInstance amountInstance = Db.Get().Amounts.RadiationBalance.Lookup(worker);
			RadiationMonitor.Instance smi = worker.GetSMI<RadiationMonitor.Instance>();
			float num = Math.Min(amountInstance.value, 100f * smi.difficultySettingMod);
			if (num >= 1f)
			{
				PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Negative, Math.Floor((double)num).ToString() + UI.UNITSUFFIXES.RADIATION.RADS, worker.transform, Vector3.up * 2f, 1.5f, false, false);
			}
			amountInstance.ApplyDelta(-num);
		}
		this.timesUsed++;
		base.Trigger(-350347868, worker);
		base.OnCompleteWork(worker);
	}

	// Token: 0x040019EE RID: 6638
	[Serialize]
	public int timesUsed;
}
