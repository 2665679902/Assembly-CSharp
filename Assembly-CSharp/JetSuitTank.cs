using System;
using System.Collections.Generic;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x020007E6 RID: 2022
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/JetSuitTank")]
public class JetSuitTank : KMonoBehaviour, IGameObjectEffectDescriptor
{
	// Token: 0x06003A42 RID: 14914 RVA: 0x00142DE6 File Offset: 0x00140FE6
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.amount = 25f;
		base.Subscribe<JetSuitTank>(-1617557748, JetSuitTank.OnEquippedDelegate);
		base.Subscribe<JetSuitTank>(-170173755, JetSuitTank.OnUnequippedDelegate);
	}

	// Token: 0x06003A43 RID: 14915 RVA: 0x00142E1B File Offset: 0x0014101B
	public float PercentFull()
	{
		return this.amount / 25f;
	}

	// Token: 0x06003A44 RID: 14916 RVA: 0x00142E29 File Offset: 0x00141029
	public bool IsEmpty()
	{
		return this.amount <= 0f;
	}

	// Token: 0x06003A45 RID: 14917 RVA: 0x00142E3B File Offset: 0x0014103B
	public bool IsFull()
	{
		return this.PercentFull() >= 1f;
	}

	// Token: 0x06003A46 RID: 14918 RVA: 0x00142E4D File Offset: 0x0014104D
	public bool NeedsRecharging()
	{
		return this.PercentFull() < 0.25f;
	}

	// Token: 0x06003A47 RID: 14919 RVA: 0x00142E5C File Offset: 0x0014105C
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		List<Descriptor> list = new List<Descriptor>();
		string text = string.Format(UI.UISIDESCREENS.FABRICATORSIDESCREEN.EFFECTS.JETSUIT_TANK, GameUtil.GetFormattedMass(this.amount, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"));
		list.Add(new Descriptor(text, text, Descriptor.DescriptorType.Effect, false));
		return list;
	}

	// Token: 0x06003A48 RID: 14920 RVA: 0x00142EA0 File Offset: 0x001410A0
	private void OnEquipped(object data)
	{
		Equipment equipment = (Equipment)data;
		NameDisplayScreen.Instance.SetSuitFuelDisplay(equipment.GetComponent<MinionAssignablesProxy>().GetTargetGameObject(), new Func<float>(this.PercentFull), true);
		this.jetSuitMonitor = new JetSuitMonitor.Instance(this, equipment.GetComponent<MinionAssignablesProxy>().GetTargetGameObject());
		this.jetSuitMonitor.StartSM();
		if (this.IsEmpty())
		{
			equipment.GetComponent<MinionAssignablesProxy>().GetTargetGameObject().AddTag(GameTags.JetSuitOutOfFuel);
		}
	}

	// Token: 0x06003A49 RID: 14921 RVA: 0x00142F18 File Offset: 0x00141118
	private void OnUnequipped(object data)
	{
		Equipment equipment = (Equipment)data;
		if (!equipment.destroyed)
		{
			equipment.GetComponent<MinionAssignablesProxy>().GetTargetGameObject().RemoveTag(GameTags.JetSuitOutOfFuel);
			NameDisplayScreen.Instance.SetSuitFuelDisplay(equipment.GetComponent<MinionAssignablesProxy>().GetTargetGameObject(), null, false);
			Navigator component = equipment.GetComponent<MinionAssignablesProxy>().GetTargetGameObject().GetComponent<Navigator>();
			if (component && component.CurrentNavType == NavType.Hover)
			{
				component.SetCurrentNavType(NavType.Floor);
			}
		}
		if (this.jetSuitMonitor != null)
		{
			this.jetSuitMonitor.StopSM("Removed jetsuit tank");
			this.jetSuitMonitor = null;
		}
	}

	// Token: 0x0400263E RID: 9790
	[MyCmpGet]
	private ElementEmitter elementConverter;

	// Token: 0x0400263F RID: 9791
	[Serialize]
	public float amount;

	// Token: 0x04002640 RID: 9792
	public const float FUEL_CAPACITY = 25f;

	// Token: 0x04002641 RID: 9793
	public const float FUEL_BURN_RATE = 0.1f;

	// Token: 0x04002642 RID: 9794
	public const float CO2_EMITTED_PER_FUEL_BURNED = 3f;

	// Token: 0x04002643 RID: 9795
	public const float EMIT_TEMPERATURE = 473.15f;

	// Token: 0x04002644 RID: 9796
	public const float REFILL_PERCENT = 0.25f;

	// Token: 0x04002645 RID: 9797
	private JetSuitMonitor.Instance jetSuitMonitor;

	// Token: 0x04002646 RID: 9798
	private static readonly EventSystem.IntraObjectHandler<JetSuitTank> OnEquippedDelegate = new EventSystem.IntraObjectHandler<JetSuitTank>(delegate(JetSuitTank component, object data)
	{
		component.OnEquipped(data);
	});

	// Token: 0x04002647 RID: 9799
	private static readonly EventSystem.IntraObjectHandler<JetSuitTank> OnUnequippedDelegate = new EventSystem.IntraObjectHandler<JetSuitTank>(delegate(JetSuitTank component, object data)
	{
		component.OnUnequipped(data);
	});
}
