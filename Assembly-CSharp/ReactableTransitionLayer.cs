using System;

// Token: 0x020004FC RID: 1276
public class ReactableTransitionLayer : TransitionDriver.InterruptOverrideLayer
{
	// Token: 0x06001E02 RID: 7682 RVA: 0x0009FF05 File Offset: 0x0009E105
	public ReactableTransitionLayer(Navigator navigator)
		: base(navigator)
	{
	}

	// Token: 0x06001E03 RID: 7683 RVA: 0x0009FF0E File Offset: 0x0009E10E
	protected override bool IsOverrideComplete()
	{
		return !this.reactionMonitor.IsReacting() && base.IsOverrideComplete();
	}

	// Token: 0x06001E04 RID: 7684 RVA: 0x0009FF28 File Offset: 0x0009E128
	public override void BeginTransition(Navigator navigator, Navigator.ActiveTransition transition)
	{
		if (this.reactionMonitor == null)
		{
			this.reactionMonitor = navigator.GetSMI<ReactionMonitor.Instance>();
		}
		this.reactionMonitor.PollForReactables(transition);
		if (this.reactionMonitor.IsReacting())
		{
			base.BeginTransition(navigator, transition);
			transition.start = this.originalTransition.start;
			transition.end = this.originalTransition.end;
		}
	}

	// Token: 0x040010CF RID: 4303
	private ReactionMonitor.Instance reactionMonitor;
}
