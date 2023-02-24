using System;
using System.Collections.Generic;
using Klei.AI;
using Klei.CustomSettings;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200083D RID: 2109
public class RadiationMonitor : GameStateMachine<RadiationMonitor, RadiationMonitor.Instance>
{
	// Token: 0x06003CDE RID: 15582 RVA: 0x00153668 File Offset: 0x00151868
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		default_state = this.init;
		this.init.Transition(null, (RadiationMonitor.Instance smi) => !Sim.IsRadiationEnabled(), UpdateRate.SIM_200ms).Transition(this.active, (RadiationMonitor.Instance smi) => Sim.IsRadiationEnabled(), UpdateRate.SIM_200ms);
		this.active.Update(new Action<RadiationMonitor.Instance, float>(RadiationMonitor.CheckRadiationLevel), UpdateRate.SIM_1000ms, false).DefaultState(this.active.idle);
		this.active.idle.DoNothing().ParamTransition<float>(this.radiationExposure, this.active.sick.deadly, RadiationMonitor.COMPARE_GTE_DEADLY).ParamTransition<float>(this.radiationExposure, this.active.sick.extreme, RadiationMonitor.COMPARE_GTE_EXTREME)
			.ParamTransition<float>(this.radiationExposure, this.active.sick.major, RadiationMonitor.COMPARE_GTE_MAJOR)
			.ParamTransition<float>(this.radiationExposure, this.active.sick.minor, RadiationMonitor.COMPARE_GTE_MINOR);
		this.active.sick.ParamTransition<float>(this.radiationExposure, this.active.idle, RadiationMonitor.COMPARE_LT_MINOR).Enter(delegate(RadiationMonitor.Instance smi)
		{
			smi.sm.isSick.Set(true, smi, false);
		}).Exit(delegate(RadiationMonitor.Instance smi)
		{
			smi.sm.isSick.Set(false, smi, false);
		});
		this.active.sick.minor.ToggleEffect(RadiationMonitor.minorSicknessEffect).ParamTransition<float>(this.radiationExposure, this.active.sick.deadly, RadiationMonitor.COMPARE_GTE_DEADLY).ParamTransition<float>(this.radiationExposure, this.active.sick.extreme, RadiationMonitor.COMPARE_GTE_EXTREME)
			.ParamTransition<float>(this.radiationExposure, this.active.sick.major, RadiationMonitor.COMPARE_GTE_MAJOR)
			.ToggleAnims("anim_loco_radiation1_kanim", 4f, "")
			.ToggleAnims("anim_idle_radiation1_kanim", 4f, "")
			.ToggleExpression(Db.Get().Expressions.Radiation1, null)
			.DefaultState(this.active.sick.minor.waiting);
		this.active.sick.minor.reacting.ToggleChore(new Func<RadiationMonitor.Instance, Chore>(this.CreateVomitChore), this.active.sick.minor.waiting);
		this.active.sick.major.ToggleEffect(RadiationMonitor.majorSicknessEffect).ParamTransition<float>(this.radiationExposure, this.active.sick.deadly, RadiationMonitor.COMPARE_GTE_DEADLY).ParamTransition<float>(this.radiationExposure, this.active.sick.extreme, RadiationMonitor.COMPARE_GTE_EXTREME)
			.ToggleAnims("anim_loco_radiation2_kanim", 4f, "")
			.ToggleAnims("anim_idle_radiation2_kanim", 4f, "")
			.ToggleExpression(Db.Get().Expressions.Radiation2, null)
			.DefaultState(this.active.sick.major.waiting);
		this.active.sick.major.waiting.ScheduleGoTo(120f, this.active.sick.major.vomiting);
		this.active.sick.major.vomiting.ToggleChore(new Func<RadiationMonitor.Instance, Chore>(this.CreateVomitChore), this.active.sick.major.waiting);
		this.active.sick.extreme.ParamTransition<float>(this.radiationExposure, this.active.sick.deadly, RadiationMonitor.COMPARE_GTE_DEADLY).ToggleEffect(RadiationMonitor.extremeSicknessEffect).ToggleAnims("anim_loco_radiation3_kanim", 4f, "")
			.ToggleAnims("anim_idle_radiation3_kanim", 4f, "")
			.ToggleExpression(Db.Get().Expressions.Radiation3, null)
			.DefaultState(this.active.sick.extreme.waiting);
		this.active.sick.extreme.waiting.ScheduleGoTo(60f, this.active.sick.extreme.vomiting);
		this.active.sick.extreme.vomiting.ToggleChore(new Func<RadiationMonitor.Instance, Chore>(this.CreateVomitChore), this.active.sick.extreme.waiting);
		this.active.sick.deadly.ToggleAnims("anim_loco_radiation4_kanim", 4f, "").ToggleAnims("anim_idle_radiation4_kanim", 4f, "").ToggleExpression(Db.Get().Expressions.Radiation4, null)
			.Enter(delegate(RadiationMonitor.Instance smi)
			{
				smi.GetComponent<Health>().Incapacitate(GameTags.RadiationSicknessIncapacitation);
			});
	}

	// Token: 0x06003CDF RID: 15583 RVA: 0x00153BAC File Offset: 0x00151DAC
	private Chore CreateVomitChore(RadiationMonitor.Instance smi)
	{
		Notification notification = new Notification(DUPLICANTS.STATUSITEMS.RADIATIONVOMITING.NOTIFICATION_NAME, NotificationType.Bad, (List<Notification> notificationList, object data) => DUPLICANTS.STATUSITEMS.RADIATIONVOMITING.NOTIFICATION_TOOLTIP + notificationList.ReduceMessages(false), null, true, 0f, null, null, null, true, false, false);
		return new VomitChore(Db.Get().ChoreTypes.Vomit, smi.master, Db.Get().DuplicantStatusItems.Vomiting, notification, null);
	}

	// Token: 0x06003CE0 RID: 15584 RVA: 0x00153C24 File Offset: 0x00151E24
	private static void RadiationRecovery(RadiationMonitor.Instance smi, float dt)
	{
		float num = Db.Get().Attributes.RadiationRecovery.Lookup(smi.gameObject).GetTotalValue() * dt;
		smi.master.gameObject.GetAmounts().Get(Db.Get().Amounts.RadiationBalance).ApplyDelta(num);
		smi.master.Trigger(1556680150, num);
	}

	// Token: 0x06003CE1 RID: 15585 RVA: 0x00153C94 File Offset: 0x00151E94
	private static void CheckRadiationLevel(RadiationMonitor.Instance smi, float dt)
	{
		RadiationMonitor.RadiationRecovery(smi, dt);
		smi.sm.timeUntilNextExposureReact.Delta(-dt, smi);
		smi.sm.timeUntilNextSickReact.Delta(-dt, smi);
		int num = Grid.PosToCell(smi.gameObject);
		if (Grid.IsValidCell(num))
		{
			float num2 = Mathf.Clamp01(1f - Db.Get().Attributes.RadiationResistance.Lookup(smi.gameObject).GetTotalValue());
			float num3 = Grid.Radiation[num] * 1f * num2 / 600f * dt;
			smi.master.gameObject.GetAmounts().Get(Db.Get().Amounts.RadiationBalance).ApplyDelta(num3);
			float num4 = num3 / dt * 600f;
			smi.sm.currentExposurePerCycle.Set(num4, smi, false);
			if (smi.sm.timeUntilNextExposureReact.Get(smi) <= 0f && !smi.HasTag(GameTags.InTransitTube) && RadiationMonitor.COMPARE_REACT(smi, num4))
			{
				smi.sm.timeUntilNextExposureReact.Set(120f, smi, false);
				Emote radiation_Glare = Db.Get().Emotes.Minion.Radiation_Glare;
				smi.master.gameObject.GetSMI<ReactionMonitor.Instance>().AddSelfEmoteReactable(smi.master.gameObject, "RadiationReact", radiation_Glare, true, Db.Get().ChoreTypes.EmoteHighPriority, 0f, 20f, float.NegativeInfinity, 0f, null);
			}
		}
		if (smi.sm.timeUntilNextSickReact.Get(smi) <= 0f && smi.sm.isSick.Get(smi) && !smi.HasTag(GameTags.InTransitTube))
		{
			smi.sm.timeUntilNextSickReact.Set(60f, smi, false);
			Emote radiation_Itch = Db.Get().Emotes.Minion.Radiation_Itch;
			smi.master.gameObject.GetSMI<ReactionMonitor.Instance>().AddSelfEmoteReactable(smi.master.gameObject, "RadiationReact", radiation_Itch, true, Db.Get().ChoreTypes.RadiationPain, 0f, 20f, float.NegativeInfinity, 0f, null);
		}
		smi.sm.radiationExposure.Set(smi.master.gameObject.GetComponent<KSelectable>().GetAmounts().GetValue("RadiationBalance"), smi, false);
	}

	// Token: 0x040027B8 RID: 10168
	public const float BASE_ABSORBTION_RATE = 1f;

	// Token: 0x040027B9 RID: 10169
	public const float MIN_TIME_BETWEEN_EXPOSURE_REACTS = 120f;

	// Token: 0x040027BA RID: 10170
	public const float MIN_TIME_BETWEEN_SICK_REACTS = 60f;

	// Token: 0x040027BB RID: 10171
	public const int VOMITS_PER_CYCLE_MAJOR = 5;

	// Token: 0x040027BC RID: 10172
	public const int VOMITS_PER_CYCLE_EXTREME = 10;

	// Token: 0x040027BD RID: 10173
	public StateMachine<RadiationMonitor, RadiationMonitor.Instance, IStateMachineTarget, object>.FloatParameter radiationExposure;

	// Token: 0x040027BE RID: 10174
	public StateMachine<RadiationMonitor, RadiationMonitor.Instance, IStateMachineTarget, object>.FloatParameter currentExposurePerCycle;

	// Token: 0x040027BF RID: 10175
	public StateMachine<RadiationMonitor, RadiationMonitor.Instance, IStateMachineTarget, object>.BoolParameter isSick;

	// Token: 0x040027C0 RID: 10176
	public StateMachine<RadiationMonitor, RadiationMonitor.Instance, IStateMachineTarget, object>.FloatParameter timeUntilNextExposureReact;

	// Token: 0x040027C1 RID: 10177
	public StateMachine<RadiationMonitor, RadiationMonitor.Instance, IStateMachineTarget, object>.FloatParameter timeUntilNextSickReact;

	// Token: 0x040027C2 RID: 10178
	public static string minorSicknessEffect = "RadiationExposureMinor";

	// Token: 0x040027C3 RID: 10179
	public static string majorSicknessEffect = "RadiationExposureMajor";

	// Token: 0x040027C4 RID: 10180
	public static string extremeSicknessEffect = "RadiationExposureExtreme";

	// Token: 0x040027C5 RID: 10181
	public GameStateMachine<RadiationMonitor, RadiationMonitor.Instance, IStateMachineTarget, object>.State init;

	// Token: 0x040027C6 RID: 10182
	public RadiationMonitor.ActiveStates active;

	// Token: 0x040027C7 RID: 10183
	public static readonly StateMachine<RadiationMonitor, RadiationMonitor.Instance, IStateMachineTarget, object>.Parameter<float>.Callback COMPARE_RECOVERY_IMMEDIATE = (RadiationMonitor.Instance smi, float p) => p > 100f * smi.difficultySettingMod / 2f;

	// Token: 0x040027C8 RID: 10184
	public static readonly StateMachine<RadiationMonitor, RadiationMonitor.Instance, IStateMachineTarget, object>.Parameter<float>.Callback COMPARE_REACT = (RadiationMonitor.Instance smi, float p) => p >= 133f * smi.difficultySettingMod;

	// Token: 0x040027C9 RID: 10185
	public static readonly StateMachine<RadiationMonitor, RadiationMonitor.Instance, IStateMachineTarget, object>.Parameter<float>.Callback COMPARE_LT_MINOR = (RadiationMonitor.Instance smi, float p) => p < 100f * smi.difficultySettingMod;

	// Token: 0x040027CA RID: 10186
	public static readonly StateMachine<RadiationMonitor, RadiationMonitor.Instance, IStateMachineTarget, object>.Parameter<float>.Callback COMPARE_GTE_MINOR = (RadiationMonitor.Instance smi, float p) => p >= 100f * smi.difficultySettingMod;

	// Token: 0x040027CB RID: 10187
	public static readonly StateMachine<RadiationMonitor, RadiationMonitor.Instance, IStateMachineTarget, object>.Parameter<float>.Callback COMPARE_GTE_MAJOR = (RadiationMonitor.Instance smi, float p) => p >= 300f * smi.difficultySettingMod;

	// Token: 0x040027CC RID: 10188
	public static readonly StateMachine<RadiationMonitor, RadiationMonitor.Instance, IStateMachineTarget, object>.Parameter<float>.Callback COMPARE_GTE_EXTREME = (RadiationMonitor.Instance smi, float p) => p >= 600f * smi.difficultySettingMod;

	// Token: 0x040027CD RID: 10189
	public static readonly StateMachine<RadiationMonitor, RadiationMonitor.Instance, IStateMachineTarget, object>.Parameter<float>.Callback COMPARE_GTE_DEADLY = (RadiationMonitor.Instance smi, float p) => p >= 900f * smi.difficultySettingMod;

	// Token: 0x020015CE RID: 5582
	public class ActiveStates : GameStateMachine<RadiationMonitor, RadiationMonitor.Instance, IStateMachineTarget, object>.State
	{
		// Token: 0x040067EB RID: 26603
		public GameStateMachine<RadiationMonitor, RadiationMonitor.Instance, IStateMachineTarget, object>.State idle;

		// Token: 0x040067EC RID: 26604
		public RadiationMonitor.SickStates sick;
	}

	// Token: 0x020015CF RID: 5583
	public class SickStates : GameStateMachine<RadiationMonitor, RadiationMonitor.Instance, IStateMachineTarget, object>.State
	{
		// Token: 0x040067ED RID: 26605
		public RadiationMonitor.SickStates.MinorStates minor;

		// Token: 0x040067EE RID: 26606
		public RadiationMonitor.SickStates.MajorStates major;

		// Token: 0x040067EF RID: 26607
		public RadiationMonitor.SickStates.ExtremeStates extreme;

		// Token: 0x040067F0 RID: 26608
		public GameStateMachine<RadiationMonitor, RadiationMonitor.Instance, IStateMachineTarget, object>.State deadly;

		// Token: 0x0200207B RID: 8315
		public class MinorStates : GameStateMachine<RadiationMonitor, RadiationMonitor.Instance, IStateMachineTarget, object>.State
		{
			// Token: 0x040090C4 RID: 37060
			public GameStateMachine<RadiationMonitor, RadiationMonitor.Instance, IStateMachineTarget, object>.State waiting;

			// Token: 0x040090C5 RID: 37061
			public GameStateMachine<RadiationMonitor, RadiationMonitor.Instance, IStateMachineTarget, object>.State reacting;
		}

		// Token: 0x0200207C RID: 8316
		public class MajorStates : GameStateMachine<RadiationMonitor, RadiationMonitor.Instance, IStateMachineTarget, object>.State
		{
			// Token: 0x040090C6 RID: 37062
			public GameStateMachine<RadiationMonitor, RadiationMonitor.Instance, IStateMachineTarget, object>.State waiting;

			// Token: 0x040090C7 RID: 37063
			public GameStateMachine<RadiationMonitor, RadiationMonitor.Instance, IStateMachineTarget, object>.State vomiting;
		}

		// Token: 0x0200207D RID: 8317
		public class ExtremeStates : GameStateMachine<RadiationMonitor, RadiationMonitor.Instance, IStateMachineTarget, object>.State
		{
			// Token: 0x040090C8 RID: 37064
			public GameStateMachine<RadiationMonitor, RadiationMonitor.Instance, IStateMachineTarget, object>.State waiting;

			// Token: 0x040090C9 RID: 37065
			public GameStateMachine<RadiationMonitor, RadiationMonitor.Instance, IStateMachineTarget, object>.State vomiting;
		}
	}

	// Token: 0x020015D0 RID: 5584
	public new class Instance : GameStateMachine<RadiationMonitor, RadiationMonitor.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x06008562 RID: 34146 RVA: 0x002EC540 File Offset: 0x002EA740
		public Instance(IStateMachineTarget master)
			: base(master)
		{
			this.effects = base.GetComponent<Effects>();
			if (Sim.IsRadiationEnabled())
			{
				SettingLevel currentQualitySetting = CustomGameSettings.Instance.GetCurrentQualitySetting(CustomGameSettingConfigs.Radiation);
				if (currentQualitySetting != null)
				{
					string id = currentQualitySetting.id;
					if (id != null)
					{
						if (id == "Easiest")
						{
							this.difficultySettingMod = DUPLICANTSTATS.RADIATION_DIFFICULTY_MODIFIERS.EASIEST;
							return;
						}
						if (id == "Easier")
						{
							this.difficultySettingMod = DUPLICANTSTATS.RADIATION_DIFFICULTY_MODIFIERS.EASIER;
							return;
						}
						if (id == "Harder")
						{
							this.difficultySettingMod = DUPLICANTSTATS.RADIATION_DIFFICULTY_MODIFIERS.HARDER;
							return;
						}
						if (!(id == "Hardest"))
						{
							return;
						}
						this.difficultySettingMod = DUPLICANTSTATS.RADIATION_DIFFICULTY_MODIFIERS.HARDEST;
					}
				}
			}
		}

		// Token: 0x06008563 RID: 34147 RVA: 0x002EC5F6 File Offset: 0x002EA7F6
		public float SicknessSecondsRemaining()
		{
			return 600f * (Mathf.Max(0f, base.sm.radiationExposure.Get(base.smi) - 100f * this.difficultySettingMod) / 100f);
		}

		// Token: 0x06008564 RID: 34148 RVA: 0x002EC634 File Offset: 0x002EA834
		public string GetEffectStatusTooltip()
		{
			if (this.effects.HasEffect(RadiationMonitor.minorSicknessEffect))
			{
				return base.smi.master.gameObject.GetComponent<Effects>().Get(RadiationMonitor.minorSicknessEffect).statusItem.GetTooltip(this.effects.Get(RadiationMonitor.minorSicknessEffect));
			}
			if (this.effects.HasEffect(RadiationMonitor.majorSicknessEffect))
			{
				return base.smi.master.gameObject.GetComponent<Effects>().Get(RadiationMonitor.majorSicknessEffect).statusItem.GetTooltip(this.effects.Get(RadiationMonitor.majorSicknessEffect));
			}
			if (this.effects.HasEffect(RadiationMonitor.extremeSicknessEffect))
			{
				return base.smi.master.gameObject.GetComponent<Effects>().Get(RadiationMonitor.extremeSicknessEffect).statusItem.GetTooltip(this.effects.Get(RadiationMonitor.extremeSicknessEffect));
			}
			return DUPLICANTS.MODIFIERS.RADIATIONEXPOSUREDEADLY.TOOLTIP;
		}

		// Token: 0x040067F1 RID: 26609
		public Effects effects;

		// Token: 0x040067F2 RID: 26610
		public float difficultySettingMod = 1f;
	}
}
