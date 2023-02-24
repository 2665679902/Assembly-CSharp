using System;
using System.Collections.Generic;
using Klei.AI;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000609 RID: 1545
public class MegaBrainTank : StateMachineComponent<MegaBrainTank.StatesInstance>
{
	// Token: 0x0600284B RID: 10315 RVA: 0x000D5D93 File Offset: 0x000D3F93
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
	}

	// Token: 0x0600284C RID: 10316 RVA: 0x000D5D9C File Offset: 0x000D3F9C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		StoryManager.Instance.ForceCreateStory(Db.Get().Stories.MegaBrainTank, base.gameObject.GetMyWorldId());
		base.smi.StartSM();
		base.Subscribe(-1503271301, new Action<object>(this.OnBuildingSelect));
		base.GetComponent<Activatable>().SetWorkTime(5f);
	}

	// Token: 0x0600284D RID: 10317 RVA: 0x000D5E06 File Offset: 0x000D4006
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		base.Unsubscribe(-1503271301);
	}

	// Token: 0x0600284E RID: 10318 RVA: 0x000D5E1C File Offset: 0x000D401C
	private void OnBuildingSelect(object obj)
	{
		if (!(bool)obj)
		{
			return;
		}
		if (!this.introDisplayed)
		{
			this.introDisplayed = true;
			EventInfoScreen.ShowPopup(EventInfoDataHelper.GenerateStoryTraitData(CODEX.STORY_TRAITS.MEGA_BRAIN_TANK.BEGIN_POPUP.NAME, CODEX.STORY_TRAITS.MEGA_BRAIN_TANK.BEGIN_POPUP.DESCRIPTION, CODEX.STORY_TRAITS.CLOSE_BUTTON, "braintankdiscovered_kanim", EventInfoDataHelper.PopupType.BEGIN, null, null, new System.Action(this.DoInitialUnlock)));
		}
		base.smi.ShowEventCompleteUI(null);
	}

	// Token: 0x0600284F RID: 10319 RVA: 0x000D5E8A File Offset: 0x000D408A
	private void DoInitialUnlock()
	{
		Game.Instance.unlocks.Unlock("story_trait_mega_brain_tank_initial", true);
	}

	// Token: 0x040017AE RID: 6062
	[Serialize]
	private bool introDisplayed;

	// Token: 0x02001274 RID: 4724
	public class States : GameStateMachine<MegaBrainTank.States, MegaBrainTank.StatesInstance, MegaBrainTank>
	{
		// Token: 0x06007A45 RID: 31301 RVA: 0x002C6F90 File Offset: 0x002C5190
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			base.serializable = StateMachine.SerializeType.ParamsOnly;
			default_state = this.brain;
			this.brain.Initialize(this);
			this.brain.DefaultState(this.brain.inactive);
			this.brain.active.ParamTransition<bool>(this.activeParam, this.brain.inactive, new StateMachine<MegaBrainTank.States, MegaBrainTank.StatesInstance, MegaBrainTank, object>.Parameter<bool>.Callback(this.brain.IsInactive));
			this.brain.active.EventTransition(GameHashes.OperationalChanged, this.brain.inactive, new StateMachine<MegaBrainTank.States, MegaBrainTank.StatesInstance, MegaBrainTank, object>.Transition.ConditionCallback(this.brain.IsInactive));
			this.brain.inactive.ParamTransition<bool>(this.activeParam, this.brain.active.dormant, new StateMachine<MegaBrainTank.States, MegaBrainTank.StatesInstance, MegaBrainTank, object>.Parameter<bool>.Callback(this.brain.IsDormant));
			this.brain.inactive.EventTransition(GameHashes.OperationalChanged, this.brain.active.dormant, new StateMachine<MegaBrainTank.States, MegaBrainTank.StatesInstance, MegaBrainTank, object>.Transition.ConditionCallback(this.brain.IsDormant));
			this.brain.active.conscious.ParamTransition<bool>(this.dormantParam, this.brain.active.dormant, new StateMachine<MegaBrainTank.States, MegaBrainTank.StatesInstance, MegaBrainTank, object>.Parameter<bool>.Callback(this.brain.IsDormant));
			this.brain.inactive.ParamTransition<bool>(this.activeParam, this.brain.active.conscious, new StateMachine<MegaBrainTank.States, MegaBrainTank.StatesInstance, MegaBrainTank, object>.Parameter<bool>.Callback(this.brain.IsConscious));
			this.brain.inactive.EventTransition(GameHashes.OperationalChanged, this.brain.active.conscious, new StateMachine<MegaBrainTank.States, MegaBrainTank.StatesInstance, MegaBrainTank, object>.Transition.ConditionCallback(this.brain.IsConscious));
			this.brain.active.dormant.ParamTransition<bool>(this.dormantParam, this.brain.active.conscious, new StateMachine<MegaBrainTank.States, MegaBrainTank.StatesInstance, MegaBrainTank, object>.Parameter<bool>.Callback(this.brain.IsConscious));
			this.StatBonus = new Effect("MegaBrainTankBonus", DUPLICANTS.MODIFIERS.MEGABRAINTANKBONUS.NAME, DUPLICANTS.MODIFIERS.MEGABRAINTANKBONUS.TOOLTIP, 0f, true, true, false, null, -1f, 0f, null, "");
			object[,] stat_BONUSES = MegaBrainTankConfig.STAT_BONUSES;
			int length = stat_BONUSES.GetLength(0);
			for (int i = 0; i < length; i++)
			{
				string text = stat_BONUSES[i, 0] as string;
				float? num = (float?)stat_BONUSES[i, 1];
				Units? units = (Units?)stat_BONUSES[i, 2];
				this.StatBonus.Add(new AttributeModifier(text, ModifierSet.ConvertValue(num.Value, units.Value), DUPLICANTS.MODIFIERS.MEGABRAINTANKBONUS.NAME, false, false, true));
			}
		}

		// Token: 0x04005DCE RID: 24014
		public Operational.Flag activationCost = new Operational.Flag("brains restored", Operational.Flag.Type.Requirement);

		// Token: 0x04005DCF RID: 24015
		public MegaBrainTank.States.BrainState brain;

		// Token: 0x04005DD0 RID: 24016
		public StateMachine<MegaBrainTank.States, MegaBrainTank.StatesInstance, MegaBrainTank, object>.BoolParameter dormantParam;

		// Token: 0x04005DD1 RID: 24017
		public StateMachine<MegaBrainTank.States, MegaBrainTank.StatesInstance, MegaBrainTank, object>.BoolParameter activeParam;

		// Token: 0x04005DD2 RID: 24018
		public Effect StatBonus;

		// Token: 0x02001FCF RID: 8143
		public class BrainState : BrainTankState
		{
			// Token: 0x0600A0E8 RID: 41192 RVA: 0x0034222F File Offset: 0x0034042F
			public bool IsInactive(MegaBrainTank.StatesInstance smi, bool _)
			{
				return !smi.IsActive;
			}

			// Token: 0x0600A0E9 RID: 41193 RVA: 0x0034223A File Offset: 0x0034043A
			public bool IsDormant(MegaBrainTank.StatesInstance smi, bool _)
			{
				return smi.IsActive && smi.IsHungry;
			}

			// Token: 0x0600A0EA RID: 41194 RVA: 0x0034224C File Offset: 0x0034044C
			public bool IsConscious(MegaBrainTank.StatesInstance smi, bool _)
			{
				return smi.IsActive && !smi.IsHungry;
			}

			// Token: 0x0600A0EB RID: 41195 RVA: 0x00342261 File Offset: 0x00340461
			public bool IsInactive(MegaBrainTank.StatesInstance smi)
			{
				return this.IsInactive(smi, false);
			}

			// Token: 0x0600A0EC RID: 41196 RVA: 0x0034226B File Offset: 0x0034046B
			public bool IsDormant(MegaBrainTank.StatesInstance smi)
			{
				return this.IsDormant(smi, false);
			}

			// Token: 0x0600A0ED RID: 41197 RVA: 0x00342275 File Offset: 0x00340475
			public bool IsConscious(MegaBrainTank.StatesInstance smi)
			{
				return this.IsConscious(smi, false);
			}

			// Token: 0x0600A0EE RID: 41198 RVA: 0x00342280 File Offset: 0x00340480
			public override void Initialize(MegaBrainTank.States sm)
			{
				base.EventHandler(GameHashes.BuildingActivated, new GameStateMachine<MegaBrainTank.States, MegaBrainTank.StatesInstance, MegaBrainTank, object>.GameEvent.Callback(this.OnActivatableChanged));
				base.EventHandler(GameHashes.OperationalChanged, new GameStateMachine<MegaBrainTank.States, MegaBrainTank.StatesInstance, MegaBrainTank, object>.GameEvent.Callback(this.OnBuildingOperationalChanged));
				base.Update(new Action<MegaBrainTank.StatesInstance, float>(this.OnUpdate), UpdateRate.SIM_33ms, false);
				this.inactive.Initialize(sm);
				this.active.Initialize(sm);
			}

			// Token: 0x0600A0EF RID: 41199 RVA: 0x003422EB File Offset: 0x003404EB
			public override void OnUpdate(MegaBrainTank.StatesInstance smi, float dt)
			{
				smi.IncrementMeter(dt);
				if (smi.UnitsFromLastStore == 0)
				{
					return;
				}
				smi.ShelveJournals(dt);
			}

			// Token: 0x0600A0F0 RID: 41200 RVA: 0x00342305 File Offset: 0x00340505
			public override void OnAnimComplete(MegaBrainTank.StatesInstance smi, HashedString completedAnim)
			{
				if (completedAnim != MegaBrainTankConfig.KACHUNK)
				{
					return;
				}
				smi.StoreJournals();
			}

			// Token: 0x0600A0F1 RID: 41201 RVA: 0x0034231B File Offset: 0x0034051B
			private void OnBuildingOperationalChanged(MegaBrainTank.StatesInstance smi, object _)
			{
				if (!smi.IsActive)
				{
					return;
				}
				smi.Operational.SetActive(true, false);
			}

			// Token: 0x0600A0F2 RID: 41202 RVA: 0x00342334 File Offset: 0x00340534
			private void OnActivatableChanged(MegaBrainTank.StatesInstance smi, object data)
			{
				if (!(bool)data)
				{
					return;
				}
				if (!this.sm.activeParam.Get(smi))
				{
					StoryManager.Instance.BeginStoryEvent(Db.Get().Stories.MegaBrainTank);
					smi.Selectable.AddStatusItem(Db.Get().BuildingStatusItems.MegaBrainTankActivationProgress, smi);
					return;
				}
				smi.Selectable.AddStatusItem(Db.Get().BuildingStatusItems.MegaBrainTankComplete, smi);
			}

			// Token: 0x04008DC8 RID: 36296
			public MegaBrainTank.States.InactiveState inactive;

			// Token: 0x04008DC9 RID: 36297
			public MegaBrainTank.States.ActiveState active;
		}

		// Token: 0x02001FD0 RID: 8144
		public class InactiveState : BrainTankState
		{
			// Token: 0x0600A0F4 RID: 41204 RVA: 0x003423B7 File Offset: 0x003405B7
			public override void Initialize(MegaBrainTank.States sm)
			{
				base.Enter(new StateMachine<MegaBrainTank.States, MegaBrainTank.StatesInstance, MegaBrainTank, object>.State.Callback(this.OnEnter));
				base.Update(new Action<MegaBrainTank.StatesInstance, float>(this.OnUpdate), UpdateRate.SIM_33ms, false);
				base.Exit(new StateMachine<MegaBrainTank.States, MegaBrainTank.StatesInstance, MegaBrainTank, object>.State.Callback(this.OnExit));
			}

			// Token: 0x0600A0F5 RID: 41205 RVA: 0x003423F8 File Offset: 0x003405F8
			public override void OnEnter(MegaBrainTank.StatesInstance smi)
			{
				smi.SetBonusActive(false);
				smi.ElementConverter.SetAllConsumedActive(false);
				smi.Selectable.RemoveStatusItem(Db.Get().BuildingStatusItems.MegaBrainTankDreamAnalysis, false);
				smi.ElementConverter.SetConsumedElementActive(DreamJournalConfig.ID, false);
				smi.master.GetComponent<Light2D>().enabled = false;
			}

			// Token: 0x0600A0F6 RID: 41206 RVA: 0x00342456 File Offset: 0x00340656
			public override void OnExit(MegaBrainTank.StatesInstance smi)
			{
				smi.ElementConverter.SetConsumedElementActive(DreamJournalConfig.ID, true);
				RequireInputs component = smi.GetComponent<RequireInputs>();
				component.requireConduitHasMass = true;
				component.visualizeRequirements = RequireInputs.Requirements.All;
			}

			// Token: 0x0600A0F7 RID: 41207 RVA: 0x0034247D File Offset: 0x0034067D
			public override void OnUpdate(MegaBrainTank.StatesInstance smi, float dt)
			{
				smi.ActivateBrains(dt);
			}

			// Token: 0x0600A0F8 RID: 41208 RVA: 0x00342486 File Offset: 0x00340686
			public override void OnAnimComplete(MegaBrainTank.StatesInstance smi, HashedString completedAnim)
			{
				if (completedAnim != smi.CurrentActivationAnim)
				{
					return;
				}
				smi.CompleteBrainActivation();
			}
		}

		// Token: 0x02001FD1 RID: 8145
		public class ActiveState : BrainTankState
		{
			// Token: 0x0600A0FA RID: 41210 RVA: 0x003424A8 File Offset: 0x003406A8
			public override void Initialize(MegaBrainTank.States sm)
			{
				base.Enter(new StateMachine<MegaBrainTank.States, MegaBrainTank.StatesInstance, MegaBrainTank, object>.State.Callback(this.OnEnter));
				base.Exit(new StateMachine<MegaBrainTank.States, MegaBrainTank.StatesInstance, MegaBrainTank, object>.State.Callback(this.OnExit));
				this.dormant.Initialize(sm);
				this.conscious.Initialize(sm);
			}

			// Token: 0x0600A0FB RID: 41211 RVA: 0x003424F5 File Offset: 0x003406F5
			public override void OnEnter(MegaBrainTank.StatesInstance smi)
			{
				smi.master.GetComponent<Light2D>().enabled = false;
			}

			// Token: 0x0600A0FC RID: 41212 RVA: 0x00342508 File Offset: 0x00340708
			public override void OnExit(MegaBrainTank.StatesInstance smi)
			{
				smi.ElementConverter.SetConsumedElementActive(DreamJournalConfig.ID, false);
			}

			// Token: 0x04008DCA RID: 36298
			public MegaBrainTank.States.DormantState dormant;

			// Token: 0x04008DCB RID: 36299
			public MegaBrainTank.States.ConsciousState conscious;
		}

		// Token: 0x02001FD2 RID: 8146
		public class DormantState : BrainTankState
		{
			// Token: 0x0600A0FE RID: 41214 RVA: 0x00342523 File Offset: 0x00340723
			public override void Initialize(MegaBrainTank.States sm)
			{
				base.EventHandler(GameHashes.OnStorageChange, new StateMachine<MegaBrainTank.States, MegaBrainTank.StatesInstance, MegaBrainTank, object>.State.Callback(this.OnStorageChanged));
				base.Enter(new StateMachine<MegaBrainTank.States, MegaBrainTank.StatesInstance, MegaBrainTank, object>.State.Callback(this.OnEnter));
			}

			// Token: 0x0600A0FF RID: 41215 RVA: 0x00342554 File Offset: 0x00340754
			public override void OnEnter(MegaBrainTank.StatesInstance smi)
			{
				smi.CleanTank();
				bool flag = smi.ElementConverter.HasEnoughMass(GameTags.Oxygen, true);
				smi.Selectable.ToggleStatusItem(Db.Get().BuildingStatusItems.MegaBrainNotEnoughOxygen, !flag, null);
				smi.master.GetComponent<Light2D>().enabled = false;
			}

			// Token: 0x0600A100 RID: 41216 RVA: 0x003425AC File Offset: 0x003407AC
			private void OnStorageChanged(MegaBrainTank.StatesInstance smi)
			{
				float massAvailable = smi.BrainStorage.GetMassAvailable(GameTags.Oxygen);
				float massAvailable2 = smi.BrainStorage.GetMassAvailable(DreamJournalConfig.ID);
				bool flag = massAvailable >= 1f;
				smi.sm.dormantParam.Set(massAvailable2 <= 0f || !flag, smi, false);
				smi.Selectable.ToggleStatusItem(Db.Get().BuildingStatusItems.MegaBrainNotEnoughOxygen, !flag, null);
			}
		}

		// Token: 0x02001FD3 RID: 8147
		public class ConsciousState : BrainTankState
		{
			// Token: 0x0600A102 RID: 41218 RVA: 0x0034262F File Offset: 0x0034082F
			public override void Initialize(MegaBrainTank.States sm)
			{
				base.Enter(new StateMachine<MegaBrainTank.States, MegaBrainTank.StatesInstance, MegaBrainTank, object>.State.Callback(this.OnEnter));
				base.Update(new Action<MegaBrainTank.StatesInstance, float>(this.OnUpdate), UpdateRate.SIM_33ms, false);
			}

			// Token: 0x0600A103 RID: 41219 RVA: 0x0034265B File Offset: 0x0034085B
			public override void OnEnter(MegaBrainTank.StatesInstance smi)
			{
				smi.CleanTank();
				smi.Selectable.RemoveStatusItem(Db.Get().BuildingStatusItems.MegaBrainNotEnoughOxygen, false);
				smi.master.GetComponent<Light2D>().enabled = true;
			}

			// Token: 0x0600A104 RID: 41220 RVA: 0x00342690 File Offset: 0x00340890
			public override void OnUpdate(MegaBrainTank.StatesInstance smi, float dt)
			{
				smi.Digest(dt);
			}

			// Token: 0x0600A105 RID: 41221 RVA: 0x00342699 File Offset: 0x00340899
			public override void OnAnimComplete(MegaBrainTank.StatesInstance smi, HashedString completedAnim)
			{
				if (completedAnim != MegaBrainTankConfig.ACTIVATE_ALL)
				{
					return;
				}
				smi.CompleteBrainActivation();
			}
		}
	}

	// Token: 0x02001275 RID: 4725
	public class StatesInstance : GameStateMachine<MegaBrainTank.States, MegaBrainTank.StatesInstance, MegaBrainTank, object>.GameInstance
	{
		// Token: 0x17000819 RID: 2073
		// (get) Token: 0x06007A47 RID: 31303 RVA: 0x002C7259 File Offset: 0x002C5459
		public KBatchedAnimController BrainController
		{
			get
			{
				return this.controllers[0];
			}
		}

		// Token: 0x1700081A RID: 2074
		// (get) Token: 0x06007A48 RID: 31304 RVA: 0x002C7263 File Offset: 0x002C5463
		public KBatchedAnimController ShelfController
		{
			get
			{
				return this.controllers[1];
			}
		}

		// Token: 0x1700081B RID: 2075
		// (get) Token: 0x06007A49 RID: 31305 RVA: 0x002C726D File Offset: 0x002C546D
		// (set) Token: 0x06007A4A RID: 31306 RVA: 0x002C7275 File Offset: 0x002C5475
		public Storage BrainStorage { get; private set; }

		// Token: 0x1700081C RID: 2076
		// (get) Token: 0x06007A4B RID: 31307 RVA: 0x002C727E File Offset: 0x002C547E
		// (set) Token: 0x06007A4C RID: 31308 RVA: 0x002C7286 File Offset: 0x002C5486
		public KSelectable Selectable { get; private set; }

		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x06007A4D RID: 31309 RVA: 0x002C728F File Offset: 0x002C548F
		// (set) Token: 0x06007A4E RID: 31310 RVA: 0x002C7297 File Offset: 0x002C5497
		public Operational Operational { get; private set; }

		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x06007A4F RID: 31311 RVA: 0x002C72A0 File Offset: 0x002C54A0
		// (set) Token: 0x06007A50 RID: 31312 RVA: 0x002C72A8 File Offset: 0x002C54A8
		public ElementConverter ElementConverter { get; private set; }

		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x06007A51 RID: 31313 RVA: 0x002C72B1 File Offset: 0x002C54B1
		// (set) Token: 0x06007A52 RID: 31314 RVA: 0x002C72B9 File Offset: 0x002C54B9
		public ManualDeliveryKG JournalDelivery { get; private set; }

		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x06007A53 RID: 31315 RVA: 0x002C72C2 File Offset: 0x002C54C2
		// (set) Token: 0x06007A54 RID: 31316 RVA: 0x002C72CA File Offset: 0x002C54CA
		public LoopingSounds BrainSounds { get; private set; }

		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x06007A55 RID: 31317 RVA: 0x002C72D3 File Offset: 0x002C54D3
		public bool IsActive
		{
			get
			{
				return this.Operational.IsOperational && base.sm.activeParam.Get(this);
			}
		}

		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x06007A56 RID: 31318 RVA: 0x002C72F5 File Offset: 0x002C54F5
		public bool IsHungry
		{
			get
			{
				return !this.ElementConverter.HasEnoughMassToStartConverting(true);
			}
		}

		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x06007A57 RID: 31319 RVA: 0x002C7306 File Offset: 0x002C5506
		public int TimeTilDigested
		{
			get
			{
				return (int)this.timeTilDigested;
			}
		}

		// Token: 0x17000824 RID: 2084
		// (get) Token: 0x06007A58 RID: 31320 RVA: 0x002C730F File Offset: 0x002C550F
		public int ActivationProgress
		{
			get
			{
				return (int)(25f * this.meterFill);
			}
		}

		// Token: 0x17000825 RID: 2085
		// (get) Token: 0x06007A59 RID: 31321 RVA: 0x002C731E File Offset: 0x002C551E
		public HashedString CurrentActivationAnim
		{
			get
			{
				return MegaBrainTankConfig.ACTIVATION_ANIMS[(int)(this.nextActiveBrain - 1)];
			}
		}

		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x06007A5A RID: 31322 RVA: 0x002C7334 File Offset: 0x002C5534
		private HashedString currentActivationLoop
		{
			get
			{
				int num = (int)(this.nextActiveBrain - 1 + 5);
				return MegaBrainTankConfig.ACTIVATION_ANIMS[num];
			}
		}

		// Token: 0x06007A5B RID: 31323 RVA: 0x002C7358 File Offset: 0x002C5558
		public StatesInstance(MegaBrainTank master)
			: base(master)
		{
			this.BrainSounds = base.GetComponent<LoopingSounds>();
			this.BrainStorage = base.GetComponent<Storage>();
			this.ElementConverter = base.GetComponent<ElementConverter>();
			this.JournalDelivery = base.GetComponent<ManualDeliveryKG>();
			this.Operational = base.GetComponent<Operational>();
			this.Selectable = base.GetComponent<KSelectable>();
			this.notifier = base.GetComponent<Notifier>();
			this.controllers = base.gameObject.GetComponentsInChildren<KBatchedAnimController>();
			this.meter = new MeterController(this.BrainController, "meter_oxygen_target", "meter", Meter.Offset.Infront, Grid.SceneLayer.NoLayer, MegaBrainTankConfig.METER_SYMBOLS);
			this.fxLink = new KAnimLink(this.BrainController, this.ShelfController);
		}

		// Token: 0x06007A5C RID: 31324 RVA: 0x002C7420 File Offset: 0x002C5620
		public override void StartSM()
		{
			this.InitializeEffectsList();
			base.StartSM();
			this.BrainController.onAnimComplete += this.OnAnimComplete;
			this.ShelfController.onAnimComplete += this.OnAnimComplete;
			Storage brainStorage = this.BrainStorage;
			brainStorage.OnWorkableEventCB = (Action<Workable, Workable.WorkableEvent>)Delegate.Combine(brainStorage.OnWorkableEventCB, new Action<Workable, Workable.WorkableEvent>(this.OnJournalDeliveryStateChanged));
			this.brainHum = GlobalAssets.GetSound("MegaBrainTank_brain_wave_LP", false);
			StoryManager.Instance.DiscoverStoryEvent(Db.Get().Stories.MegaBrainTank);
			bool flag = base.sm.activeParam.Get(this);
			this.Operational.SetFlag(base.sm.activationCost, flag);
			float unitsAvailable = this.BrainStorage.GetUnitsAvailable(DreamJournalConfig.ID);
			if (!flag)
			{
				this.meterFill = (this.targetProgress = unitsAvailable / 25f);
				this.meter.SetPositionPercent(this.meterFill);
				short num = (short)(5f * this.meterFill);
				if (num > 0)
				{
					this.nextActiveBrain = num;
					this.BrainSounds.StartSound(this.brainHum);
					this.BrainSounds.SetParameter(this.brainHum, "BrainTankProgress", (float)num);
					this.CompleteBrainActivation();
				}
				return;
			}
			this.timeTilDigested = unitsAvailable * 60f;
			this.meterFill = this.timeTilDigested - this.timeTilDigested % 0.04f;
			this.meterFill /= 1500f;
			this.meter.SetPositionPercent(this.meterFill);
			StoryManager.Instance.BeginStoryEvent(Db.Get().Stories.MegaBrainTank);
			this.nextActiveBrain = 5;
			this.CompleteBrainActivation();
		}

		// Token: 0x06007A5D RID: 31325 RVA: 0x002C75DC File Offset: 0x002C57DC
		public override void StopSM(string reason)
		{
			this.BrainController.onAnimComplete -= this.OnAnimComplete;
			this.ShelfController.onAnimComplete -= this.OnAnimComplete;
			Storage brainStorage = this.BrainStorage;
			brainStorage.OnWorkableEventCB = (Action<Workable, Workable.WorkableEvent>)Delegate.Remove(brainStorage.OnWorkableEventCB, new Action<Workable, Workable.WorkableEvent>(this.OnJournalDeliveryStateChanged));
			base.StopSM(reason);
		}

		// Token: 0x06007A5E RID: 31326 RVA: 0x002C7648 File Offset: 0x002C5848
		private void InitializeEffectsList()
		{
			Components.Cmps<MinionIdentity> liveMinionIdentities = Components.LiveMinionIdentities;
			liveMinionIdentities.OnAdd += this.OnLiveMinionIdAdded;
			liveMinionIdentities.OnRemove += this.OnLiveMinionIdRemoved;
			MegaBrainTank.StatesInstance.minionEffects = new List<Effects>((liveMinionIdentities.Count > 32) ? liveMinionIdentities.Count : 32);
			for (int i = 0; i < liveMinionIdentities.Count; i++)
			{
				this.OnLiveMinionIdAdded(liveMinionIdentities[i]);
			}
		}

		// Token: 0x06007A5F RID: 31327 RVA: 0x002C76BC File Offset: 0x002C58BC
		private void OnLiveMinionIdAdded(MinionIdentity id)
		{
			Effects component = id.GetComponent<Effects>();
			MegaBrainTank.StatesInstance.minionEffects.Add(component);
			if (!base.sm.activeParam.Get(this) || !base.sm.dormantParam.Get(this) || !this.IsActive)
			{
				return;
			}
			component.Add(base.sm.StatBonus, false);
		}

		// Token: 0x06007A60 RID: 31328 RVA: 0x002C7720 File Offset: 0x002C5920
		private void OnLiveMinionIdRemoved(MinionIdentity id)
		{
			Effects component = id.GetComponent<Effects>();
			MegaBrainTank.StatesInstance.minionEffects.Remove(component);
		}

		// Token: 0x06007A61 RID: 31329 RVA: 0x002C7740 File Offset: 0x002C5940
		public void SetBonusActive(bool active)
		{
			for (int i = 0; i < MegaBrainTank.StatesInstance.minionEffects.Count; i++)
			{
				if (active)
				{
					MegaBrainTank.StatesInstance.minionEffects[i].Add(base.sm.StatBonus, false);
				}
				else
				{
					MegaBrainTank.StatesInstance.minionEffects[i].Remove(base.sm.StatBonus);
				}
			}
		}

		// Token: 0x06007A62 RID: 31330 RVA: 0x002C77A0 File Offset: 0x002C59A0
		private void OnAnimComplete(HashedString anim)
		{
			BrainTankState brainTankState = this.GetCurrentState() as BrainTankState;
			if (brainTankState == null)
			{
				return;
			}
			while (brainTankState != null)
			{
				brainTankState.OnAnimComplete(this, anim);
				brainTankState = brainTankState.parent as BrainTankState;
			}
		}

		// Token: 0x06007A63 RID: 31331 RVA: 0x002C77D4 File Offset: 0x002C59D4
		private void OnJournalDeliveryStateChanged(Workable w, Workable.WorkableEvent state)
		{
			if (state == Workable.WorkableEvent.WorkStopped)
			{
				return;
			}
			if (state != Workable.WorkableEvent.WorkStarted)
			{
				this.ShelfController.Play(MegaBrainTankConfig.KACHUNK, KAnim.PlayMode.Once, 1f, 0f);
				return;
			}
			FetchAreaChore.StatesInstance smi = w.worker.GetSMI<FetchAreaChore.StatesInstance>();
			if (smi.IsNullOrStopped())
			{
				return;
			}
			Pickupable component = smi.sm.deliveryObject.Get(smi).GetComponent<Pickupable>();
			this.UnitsFromLastStore = (short)component.PrimaryElement.Units;
			float num = Mathf.Clamp01(component.PrimaryElement.Units / 5f);
			this.BrainStorage.SetWorkTime(num * this.BrainStorage.storageWorkTime);
		}

		// Token: 0x06007A64 RID: 31332 RVA: 0x002C7874 File Offset: 0x002C5A74
		public void ShelveJournals(float dt)
		{
			float num = this.lastRemainingTime - this.BrainStorage.WorkTimeRemaining;
			if (num <= 0f)
			{
				num = this.BrainStorage.storageWorkTime - this.BrainStorage.WorkTimeRemaining;
			}
			this.lastRemainingTime = this.BrainStorage.WorkTimeRemaining;
			if (this.BrainStorage.storageWorkTime / 5f - this.journalActivationTimer > 0.001f)
			{
				this.journalActivationTimer += num;
				return;
			}
			int num2 = -1;
			this.journalActivationTimer = 0f;
			for (int i = 0; i < MegaBrainTankConfig.JOURNAL_SYMBOLS.Length; i++)
			{
				byte b = (byte)(1 << i);
				bool flag = (this.activatedJournals & b) == 0;
				if (flag && num2 == -1)
				{
					num2 = i;
				}
				if (flag & (UnityEngine.Random.Range(0f, 1f) >= 0.5f))
				{
					num2 = -1;
					this.activatedJournals |= b;
					this.ShelfController.SetSymbolVisiblity(MegaBrainTankConfig.JOURNAL_SYMBOLS[i], true);
					break;
				}
			}
			if (num2 != -1)
			{
				this.ShelfController.SetSymbolVisiblity(MegaBrainTankConfig.JOURNAL_SYMBOLS[num2], true);
			}
			this.UnitsFromLastStore -= 1;
		}

		// Token: 0x06007A65 RID: 31333 RVA: 0x002C79A8 File Offset: 0x002C5BA8
		public void StoreJournals()
		{
			this.lastRemainingTime = 0f;
			this.activatedJournals = 0;
			for (int i = 0; i < MegaBrainTankConfig.JOURNAL_SYMBOLS.Length; i++)
			{
				this.ShelfController.SetSymbolVisiblity(MegaBrainTankConfig.JOURNAL_SYMBOLS[i], false);
			}
			this.ShelfController.PlayMode = KAnim.PlayMode.Paused;
			this.ShelfController.SetPositionPercent(0f);
			float unitsAvailable = this.BrainStorage.GetUnitsAvailable(DreamJournalConfig.ID);
			this.targetProgress = Mathf.Clamp01(unitsAvailable / 25f);
		}

		// Token: 0x06007A66 RID: 31334 RVA: 0x002C7A34 File Offset: 0x002C5C34
		public void ActivateBrains(float dt)
		{
			if (this.currentlyActivating)
			{
				return;
			}
			this.currentlyActivating = (float)this.nextActiveBrain / 5f - this.meterFill <= 0.001f;
			if (!this.currentlyActivating)
			{
				return;
			}
			this.BrainController.QueueAndSyncTransition(this.CurrentActivationAnim, KAnim.PlayMode.Once, 1f, 0f);
			if (this.nextActiveBrain > 0)
			{
				this.BrainSounds.StartSound(this.brainHum);
				this.BrainSounds.SetParameter(this.brainHum, "BrainTankProgress", (float)this.nextActiveBrain);
			}
		}

		// Token: 0x06007A67 RID: 31335 RVA: 0x002C7AD0 File Offset: 0x002C5CD0
		public void CompleteBrainActivation()
		{
			this.BrainController.Play(this.currentActivationLoop, KAnim.PlayMode.Loop, 1f, 0f);
			this.nextActiveBrain += 1;
			this.currentlyActivating = false;
			if (this.nextActiveBrain > 5)
			{
				float unitsAvailable = this.BrainStorage.GetUnitsAvailable(DreamJournalConfig.ID);
				this.timeTilDigested = unitsAvailable * 60f;
				this.Operational.SetFlag(base.sm.activationCost, true);
				this.CompleteEvent();
			}
		}

		// Token: 0x06007A68 RID: 31336 RVA: 0x002C7B54 File Offset: 0x002C5D54
		public void Digest(float dt)
		{
			float unitsAvailable = this.BrainStorage.GetUnitsAvailable(DreamJournalConfig.ID);
			this.timeTilDigested = unitsAvailable * 60f;
			base.sm.dormantParam.Set(this.IsHungry, this, false);
			if (this.targetProgress - this.meterFill > Mathf.Epsilon)
			{
				return;
			}
			this.targetProgress = 0f;
			float num = this.meterFill - this.timeTilDigested / 1500f;
			if (num >= 0.04f)
			{
				this.meterFill -= num - num % 0.04f;
				this.meter.SetPositionPercent(this.meterFill);
			}
		}

		// Token: 0x06007A69 RID: 31337 RVA: 0x002C7BFC File Offset: 0x002C5DFC
		public void CleanTank()
		{
			bool flag = !base.sm.dormantParam.Get(this);
			this.SetBonusActive(flag);
			this.Selectable.ToggleStatusItem(Db.Get().BuildingStatusItems.MegaBrainTankDreamAnalysis, flag, this);
			this.ElementConverter.SetAllConsumedActive(flag);
			if (flag)
			{
				this.nextActiveBrain = 5;
				this.BrainController.QueueAndSyncTransition(MegaBrainTankConfig.ACTIVATE_ALL, KAnim.PlayMode.Once, 1f, 0f);
				this.BrainSounds.StartSound(this.brainHum);
				this.BrainSounds.SetParameter(this.brainHum, "BrainTankProgress", (float)this.nextActiveBrain);
				return;
			}
			if (this.timeTilDigested < 0.016666668f)
			{
				this.BrainStorage.ConsumeAllIgnoringDisease(DreamJournalConfig.ID);
				this.timeTilDigested = 0f;
				this.meterFill = 0f;
				this.meter.SetPositionPercent(this.meterFill);
			}
			this.BrainController.QueueAndSyncTransition(MegaBrainTankConfig.DEACTIVATE_ALL, KAnim.PlayMode.Once, 1f, 0f);
			this.BrainSounds.StopSound(this.brainHum);
		}

		// Token: 0x06007A6A RID: 31338 RVA: 0x002C7D18 File Offset: 0x002C5F18
		public bool IncrementMeter(float dt)
		{
			if (this.targetProgress - this.meterFill <= Mathf.Epsilon)
			{
				return false;
			}
			this.meterFill += Mathf.Lerp(0f, 1f, 0.04f * dt);
			if (1f - this.meterFill <= 0.001f)
			{
				this.meterFill = 1f;
			}
			this.meter.SetPositionPercent(this.meterFill);
			return this.targetProgress - this.meterFill > 0.001f;
		}

		// Token: 0x06007A6B RID: 31339 RVA: 0x002C7DA4 File Offset: 0x002C5FA4
		public void CompleteEvent()
		{
			this.Selectable.RemoveStatusItem(Db.Get().BuildingStatusItems.MegaBrainTankActivationProgress, false);
			this.Selectable.AddStatusItem(Db.Get().BuildingStatusItems.MegaBrainTankComplete, base.smi);
			StoryInstance storyInstance = StoryManager.Instance.GetStoryInstance(Db.Get().Stories.MegaBrainTank.HashId);
			if (storyInstance == null || (base.sm.activeParam.Get(this) && storyInstance.CurrentState == StoryInstance.State.COMPLETE))
			{
				return;
			}
			this.eventInfo = EventInfoDataHelper.GenerateStoryTraitData(CODEX.STORY_TRAITS.MEGA_BRAIN_TANK.END_POPUP.NAME, CODEX.STORY_TRAITS.MEGA_BRAIN_TANK.END_POPUP.DESCRIPTION, CODEX.STORY_TRAITS.MEGA_BRAIN_TANK.END_POPUP.BUTTON, "braintankcomplete_kanim", EventInfoDataHelper.PopupType.COMPLETE, null, null, null);
			base.smi.Selectable.AddStatusItem(Db.Get().MiscStatusItems.AttentionRequired, base.smi);
			this.eventComplete = EventInfoScreen.CreateNotification(this.eventInfo, new Notification.ClickCallback(this.ShowEventCompleteUI));
			this.notifier.Add(this.eventComplete, "");
		}

		// Token: 0x06007A6C RID: 31340 RVA: 0x002C7EB8 File Offset: 0x002C60B8
		public void ShowEventCompleteUI(object _ = null)
		{
			if (this.eventComplete == null)
			{
				return;
			}
			base.smi.Selectable.RemoveStatusItem(Db.Get().MiscStatusItems.AttentionRequired, false);
			this.notifier.Remove(this.eventComplete);
			this.eventComplete = null;
			Game.Instance.unlocks.Unlock("story_trait_mega_brain_tank_competed", true);
			Vector3 vector = Grid.CellToPosCCC(Grid.OffsetCell(Grid.PosToCell(base.master), new CellOffset(0, 3)), Grid.SceneLayer.Ore);
			StoryManager.Instance.CompleteStoryEvent(Db.Get().Stories.MegaBrainTank, base.master, new FocusTargetSequence.Data
			{
				WorldId = base.master.GetMyWorldId(),
				OrthographicSize = 6f,
				TargetSize = 6f,
				Target = vector,
				PopupData = this.eventInfo,
				CompleteCB = new System.Action(this.OnCompleteStorySequence),
				CanCompleteCB = null
			});
		}

		// Token: 0x06007A6D RID: 31341 RVA: 0x002C7FC0 File Offset: 0x002C61C0
		private void OnCompleteStorySequence()
		{
			Vector3 vector = Grid.CellToPosCCC(Grid.OffsetCell(Grid.PosToCell(base.master), new CellOffset(0, 2)), Grid.SceneLayer.Ore);
			StoryManager.Instance.CompleteStoryEvent(Db.Get().Stories.MegaBrainTank, vector);
			this.eventInfo = null;
			base.sm.dormantParam.Set(base.smi.IsHungry, base.smi, false);
			base.sm.activeParam.Set(true, this, false);
		}

		// Token: 0x04005DD3 RID: 24019
		private static List<Effects> minionEffects;

		// Token: 0x04005DDA RID: 24026
		public short UnitsFromLastStore;

		// Token: 0x04005DDB RID: 24027
		private float meterFill = 0.04f;

		// Token: 0x04005DDC RID: 24028
		private float targetProgress;

		// Token: 0x04005DDD RID: 24029
		private float timeTilDigested;

		// Token: 0x04005DDE RID: 24030
		private float journalActivationTimer;

		// Token: 0x04005DDF RID: 24031
		private float lastRemainingTime;

		// Token: 0x04005DE0 RID: 24032
		private byte activatedJournals;

		// Token: 0x04005DE1 RID: 24033
		private bool currentlyActivating;

		// Token: 0x04005DE2 RID: 24034
		private short nextActiveBrain = 1;

		// Token: 0x04005DE3 RID: 24035
		private string brainHum;

		// Token: 0x04005DE4 RID: 24036
		private KBatchedAnimController[] controllers;

		// Token: 0x04005DE5 RID: 24037
		private KAnimLink fxLink;

		// Token: 0x04005DE6 RID: 24038
		private MeterController meter;

		// Token: 0x04005DE7 RID: 24039
		private EventInfoData eventInfo;

		// Token: 0x04005DE8 RID: 24040
		private Notification eventComplete;

		// Token: 0x04005DE9 RID: 24041
		private Notifier notifier;
	}
}
