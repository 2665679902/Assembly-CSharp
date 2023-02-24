using System;

// Token: 0x020004FD RID: 1277
public class NavTeleportTransitionLayer : TransitionDriver.OverrideLayer
{
	// Token: 0x06001E05 RID: 7685 RVA: 0x0009FF8C File Offset: 0x0009E18C
	public NavTeleportTransitionLayer(Navigator navigator)
		: base(navigator)
	{
	}

	// Token: 0x06001E06 RID: 7686 RVA: 0x0009FF98 File Offset: 0x0009E198
	public override void BeginTransition(Navigator navigator, Navigator.ActiveTransition transition)
	{
		base.BeginTransition(navigator, transition);
		if (transition.start == NavType.Teleport)
		{
			int num = Grid.PosToCell(navigator);
			int num2;
			int num3;
			Grid.CellToXY(num, out num2, out num3);
			int num4 = navigator.NavGrid.teleportTransitions[num];
			int num5;
			int num6;
			Grid.CellToXY(navigator.NavGrid.teleportTransitions[num], out num5, out num6);
			transition.x = num5 - num2;
			transition.y = num6 - num3;
		}
	}
}
