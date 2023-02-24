using System;
using UnityEngine;

// Token: 0x02000C0A RID: 3082
public abstract class TargetScreen : KScreen
{
	// Token: 0x060061AD RID: 25005
	public abstract bool IsValidForTarget(GameObject target);

	// Token: 0x060061AE RID: 25006 RVA: 0x00241244 File Offset: 0x0023F444
	public void SetTarget(GameObject target)
	{
		if (this.selectedTarget != target)
		{
			if (this.selectedTarget != null)
			{
				this.OnDeselectTarget(this.selectedTarget);
			}
			this.selectedTarget = target;
			if (this.selectedTarget != null)
			{
				this.OnSelectTarget(this.selectedTarget);
			}
		}
	}

	// Token: 0x060061AF RID: 25007 RVA: 0x0024129A File Offset: 0x0023F49A
	protected override void OnDeactivate()
	{
		base.OnDeactivate();
		this.SetTarget(null);
	}

	// Token: 0x060061B0 RID: 25008 RVA: 0x002412A9 File Offset: 0x0023F4A9
	public virtual void OnSelectTarget(GameObject target)
	{
		target.Subscribe(1502190696, new Action<object>(this.OnTargetDestroyed));
	}

	// Token: 0x060061B1 RID: 25009 RVA: 0x002412C3 File Offset: 0x0023F4C3
	public virtual void OnDeselectTarget(GameObject target)
	{
		target.Unsubscribe(1502190696, new Action<object>(this.OnTargetDestroyed));
	}

	// Token: 0x060061B2 RID: 25010 RVA: 0x002412DC File Offset: 0x0023F4DC
	private void OnTargetDestroyed(object data)
	{
		DetailsScreen.Instance.Show(false);
		this.SetTarget(null);
	}

	// Token: 0x04004389 RID: 17289
	protected GameObject selectedTarget;
}
