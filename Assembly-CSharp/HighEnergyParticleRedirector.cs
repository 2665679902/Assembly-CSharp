using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x020005CF RID: 1487
[SerializationConfig(MemberSerialization.OptIn)]
public class HighEnergyParticleRedirector : StateMachineComponent<HighEnergyParticleRedirector.StatesInstance>, IHighEnergyParticleDirection
{
	// Token: 0x170001FD RID: 509
	// (get) Token: 0x06002502 RID: 9474 RVA: 0x000C83D8 File Offset: 0x000C65D8
	// (set) Token: 0x06002503 RID: 9475 RVA: 0x000C83E0 File Offset: 0x000C65E0
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

	// Token: 0x06002504 RID: 9476 RVA: 0x000C8438 File Offset: 0x000C6638
	private void OnCopySettings(object data)
	{
		HighEnergyParticleRedirector component = ((GameObject)data).GetComponent<HighEnergyParticleRedirector>();
		if (component != null)
		{
			this.Direction = component.Direction;
		}
	}

	// Token: 0x06002505 RID: 9477 RVA: 0x000C8466 File Offset: 0x000C6666
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<HighEnergyParticleRedirector>(-905833192, HighEnergyParticleRedirector.OnCopySettingsDelegate);
		base.Subscribe<HighEnergyParticleRedirector>(-801688580, HighEnergyParticleRedirector.OnLogicValueChangedDelegate);
	}

	// Token: 0x06002506 RID: 9478 RVA: 0x000C8490 File Offset: 0x000C6690
	protected override void OnSpawn()
	{
		base.OnSpawn();
		HighEnergyParticlePort component = base.GetComponent<HighEnergyParticlePort>();
		if (component)
		{
			HighEnergyParticlePort highEnergyParticlePort = component;
			highEnergyParticlePort.onParticleCaptureAllowed = (HighEnergyParticlePort.OnParticleCaptureAllowed)Delegate.Combine(highEnergyParticlePort.onParticleCaptureAllowed, new HighEnergyParticlePort.OnParticleCaptureAllowed(this.OnParticleCaptureAllowed));
		}
		if (HighEnergyParticleRedirector.infoStatusItem_Logic == null)
		{
			HighEnergyParticleRedirector.infoStatusItem_Logic = new StatusItem("HEPRedirectorLogic", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022, null);
			HighEnergyParticleRedirector.infoStatusItem_Logic.resolveStringCallback = new Func<string, object, string>(HighEnergyParticleRedirector.ResolveInfoStatusItem);
			HighEnergyParticleRedirector.infoStatusItem_Logic.resolveTooltipCallback = new Func<string, object, string>(HighEnergyParticleRedirector.ResolveInfoStatusItemTooltip);
		}
		this.selectable.AddStatusItem(HighEnergyParticleRedirector.infoStatusItem_Logic, this);
		this.directionController = new EightDirectionController(base.GetComponent<KBatchedAnimController>(), "redirector_target", "redirector", EightDirectionController.Offset.Infront);
		this.Direction = this.Direction;
		base.smi.StartSM();
		Tutorial.Instance.TutorialMessage(Tutorial.TutorialMessages.TM_Radiation, true);
	}

	// Token: 0x06002507 RID: 9479 RVA: 0x000C8582 File Offset: 0x000C6782
	private bool OnParticleCaptureAllowed(HighEnergyParticle particle)
	{
		return this.AllowIncomingParticles;
	}

	// Token: 0x06002508 RID: 9480 RVA: 0x000C858C File Offset: 0x000C678C
	private void LaunchParticle()
	{
		if (base.smi.master.storage.Particles < 0.1f)
		{
			base.smi.master.storage.ConsumeAll();
			return;
		}
		int highEnergyParticleOutputCell = base.GetComponent<Building>().GetHighEnergyParticleOutputCell();
		GameObject gameObject = GameUtil.KInstantiate(Assets.GetPrefab("HighEnergyParticle"), Grid.CellToPosCCC(highEnergyParticleOutputCell, Grid.SceneLayer.FXFront2), Grid.SceneLayer.FXFront2, null, 0);
		gameObject.SetActive(true);
		if (gameObject != null)
		{
			HighEnergyParticle component = gameObject.GetComponent<HighEnergyParticle>();
			component.payload = base.smi.master.storage.ConsumeAll();
			component.payload -= 0.1f;
			component.capturedBy = this.port;
			component.SetDirection(this.Direction);
			this.directionController.PlayAnim("redirector_send", KAnim.PlayMode.Once);
			this.directionController.controller.Queue("redirector", KAnim.PlayMode.Once, 1f, 0f);
		}
	}

	// Token: 0x06002509 RID: 9481 RVA: 0x000C868C File Offset: 0x000C688C
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		HighEnergyParticlePort component = base.GetComponent<HighEnergyParticlePort>();
		if (component != null)
		{
			HighEnergyParticlePort highEnergyParticlePort = component;
			highEnergyParticlePort.onParticleCaptureAllowed = (HighEnergyParticlePort.OnParticleCaptureAllowed)Delegate.Remove(highEnergyParticlePort.onParticleCaptureAllowed, new HighEnergyParticlePort.OnParticleCaptureAllowed(this.OnParticleCaptureAllowed));
		}
	}

	// Token: 0x170001FE RID: 510
	// (get) Token: 0x0600250A RID: 9482 RVA: 0x000C86D1 File Offset: 0x000C68D1
	public bool AllowIncomingParticles
	{
		get
		{
			return !this.hasLogicWire || (this.hasLogicWire && this.isLogicActive);
		}
	}

	// Token: 0x170001FF RID: 511
	// (get) Token: 0x0600250B RID: 9483 RVA: 0x000C86ED File Offset: 0x000C68ED
	public bool HasLogicWire
	{
		get
		{
			return this.hasLogicWire;
		}
	}

	// Token: 0x17000200 RID: 512
	// (get) Token: 0x0600250C RID: 9484 RVA: 0x000C86F5 File Offset: 0x000C68F5
	public bool IsLogicActive
	{
		get
		{
			return this.isLogicActive;
		}
	}

	// Token: 0x0600250D RID: 9485 RVA: 0x000C8700 File Offset: 0x000C6900
	private LogicCircuitNetwork GetNetwork()
	{
		int portCell = base.GetComponent<LogicPorts>().GetPortCell(HighEnergyParticleRedirector.PORT_ID);
		return Game.Instance.logicCircuitManager.GetNetworkForCell(portCell);
	}

	// Token: 0x0600250E RID: 9486 RVA: 0x000C8730 File Offset: 0x000C6930
	private void OnLogicValueChanged(object data)
	{
		LogicValueChanged logicValueChanged = (LogicValueChanged)data;
		if (logicValueChanged.portID == HighEnergyParticleRedirector.PORT_ID)
		{
			this.isLogicActive = logicValueChanged.newValue > 0;
			this.hasLogicWire = this.GetNetwork() != null;
		}
	}

	// Token: 0x0600250F RID: 9487 RVA: 0x000C8774 File Offset: 0x000C6974
	private static string ResolveInfoStatusItem(string format_str, object data)
	{
		HighEnergyParticleRedirector highEnergyParticleRedirector = (HighEnergyParticleRedirector)data;
		if (!highEnergyParticleRedirector.HasLogicWire)
		{
			return BUILDING.STATUSITEMS.HIGHENERGYPARTICLEREDIRECTOR.NORMAL;
		}
		if (highEnergyParticleRedirector.IsLogicActive)
		{
			return BUILDING.STATUSITEMS.HIGHENERGYPARTICLEREDIRECTOR.LOGIC_CONTROLLED_ACTIVE;
		}
		return BUILDING.STATUSITEMS.HIGHENERGYPARTICLEREDIRECTOR.LOGIC_CONTROLLED_STANDBY;
	}

	// Token: 0x06002510 RID: 9488 RVA: 0x000C87B8 File Offset: 0x000C69B8
	private static string ResolveInfoStatusItemTooltip(string format_str, object data)
	{
		HighEnergyParticleRedirector highEnergyParticleRedirector = (HighEnergyParticleRedirector)data;
		if (!highEnergyParticleRedirector.HasLogicWire)
		{
			return BUILDING.STATUSITEMS.HIGHENERGYPARTICLEREDIRECTOR.TOOLTIPS.NORMAL;
		}
		if (highEnergyParticleRedirector.IsLogicActive)
		{
			return BUILDING.STATUSITEMS.HIGHENERGYPARTICLEREDIRECTOR.TOOLTIPS.LOGIC_CONTROLLED_ACTIVE;
		}
		return BUILDING.STATUSITEMS.HIGHENERGYPARTICLEREDIRECTOR.TOOLTIPS.LOGIC_CONTROLLED_STANDBY;
	}

	// Token: 0x04001567 RID: 5479
	public static readonly HashedString PORT_ID = "HEPRedirector";

	// Token: 0x04001568 RID: 5480
	[MyCmpReq]
	private KSelectable selectable;

	// Token: 0x04001569 RID: 5481
	[MyCmpReq]
	private HighEnergyParticleStorage storage;

	// Token: 0x0400156A RID: 5482
	[MyCmpGet]
	private HighEnergyParticlePort port;

	// Token: 0x0400156B RID: 5483
	public float directorDelay;

	// Token: 0x0400156C RID: 5484
	public bool directionControllable = true;

	// Token: 0x0400156D RID: 5485
	[Serialize]
	private EightDirection _direction;

	// Token: 0x0400156E RID: 5486
	private EightDirectionController directionController;

	// Token: 0x0400156F RID: 5487
	[MyCmpAdd]
	private CopyBuildingSettings copyBuildingSettings;

	// Token: 0x04001570 RID: 5488
	private static readonly EventSystem.IntraObjectHandler<HighEnergyParticleRedirector> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<HighEnergyParticleRedirector>(delegate(HighEnergyParticleRedirector component, object data)
	{
		component.OnCopySettings(data);
	});

	// Token: 0x04001571 RID: 5489
	private static readonly EventSystem.IntraObjectHandler<HighEnergyParticleRedirector> OnLogicValueChangedDelegate = new EventSystem.IntraObjectHandler<HighEnergyParticleRedirector>(delegate(HighEnergyParticleRedirector component, object data)
	{
		component.OnLogicValueChanged(data);
	});

	// Token: 0x04001572 RID: 5490
	private bool hasLogicWire;

	// Token: 0x04001573 RID: 5491
	private bool isLogicActive;

	// Token: 0x04001574 RID: 5492
	private static StatusItem infoStatusItem_Logic;

	// Token: 0x02001223 RID: 4643
	public class StatesInstance : GameStateMachine<HighEnergyParticleRedirector.States, HighEnergyParticleRedirector.StatesInstance, HighEnergyParticleRedirector, object>.GameInstance
	{
		// Token: 0x06007935 RID: 31029 RVA: 0x002C2738 File Offset: 0x002C0938
		public StatesInstance(HighEnergyParticleRedirector smi)
			: base(smi)
		{
		}
	}

	// Token: 0x02001224 RID: 4644
	public class States : GameStateMachine<HighEnergyParticleRedirector.States, HighEnergyParticleRedirector.StatesInstance, HighEnergyParticleRedirector>
	{
		// Token: 0x06007936 RID: 31030 RVA: 0x002C2744 File Offset: 0x002C0944
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.inoperational;
			this.inoperational.PlayAnim("off").TagTransition(GameTags.Operational, this.ready, false);
			this.ready.PlayAnim("on").TagTransition(GameTags.Operational, this.inoperational, true).EventTransition(GameHashes.OnParticleStorageChanged, this.redirect, null);
			this.redirect.PlayAnim("working_pre").QueueAnim("working_loop", false, null).QueueAnim("working_pst", false, null)
				.ScheduleGoTo((HighEnergyParticleRedirector.StatesInstance smi) => smi.master.directorDelay, this.ready)
				.Exit(delegate(HighEnergyParticleRedirector.StatesInstance smi)
				{
					smi.master.LaunchParticle();
				});
		}

		// Token: 0x04005D27 RID: 23847
		public GameStateMachine<HighEnergyParticleRedirector.States, HighEnergyParticleRedirector.StatesInstance, HighEnergyParticleRedirector, object>.State inoperational;

		// Token: 0x04005D28 RID: 23848
		public GameStateMachine<HighEnergyParticleRedirector.States, HighEnergyParticleRedirector.StatesInstance, HighEnergyParticleRedirector, object>.State ready;

		// Token: 0x04005D29 RID: 23849
		public GameStateMachine<HighEnergyParticleRedirector.States, HighEnergyParticleRedirector.StatesInstance, HighEnergyParticleRedirector, object>.State redirect;

		// Token: 0x04005D2A RID: 23850
		public GameStateMachine<HighEnergyParticleRedirector.States, HighEnergyParticleRedirector.StatesInstance, HighEnergyParticleRedirector, object>.State launch;
	}
}
