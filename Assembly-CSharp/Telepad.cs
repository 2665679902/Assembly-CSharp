using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x02000655 RID: 1621
public class Telepad : StateMachineComponent<Telepad.StatesInstance>
{
	// Token: 0x06002B6B RID: 11115 RVA: 0x000E4594 File Offset: 0x000E2794
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.GetComponent<Deconstructable>().allowDeconstruction = false;
		int num = 0;
		int num2 = 0;
		Grid.CellToXY(Grid.PosToCell(this), out num, out num2);
		if (num == 0)
		{
			global::Debug.LogError(string.Concat(new string[]
			{
				"Headquarters spawned at: (",
				num.ToString(),
				",",
				num2.ToString(),
				")"
			}));
		}
	}

	// Token: 0x06002B6C RID: 11116 RVA: 0x000E4608 File Offset: 0x000E2808
	protected override void OnSpawn()
	{
		base.OnSpawn();
		Components.Telepads.Add(this);
		this.meter = new MeterController(base.GetComponent<KBatchedAnimController>(), "meter_target", "meter", Meter.Offset.Behind, Grid.SceneLayer.NoLayer, new string[] { "meter_target", "meter_fill", "meter_frame", "meter_OL" });
		this.meter.gameObject.GetComponent<KBatchedAnimController>().SetDirty();
		base.smi.StartSM();
	}

	// Token: 0x06002B6D RID: 11117 RVA: 0x000E468A File Offset: 0x000E288A
	protected override void OnCleanUp()
	{
		Components.Telepads.Remove(this);
		base.OnCleanUp();
	}

	// Token: 0x06002B6E RID: 11118 RVA: 0x000E46A0 File Offset: 0x000E28A0
	public void Update()
	{
		if (base.smi.IsColonyLost())
		{
			return;
		}
		if (Immigration.Instance.ImmigrantsAvailable && base.GetComponent<Operational>().IsOperational)
		{
			base.smi.sm.openPortal.Trigger(base.smi);
			this.selectable.SetStatusItem(Db.Get().StatusItemCategories.Main, Db.Get().BuildingStatusItems.NewDuplicantsAvailable, this);
		}
		else
		{
			base.smi.sm.closePortal.Trigger(base.smi);
			this.selectable.SetStatusItem(Db.Get().StatusItemCategories.Main, Db.Get().BuildingStatusItems.Wattson, this);
		}
		if (this.GetTimeRemaining() < -120f)
		{
			Messenger.Instance.QueueMessage(new DuplicantsLeftMessage());
			Immigration.Instance.EndImmigration();
		}
	}

	// Token: 0x06002B6F RID: 11119 RVA: 0x000E4789 File Offset: 0x000E2989
	public void RejectAll()
	{
		Immigration.Instance.EndImmigration();
		base.smi.sm.closePortal.Trigger(base.smi);
	}

	// Token: 0x06002B70 RID: 11120 RVA: 0x000E47B4 File Offset: 0x000E29B4
	public void OnAcceptDelivery(ITelepadDeliverable delivery)
	{
		int num = Grid.PosToCell(this);
		Immigration.Instance.EndImmigration();
		GameObject gameObject = delivery.Deliver(Grid.CellToPosCBC(num, Grid.SceneLayer.Move));
		MinionIdentity component = gameObject.GetComponent<MinionIdentity>();
		if (component != null)
		{
			ReportManager.Instance.ReportValue(ReportManager.ReportType.PersonalTime, GameClock.Instance.GetTimeSinceStartOfReport(), string.Format(UI.ENDOFDAYREPORT.NOTES.PERSONAL_TIME, DUPLICANTS.CHORES.NOT_EXISTING_TASK), gameObject.GetProperName());
			foreach (MinionIdentity minionIdentity in Components.LiveMinionIdentities.GetWorldItems(base.gameObject.GetComponent<KSelectable>().GetMyWorldId(), false))
			{
				minionIdentity.GetComponent<Effects>().Add("NewCrewArrival", true);
			}
			MinionResume component2 = component.GetComponent<MinionResume>();
			int num2 = 0;
			while ((float)num2 < this.startingSkillPoints)
			{
				component2.ForceAddSkillPoint();
				num2++;
			}
		}
		base.smi.sm.closePortal.Trigger(base.smi);
	}

	// Token: 0x06002B71 RID: 11121 RVA: 0x000E48C8 File Offset: 0x000E2AC8
	public float GetTimeRemaining()
	{
		return Immigration.Instance.GetTimeRemaining();
	}

	// Token: 0x040019B2 RID: 6578
	[MyCmpReq]
	private KSelectable selectable;

	// Token: 0x040019B3 RID: 6579
	private MeterController meter;

	// Token: 0x040019B4 RID: 6580
	private const float MAX_IMMIGRATION_TIME = 120f;

	// Token: 0x040019B5 RID: 6581
	private const int NUM_METER_NOTCHES = 8;

	// Token: 0x040019B6 RID: 6582
	private List<MinionStartingStats> minionStats;

	// Token: 0x040019B7 RID: 6583
	public float startingSkillPoints;

	// Token: 0x040019B8 RID: 6584
	public static readonly HashedString[] PortalBirthAnim = new HashedString[] { "portalbirth" };

	// Token: 0x02001308 RID: 4872
	public class StatesInstance : GameStateMachine<Telepad.States, Telepad.StatesInstance, Telepad, object>.GameInstance
	{
		// Token: 0x06007C48 RID: 31816 RVA: 0x002CF9F9 File Offset: 0x002CDBF9
		public StatesInstance(Telepad master)
			: base(master)
		{
		}

		// Token: 0x06007C49 RID: 31817 RVA: 0x002CFA02 File Offset: 0x002CDC02
		public bool IsColonyLost()
		{
			return GameFlowManager.Instance != null && GameFlowManager.Instance.IsGameOver();
		}

		// Token: 0x06007C4A RID: 31818 RVA: 0x002CFA20 File Offset: 0x002CDC20
		public void UpdateMeter()
		{
			float timeRemaining = Immigration.Instance.GetTimeRemaining();
			float totalWaitTime = Immigration.Instance.GetTotalWaitTime();
			float num = Mathf.Clamp01(1f - timeRemaining / totalWaitTime);
			base.master.meter.SetPositionPercent(num);
		}
	}

	// Token: 0x02001309 RID: 4873
	public class States : GameStateMachine<Telepad.States, Telepad.StatesInstance, Telepad>
	{
		// Token: 0x06007C4B RID: 31819 RVA: 0x002CFA64 File Offset: 0x002CDC64
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.idle;
			base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
			this.root.OnSignal(this.idlePortal, this.resetToIdle);
			this.resetToIdle.GoTo(this.idle);
			this.idle.Enter(delegate(Telepad.StatesInstance smi)
			{
				smi.UpdateMeter();
			}).Update("TelepadMeter", delegate(Telepad.StatesInstance smi, float dt)
			{
				smi.UpdateMeter();
			}, UpdateRate.SIM_4000ms, false).EventTransition(GameHashes.OperationalChanged, this.unoperational, (Telepad.StatesInstance smi) => !smi.GetComponent<Operational>().IsOperational)
				.PlayAnim("idle")
				.OnSignal(this.openPortal, this.opening);
			this.unoperational.PlayAnim("idle").Enter("StopImmigration", delegate(Telepad.StatesInstance smi)
			{
				smi.master.meter.SetPositionPercent(0f);
			}).EventTransition(GameHashes.OperationalChanged, this.idle, (Telepad.StatesInstance smi) => smi.GetComponent<Operational>().IsOperational);
			this.opening.Enter(delegate(Telepad.StatesInstance smi)
			{
				smi.master.meter.SetPositionPercent(1f);
			}).PlayAnim("working_pre").OnAnimQueueComplete(this.open);
			this.open.OnSignal(this.closePortal, this.close).Enter(delegate(Telepad.StatesInstance smi)
			{
				smi.master.meter.SetPositionPercent(1f);
			}).PlayAnim("working_loop", KAnim.PlayMode.Loop)
				.Transition(this.close, (Telepad.StatesInstance smi) => smi.IsColonyLost(), UpdateRate.SIM_200ms)
				.EventTransition(GameHashes.OperationalChanged, this.close, (Telepad.StatesInstance smi) => !smi.GetComponent<Operational>().IsOperational);
			this.close.Enter(delegate(Telepad.StatesInstance smi)
			{
				smi.master.meter.SetPositionPercent(0f);
			}).PlayAnims((Telepad.StatesInstance smi) => Telepad.States.workingAnims, KAnim.PlayMode.Once).OnAnimQueueComplete(this.idle);
		}

		// Token: 0x04005F4A RID: 24394
		public StateMachine<Telepad.States, Telepad.StatesInstance, Telepad, object>.Signal openPortal;

		// Token: 0x04005F4B RID: 24395
		public StateMachine<Telepad.States, Telepad.StatesInstance, Telepad, object>.Signal closePortal;

		// Token: 0x04005F4C RID: 24396
		public StateMachine<Telepad.States, Telepad.StatesInstance, Telepad, object>.Signal idlePortal;

		// Token: 0x04005F4D RID: 24397
		public GameStateMachine<Telepad.States, Telepad.StatesInstance, Telepad, object>.State idle;

		// Token: 0x04005F4E RID: 24398
		public GameStateMachine<Telepad.States, Telepad.StatesInstance, Telepad, object>.State resetToIdle;

		// Token: 0x04005F4F RID: 24399
		public GameStateMachine<Telepad.States, Telepad.StatesInstance, Telepad, object>.State opening;

		// Token: 0x04005F50 RID: 24400
		public GameStateMachine<Telepad.States, Telepad.StatesInstance, Telepad, object>.State open;

		// Token: 0x04005F51 RID: 24401
		public GameStateMachine<Telepad.States, Telepad.StatesInstance, Telepad, object>.State close;

		// Token: 0x04005F52 RID: 24402
		public GameStateMachine<Telepad.States, Telepad.StatesInstance, Telepad, object>.State unoperational;

		// Token: 0x04005F53 RID: 24403
		private static readonly HashedString[] workingAnims = new HashedString[] { "working_loop", "working_pst" };
	}
}
