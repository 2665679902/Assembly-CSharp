using System;
using KSerialization;
using UnityEngine;

// Token: 0x02000603 RID: 1539
[SerializationConfig(MemberSerialization.OptIn)]
public class ManualHighEnergyParticleSpawner : StateMachineComponent<ManualHighEnergyParticleSpawner.StatesInstance>, IHighEnergyParticleDirection
{
	// Token: 0x1700028C RID: 652
	// (get) Token: 0x0600280D RID: 10253 RVA: 0x000D5332 File Offset: 0x000D3532
	// (set) Token: 0x0600280E RID: 10254 RVA: 0x000D533C File Offset: 0x000D353C
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

	// Token: 0x0600280F RID: 10255 RVA: 0x000D5394 File Offset: 0x000D3594
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<ManualHighEnergyParticleSpawner>(-905833192, ManualHighEnergyParticleSpawner.OnCopySettingsDelegate);
	}

	// Token: 0x06002810 RID: 10256 RVA: 0x000D53B0 File Offset: 0x000D35B0
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
		this.radiationEmitter.SetEmitting(false);
		this.directionController = new EightDirectionController(base.GetComponent<KBatchedAnimController>(), "redirector_target", "redirect", EightDirectionController.Offset.Infront);
		this.Direction = this.Direction;
		Tutorial.Instance.TutorialMessage(Tutorial.TutorialMessages.TM_Radiation, true);
	}

	// Token: 0x06002811 RID: 10257 RVA: 0x000D5410 File Offset: 0x000D3610
	private void OnCopySettings(object data)
	{
		ManualHighEnergyParticleSpawner component = ((GameObject)data).GetComponent<ManualHighEnergyParticleSpawner>();
		if (component != null)
		{
			this.Direction = component.Direction;
		}
	}

	// Token: 0x06002812 RID: 10258 RVA: 0x000D5440 File Offset: 0x000D3640
	public void LauncherUpdate()
	{
		if (this.particleStorage.Particles > 0f)
		{
			int highEnergyParticleOutputCell = base.GetComponent<Building>().GetHighEnergyParticleOutputCell();
			GameObject gameObject = GameUtil.KInstantiate(Assets.GetPrefab("HighEnergyParticle"), Grid.CellToPosCCC(highEnergyParticleOutputCell, Grid.SceneLayer.FXFront2), Grid.SceneLayer.FXFront2, null, 0);
			gameObject.SetActive(true);
			if (gameObject != null)
			{
				HighEnergyParticle component = gameObject.GetComponent<HighEnergyParticle>();
				component.payload = this.particleStorage.ConsumeAndGet(this.particleStorage.Particles);
				component.SetDirection(this.Direction);
				this.directionController.PlayAnim("redirect_send", KAnim.PlayMode.Once);
				this.directionController.controller.Queue("redirect", KAnim.PlayMode.Once, 1f, 0f);
			}
		}
	}

	// Token: 0x04001789 RID: 6025
	[MyCmpReq]
	private HighEnergyParticleStorage particleStorage;

	// Token: 0x0400178A RID: 6026
	[MyCmpGet]
	private RadiationEmitter radiationEmitter;

	// Token: 0x0400178B RID: 6027
	[Serialize]
	private EightDirection _direction;

	// Token: 0x0400178C RID: 6028
	private EightDirectionController directionController;

	// Token: 0x0400178D RID: 6029
	[MyCmpAdd]
	private CopyBuildingSettings copyBuildingSettings;

	// Token: 0x0400178E RID: 6030
	private static readonly EventSystem.IntraObjectHandler<ManualHighEnergyParticleSpawner> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<ManualHighEnergyParticleSpawner>(delegate(ManualHighEnergyParticleSpawner component, object data)
	{
		component.OnCopySettings(data);
	});

	// Token: 0x0200126A RID: 4714
	public class StatesInstance : GameStateMachine<ManualHighEnergyParticleSpawner.States, ManualHighEnergyParticleSpawner.StatesInstance, ManualHighEnergyParticleSpawner, object>.GameInstance
	{
		// Token: 0x06007A21 RID: 31265 RVA: 0x002C6231 File Offset: 0x002C4431
		public StatesInstance(ManualHighEnergyParticleSpawner smi)
			: base(smi)
		{
		}

		// Token: 0x06007A22 RID: 31266 RVA: 0x002C623A File Offset: 0x002C443A
		public bool IsComplexFabricatorWorkable(object data)
		{
			return data as ComplexFabricatorWorkable != null;
		}
	}

	// Token: 0x0200126B RID: 4715
	public class States : GameStateMachine<ManualHighEnergyParticleSpawner.States, ManualHighEnergyParticleSpawner.StatesInstance, ManualHighEnergyParticleSpawner>
	{
		// Token: 0x06007A23 RID: 31267 RVA: 0x002C6248 File Offset: 0x002C4448
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.inoperational;
			this.inoperational.Enter(delegate(ManualHighEnergyParticleSpawner.StatesInstance smi)
			{
				smi.master.radiationEmitter.SetEmitting(false);
			}).TagTransition(GameTags.Operational, this.ready, false);
			this.ready.DefaultState(this.ready.idle).TagTransition(GameTags.Operational, this.inoperational, true).Update(delegate(ManualHighEnergyParticleSpawner.StatesInstance smi, float dt)
			{
				smi.master.LauncherUpdate();
			}, UpdateRate.SIM_200ms, false);
			this.ready.idle.EventHandlerTransition(GameHashes.WorkableStartWork, this.ready.working, (ManualHighEnergyParticleSpawner.StatesInstance smi, object data) => smi.IsComplexFabricatorWorkable(data));
			this.ready.working.Enter(delegate(ManualHighEnergyParticleSpawner.StatesInstance smi)
			{
				smi.master.radiationEmitter.SetEmitting(true);
			}).EventHandlerTransition(GameHashes.WorkableCompleteWork, this.ready.idle, (ManualHighEnergyParticleSpawner.StatesInstance smi, object data) => smi.IsComplexFabricatorWorkable(data)).EventHandlerTransition(GameHashes.WorkableStopWork, this.ready.idle, (ManualHighEnergyParticleSpawner.StatesInstance smi, object data) => smi.IsComplexFabricatorWorkable(data))
				.Exit(delegate(ManualHighEnergyParticleSpawner.StatesInstance smi)
				{
					smi.master.radiationEmitter.SetEmitting(false);
				});
		}

		// Token: 0x04005DC1 RID: 24001
		public GameStateMachine<ManualHighEnergyParticleSpawner.States, ManualHighEnergyParticleSpawner.StatesInstance, ManualHighEnergyParticleSpawner, object>.State inoperational;

		// Token: 0x04005DC2 RID: 24002
		public ManualHighEnergyParticleSpawner.States.ReadyStates ready;

		// Token: 0x02001FC8 RID: 8136
		public class ReadyStates : GameStateMachine<ManualHighEnergyParticleSpawner.States, ManualHighEnergyParticleSpawner.StatesInstance, ManualHighEnergyParticleSpawner, object>.State
		{
			// Token: 0x04008D9F RID: 36255
			public GameStateMachine<ManualHighEnergyParticleSpawner.States, ManualHighEnergyParticleSpawner.StatesInstance, ManualHighEnergyParticleSpawner, object>.State idle;

			// Token: 0x04008DA0 RID: 36256
			public GameStateMachine<ManualHighEnergyParticleSpawner.States, ManualHighEnergyParticleSpawner.StatesInstance, ManualHighEnergyParticleSpawner, object>.State working;
		}
	}
}
