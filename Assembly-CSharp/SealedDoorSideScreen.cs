using System;
using UnityEngine;

// Token: 0x02000BDA RID: 3034
public class SealedDoorSideScreen : SideScreenContent
{
	// Token: 0x06005F86 RID: 24454 RVA: 0x0022F048 File Offset: 0x0022D248
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.button.onClick += delegate
		{
			this.target.OrderUnseal();
		};
		this.Refresh();
	}

	// Token: 0x06005F87 RID: 24455 RVA: 0x0022F06D File Offset: 0x0022D26D
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetComponent<Door>() != null;
	}

	// Token: 0x06005F88 RID: 24456 RVA: 0x0022F07C File Offset: 0x0022D27C
	public override void SetTarget(GameObject target)
	{
		Door component = target.GetComponent<Door>();
		if (component == null)
		{
			global::Debug.LogError("Target doesn't have a Door associated with it.");
			return;
		}
		this.target = component;
		this.Refresh();
	}

	// Token: 0x06005F89 RID: 24457 RVA: 0x0022F0B1 File Offset: 0x0022D2B1
	private void Refresh()
	{
		if (!this.target.isSealed)
		{
			this.ContentContainer.SetActive(false);
			return;
		}
		this.ContentContainer.SetActive(true);
	}

	// Token: 0x0400416E RID: 16750
	[SerializeField]
	private LocText label;

	// Token: 0x0400416F RID: 16751
	[SerializeField]
	private KButton button;

	// Token: 0x04004170 RID: 16752
	[SerializeField]
	private Door target;
}
