using System;
using UnityEngine;

// Token: 0x020009C9 RID: 2505
public class WindTunnelWorkerStateMachine : GameStateMachine<WindTunnelWorkerStateMachine, WindTunnelWorkerStateMachine.StatesInstance, Worker>
{
	// Token: 0x06004A85 RID: 19077 RVA: 0x001A16B4 File Offset: 0x0019F8B4
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.pre_front;
		base.Target(this.worker);
		this.root.ToggleAnims((WindTunnelWorkerStateMachine.StatesInstance smi) => smi.OverrideAnim);
		this.pre_front.PlayAnim((WindTunnelWorkerStateMachine.StatesInstance smi) => smi.PreFrontAnim, KAnim.PlayMode.Once).OnAnimQueueComplete(this.pre_back);
		this.pre_back.PlayAnim((WindTunnelWorkerStateMachine.StatesInstance smi) => smi.PreBackAnim, KAnim.PlayMode.Once).Enter(delegate(WindTunnelWorkerStateMachine.StatesInstance smi)
		{
			Vector3 position = smi.transform.GetPosition();
			position.z = Grid.GetLayerZ(Grid.SceneLayer.BuildingUse);
			smi.transform.SetPosition(position);
		}).OnAnimQueueComplete(this.loop);
		this.loop.PlayAnim((WindTunnelWorkerStateMachine.StatesInstance smi) => smi.LoopAnim, KAnim.PlayMode.Loop).EventTransition(GameHashes.WorkerPlayPostAnim, this.pst_back, (WindTunnelWorkerStateMachine.StatesInstance smi) => smi.GetComponent<Worker>().state == Worker.State.PendingCompletion);
		this.pst_back.PlayAnim((WindTunnelWorkerStateMachine.StatesInstance smi) => smi.PstBackAnim, KAnim.PlayMode.Once).OnAnimQueueComplete(this.pst_front);
		this.pst_front.PlayAnim((WindTunnelWorkerStateMachine.StatesInstance smi) => smi.PstFrontAnim, KAnim.PlayMode.Once).Enter(delegate(WindTunnelWorkerStateMachine.StatesInstance smi)
		{
			Vector3 position2 = smi.transform.GetPosition();
			position2.z = Grid.GetLayerZ(Grid.SceneLayer.Move);
			smi.transform.SetPosition(position2);
		}).OnAnimQueueComplete(this.complete);
	}

	// Token: 0x040030E9 RID: 12521
	private GameStateMachine<WindTunnelWorkerStateMachine, WindTunnelWorkerStateMachine.StatesInstance, Worker, object>.State pre_front;

	// Token: 0x040030EA RID: 12522
	private GameStateMachine<WindTunnelWorkerStateMachine, WindTunnelWorkerStateMachine.StatesInstance, Worker, object>.State pre_back;

	// Token: 0x040030EB RID: 12523
	private GameStateMachine<WindTunnelWorkerStateMachine, WindTunnelWorkerStateMachine.StatesInstance, Worker, object>.State loop;

	// Token: 0x040030EC RID: 12524
	private GameStateMachine<WindTunnelWorkerStateMachine, WindTunnelWorkerStateMachine.StatesInstance, Worker, object>.State pst_back;

	// Token: 0x040030ED RID: 12525
	private GameStateMachine<WindTunnelWorkerStateMachine, WindTunnelWorkerStateMachine.StatesInstance, Worker, object>.State pst_front;

	// Token: 0x040030EE RID: 12526
	private GameStateMachine<WindTunnelWorkerStateMachine, WindTunnelWorkerStateMachine.StatesInstance, Worker, object>.State complete;

	// Token: 0x040030EF RID: 12527
	public StateMachine<WindTunnelWorkerStateMachine, WindTunnelWorkerStateMachine.StatesInstance, Worker, object>.TargetParameter worker;

	// Token: 0x020017CA RID: 6090
	public class StatesInstance : GameStateMachine<WindTunnelWorkerStateMachine, WindTunnelWorkerStateMachine.StatesInstance, Worker, object>.GameInstance
	{
		// Token: 0x06008BE0 RID: 35808 RVA: 0x003009A3 File Offset: 0x002FEBA3
		public StatesInstance(Worker master, VerticalWindTunnelWorkable workable)
			: base(master)
		{
			this.workable = workable;
			base.sm.worker.Set(master, base.smi);
		}

		// Token: 0x17000944 RID: 2372
		// (get) Token: 0x06008BE1 RID: 35809 RVA: 0x003009CA File Offset: 0x002FEBCA
		public HashedString OverrideAnim
		{
			get
			{
				return this.workable.overrideAnim;
			}
		}

		// Token: 0x17000945 RID: 2373
		// (get) Token: 0x06008BE2 RID: 35810 RVA: 0x003009D7 File Offset: 0x002FEBD7
		public string PreFrontAnim
		{
			get
			{
				return this.workable.preAnims[0];
			}
		}

		// Token: 0x17000946 RID: 2374
		// (get) Token: 0x06008BE3 RID: 35811 RVA: 0x003009E6 File Offset: 0x002FEBE6
		public string PreBackAnim
		{
			get
			{
				return this.workable.preAnims[1];
			}
		}

		// Token: 0x17000947 RID: 2375
		// (get) Token: 0x06008BE4 RID: 35812 RVA: 0x003009F5 File Offset: 0x002FEBF5
		public string LoopAnim
		{
			get
			{
				return this.workable.loopAnim;
			}
		}

		// Token: 0x17000948 RID: 2376
		// (get) Token: 0x06008BE5 RID: 35813 RVA: 0x00300A02 File Offset: 0x002FEC02
		public string PstBackAnim
		{
			get
			{
				return this.workable.pstAnims[0];
			}
		}

		// Token: 0x17000949 RID: 2377
		// (get) Token: 0x06008BE6 RID: 35814 RVA: 0x00300A11 File Offset: 0x002FEC11
		public string PstFrontAnim
		{
			get
			{
				return this.workable.pstAnims[1];
			}
		}

		// Token: 0x04006E04 RID: 28164
		private VerticalWindTunnelWorkable workable;
	}
}
