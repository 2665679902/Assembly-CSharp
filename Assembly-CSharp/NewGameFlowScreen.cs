using System;

// Token: 0x02000B36 RID: 2870
public abstract class NewGameFlowScreen : KModalScreen
{
	// Token: 0x14000026 RID: 38
	// (add) Token: 0x060058E4 RID: 22756 RVA: 0x002038EC File Offset: 0x00201AEC
	// (remove) Token: 0x060058E5 RID: 22757 RVA: 0x00203924 File Offset: 0x00201B24
	public event System.Action OnNavigateForward;

	// Token: 0x14000027 RID: 39
	// (add) Token: 0x060058E6 RID: 22758 RVA: 0x0020395C File Offset: 0x00201B5C
	// (remove) Token: 0x060058E7 RID: 22759 RVA: 0x00203994 File Offset: 0x00201B94
	public event System.Action OnNavigateBackward;

	// Token: 0x060058E8 RID: 22760 RVA: 0x002039C9 File Offset: 0x00201BC9
	protected void NavigateBackward()
	{
		this.OnNavigateBackward();
	}

	// Token: 0x060058E9 RID: 22761 RVA: 0x002039D6 File Offset: 0x00201BD6
	protected void NavigateForward()
	{
		this.OnNavigateForward();
	}

	// Token: 0x060058EA RID: 22762 RVA: 0x002039E3 File Offset: 0x00201BE3
	public override void OnKeyDown(KButtonEvent e)
	{
		if (e.Consumed)
		{
			return;
		}
		if (e.TryConsume(global::Action.MouseRight))
		{
			this.NavigateBackward();
		}
		base.OnKeyDown(e);
	}
}
