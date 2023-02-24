using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x020005D0 RID: 1488
[SerializationConfig(MemberSerialization.OptIn)]
public class HighEnergyParticleSpawner : StateMachineComponent<HighEnergyParticleSpawner.StatesInstance>, IHighEnergyParticleDirection, IProgressBarSideScreen, ISingleSliderControl, ISliderControl
{
	// Token: 0x17000201 RID: 513
	// (get) Token: 0x06002513 RID: 9491 RVA: 0x000C885C File Offset: 0x000C6A5C
	public float PredictedPerCycleConsumptionRate
	{
		get
		{
			return (float)Mathf.FloorToInt(this.recentPerSecondConsumptionRate * 0.1f * 600f);
		}
	}

	// Token: 0x17000202 RID: 514
	// (get) Token: 0x06002514 RID: 9492 RVA: 0x000C8876 File Offset: 0x000C6A76
	// (set) Token: 0x06002515 RID: 9493 RVA: 0x000C8880 File Offset: 0x000C6A80
	public EightDirection Direction
	{
		get
		{
			return this._direction;
		}
		set
		{
			this._direction = value;
			if (this.directionController != null)
			{
				this.directionController.SetRotation((float)(45 * EightDirectionUtil.GetDirectionIndex(this._direction)));
				this.directionController.controller.enabled = false;
				this.directionController.controller.enabled = true;
			}
		}
	}

	// Token: 0x06002516 RID: 9494 RVA: 0x000C88D8 File Offset: 0x000C6AD8
	private void OnCopySettings(object data)
	{
		HighEnergyParticleSpawner component = ((GameObject)data).GetComponent<HighEnergyParticleSpawner>();
		if (component != null)
		{
			this.Direction = component.Direction;
			this.particleThreshold = component.particleThreshold;
		}
	}

	// Token: 0x06002517 RID: 9495 RVA: 0x000C8912 File Offset: 0x000C6B12
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<HighEnergyParticleSpawner>(-905833192, HighEnergyParticleSpawner.OnCopySettingsDelegate);
	}

	// Token: 0x06002518 RID: 9496 RVA: 0x000C892C File Offset: 0x000C6B2C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
		this.directionController = new EightDirectionController(base.GetComponent<KBatchedAnimController>(), "redirector_target", "redirect", EightDirectionController.Offset.Infront);
		this.Direction = this.Direction;
		this.particleController = new MeterController(base.GetComponent<KBatchedAnimController>(), "orb_target", "orb_off", Meter.Offset.NoChange, Grid.SceneLayer.NoLayer, Array.Empty<string>());
		this.particleController.gameObject.AddOrGet<LoopingSounds>();
		this.progressMeterController = new MeterController(base.GetComponent<KBatchedAnimController>(), "meter_target", "meter", Meter.Offset.Infront, Grid.SceneLayer.NoLayer, Array.Empty<string>());
		Tutorial.Instance.TutorialMessage(Tutorial.TutorialMessages.TM_Radiation, true);
	}

	// Token: 0x06002519 RID: 9497 RVA: 0x000C89D7 File Offset: 0x000C6BD7
	public float GetProgressBarMaxValue()
	{
		return this.particleThreshold;
	}

	// Token: 0x0600251A RID: 9498 RVA: 0x000C89DF File Offset: 0x000C6BDF
	public float GetProgressBarFillPercentage()
	{
		return this.particleStorage.Particles / this.particleThreshold;
	}

	// Token: 0x0600251B RID: 9499 RVA: 0x000C89F3 File Offset: 0x000C6BF3
	public string GetProgressBarTitleLabel()
	{
		return UI.UISIDESCREENS.RADBOLTTHRESHOLDSIDESCREEN.PROGRESS_BAR_LABEL;
	}

	// Token: 0x0600251C RID: 9500 RVA: 0x000C8A00 File Offset: 0x000C6C00
	public string GetProgressBarLabel()
	{
		return Mathf.FloorToInt(this.particleStorage.Particles).ToString() + "/" + Mathf.FloorToInt(this.particleThreshold).ToString();
	}

	// Token: 0x0600251D RID: 9501 RVA: 0x000C8A42 File Offset: 0x000C6C42
	public string GetProgressBarTooltip()
	{
		return UI.UISIDESCREENS.RADBOLTTHRESHOLDSIDESCREEN.PROGRESS_BAR_TOOLTIP;
	}

	// Token: 0x0600251E RID: 9502 RVA: 0x000C8A4E File Offset: 0x000C6C4E
	public void DoConsumeParticlesWhileDisabled(float dt)
	{
		this.particleStorage.ConsumeAndGet(dt * 1f);
		this.progressMeterController.SetPositionPercent(this.GetProgressBarFillPercentage());
	}

	// Token: 0x0600251F RID: 9503 RVA: 0x000C8A74 File Offset: 0x000C6C74
	public void LauncherUpdate(float dt)
	{
		this.radiationSampleTimer += dt;
		if (this.radiationSampleTimer >= this.radiationSampleRate)
		{
			this.radiationSampleTimer -= this.radiationSampleRate;
			int num = Grid.PosToCell(this);
			float num2 = Grid.Radiation[num];
			if (num2 != 0f && this.particleStorage.RemainingCapacity() > 0f)
			{
				base.smi.sm.isAbsorbingRadiation.Set(true, base.smi, false);
				this.recentPerSecondConsumptionRate = num2 / 600f;
				this.particleStorage.Store(this.recentPerSecondConsumptionRate * this.radiationSampleRate * 0.1f);
			}
			else
			{
				this.recentPerSecondConsumptionRate = 0f;
				base.smi.sm.isAbsorbingRadiation.Set(false, base.smi, false);
			}
		}
		this.progressMeterController.SetPositionPercent(this.GetProgressBarFillPercentage());
		if (!this.particleVisualPlaying && this.particleStorage.Particles > this.particleThreshold / 2f)
		{
			this.particleController.meterController.Play("orb_pre", KAnim.PlayMode.Once, 1f, 0f);
			this.particleController.meterController.Queue("orb_idle", KAnim.PlayMode.Loop, 1f, 0f);
			this.particleVisualPlaying = true;
		}
		this.launcherTimer += dt;
		if (this.launcherTimer < this.minLaunchInterval)
		{
			return;
		}
		if (this.particleStorage.Particles >= this.particleThreshold)
		{
			this.launcherTimer = 0f;
			int highEnergyParticleOutputCell = base.GetComponent<Building>().GetHighEnergyParticleOutputCell();
			GameObject gameObject = GameUtil.KInstantiate(Assets.GetPrefab("HighEnergyParticle"), Grid.CellToPosCCC(highEnergyParticleOutputCell, Grid.SceneLayer.FXFront2), Grid.SceneLayer.FXFront2, null, 0);
			gameObject.SetActive(true);
			if (gameObject != null)
			{
				HighEnergyParticle component = gameObject.GetComponent<HighEnergyParticle>();
				component.payload = this.particleStorage.ConsumeAndGet(this.particleThreshold);
				component.SetDirection(this.Direction);
				this.directionController.PlayAnim("redirect_send", KAnim.PlayMode.Once);
				this.directionController.controller.Queue("redirect", KAnim.PlayMode.Once, 1f, 0f);
				this.particleController.meterController.Play("orb_send", KAnim.PlayMode.Once, 1f, 0f);
				this.particleController.meterController.Queue("orb_off", KAnim.PlayMode.Once, 1f, 0f);
				this.particleVisualPlaying = false;
			}
		}
	}

	// Token: 0x17000203 RID: 515
	// (get) Token: 0x06002520 RID: 9504 RVA: 0x000C8D05 File Offset: 0x000C6F05
	public string SliderTitleKey
	{
		get
		{
			return "STRINGS.UI.UISIDESCREENS.RADBOLTTHRESHOLDSIDESCREEN.TITLE";
		}
	}

	// Token: 0x17000204 RID: 516
	// (get) Token: 0x06002521 RID: 9505 RVA: 0x000C8D0C File Offset: 0x000C6F0C
	public string SliderUnits
	{
		get
		{
			return UI.UNITSUFFIXES.HIGHENERGYPARTICLES.PARTRICLES;
		}
	}

	// Token: 0x06002522 RID: 9506 RVA: 0x000C8D18 File Offset: 0x000C6F18
	public int SliderDecimalPlaces(int index)
	{
		return 0;
	}

	// Token: 0x06002523 RID: 9507 RVA: 0x000C8D1B File Offset: 0x000C6F1B
	public float GetSliderMin(int index)
	{
		return (float)this.minSlider;
	}

	// Token: 0x06002524 RID: 9508 RVA: 0x000C8D24 File Offset: 0x000C6F24
	public float GetSliderMax(int index)
	{
		return (float)this.maxSlider;
	}

	// Token: 0x06002525 RID: 9509 RVA: 0x000C8D2D File Offset: 0x000C6F2D
	public float GetSliderValue(int index)
	{
		return this.particleThreshold;
	}

	// Token: 0x06002526 RID: 9510 RVA: 0x000C8D35 File Offset: 0x000C6F35
	public void SetSliderValue(float value, int index)
	{
		this.particleThreshold = value;
	}

	// Token: 0x06002527 RID: 9511 RVA: 0x000C8D3E File Offset: 0x000C6F3E
	public string GetSliderTooltipKey(int index)
	{
		return "STRINGS.UI.UISIDESCREENS.RADBOLTTHRESHOLDSIDESCREEN.TOOLTIP";
	}

	// Token: 0x06002528 RID: 9512 RVA: 0x000C8D45 File Offset: 0x000C6F45
	string ISliderControl.GetSliderTooltip()
	{
		return string.Format(Strings.Get("STRINGS.UI.UISIDESCREENS.RADBOLTTHRESHOLDSIDESCREEN.TOOLTIP"), this.particleThreshold);
	}

	// Token: 0x04001575 RID: 5493
	[MyCmpReq]
	private HighEnergyParticleStorage particleStorage;

	// Token: 0x04001576 RID: 5494
	[MyCmpGet]
	private Operational operational;

	// Token: 0x04001577 RID: 5495
	private float recentPerSecondConsumptionRate;

	// Token: 0x04001578 RID: 5496
	public int minSlider;

	// Token: 0x04001579 RID: 5497
	public int maxSlider;

	// Token: 0x0400157A RID: 5498
	[Serialize]
	private EightDirection _direction;

	// Token: 0x0400157B RID: 5499
	public float minLaunchInterval;

	// Token: 0x0400157C RID: 5500
	public float radiationSampleRate;

	// Token: 0x0400157D RID: 5501
	[Serialize]
	public float particleThreshold = 50f;

	// Token: 0x0400157E RID: 5502
	private EightDirectionController directionController;

	// Token: 0x0400157F RID: 5503
	private float launcherTimer;

	// Token: 0x04001580 RID: 5504
	private float radiationSampleTimer;

	// Token: 0x04001581 RID: 5505
	private MeterController particleController;

	// Token: 0x04001582 RID: 5506
	private bool particleVisualPlaying;

	// Token: 0x04001583 RID: 5507
	private MeterController progressMeterController;

	// Token: 0x04001584 RID: 5508
	[Serialize]
	public Ref<HighEnergyParticlePort> capturedByRef = new Ref<HighEnergyParticlePort>();

	// Token: 0x04001585 RID: 5509
	[MyCmpAdd]
	private CopyBuildingSettings copyBuildingSettings;

	// Token: 0x04001586 RID: 5510
	private static readonly EventSystem.IntraObjectHandler<HighEnergyParticleSpawner> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<HighEnergyParticleSpawner>(delegate(HighEnergyParticleSpawner component, object data)
	{
		component.OnCopySettings(data);
	});

	// Token: 0x02001226 RID: 4646
	public class StatesInstance : GameStateMachine<HighEnergyParticleSpawner.States, HighEnergyParticleSpawner.StatesInstance, HighEnergyParticleSpawner, object>.GameInstance
	{
		// Token: 0x0600793C RID: 31036 RVA: 0x002C2853 File Offset: 0x002C0A53
		public StatesInstance(HighEnergyParticleSpawner smi)
			: base(smi)
		{
		}
	}

	// Token: 0x02001227 RID: 4647
	public class States : GameStateMachine<HighEnergyParticleSpawner.States, HighEnergyParticleSpawner.StatesInstance, HighEnergyParticleSpawner>
	{
		// Token: 0x0600793D RID: 31037 RVA: 0x002C285C File Offset: 0x002C0A5C
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.inoperational;
			this.inoperational.PlayAnim("off").TagTransition(GameTags.Operational, this.ready, false).DefaultState(this.inoperational.empty);
			this.inoperational.empty.EventTransition(GameHashes.OnParticleStorageChanged, this.inoperational.losing, (HighEnergyParticleSpawner.StatesInstance smi) => !smi.GetComponent<HighEnergyParticleStorage>().IsEmpty());
			this.inoperational.losing.ToggleStatusItem(Db.Get().BuildingStatusItems.LosingRadbolts, null).Update(delegate(HighEnergyParticleSpawner.StatesInstance smi, float dt)
			{
				smi.master.DoConsumeParticlesWhileDisabled(dt);
			}, UpdateRate.SIM_1000ms, false).EventTransition(GameHashes.OnParticleStorageChanged, this.inoperational.empty, (HighEnergyParticleSpawner.StatesInstance smi) => smi.GetComponent<HighEnergyParticleStorage>().IsEmpty());
			this.ready.TagTransition(GameTags.Operational, this.inoperational, true).DefaultState(this.ready.idle).Update(delegate(HighEnergyParticleSpawner.StatesInstance smi, float dt)
			{
				smi.master.LauncherUpdate(dt);
			}, UpdateRate.SIM_EVERY_TICK, false);
			this.ready.idle.ParamTransition<bool>(this.isAbsorbingRadiation, this.ready.absorbing, GameStateMachine<HighEnergyParticleSpawner.States, HighEnergyParticleSpawner.StatesInstance, HighEnergyParticleSpawner, object>.IsTrue).PlayAnim("on");
			this.ready.absorbing.Enter("SetActive(true)", delegate(HighEnergyParticleSpawner.StatesInstance smi)
			{
				smi.master.operational.SetActive(true, false);
			}).Exit("SetActive(false)", delegate(HighEnergyParticleSpawner.StatesInstance smi)
			{
				smi.master.operational.SetActive(false, false);
			}).ParamTransition<bool>(this.isAbsorbingRadiation, this.ready.idle, GameStateMachine<HighEnergyParticleSpawner.States, HighEnergyParticleSpawner.StatesInstance, HighEnergyParticleSpawner, object>.IsFalse)
				.ToggleStatusItem(Db.Get().BuildingStatusItems.CollectingHEP, (HighEnergyParticleSpawner.StatesInstance smi) => smi.master)
				.PlayAnim("working_loop", KAnim.PlayMode.Loop);
		}

		// Token: 0x04005D2C RID: 23852
		public StateMachine<HighEnergyParticleSpawner.States, HighEnergyParticleSpawner.StatesInstance, HighEnergyParticleSpawner, object>.BoolParameter isAbsorbingRadiation;

		// Token: 0x04005D2D RID: 23853
		public HighEnergyParticleSpawner.States.ReadyStates ready;

		// Token: 0x04005D2E RID: 23854
		public HighEnergyParticleSpawner.States.InoperationalStates inoperational;

		// Token: 0x02001FB7 RID: 8119
		public class InoperationalStates : GameStateMachine<HighEnergyParticleSpawner.States, HighEnergyParticleSpawner.StatesInstance, HighEnergyParticleSpawner, object>.State
		{
			// Token: 0x04008D44 RID: 36164
			public GameStateMachine<HighEnergyParticleSpawner.States, HighEnergyParticleSpawner.StatesInstance, HighEnergyParticleSpawner, object>.State empty;

			// Token: 0x04008D45 RID: 36165
			public GameStateMachine<HighEnergyParticleSpawner.States, HighEnergyParticleSpawner.StatesInstance, HighEnergyParticleSpawner, object>.State losing;
		}

		// Token: 0x02001FB8 RID: 8120
		public class ReadyStates : GameStateMachine<HighEnergyParticleSpawner.States, HighEnergyParticleSpawner.StatesInstance, HighEnergyParticleSpawner, object>.State
		{
			// Token: 0x04008D46 RID: 36166
			public GameStateMachine<HighEnergyParticleSpawner.States, HighEnergyParticleSpawner.StatesInstance, HighEnergyParticleSpawner, object>.State idle;

			// Token: 0x04008D47 RID: 36167
			public GameStateMachine<HighEnergyParticleSpawner.States, HighEnergyParticleSpawner.StatesInstance, HighEnergyParticleSpawner, object>.State absorbing;
		}
	}
}
