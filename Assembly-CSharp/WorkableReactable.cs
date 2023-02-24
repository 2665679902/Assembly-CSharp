using System;
using UnityEngine;

// Token: 0x020003E0 RID: 992
public class WorkableReactable : Reactable
{
	// Token: 0x06001496 RID: 5270 RVA: 0x0006C92C File Offset: 0x0006AB2C
	public WorkableReactable(Workable workable, HashedString id, ChoreType chore_type, WorkableReactable.AllowedDirection allowed_direction = WorkableReactable.AllowedDirection.Any)
		: base(workable.gameObject, id, chore_type, 1, 1, false, 0f, 0f, float.PositiveInfinity, 0f, ObjectLayer.NumLayers)
	{
		this.workable = workable;
		this.allowedDirection = allowed_direction;
	}

	// Token: 0x06001497 RID: 5271 RVA: 0x0006C970 File Offset: 0x0006AB70
	public override bool InternalCanBegin(GameObject new_reactor, Navigator.ActiveTransition transition)
	{
		if (this.workable == null)
		{
			return false;
		}
		if (this.reactor != null)
		{
			return false;
		}
		Brain component = new_reactor.GetComponent<Brain>();
		if (component == null)
		{
			return false;
		}
		if (!component.IsRunning())
		{
			return false;
		}
		Navigator component2 = new_reactor.GetComponent<Navigator>();
		if (component2 == null)
		{
			return false;
		}
		if (!component2.IsMoving())
		{
			return false;
		}
		if (this.allowedDirection == WorkableReactable.AllowedDirection.Any)
		{
			return true;
		}
		Facing component3 = new_reactor.GetComponent<Facing>();
		if (component3 == null)
		{
			return false;
		}
		bool facing = component3.GetFacing();
		return (!facing || this.allowedDirection != WorkableReactable.AllowedDirection.Right) && (facing || this.allowedDirection != WorkableReactable.AllowedDirection.Left);
	}

	// Token: 0x06001498 RID: 5272 RVA: 0x0006CA15 File Offset: 0x0006AC15
	protected override void InternalBegin()
	{
		this.worker = this.reactor.GetComponent<Worker>();
		this.worker.StartWork(new Worker.StartWorkInfo(this.workable));
	}

	// Token: 0x06001499 RID: 5273 RVA: 0x0006CA3E File Offset: 0x0006AC3E
	public override void Update(float dt)
	{
		if (this.worker.workable == null)
		{
			base.End();
			return;
		}
		if (this.worker.Work(dt) != Worker.WorkResult.InProgress)
		{
			base.End();
		}
	}

	// Token: 0x0600149A RID: 5274 RVA: 0x0006CA6F File Offset: 0x0006AC6F
	protected override void InternalEnd()
	{
		if (this.worker != null)
		{
			this.worker.StopWork();
		}
	}

	// Token: 0x0600149B RID: 5275 RVA: 0x0006CA8A File Offset: 0x0006AC8A
	protected override void InternalCleanup()
	{
	}

	// Token: 0x04000B90 RID: 2960
	protected Workable workable;

	// Token: 0x04000B91 RID: 2961
	private Worker worker;

	// Token: 0x04000B92 RID: 2962
	public WorkableReactable.AllowedDirection allowedDirection;

	// Token: 0x02000FFF RID: 4095
	public enum AllowedDirection
	{
		// Token: 0x0400562F RID: 22063
		Any,
		// Token: 0x04005630 RID: 22064
		Left,
		// Token: 0x04005631 RID: 22065
		Right
	}
}
