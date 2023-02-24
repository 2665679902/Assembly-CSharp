using System;

// Token: 0x020005D8 RID: 1496
public class LandingBeacon : GameStateMachine<LandingBeacon, LandingBeacon.Instance>
{
	// Token: 0x06002556 RID: 9558 RVA: 0x000C9C40 File Offset: 0x000C7E40
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.off;
		this.root.Update(new Action<LandingBeacon.Instance, float>(LandingBeacon.UpdateLineOfSight), UpdateRate.SIM_200ms, false);
		this.off.PlayAnim("off").EventTransition(GameHashes.OperationalChanged, this.working, (LandingBeacon.Instance smi) => smi.operational.IsOperational);
		this.working.DefaultState(this.working.pre).EventTransition(GameHashes.OperationalChanged, this.off, (LandingBeacon.Instance smi) => !smi.operational.IsOperational);
		this.working.pre.PlayAnim("working_pre").OnAnimQueueComplete(this.working.loop);
		this.working.loop.PlayAnim("working_loop", KAnim.PlayMode.Loop).Enter("SetActive", delegate(LandingBeacon.Instance smi)
		{
			smi.operational.SetActive(true, false);
		}).Exit("SetActive", delegate(LandingBeacon.Instance smi)
		{
			smi.operational.SetActive(false, false);
		});
	}

	// Token: 0x06002557 RID: 9559 RVA: 0x000C9D84 File Offset: 0x000C7F84
	public static void UpdateLineOfSight(LandingBeacon.Instance smi, float dt)
	{
		WorldContainer myWorld = smi.GetMyWorld();
		bool flag = true;
		int num = Grid.PosToCell(smi);
		int num2 = (int)myWorld.maximumBounds.y;
		while (Grid.CellRow(num) <= num2)
		{
			if (!Grid.IsValidCell(num) || Grid.Solid[num])
			{
				flag = false;
				break;
			}
			num = Grid.CellAbove(num);
		}
		if (smi.skyLastVisible != flag)
		{
			smi.selectable.ToggleStatusItem(Db.Get().BuildingStatusItems.NoSurfaceSight, !flag, null);
			smi.operational.SetFlag(LandingBeacon.noSurfaceSight, flag);
			smi.skyLastVisible = flag;
		}
	}

	// Token: 0x040015B3 RID: 5555
	public GameStateMachine<LandingBeacon, LandingBeacon.Instance, IStateMachineTarget, object>.State off;

	// Token: 0x040015B4 RID: 5556
	public LandingBeacon.WorkingStates working;

	// Token: 0x040015B5 RID: 5557
	public static readonly Operational.Flag noSurfaceSight = new Operational.Flag("noSurfaceSight", Operational.Flag.Type.Requirement);

	// Token: 0x02001231 RID: 4657
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02001232 RID: 4658
	public class WorkingStates : GameStateMachine<LandingBeacon, LandingBeacon.Instance, IStateMachineTarget, object>.State
	{
		// Token: 0x04005D40 RID: 23872
		public GameStateMachine<LandingBeacon, LandingBeacon.Instance, IStateMachineTarget, object>.State pre;

		// Token: 0x04005D41 RID: 23873
		public GameStateMachine<LandingBeacon, LandingBeacon.Instance, IStateMachineTarget, object>.State loop;

		// Token: 0x04005D42 RID: 23874
		public GameStateMachine<LandingBeacon, LandingBeacon.Instance, IStateMachineTarget, object>.State pst;
	}

	// Token: 0x02001233 RID: 4659
	public new class Instance : GameStateMachine<LandingBeacon, LandingBeacon.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x0600795B RID: 31067 RVA: 0x002C353C File Offset: 0x002C173C
		public Instance(IStateMachineTarget master, LandingBeacon.Def def)
			: base(master, def)
		{
			Components.LandingBeacons.Add(this);
			this.operational = base.GetComponent<Operational>();
			this.selectable = base.GetComponent<KSelectable>();
		}

		// Token: 0x0600795C RID: 31068 RVA: 0x002C3570 File Offset: 0x002C1770
		public override void StartSM()
		{
			base.StartSM();
			LandingBeacon.UpdateLineOfSight(this, 0f);
		}

		// Token: 0x0600795D RID: 31069 RVA: 0x002C3583 File Offset: 0x002C1783
		protected override void OnCleanUp()
		{
			base.OnCleanUp();
			Components.LandingBeacons.Remove(this);
		}

		// Token: 0x0600795E RID: 31070 RVA: 0x002C3596 File Offset: 0x002C1796
		public bool CanBeTargeted()
		{
			return base.IsInsideState(base.sm.working);
		}

		// Token: 0x04005D43 RID: 23875
		public Operational operational;

		// Token: 0x04005D44 RID: 23876
		public KSelectable selectable;

		// Token: 0x04005D45 RID: 23877
		public bool skyLastVisible = true;
	}
}
