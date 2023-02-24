using System;
using System.Collections.Generic;
using Klei.AI;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000605 RID: 1541
public class MassageTable : RelaxationPoint, IGameObjectEffectDescriptor, IActivationRangeTarget
{
	// Token: 0x1700028F RID: 655
	// (get) Token: 0x06002829 RID: 10281 RVA: 0x000D5A23 File Offset: 0x000D3C23
	public string ActivateTooltip
	{
		get
		{
			return BUILDINGS.PREFABS.MASSAGETABLE.ACTIVATE_TOOLTIP;
		}
	}

	// Token: 0x17000290 RID: 656
	// (get) Token: 0x0600282A RID: 10282 RVA: 0x000D5A2F File Offset: 0x000D3C2F
	public string DeactivateTooltip
	{
		get
		{
			return BUILDINGS.PREFABS.MASSAGETABLE.DEACTIVATE_TOOLTIP;
		}
	}

	// Token: 0x0600282B RID: 10283 RVA: 0x000D5A3B File Offset: 0x000D3C3B
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<MassageTable>(-905833192, MassageTable.OnCopySettingsDelegate);
	}

	// Token: 0x0600282C RID: 10284 RVA: 0x000D5A54 File Offset: 0x000D3C54
	private void OnCopySettings(object data)
	{
		MassageTable component = ((GameObject)data).GetComponent<MassageTable>();
		if (component != null)
		{
			this.ActivateValue = component.ActivateValue;
			this.DeactivateValue = component.DeactivateValue;
		}
	}

	// Token: 0x0600282D RID: 10285 RVA: 0x000D5A90 File Offset: 0x000D3C90
	protected override void OnCompleteWork(Worker worker)
	{
		base.OnCompleteWork(worker);
		Effects component = worker.GetComponent<Effects>();
		for (int i = 0; i < MassageTable.EffectsRemoved.Length; i++)
		{
			string text = MassageTable.EffectsRemoved[i];
			component.Remove(text);
		}
	}

	// Token: 0x0600282E RID: 10286 RVA: 0x000D5ACC File Offset: 0x000D3CCC
	public new List<Descriptor> GetDescriptors(GameObject go)
	{
		List<Descriptor> list = new List<Descriptor>();
		Descriptor descriptor = default(Descriptor);
		descriptor.SetupDescriptor(string.Format(UI.BUILDINGEFFECTS.STRESSREDUCEDPERMINUTE, GameUtil.GetFormattedPercent(this.stressModificationValue / 600f * 60f, GameUtil.TimeSlice.None)), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.STRESSREDUCEDPERMINUTE, GameUtil.GetFormattedPercent(this.stressModificationValue / 600f * 60f, GameUtil.TimeSlice.None)), Descriptor.DescriptorType.Effect);
		list.Add(descriptor);
		if (MassageTable.EffectsRemoved.Length != 0)
		{
			Descriptor descriptor2 = default(Descriptor);
			descriptor2.SetupDescriptor(UI.BUILDINGEFFECTS.REMOVESEFFECTSUBTITLE, UI.BUILDINGEFFECTS.TOOLTIPS.REMOVESEFFECTSUBTITLE, Descriptor.DescriptorType.Effect);
			list.Add(descriptor2);
			for (int i = 0; i < MassageTable.EffectsRemoved.Length; i++)
			{
				string text = MassageTable.EffectsRemoved[i];
				string text2 = Strings.Get("STRINGS.DUPLICANTS.MODIFIERS." + text.ToUpper() + ".NAME");
				string text3 = Strings.Get("STRINGS.DUPLICANTS.MODIFIERS." + text.ToUpper() + ".CAUSE");
				Descriptor descriptor3 = default(Descriptor);
				descriptor3.IncreaseIndent();
				descriptor3.SetupDescriptor("• " + string.Format(UI.BUILDINGEFFECTS.REMOVEDEFFECT, text2), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.REMOVEDEFFECT, text3), Descriptor.DescriptorType.Effect);
				list.Add(descriptor3);
			}
		}
		return list;
	}

	// Token: 0x0600282F RID: 10287 RVA: 0x000D5C2C File Offset: 0x000D3E2C
	protected override WorkChore<RelaxationPoint> CreateWorkChore()
	{
		WorkChore<RelaxationPoint> workChore = new WorkChore<RelaxationPoint>(Db.Get().ChoreTypes.StressHeal, this, null, true, null, null, null, false, null, true, true, null, false, true, false, PriorityScreen.PriorityClass.high, 5, false, true);
		workChore.AddPrecondition(ChorePreconditions.instance.IsNotARobot, this);
		workChore.AddPrecondition(MassageTable.IsStressAboveActivationRange, this);
		return workChore;
	}

	// Token: 0x17000291 RID: 657
	// (get) Token: 0x06002830 RID: 10288 RVA: 0x000D5C7C File Offset: 0x000D3E7C
	// (set) Token: 0x06002831 RID: 10289 RVA: 0x000D5C84 File Offset: 0x000D3E84
	public float ActivateValue
	{
		get
		{
			return this.activateValue;
		}
		set
		{
			this.activateValue = value;
		}
	}

	// Token: 0x17000292 RID: 658
	// (get) Token: 0x06002832 RID: 10290 RVA: 0x000D5C8D File Offset: 0x000D3E8D
	// (set) Token: 0x06002833 RID: 10291 RVA: 0x000D5C95 File Offset: 0x000D3E95
	public float DeactivateValue
	{
		get
		{
			return this.stopStressingValue;
		}
		set
		{
			this.stopStressingValue = value;
		}
	}

	// Token: 0x17000293 RID: 659
	// (get) Token: 0x06002834 RID: 10292 RVA: 0x000D5C9E File Offset: 0x000D3E9E
	public bool UseWholeNumbers
	{
		get
		{
			return true;
		}
	}

	// Token: 0x17000294 RID: 660
	// (get) Token: 0x06002835 RID: 10293 RVA: 0x000D5CA1 File Offset: 0x000D3EA1
	public float MinValue
	{
		get
		{
			return 0f;
		}
	}

	// Token: 0x17000295 RID: 661
	// (get) Token: 0x06002836 RID: 10294 RVA: 0x000D5CA8 File Offset: 0x000D3EA8
	public float MaxValue
	{
		get
		{
			return 100f;
		}
	}

	// Token: 0x17000296 RID: 662
	// (get) Token: 0x06002837 RID: 10295 RVA: 0x000D5CAF File Offset: 0x000D3EAF
	public string ActivationRangeTitleText
	{
		get
		{
			return UI.UISIDESCREENS.ACTIVATION_RANGE_SIDE_SCREEN.NAME;
		}
	}

	// Token: 0x17000297 RID: 663
	// (get) Token: 0x06002838 RID: 10296 RVA: 0x000D5CBB File Offset: 0x000D3EBB
	public string ActivateSliderLabelText
	{
		get
		{
			return UI.UISIDESCREENS.ACTIVATION_RANGE_SIDE_SCREEN.ACTIVATE;
		}
	}

	// Token: 0x17000298 RID: 664
	// (get) Token: 0x06002839 RID: 10297 RVA: 0x000D5CC7 File Offset: 0x000D3EC7
	public string DeactivateSliderLabelText
	{
		get
		{
			return UI.UISIDESCREENS.ACTIVATION_RANGE_SIDE_SCREEN.DEACTIVATE;
		}
	}

	// Token: 0x040017A8 RID: 6056
	[Serialize]
	private float activateValue = 50f;

	// Token: 0x040017A9 RID: 6057
	private static readonly string[] EffectsRemoved = new string[] { "SoreBack" };

	// Token: 0x040017AA RID: 6058
	private static readonly EventSystem.IntraObjectHandler<MassageTable> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<MassageTable>(delegate(MassageTable component, object data)
	{
		component.OnCopySettings(data);
	});

	// Token: 0x040017AB RID: 6059
	private static readonly Chore.Precondition IsStressAboveActivationRange = new Chore.Precondition
	{
		id = "IsStressAboveActivationRange",
		description = DUPLICANTS.CHORES.PRECONDITIONS.IS_STRESS_ABOVE_ACTIVATION_RANGE,
		fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			IActivationRangeTarget activationRangeTarget = (IActivationRangeTarget)data;
			return Db.Get().Amounts.Stress.Lookup(context.consumerState.gameObject).value >= activationRangeTarget.ActivateValue;
		}
	};
}
