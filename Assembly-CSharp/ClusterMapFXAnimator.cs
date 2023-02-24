using System;

// Token: 0x0200096F RID: 2415
public class ClusterMapFXAnimator : GameStateMachine<ClusterMapFXAnimator, ClusterMapFXAnimator.StatesInstance, ClusterMapVisualizer>
{
	// Token: 0x060047B0 RID: 18352 RVA: 0x0019343C File Offset: 0x0019163C
	public override void InitializeStates(out StateMachine.BaseState defaultState)
	{
		defaultState = this.play;
		this.play.OnSignal(this.onAnimComplete, this.finished);
		this.finished.Enter(delegate(ClusterMapFXAnimator.StatesInstance smi)
		{
			smi.DestroyEntity();
		});
	}

	// Token: 0x04002F55 RID: 12117
	private KBatchedAnimController animController;

	// Token: 0x04002F56 RID: 12118
	public StateMachine<ClusterMapFXAnimator, ClusterMapFXAnimator.StatesInstance, ClusterMapVisualizer, object>.TargetParameter entityTarget;

	// Token: 0x04002F57 RID: 12119
	public GameStateMachine<ClusterMapFXAnimator, ClusterMapFXAnimator.StatesInstance, ClusterMapVisualizer, object>.State play;

	// Token: 0x04002F58 RID: 12120
	public GameStateMachine<ClusterMapFXAnimator, ClusterMapFXAnimator.StatesInstance, ClusterMapVisualizer, object>.State finished;

	// Token: 0x04002F59 RID: 12121
	public StateMachine<ClusterMapFXAnimator, ClusterMapFXAnimator.StatesInstance, ClusterMapVisualizer, object>.Signal onAnimComplete;

	// Token: 0x02001775 RID: 6005
	public class StatesInstance : GameStateMachine<ClusterMapFXAnimator, ClusterMapFXAnimator.StatesInstance, ClusterMapVisualizer, object>.GameInstance
	{
		// Token: 0x06008AFE RID: 35582 RVA: 0x002FE744 File Offset: 0x002FC944
		public StatesInstance(ClusterMapVisualizer visualizer, ClusterGridEntity entity)
			: base(visualizer)
		{
			base.sm.entityTarget.Set(entity, this);
			visualizer.GetFirstAnimController().gameObject.Subscribe(-1061186183, new Action<object>(this.OnAnimQueueComplete));
		}

		// Token: 0x06008AFF RID: 35583 RVA: 0x002FE781 File Offset: 0x002FC981
		private void OnAnimQueueComplete(object data)
		{
			base.sm.onAnimComplete.Trigger(this);
		}

		// Token: 0x06008B00 RID: 35584 RVA: 0x002FE794 File Offset: 0x002FC994
		public void DestroyEntity()
		{
			base.sm.entityTarget.Get(this).DeleteObject();
		}
	}
}
