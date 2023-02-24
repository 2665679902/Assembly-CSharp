using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004F9 RID: 1273
public class DoorTransitionLayer : TransitionDriver.InterruptOverrideLayer
{
	// Token: 0x06001DF5 RID: 7669 RVA: 0x0009F9E3 File Offset: 0x0009DBE3
	public DoorTransitionLayer(Navigator navigator)
		: base(navigator)
	{
	}

	// Token: 0x06001DF6 RID: 7670 RVA: 0x0009F9F8 File Offset: 0x0009DBF8
	private bool AreAllDoorsOpen()
	{
		foreach (INavDoor navDoor in this.doors)
		{
			if (navDoor != null && !navDoor.IsOpen())
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06001DF7 RID: 7671 RVA: 0x0009FA58 File Offset: 0x0009DC58
	protected override bool IsOverrideComplete()
	{
		return base.IsOverrideComplete() && this.AreAllDoorsOpen();
	}

	// Token: 0x06001DF8 RID: 7672 RVA: 0x0009FA6C File Offset: 0x0009DC6C
	public override void BeginTransition(Navigator navigator, Navigator.ActiveTransition transition)
	{
		if (this.doors.Count > 0)
		{
			return;
		}
		int num = Grid.PosToCell(navigator);
		int num2 = Grid.OffsetCell(num, transition.x, transition.y);
		this.AddDoor(num2);
		if (navigator.CurrentNavType != NavType.Tube)
		{
			this.AddDoor(Grid.CellAbove(num2));
		}
		for (int i = 0; i < transition.navGridTransition.voidOffsets.Length; i++)
		{
			int num3 = Grid.OffsetCell(num, transition.navGridTransition.voidOffsets[i]);
			this.AddDoor(num3);
		}
		if (this.doors.Count == 0)
		{
			return;
		}
		if (!this.AreAllDoorsOpen())
		{
			base.BeginTransition(navigator, transition);
			transition.anim = navigator.NavGrid.GetIdleAnim(navigator.CurrentNavType);
			transition.start = this.originalTransition.start;
			transition.end = this.originalTransition.start;
		}
		foreach (INavDoor navDoor in this.doors)
		{
			navDoor.Open();
		}
	}

	// Token: 0x06001DF9 RID: 7673 RVA: 0x0009FB90 File Offset: 0x0009DD90
	public override void EndTransition(Navigator navigator, Navigator.ActiveTransition transition)
	{
		base.EndTransition(navigator, transition);
		if (this.doors.Count == 0)
		{
			return;
		}
		foreach (INavDoor navDoor in this.doors)
		{
			if (!navDoor.IsNullOrDestroyed())
			{
				navDoor.Close();
			}
		}
		this.doors.Clear();
	}

	// Token: 0x06001DFA RID: 7674 RVA: 0x0009FC0C File Offset: 0x0009DE0C
	private void AddDoor(int cell)
	{
		INavDoor door = this.GetDoor(cell);
		if (!door.IsNullOrDestroyed() && !this.doors.Contains(door))
		{
			this.doors.Add(door);
		}
	}

	// Token: 0x06001DFB RID: 7675 RVA: 0x0009FC44 File Offset: 0x0009DE44
	private INavDoor GetDoor(int cell)
	{
		if (!Grid.HasDoor[cell])
		{
			return null;
		}
		GameObject gameObject = Grid.Objects[cell, 1];
		if (gameObject != null)
		{
			INavDoor navDoor = gameObject.GetComponent<INavDoor>();
			if (navDoor == null)
			{
				navDoor = gameObject.GetSMI<INavDoor>();
			}
			if (navDoor != null && navDoor.isSpawned)
			{
				return navDoor;
			}
		}
		return null;
	}

	// Token: 0x040010CC RID: 4300
	private List<INavDoor> doors = new List<INavDoor>();
}
