using System;
using KSerialization;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200095D RID: 2397
[SerializationConfig(MemberSerialization.OptIn)]
public class RocketEngineCluster : StateMachineComponent<RocketEngineCluster.StatesInstance>
{
	// Token: 0x060046CF RID: 18127 RVA: 0x0018E944 File Offset: 0x0018CB44
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
		if (this.mainEngine)
		{
			base.GetComponent<RocketModule>().AddModuleCondition(ProcessCondition.ProcessConditionType.RocketPrep, new RequireAttachedComponent(base.gameObject.GetComponent<AttachableBuilding>(), typeof(IFuelTank), UI.STARMAP.COMPONENT.FUEL_TANK));
			if (this.requireOxidizer)
			{
				base.GetComponent<RocketModule>().AddModuleCondition(ProcessCondition.ProcessConditionType.RocketPrep, new RequireAttachedComponent(base.gameObject.GetComponent<AttachableBuilding>(), typeof(OxidizerTank), UI.STARMAP.COMPONENT.OXIDIZER_TANK));
			}
			base.GetComponent<RocketModule>().AddModuleCondition(ProcessCondition.ProcessConditionType.RocketPrep, new ConditionRocketHeight(this));
		}
	}

	// Token: 0x060046D0 RID: 18128 RVA: 0x0018E9E8 File Offset: 0x0018CBE8
	private void ConfigureFlameLight()
	{
		this.flameLight = base.gameObject.AddOrGet<Light2D>();
		this.flameLight.Color = Color.white;
		this.flameLight.overlayColour = LIGHT2D.LIGHTBUG_OVERLAYCOLOR;
		this.flameLight.Range = 10f;
		this.flameLight.Angle = 0f;
		this.flameLight.Direction = LIGHT2D.LIGHTBUG_DIRECTION;
		this.flameLight.Offset = LIGHT2D.LIGHTBUG_OFFSET;
		this.flameLight.shape = global::LightShape.Circle;
		this.flameLight.drawOverlay = true;
		this.flameLight.Lux = 80000;
		this.flameLight.emitter.RemoveFromGrid();
		base.gameObject.AddOrGet<LightSymbolTracker>().targetSymbol = base.GetComponent<KBatchedAnimController>().CurrentAnim.rootSymbol;
		this.flameLight.enabled = false;
	}

	// Token: 0x060046D1 RID: 18129 RVA: 0x0018EACC File Offset: 0x0018CCCC
	private void UpdateFlameLight(int cell)
	{
		base.smi.master.flameLight.RefreshShapeAndPosition();
		if (Grid.IsValidCell(cell))
		{
			if (!base.smi.master.flameLight.enabled && base.smi.timeinstate > 3f)
			{
				base.smi.master.flameLight.enabled = true;
				return;
			}
		}
		else
		{
			base.smi.master.flameLight.enabled = false;
		}
	}

	// Token: 0x060046D2 RID: 18130 RVA: 0x0018EB4D File Offset: 0x0018CD4D
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
	}

	// Token: 0x04002EE3 RID: 12003
	public float exhaustEmitRate = 50f;

	// Token: 0x04002EE4 RID: 12004
	public float exhaustTemperature = 1500f;

	// Token: 0x04002EE5 RID: 12005
	public SpawnFXHashes explosionEffectHash;

	// Token: 0x04002EE6 RID: 12006
	public SimHashes exhaustElement = SimHashes.CarbonDioxide;

	// Token: 0x04002EE7 RID: 12007
	public Tag fuelTag;

	// Token: 0x04002EE8 RID: 12008
	public float efficiency = 1f;

	// Token: 0x04002EE9 RID: 12009
	public bool requireOxidizer = true;

	// Token: 0x04002EEA RID: 12010
	public int maxModules = 32;

	// Token: 0x04002EEB RID: 12011
	public int maxHeight;

	// Token: 0x04002EEC RID: 12012
	public bool mainEngine = true;

	// Token: 0x04002EED RID: 12013
	public byte exhaustDiseaseIdx = byte.MaxValue;

	// Token: 0x04002EEE RID: 12014
	public int exhaustDiseaseCount;

	// Token: 0x04002EEF RID: 12015
	public bool emitRadiation;

	// Token: 0x04002EF0 RID: 12016
	[MyCmpGet]
	private RadiationEmitter radiationEmitter;

	// Token: 0x04002EF1 RID: 12017
	[MyCmpGet]
	private Generator powerGenerator;

	// Token: 0x04002EF2 RID: 12018
	[MyCmpReq]
	private KBatchedAnimController animController;

	// Token: 0x04002EF3 RID: 12019
	public Light2D flameLight;

	// Token: 0x0200175C RID: 5980
	public class StatesInstance : GameStateMachine<RocketEngineCluster.States, RocketEngineCluster.StatesInstance, RocketEngineCluster, object>.GameInstance
	{
		// Token: 0x06008AAE RID: 35502 RVA: 0x002FD9FB File Offset: 0x002FBBFB
		public StatesInstance(RocketEngineCluster smi)
			: base(smi)
		{
			if (smi.emitRadiation)
			{
				DebugUtil.Assert(smi.radiationEmitter != null, "emitRadiation enabled but no RadiationEmitter component");
				this.radiationEmissionBaseOffset = smi.radiationEmitter.emissionOffset;
			}
		}

		// Token: 0x06008AAF RID: 35503 RVA: 0x002FDA34 File Offset: 0x002FBC34
		public void BeginBurn()
		{
			if (base.smi.master.emitRadiation)
			{
				base.smi.master.radiationEmitter.SetEmitting(true);
			}
			LaunchPad currentPad = base.smi.master.GetComponent<RocketModuleCluster>().CraftInterface.CurrentPad;
			if (currentPad != null)
			{
				this.pad_cell = Grid.PosToCell(currentPad.gameObject.transform.GetPosition());
				if (base.smi.master.exhaustDiseaseIdx != 255)
				{
					currentPad.GetComponent<PrimaryElement>().AddDisease(base.smi.master.exhaustDiseaseIdx, base.smi.master.exhaustDiseaseCount, "rocket exhaust");
					return;
				}
			}
			else
			{
				global::Debug.LogWarning("RocketEngineCluster missing LaunchPad for burn.");
				this.pad_cell = Grid.InvalidCell;
			}
		}

		// Token: 0x06008AB0 RID: 35504 RVA: 0x002FDB08 File Offset: 0x002FBD08
		public void DoBurn(float dt)
		{
			int num = Grid.PosToCell(base.smi.master.gameObject.transform.GetPosition() + base.smi.master.animController.Offset);
			if (Grid.AreCellsInSameWorld(num, this.pad_cell))
			{
				SimMessages.EmitMass(num, ElementLoader.GetElementIndex(base.smi.master.exhaustElement), dt * base.smi.master.exhaustEmitRate, base.smi.master.exhaustTemperature, base.smi.master.exhaustDiseaseIdx, base.smi.master.exhaustDiseaseCount, -1);
			}
			if (base.smi.master.emitRadiation)
			{
				Vector3 emissionOffset = base.smi.master.radiationEmitter.emissionOffset;
				base.smi.master.radiationEmitter.emissionOffset = base.smi.radiationEmissionBaseOffset + base.smi.master.animController.Offset;
				if (Grid.AreCellsInSameWorld(base.smi.master.radiationEmitter.GetEmissionCell(), this.pad_cell))
				{
					base.smi.master.radiationEmitter.Refresh();
				}
				else
				{
					base.smi.master.radiationEmitter.emissionOffset = emissionOffset;
					base.smi.master.radiationEmitter.SetEmitting(false);
				}
			}
			int num2 = 10;
			for (int i = 1; i < num2; i++)
			{
				int num3 = Grid.OffsetCell(num, -1, -i);
				int num4 = Grid.OffsetCell(num, 0, -i);
				int num5 = Grid.OffsetCell(num, 1, -i);
				if (Grid.AreCellsInSameWorld(num3, this.pad_cell))
				{
					if (base.smi.master.exhaustDiseaseIdx != 255)
					{
						SimMessages.ModifyDiseaseOnCell(num3, base.smi.master.exhaustDiseaseIdx, (int)((float)base.smi.master.exhaustDiseaseCount / ((float)i + 1f)));
					}
					SimMessages.ModifyEnergy(num3, base.smi.master.exhaustTemperature / (float)(i + 1), 3200f, SimMessages.EnergySourceID.Burner);
				}
				if (Grid.AreCellsInSameWorld(num4, this.pad_cell))
				{
					if (base.smi.master.exhaustDiseaseIdx != 255)
					{
						SimMessages.ModifyDiseaseOnCell(num4, base.smi.master.exhaustDiseaseIdx, (int)((float)base.smi.master.exhaustDiseaseCount / (float)i));
					}
					SimMessages.ModifyEnergy(num4, base.smi.master.exhaustTemperature / (float)i, 3200f, SimMessages.EnergySourceID.Burner);
				}
				if (Grid.AreCellsInSameWorld(num5, this.pad_cell))
				{
					if (base.smi.master.exhaustDiseaseIdx != 255)
					{
						SimMessages.ModifyDiseaseOnCell(num5, base.smi.master.exhaustDiseaseIdx, (int)((float)base.smi.master.exhaustDiseaseCount / ((float)i + 1f)));
					}
					SimMessages.ModifyEnergy(num5, base.smi.master.exhaustTemperature / (float)(i + 1), 3200f, SimMessages.EnergySourceID.Burner);
				}
			}
		}

		// Token: 0x06008AB1 RID: 35505 RVA: 0x002FDE30 File Offset: 0x002FC030
		public void EndBurn()
		{
			if (base.smi.master.emitRadiation)
			{
				base.smi.master.radiationEmitter.emissionOffset = base.smi.radiationEmissionBaseOffset;
				base.smi.master.radiationEmitter.SetEmitting(false);
			}
			this.pad_cell = Grid.InvalidCell;
		}

		// Token: 0x04006CD5 RID: 27861
		public Vector3 radiationEmissionBaseOffset;

		// Token: 0x04006CD6 RID: 27862
		private int pad_cell;
	}

	// Token: 0x0200175D RID: 5981
	public class States : GameStateMachine<RocketEngineCluster.States, RocketEngineCluster.StatesInstance, RocketEngineCluster>
	{
		// Token: 0x06008AB2 RID: 35506 RVA: 0x002FDE90 File Offset: 0x002FC090
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.initializing.load;
			this.initializing.load.ScheduleGoTo(0f, this.initializing.decide);
			this.initializing.decide.Transition(this.space, new StateMachine<RocketEngineCluster.States, RocketEngineCluster.StatesInstance, RocketEngineCluster, object>.Transition.ConditionCallback(this.IsRocketInSpace), UpdateRate.SIM_200ms).Transition(this.burning, new StateMachine<RocketEngineCluster.States, RocketEngineCluster.StatesInstance, RocketEngineCluster, object>.Transition.ConditionCallback(this.IsRocketAirborne), UpdateRate.SIM_200ms).Transition(this.idle, new StateMachine<RocketEngineCluster.States, RocketEngineCluster.StatesInstance, RocketEngineCluster, object>.Transition.ConditionCallback(this.IsRocketGrounded), UpdateRate.SIM_200ms);
			this.idle.DefaultState(this.idle.grounded).EventTransition(GameHashes.RocketLaunched, this.burning_pre, null);
			this.idle.grounded.EventTransition(GameHashes.LaunchConditionChanged, this.idle.ready, new StateMachine<RocketEngineCluster.States, RocketEngineCluster.StatesInstance, RocketEngineCluster, object>.Transition.ConditionCallback(this.IsReadyToLaunch)).QueueAnim("grounded", true, null);
			this.idle.ready.EventTransition(GameHashes.LaunchConditionChanged, this.idle.grounded, GameStateMachine<RocketEngineCluster.States, RocketEngineCluster.StatesInstance, RocketEngineCluster, object>.Not(new StateMachine<RocketEngineCluster.States, RocketEngineCluster.StatesInstance, RocketEngineCluster, object>.Transition.ConditionCallback(this.IsReadyToLaunch))).PlayAnim("pre_ready_to_launch", KAnim.PlayMode.Once).QueueAnim("ready_to_launch", true, null)
				.Exit(delegate(RocketEngineCluster.StatesInstance smi)
				{
					KAnimControllerBase component = smi.GetComponent<KAnimControllerBase>();
					if (component != null)
					{
						component.Play("pst_ready_to_launch", KAnim.PlayMode.Once, 1f, 0f);
					}
				});
			this.burning_pre.PlayAnim("launch_pre").OnAnimQueueComplete(this.burning);
			this.burning.EventTransition(GameHashes.RocketLanded, this.burnComplete, null).PlayAnim("launch_loop", KAnim.PlayMode.Loop).Enter(delegate(RocketEngineCluster.StatesInstance smi)
			{
				smi.BeginBurn();
			})
				.Update(delegate(RocketEngineCluster.StatesInstance smi, float dt)
				{
					smi.DoBurn(dt);
				}, UpdateRate.SIM_200ms, false)
				.Exit(delegate(RocketEngineCluster.StatesInstance smi)
				{
					smi.EndBurn();
				})
				.TagTransition(GameTags.RocketInSpace, this.space, false);
			this.space.EventTransition(GameHashes.DoReturnRocket, this.burning, null);
			this.burnComplete.PlayAnim("launch_pst", KAnim.PlayMode.Loop).GoTo(this.idle);
		}

		// Token: 0x06008AB3 RID: 35507 RVA: 0x002FE0E0 File Offset: 0x002FC2E0
		private bool IsReadyToLaunch(RocketEngineCluster.StatesInstance smi)
		{
			return smi.GetComponent<RocketModuleCluster>().CraftInterface.CheckPreppedForLaunch();
		}

		// Token: 0x06008AB4 RID: 35508 RVA: 0x002FE0F2 File Offset: 0x002FC2F2
		public bool IsRocketAirborne(RocketEngineCluster.StatesInstance smi)
		{
			return smi.master.HasTag(GameTags.RocketNotOnGround) && !smi.master.HasTag(GameTags.RocketInSpace);
		}

		// Token: 0x06008AB5 RID: 35509 RVA: 0x002FE11B File Offset: 0x002FC31B
		public bool IsRocketGrounded(RocketEngineCluster.StatesInstance smi)
		{
			return smi.master.HasTag(GameTags.RocketOnGround);
		}

		// Token: 0x06008AB6 RID: 35510 RVA: 0x002FE12D File Offset: 0x002FC32D
		public bool IsRocketInSpace(RocketEngineCluster.StatesInstance smi)
		{
			return smi.master.HasTag(GameTags.RocketInSpace);
		}

		// Token: 0x04006CD7 RID: 27863
		public RocketEngineCluster.States.InitializingStates initializing;

		// Token: 0x04006CD8 RID: 27864
		public RocketEngineCluster.States.IdleStates idle;

		// Token: 0x04006CD9 RID: 27865
		public GameStateMachine<RocketEngineCluster.States, RocketEngineCluster.StatesInstance, RocketEngineCluster, object>.State burning_pre;

		// Token: 0x04006CDA RID: 27866
		public GameStateMachine<RocketEngineCluster.States, RocketEngineCluster.StatesInstance, RocketEngineCluster, object>.State burning;

		// Token: 0x04006CDB RID: 27867
		public GameStateMachine<RocketEngineCluster.States, RocketEngineCluster.StatesInstance, RocketEngineCluster, object>.State burnComplete;

		// Token: 0x04006CDC RID: 27868
		public GameStateMachine<RocketEngineCluster.States, RocketEngineCluster.StatesInstance, RocketEngineCluster, object>.State space;

		// Token: 0x020020C6 RID: 8390
		public class InitializingStates : GameStateMachine<RocketEngineCluster.States, RocketEngineCluster.StatesInstance, RocketEngineCluster, object>.State
		{
			// Token: 0x040091FF RID: 37375
			public GameStateMachine<RocketEngineCluster.States, RocketEngineCluster.StatesInstance, RocketEngineCluster, object>.State load;

			// Token: 0x04009200 RID: 37376
			public GameStateMachine<RocketEngineCluster.States, RocketEngineCluster.StatesInstance, RocketEngineCluster, object>.State decide;
		}

		// Token: 0x020020C7 RID: 8391
		public class IdleStates : GameStateMachine<RocketEngineCluster.States, RocketEngineCluster.StatesInstance, RocketEngineCluster, object>.State
		{
			// Token: 0x04009201 RID: 37377
			public GameStateMachine<RocketEngineCluster.States, RocketEngineCluster.StatesInstance, RocketEngineCluster, object>.State grounded;

			// Token: 0x04009202 RID: 37378
			public GameStateMachine<RocketEngineCluster.States, RocketEngineCluster.StatesInstance, RocketEngineCluster, object>.State ready;
		}
	}
}
