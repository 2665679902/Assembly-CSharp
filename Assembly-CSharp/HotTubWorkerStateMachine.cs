using System;
using UnityEngine;

// Token: 0x020007AF RID: 1967
public class HotTubWorkerStateMachine : GameStateMachine<HotTubWorkerStateMachine, HotTubWorkerStateMachine.StatesInstance, Worker>
{
	// Token: 0x060037BC RID: 14268 RVA: 0x001358AC File Offset: 0x00133AAC
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.pre_front;
		base.Target(this.worker);
		this.root.ToggleAnims("anim_interacts_hottub_kanim", 0f, "");
		this.pre_front.PlayAnim("working_pre_front").OnAnimQueueComplete(this.pre_back);
		this.pre_back.PlayAnim("working_pre_back").Enter(delegate(HotTubWorkerStateMachine.StatesInstance smi)
		{
			Vector3 position = smi.transform.GetPosition();
			position.z = Grid.GetLayerZ(Grid.SceneLayer.BuildingUse);
			smi.transform.SetPosition(position);
		}).OnAnimQueueComplete(this.loop);
		this.loop.PlayAnim((HotTubWorkerStateMachine.StatesInstance smi) => HotTubWorkerStateMachine.workAnimLoopVariants[UnityEngine.Random.Range(0, HotTubWorkerStateMachine.workAnimLoopVariants.Length)], KAnim.PlayMode.Once).OnAnimQueueComplete(this.loop_reenter).EventTransition(GameHashes.WorkerPlayPostAnim, this.pst_back, (HotTubWorkerStateMachine.StatesInstance smi) => smi.GetComponent<Worker>().state == Worker.State.PendingCompletion);
		this.loop_reenter.GoTo(this.loop).EventTransition(GameHashes.WorkerPlayPostAnim, this.pst_back, (HotTubWorkerStateMachine.StatesInstance smi) => smi.GetComponent<Worker>().state == Worker.State.PendingCompletion);
		this.pst_back.PlayAnim("working_pst_back").OnAnimQueueComplete(this.pst_front);
		this.pst_front.PlayAnim("working_pst_front").Enter(delegate(HotTubWorkerStateMachine.StatesInstance smi)
		{
			Vector3 position2 = smi.transform.GetPosition();
			position2.z = Grid.GetLayerZ(Grid.SceneLayer.Move);
			smi.transform.SetPosition(position2);
		}).OnAnimQueueComplete(this.complete);
	}

	// Token: 0x04002559 RID: 9561
	private GameStateMachine<HotTubWorkerStateMachine, HotTubWorkerStateMachine.StatesInstance, Worker, object>.State pre_front;

	// Token: 0x0400255A RID: 9562
	private GameStateMachine<HotTubWorkerStateMachine, HotTubWorkerStateMachine.StatesInstance, Worker, object>.State pre_back;

	// Token: 0x0400255B RID: 9563
	private GameStateMachine<HotTubWorkerStateMachine, HotTubWorkerStateMachine.StatesInstance, Worker, object>.State loop;

	// Token: 0x0400255C RID: 9564
	private GameStateMachine<HotTubWorkerStateMachine, HotTubWorkerStateMachine.StatesInstance, Worker, object>.State loop_reenter;

	// Token: 0x0400255D RID: 9565
	private GameStateMachine<HotTubWorkerStateMachine, HotTubWorkerStateMachine.StatesInstance, Worker, object>.State pst_back;

	// Token: 0x0400255E RID: 9566
	private GameStateMachine<HotTubWorkerStateMachine, HotTubWorkerStateMachine.StatesInstance, Worker, object>.State pst_front;

	// Token: 0x0400255F RID: 9567
	private GameStateMachine<HotTubWorkerStateMachine, HotTubWorkerStateMachine.StatesInstance, Worker, object>.State complete;

	// Token: 0x04002560 RID: 9568
	public StateMachine<HotTubWorkerStateMachine, HotTubWorkerStateMachine.StatesInstance, Worker, object>.TargetParameter worker;

	// Token: 0x04002561 RID: 9569
	public static string[] workAnimLoopVariants = new string[] { "working_loop1", "working_loop2", "working_loop3" };

	// Token: 0x02001516 RID: 5398
	public class StatesInstance : GameStateMachine<HotTubWorkerStateMachine, HotTubWorkerStateMachine.StatesInstance, Worker, object>.GameInstance
	{
		// Token: 0x060082A4 RID: 33444 RVA: 0x002E5BD7 File Offset: 0x002E3DD7
		public StatesInstance(Worker master)
			: base(master)
		{
			base.sm.worker.Set(master, base.smi);
		}
	}
}
