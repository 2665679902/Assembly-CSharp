using System;
using System.Collections.Generic;
using Klei;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x020009A1 RID: 2465
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/SuitTank")]
public class SuitTank : KMonoBehaviour, IGameObjectEffectDescriptor, OxygenBreather.IGasProvider
{
	// Token: 0x06004923 RID: 18723 RVA: 0x00199B88 File Offset: 0x00197D88
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<SuitTank>(-1617557748, SuitTank.OnEquippedDelegate);
		base.Subscribe<SuitTank>(-170173755, SuitTank.OnUnequippedDelegate);
	}

	// Token: 0x06004924 RID: 18724 RVA: 0x00199BB4 File Offset: 0x00197DB4
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (this.amount != 0f)
		{
			this.storage.AddGasChunk(SimHashes.Oxygen, this.amount, base.GetComponent<PrimaryElement>().Temperature, byte.MaxValue, 0, false, true);
			this.amount = 0f;
		}
	}

	// Token: 0x06004925 RID: 18725 RVA: 0x00199C09 File Offset: 0x00197E09
	public float GetTankAmount()
	{
		if (this.storage == null)
		{
			this.storage = base.GetComponent<Storage>();
		}
		return this.storage.GetMassAvailable(this.elementTag);
	}

	// Token: 0x06004926 RID: 18726 RVA: 0x00199C36 File Offset: 0x00197E36
	public float PercentFull()
	{
		return this.GetTankAmount() / this.capacity;
	}

	// Token: 0x06004927 RID: 18727 RVA: 0x00199C45 File Offset: 0x00197E45
	public bool IsEmpty()
	{
		return this.GetTankAmount() <= 0f;
	}

	// Token: 0x06004928 RID: 18728 RVA: 0x00199C57 File Offset: 0x00197E57
	public bool IsFull()
	{
		return this.PercentFull() >= 1f;
	}

	// Token: 0x06004929 RID: 18729 RVA: 0x00199C69 File Offset: 0x00197E69
	public bool NeedsRecharging()
	{
		return this.PercentFull() < 0.25f;
	}

	// Token: 0x0600492A RID: 18730 RVA: 0x00199C78 File Offset: 0x00197E78
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		List<Descriptor> list = new List<Descriptor>();
		if (this.elementTag == GameTags.Breathable)
		{
			string text = (this.underwaterSupport ? string.Format(UI.UISIDESCREENS.FABRICATORSIDESCREEN.EFFECTS.OXYGEN_TANK_UNDERWATER, GameUtil.GetFormattedMass(this.GetTankAmount(), GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}")) : string.Format(UI.UISIDESCREENS.FABRICATORSIDESCREEN.EFFECTS.OXYGEN_TANK, GameUtil.GetFormattedMass(this.GetTankAmount(), GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}")));
			list.Add(new Descriptor(text, text, Descriptor.DescriptorType.Effect, false));
		}
		return list;
	}

	// Token: 0x0600492B RID: 18731 RVA: 0x00199CFC File Offset: 0x00197EFC
	private void OnEquipped(object data)
	{
		Equipment equipment = (Equipment)data;
		NameDisplayScreen.Instance.SetSuitTankDisplay(equipment.GetComponent<MinionAssignablesProxy>().GetTargetGameObject(), new Func<float>(this.PercentFull), true);
		OxygenBreather component = equipment.GetComponent<MinionAssignablesProxy>().GetTargetGameObject().GetComponent<OxygenBreather>();
		if (component != null)
		{
			component.SetGasProvider(this);
			component.AddTag(GameTags.HasSuitTank);
		}
	}

	// Token: 0x0600492C RID: 18732 RVA: 0x00199D60 File Offset: 0x00197F60
	private void OnUnequipped(object data)
	{
		Equipment equipment = (Equipment)data;
		if (!equipment.destroyed)
		{
			NameDisplayScreen.Instance.SetSuitTankDisplay(equipment.GetComponent<MinionAssignablesProxy>().GetTargetGameObject(), new Func<float>(this.PercentFull), false);
			OxygenBreather component = equipment.GetComponent<MinionAssignablesProxy>().GetTargetGameObject().GetComponent<OxygenBreather>();
			if (component != null)
			{
				component.SetGasProvider(new GasBreatherFromWorldProvider());
				component.RemoveTag(GameTags.HasSuitTank);
			}
		}
	}

	// Token: 0x0600492D RID: 18733 RVA: 0x00199DCE File Offset: 0x00197FCE
	public void OnSetOxygenBreather(OxygenBreather oxygen_breather)
	{
		this.suitSuffocationMonitor = new SuitSuffocationMonitor.Instance(oxygen_breather, this);
		this.suitSuffocationMonitor.StartSM();
	}

	// Token: 0x0600492E RID: 18734 RVA: 0x00199DE8 File Offset: 0x00197FE8
	public void OnClearOxygenBreather(OxygenBreather oxygen_breather)
	{
		this.suitSuffocationMonitor.StopSM("Removed suit tank");
		this.suitSuffocationMonitor = null;
	}

	// Token: 0x0600492F RID: 18735 RVA: 0x00199E04 File Offset: 0x00198004
	public bool ConsumeGas(OxygenBreather oxygen_breather, float gas_consumed)
	{
		if (this.IsEmpty())
		{
			return false;
		}
		float num;
		SimUtil.DiseaseInfo diseaseInfo;
		float num2;
		this.storage.ConsumeAndGetDisease(this.elementTag, gas_consumed, out num, out diseaseInfo, out num2);
		Game.Instance.accumulators.Accumulate(oxygen_breather.O2Accumulator, num);
		ReportManager.Instance.ReportValue(ReportManager.ReportType.OxygenCreated, -num, oxygen_breather.GetProperName(), null);
		base.Trigger(608245985, base.gameObject);
		return true;
	}

	// Token: 0x06004930 RID: 18736 RVA: 0x00199E70 File Offset: 0x00198070
	public bool ShouldEmitCO2()
	{
		return !base.GetComponent<KPrefabID>().HasTag(GameTags.AirtightSuit);
	}

	// Token: 0x06004931 RID: 18737 RVA: 0x00199E85 File Offset: 0x00198085
	public bool ShouldStoreCO2()
	{
		return base.GetComponent<KPrefabID>().HasTag(GameTags.AirtightSuit);
	}

	// Token: 0x06004932 RID: 18738 RVA: 0x00199E98 File Offset: 0x00198098
	[ContextMenu("SetToRefillAmount")]
	public void SetToRefillAmount()
	{
		float tankAmount = this.GetTankAmount();
		float num = 0.25f * this.capacity;
		if (tankAmount > num)
		{
			this.storage.ConsumeIgnoringDisease(this.elementTag, tankAmount - num);
		}
	}

	// Token: 0x06004933 RID: 18739 RVA: 0x00199ED1 File Offset: 0x001980D1
	[ContextMenu("Empty")]
	public void Empty()
	{
		this.storage.ConsumeIgnoringDisease(this.elementTag, this.GetTankAmount());
	}

	// Token: 0x06004934 RID: 18740 RVA: 0x00199EEA File Offset: 0x001980EA
	[ContextMenu("Fill Tank")]
	public void FillTank()
	{
		this.Empty();
		this.storage.AddGasChunk(SimHashes.Oxygen, this.capacity, 15f, 0, 0, false, false);
	}

	// Token: 0x0400300D RID: 12301
	[Serialize]
	public string element;

	// Token: 0x0400300E RID: 12302
	[Serialize]
	public float amount;

	// Token: 0x0400300F RID: 12303
	public Tag elementTag;

	// Token: 0x04003010 RID: 12304
	[MyCmpReq]
	public Storage storage;

	// Token: 0x04003011 RID: 12305
	public float capacity;

	// Token: 0x04003012 RID: 12306
	public const float REFILL_PERCENT = 0.25f;

	// Token: 0x04003013 RID: 12307
	public bool underwaterSupport;

	// Token: 0x04003014 RID: 12308
	private SuitSuffocationMonitor.Instance suitSuffocationMonitor;

	// Token: 0x04003015 RID: 12309
	private static readonly EventSystem.IntraObjectHandler<SuitTank> OnEquippedDelegate = new EventSystem.IntraObjectHandler<SuitTank>(delegate(SuitTank component, object data)
	{
		component.OnEquipped(data);
	});

	// Token: 0x04003016 RID: 12310
	private static readonly EventSystem.IntraObjectHandler<SuitTank> OnUnequippedDelegate = new EventSystem.IntraObjectHandler<SuitTank>(delegate(SuitTank component, object data)
	{
		component.OnUnequipped(data);
	});
}
