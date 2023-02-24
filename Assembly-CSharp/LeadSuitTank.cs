using System;
using System.Collections.Generic;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x020007F1 RID: 2033
[SerializationConfig(MemberSerialization.OptIn)]
public class LeadSuitTank : KMonoBehaviour, IGameObjectEffectDescriptor
{
	// Token: 0x06003AA3 RID: 15011 RVA: 0x00144CB8 File Offset: 0x00142EB8
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<LeadSuitTank>(-1617557748, LeadSuitTank.OnEquippedDelegate);
		base.Subscribe<LeadSuitTank>(-170173755, LeadSuitTank.OnUnequippedDelegate);
	}

	// Token: 0x06003AA4 RID: 15012 RVA: 0x00144CE2 File Offset: 0x00142EE2
	public float PercentFull()
	{
		return this.batteryCharge;
	}

	// Token: 0x06003AA5 RID: 15013 RVA: 0x00144CEA File Offset: 0x00142EEA
	public bool IsEmpty()
	{
		return this.batteryCharge <= 0f;
	}

	// Token: 0x06003AA6 RID: 15014 RVA: 0x00144CFC File Offset: 0x00142EFC
	public bool IsFull()
	{
		return this.PercentFull() >= 1f;
	}

	// Token: 0x06003AA7 RID: 15015 RVA: 0x00144D0E File Offset: 0x00142F0E
	public bool NeedsRecharging()
	{
		return this.PercentFull() <= 0.25f;
	}

	// Token: 0x06003AA8 RID: 15016 RVA: 0x00144D20 File Offset: 0x00142F20
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		List<Descriptor> list = new List<Descriptor>();
		string text = string.Format(UI.UISIDESCREENS.FABRICATORSIDESCREEN.EFFECTS.LEADSUIT_BATTERY, GameUtil.GetFormattedPercent(this.PercentFull() * 100f, GameUtil.TimeSlice.None));
		list.Add(new Descriptor(text, text, Descriptor.DescriptorType.Effect, false));
		return list;
	}

	// Token: 0x06003AA9 RID: 15017 RVA: 0x00144D64 File Offset: 0x00142F64
	private void OnEquipped(object data)
	{
		Equipment equipment = (Equipment)data;
		NameDisplayScreen.Instance.SetSuitBatteryDisplay(equipment.GetComponent<MinionAssignablesProxy>().GetTargetGameObject(), new Func<float>(this.PercentFull), true);
		this.leadSuitMonitor = new LeadSuitMonitor.Instance(this, equipment.GetComponent<MinionAssignablesProxy>().GetTargetGameObject());
		this.leadSuitMonitor.StartSM();
		if (this.NeedsRecharging())
		{
			equipment.GetComponent<MinionAssignablesProxy>().GetTargetGameObject().AddTag(GameTags.SuitBatteryLow);
		}
	}

	// Token: 0x06003AAA RID: 15018 RVA: 0x00144DDC File Offset: 0x00142FDC
	private void OnUnequipped(object data)
	{
		Equipment equipment = (Equipment)data;
		if (!equipment.destroyed)
		{
			equipment.GetComponent<MinionAssignablesProxy>().GetTargetGameObject().RemoveTag(GameTags.SuitBatteryLow);
			equipment.GetComponent<MinionAssignablesProxy>().GetTargetGameObject().RemoveTag(GameTags.SuitBatteryOut);
			NameDisplayScreen.Instance.SetSuitBatteryDisplay(equipment.GetComponent<MinionAssignablesProxy>().GetTargetGameObject(), null, false);
		}
		if (this.leadSuitMonitor != null)
		{
			this.leadSuitMonitor.StopSM("Removed leadsuit tank");
			this.leadSuitMonitor = null;
		}
	}

	// Token: 0x04002675 RID: 9845
	[Serialize]
	public float batteryCharge = 1f;

	// Token: 0x04002676 RID: 9846
	public const float REFILL_PERCENT = 0.25f;

	// Token: 0x04002677 RID: 9847
	public float batteryDuration = 200f;

	// Token: 0x04002678 RID: 9848
	public float coolingOperationalTemperature = 333.15f;

	// Token: 0x04002679 RID: 9849
	public Tag coolantTag;

	// Token: 0x0400267A RID: 9850
	private LeadSuitMonitor.Instance leadSuitMonitor;

	// Token: 0x0400267B RID: 9851
	private static readonly EventSystem.IntraObjectHandler<LeadSuitTank> OnEquippedDelegate = new EventSystem.IntraObjectHandler<LeadSuitTank>(delegate(LeadSuitTank component, object data)
	{
		component.OnEquipped(data);
	});

	// Token: 0x0400267C RID: 9852
	private static readonly EventSystem.IntraObjectHandler<LeadSuitTank> OnUnequippedDelegate = new EventSystem.IntraObjectHandler<LeadSuitTank>(delegate(LeadSuitTank component, object data)
	{
		component.OnUnequipped(data);
	});
}
