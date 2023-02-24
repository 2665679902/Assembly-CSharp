using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000583 RID: 1411
public class Checkpoint : StateMachineComponent<Checkpoint.SMInstance>
{
	// Token: 0x170001BD RID: 445
	// (get) Token: 0x0600225A RID: 8794 RVA: 0x000BA642 File Offset: 0x000B8842
	private bool RedLightDesiredState
	{
		get
		{
			return this.hasLogicWire && !this.hasInputHigh && this.operational.IsOperational;
		}
	}

	// Token: 0x0600225B RID: 8795 RVA: 0x000BA664 File Offset: 0x000B8864
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.Subscribe<Checkpoint>(-801688580, Checkpoint.OnLogicValueChangedDelegate);
		base.Subscribe<Checkpoint>(-592767678, Checkpoint.OnOperationalChangedDelegate);
		base.smi.StartSM();
		if (Checkpoint.infoStatusItem_Logic == null)
		{
			Checkpoint.infoStatusItem_Logic = new StatusItem("CheckpointLogic", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022, null);
			Checkpoint.infoStatusItem_Logic.resolveStringCallback = new Func<string, object, string>(Checkpoint.ResolveInfoStatusItem_Logic);
		}
		this.Refresh(this.redLight);
	}

	// Token: 0x0600225C RID: 8796 RVA: 0x000BA6F5 File Offset: 0x000B88F5
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		this.ClearReactable();
	}

	// Token: 0x0600225D RID: 8797 RVA: 0x000BA703 File Offset: 0x000B8903
	public void RefreshLight()
	{
		if (this.redLight != this.RedLightDesiredState)
		{
			this.Refresh(this.RedLightDesiredState);
			this.statusDirty = true;
		}
		if (this.statusDirty)
		{
			this.RefreshStatusItem();
		}
	}

	// Token: 0x0600225E RID: 8798 RVA: 0x000BA734 File Offset: 0x000B8934
	private LogicCircuitNetwork GetNetwork()
	{
		int portCell = base.GetComponent<LogicPorts>().GetPortCell(Checkpoint.PORT_ID);
		return Game.Instance.logicCircuitManager.GetNetworkForCell(portCell);
	}

	// Token: 0x0600225F RID: 8799 RVA: 0x000BA762 File Offset: 0x000B8962
	private static string ResolveInfoStatusItem_Logic(string format_str, object data)
	{
		return ((Checkpoint)data).RedLight ? BUILDING.STATUSITEMS.CHECKPOINT.LOGIC_CONTROLLED_CLOSED : BUILDING.STATUSITEMS.CHECKPOINT.LOGIC_CONTROLLED_OPEN;
	}

	// Token: 0x06002260 RID: 8800 RVA: 0x000BA782 File Offset: 0x000B8982
	private void CreateNewReactable()
	{
		if (this.reactable == null)
		{
			this.reactable = new Checkpoint.CheckpointReactable(this);
		}
	}

	// Token: 0x06002261 RID: 8801 RVA: 0x000BA798 File Offset: 0x000B8998
	private void OrphanReactable()
	{
		this.reactable = null;
	}

	// Token: 0x06002262 RID: 8802 RVA: 0x000BA7A1 File Offset: 0x000B89A1
	private void ClearReactable()
	{
		if (this.reactable != null)
		{
			this.reactable.Cleanup();
			this.reactable = null;
		}
	}

	// Token: 0x170001BE RID: 446
	// (get) Token: 0x06002263 RID: 8803 RVA: 0x000BA7BD File Offset: 0x000B89BD
	public bool RedLight
	{
		get
		{
			return this.redLight;
		}
	}

	// Token: 0x06002264 RID: 8804 RVA: 0x000BA7C8 File Offset: 0x000B89C8
	private void OnLogicValueChanged(object data)
	{
		LogicValueChanged logicValueChanged = (LogicValueChanged)data;
		if (logicValueChanged.portID == Checkpoint.PORT_ID)
		{
			this.hasInputHigh = LogicCircuitNetwork.IsBitActive(0, logicValueChanged.newValue);
			this.hasLogicWire = this.GetNetwork() != null;
			this.statusDirty = true;
		}
	}

	// Token: 0x06002265 RID: 8805 RVA: 0x000BA816 File Offset: 0x000B8A16
	private void OnOperationalChanged(object data)
	{
		this.statusDirty = true;
	}

	// Token: 0x06002266 RID: 8806 RVA: 0x000BA820 File Offset: 0x000B8A20
	private void RefreshStatusItem()
	{
		bool flag = this.operational.IsOperational && this.hasLogicWire;
		this.selectable.ToggleStatusItem(Checkpoint.infoStatusItem_Logic, flag, this);
		this.statusDirty = false;
	}

	// Token: 0x06002267 RID: 8807 RVA: 0x000BA860 File Offset: 0x000B8A60
	private void Refresh(bool redLightState)
	{
		this.redLight = redLightState;
		this.operational.SetActive(this.operational.IsOperational && this.redLight, false);
		base.smi.sm.redLight.Set(this.redLight, base.smi, false);
		if (this.redLight)
		{
			this.CreateNewReactable();
			return;
		}
		this.ClearReactable();
	}

	// Token: 0x040013D3 RID: 5075
	[MyCmpReq]
	public Operational operational;

	// Token: 0x040013D4 RID: 5076
	[MyCmpReq]
	private KSelectable selectable;

	// Token: 0x040013D5 RID: 5077
	private static StatusItem infoStatusItem_Logic;

	// Token: 0x040013D6 RID: 5078
	private Checkpoint.CheckpointReactable reactable;

	// Token: 0x040013D7 RID: 5079
	public static readonly HashedString PORT_ID = "Checkpoint";

	// Token: 0x040013D8 RID: 5080
	private bool hasLogicWire;

	// Token: 0x040013D9 RID: 5081
	private bool hasInputHigh;

	// Token: 0x040013DA RID: 5082
	private bool redLight;

	// Token: 0x040013DB RID: 5083
	private bool statusDirty = true;

	// Token: 0x040013DC RID: 5084
	private static readonly EventSystem.IntraObjectHandler<Checkpoint> OnLogicValueChangedDelegate = new EventSystem.IntraObjectHandler<Checkpoint>(delegate(Checkpoint component, object data)
	{
		component.OnLogicValueChanged(data);
	});

	// Token: 0x040013DD RID: 5085
	private static readonly EventSystem.IntraObjectHandler<Checkpoint> OnOperationalChangedDelegate = new EventSystem.IntraObjectHandler<Checkpoint>(delegate(Checkpoint component, object data)
	{
		component.OnOperationalChanged(data);
	});

	// Token: 0x020011AD RID: 4525
	private class CheckpointReactable : Reactable
	{
		// Token: 0x0600777C RID: 30588 RVA: 0x002BB73C File Offset: 0x002B993C
		public CheckpointReactable(Checkpoint checkpoint)
			: base(checkpoint.gameObject, "CheckpointReactable", Db.Get().ChoreTypes.Checkpoint, 1, 1, false, 0f, 0f, float.PositiveInfinity, 0f, ObjectLayer.NumLayers)
		{
			this.checkpoint = checkpoint;
			this.rotated = this.gameObject.GetComponent<Rotatable>().IsRotated;
			this.preventChoreInterruption = false;
		}

		// Token: 0x0600777D RID: 30589 RVA: 0x002BB7AC File Offset: 0x002B99AC
		public override bool InternalCanBegin(GameObject new_reactor, Navigator.ActiveTransition transition)
		{
			if (this.reactor != null)
			{
				return false;
			}
			if (this.checkpoint == null)
			{
				base.Cleanup();
				return false;
			}
			if (!this.checkpoint.RedLight)
			{
				return false;
			}
			if (this.rotated)
			{
				return transition.x < 0;
			}
			return transition.x > 0;
		}

		// Token: 0x0600777E RID: 30590 RVA: 0x002BB80C File Offset: 0x002B9A0C
		protected override void InternalBegin()
		{
			this.reactor_navigator = this.reactor.GetComponent<Navigator>();
			KBatchedAnimController component = this.reactor.GetComponent<KBatchedAnimController>();
			component.AddAnimOverrides(Assets.GetAnim("anim_idle_distracted_kanim"), 1f);
			component.Play("idle_pre", KAnim.PlayMode.Once, 1f, 0f);
			component.Queue("idle_default", KAnim.PlayMode.Loop, 1f, 0f);
			this.checkpoint.OrphanReactable();
			this.checkpoint.CreateNewReactable();
		}

		// Token: 0x0600777F RID: 30591 RVA: 0x002BB89C File Offset: 0x002B9A9C
		public override void Update(float dt)
		{
			if (this.checkpoint == null || !this.checkpoint.RedLight || this.reactor_navigator == null)
			{
				base.Cleanup();
				return;
			}
			this.reactor_navigator.AdvancePath(false);
			if (!this.reactor_navigator.path.IsValid())
			{
				base.Cleanup();
				return;
			}
			NavGrid.Transition nextTransition = this.reactor_navigator.GetNextTransition();
			if (!(this.rotated ? (nextTransition.x < 0) : (nextTransition.x > 0)))
			{
				base.Cleanup();
			}
		}

		// Token: 0x06007780 RID: 30592 RVA: 0x002BB92E File Offset: 0x002B9B2E
		protected override void InternalEnd()
		{
			if (this.reactor != null)
			{
				this.reactor.GetComponent<KBatchedAnimController>().RemoveAnimOverrides(Assets.GetAnim("anim_idle_distracted_kanim"));
			}
		}

		// Token: 0x06007781 RID: 30593 RVA: 0x002BB95D File Offset: 0x002B9B5D
		protected override void InternalCleanup()
		{
		}

		// Token: 0x04005B90 RID: 23440
		private Checkpoint checkpoint;

		// Token: 0x04005B91 RID: 23441
		private Navigator reactor_navigator;

		// Token: 0x04005B92 RID: 23442
		private bool rotated;
	}

	// Token: 0x020011AE RID: 4526
	public class SMInstance : GameStateMachine<Checkpoint.States, Checkpoint.SMInstance, Checkpoint, object>.GameInstance
	{
		// Token: 0x06007782 RID: 30594 RVA: 0x002BB95F File Offset: 0x002B9B5F
		public SMInstance(Checkpoint master)
			: base(master)
		{
		}
	}

	// Token: 0x020011AF RID: 4527
	public class States : GameStateMachine<Checkpoint.States, Checkpoint.SMInstance, Checkpoint>
	{
		// Token: 0x06007783 RID: 30595 RVA: 0x002BB968 File Offset: 0x002B9B68
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.go;
			this.root.Update("RefreshLight", delegate(Checkpoint.SMInstance smi, float dt)
			{
				smi.master.RefreshLight();
			}, UpdateRate.SIM_200ms, false);
			this.stop.ParamTransition<bool>(this.redLight, this.go, GameStateMachine<Checkpoint.States, Checkpoint.SMInstance, Checkpoint, object>.IsFalse).PlayAnim("red_light");
			this.go.ParamTransition<bool>(this.redLight, this.stop, GameStateMachine<Checkpoint.States, Checkpoint.SMInstance, Checkpoint, object>.IsTrue).PlayAnim("green_light");
		}

		// Token: 0x04005B93 RID: 23443
		public StateMachine<Checkpoint.States, Checkpoint.SMInstance, Checkpoint, object>.BoolParameter redLight;

		// Token: 0x04005B94 RID: 23444
		public GameStateMachine<Checkpoint.States, Checkpoint.SMInstance, Checkpoint, object>.State stop;

		// Token: 0x04005B95 RID: 23445
		public GameStateMachine<Checkpoint.States, Checkpoint.SMInstance, Checkpoint, object>.State go;
	}
}
