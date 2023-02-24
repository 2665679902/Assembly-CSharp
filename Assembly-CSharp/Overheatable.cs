using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x020004B3 RID: 1203
[SkipSaveFileSerialization]
public class Overheatable : StateMachineComponent<Overheatable.StatesInstance>, IGameObjectEffectDescriptor
{
	// Token: 0x06001B78 RID: 7032 RVA: 0x00091CE1 File Offset: 0x0008FEE1
	public void ResetTemperature()
	{
		base.GetComponent<PrimaryElement>().Temperature = 293.15f;
	}

	// Token: 0x06001B79 RID: 7033 RVA: 0x00091CF4 File Offset: 0x0008FEF4
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.overheatTemp = this.GetAttributes().Add(Db.Get().BuildingAttributes.OverheatTemperature);
		this.fatalTemp = this.GetAttributes().Add(Db.Get().BuildingAttributes.FatalTemperature);
	}

	// Token: 0x06001B7A RID: 7034 RVA: 0x00091D48 File Offset: 0x0008FF48
	private void InitializeModifiers()
	{
		if (this.modifiersInitialized)
		{
			return;
		}
		this.modifiersInitialized = true;
		AttributeModifier attributeModifier = new AttributeModifier(this.overheatTemp.Id, this.baseOverheatTemp, UI.TOOLTIPS.BASE_VALUE, false, false, true);
		AttributeModifier attributeModifier2 = new AttributeModifier(this.fatalTemp.Id, this.baseFatalTemp, UI.TOOLTIPS.BASE_VALUE, false, false, true);
		this.GetAttributes().Add(attributeModifier);
		this.GetAttributes().Add(attributeModifier2);
	}

	// Token: 0x06001B7B RID: 7035 RVA: 0x00091DC8 File Offset: 0x0008FFC8
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.InitializeModifiers();
		HandleVector<int>.Handle handle = GameComps.StructureTemperatures.GetHandle(base.gameObject);
		if (handle.IsValid() && GameComps.StructureTemperatures.IsEnabled(handle))
		{
			GameComps.StructureTemperatures.Disable(handle);
			GameComps.StructureTemperatures.Enable(handle);
		}
		base.smi.StartSM();
	}

	// Token: 0x1700011C RID: 284
	// (get) Token: 0x06001B7C RID: 7036 RVA: 0x00091E29 File Offset: 0x00090029
	public float OverheatTemperature
	{
		get
		{
			this.InitializeModifiers();
			if (this.overheatTemp == null)
			{
				return 10000f;
			}
			return this.overheatTemp.GetTotalValue();
		}
	}

	// Token: 0x06001B7D RID: 7037 RVA: 0x00091E4C File Offset: 0x0009004C
	public Notification CreateOverheatedNotification()
	{
		KSelectable component = base.GetComponent<KSelectable>();
		return new Notification(MISC.NOTIFICATIONS.BUILDINGOVERHEATED.NAME, NotificationType.BadMinor, (List<Notification> notificationList, object data) => MISC.NOTIFICATIONS.BUILDINGOVERHEATED.TOOLTIP + notificationList.ReduceMessages(false), "/t• " + component.GetProperName(), false, 0f, null, null, null, true, false, false);
	}

	// Token: 0x06001B7E RID: 7038 RVA: 0x00091EAC File Offset: 0x000900AC
	private static string ToolTipResolver(List<Notification> notificationList, object data)
	{
		string text = "";
		for (int i = 0; i < notificationList.Count; i++)
		{
			Notification notification = notificationList[i];
			text += (string)notification.tooltipData;
			if (i < notificationList.Count - 1)
			{
				text += "\n";
			}
		}
		return string.Format(MISC.NOTIFICATIONS.BUILDINGOVERHEATED.TOOLTIP, text);
	}

	// Token: 0x06001B7F RID: 7039 RVA: 0x00091F14 File Offset: 0x00090114
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		List<Descriptor> list = new List<Descriptor>();
		if (this.overheatTemp != null && this.fatalTemp != null)
		{
			string formattedValue = this.overheatTemp.GetFormattedValue();
			string formattedValue2 = this.fatalTemp.GetFormattedValue();
			string text = UI.BUILDINGEFFECTS.TOOLTIPS.OVERHEAT_TEMP;
			text = text + "\n\n" + this.overheatTemp.GetAttributeValueTooltip();
			Descriptor descriptor = new Descriptor(string.Format(UI.BUILDINGEFFECTS.OVERHEAT_TEMP, formattedValue, formattedValue2), string.Format(text, formattedValue, formattedValue2), Descriptor.DescriptorType.Effect, false);
			list.Add(descriptor);
		}
		else if (this.baseOverheatTemp != 0f)
		{
			string formattedTemperature = GameUtil.GetFormattedTemperature(this.baseOverheatTemp, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false);
			string formattedTemperature2 = GameUtil.GetFormattedTemperature(this.baseFatalTemp, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false);
			string text2 = UI.BUILDINGEFFECTS.TOOLTIPS.OVERHEAT_TEMP;
			Descriptor descriptor2 = new Descriptor(string.Format(UI.BUILDINGEFFECTS.OVERHEAT_TEMP, formattedTemperature, formattedTemperature2), string.Format(text2, formattedTemperature, formattedTemperature2), Descriptor.DescriptorType.Effect, false);
			list.Add(descriptor2);
		}
		return list;
	}

	// Token: 0x04000F56 RID: 3926
	private bool modifiersInitialized;

	// Token: 0x04000F57 RID: 3927
	private AttributeInstance overheatTemp;

	// Token: 0x04000F58 RID: 3928
	private AttributeInstance fatalTemp;

	// Token: 0x04000F59 RID: 3929
	public float baseOverheatTemp;

	// Token: 0x04000F5A RID: 3930
	public float baseFatalTemp;

	// Token: 0x020010EF RID: 4335
	public class StatesInstance : GameStateMachine<Overheatable.States, Overheatable.StatesInstance, Overheatable, object>.GameInstance
	{
		// Token: 0x060074E4 RID: 29924 RVA: 0x002B45EC File Offset: 0x002B27EC
		public StatesInstance(Overheatable smi)
			: base(smi)
		{
		}

		// Token: 0x060074E5 RID: 29925 RVA: 0x002B45F8 File Offset: 0x002B27F8
		public void TryDoOverheatDamage()
		{
			if (Time.time - this.lastOverheatDamageTime < 7.5f)
			{
				return;
			}
			this.lastOverheatDamageTime += 7.5f;
			base.master.Trigger(-794517298, new BuildingHP.DamageSourceInfo
			{
				damage = 1,
				source = BUILDINGS.DAMAGESOURCES.BUILDING_OVERHEATED,
				popString = UI.GAMEOBJECTEFFECTS.DAMAGE_POPS.OVERHEAT,
				fullDamageEffectName = "smoke_damage_kanim"
			});
		}

		// Token: 0x04005922 RID: 22818
		public float lastOverheatDamageTime;
	}

	// Token: 0x020010F0 RID: 4336
	public class States : GameStateMachine<Overheatable.States, Overheatable.StatesInstance, Overheatable>
	{
		// Token: 0x060074E6 RID: 29926 RVA: 0x002B4680 File Offset: 0x002B2880
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.safeTemperature;
			this.root.EventTransition(GameHashes.BuildingBroken, this.invulnerable, null);
			this.invulnerable.EventHandler(GameHashes.BuildingPartiallyRepaired, delegate(Overheatable.StatesInstance smi)
			{
				smi.master.ResetTemperature();
			}).EventTransition(GameHashes.BuildingPartiallyRepaired, this.safeTemperature, null);
			this.safeTemperature.TriggerOnEnter(GameHashes.OptimalTemperatureAchieved, null).EventTransition(GameHashes.BuildingOverheated, this.overheated, null);
			this.overheated.Enter(delegate(Overheatable.StatesInstance smi)
			{
				Tutorial.Instance.TutorialMessage(Tutorial.TutorialMessages.TM_OverheatingBuildings, true);
			}).EventTransition(GameHashes.BuildingNoLongerOverheated, this.safeTemperature, null).ToggleStatusItem(Db.Get().BuildingStatusItems.Overheated, null)
				.ToggleNotification((Overheatable.StatesInstance smi) => smi.master.CreateOverheatedNotification())
				.TriggerOnEnter(GameHashes.TooHotWarning, null)
				.Enter("InitOverheatTime", delegate(Overheatable.StatesInstance smi)
				{
					smi.lastOverheatDamageTime = Time.time;
				})
				.Update("OverheatDamage", delegate(Overheatable.StatesInstance smi, float dt)
				{
					smi.TryDoOverheatDamage();
				}, UpdateRate.SIM_4000ms, false);
		}

		// Token: 0x04005923 RID: 22819
		public GameStateMachine<Overheatable.States, Overheatable.StatesInstance, Overheatable, object>.State invulnerable;

		// Token: 0x04005924 RID: 22820
		public GameStateMachine<Overheatable.States, Overheatable.StatesInstance, Overheatable, object>.State safeTemperature;

		// Token: 0x04005925 RID: 22821
		public GameStateMachine<Overheatable.States, Overheatable.StatesInstance, Overheatable, object>.State overheated;
	}
}
