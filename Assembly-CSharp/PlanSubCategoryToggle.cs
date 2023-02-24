using System;
using UnityEngine;

// Token: 0x02000B5B RID: 2907
public class PlanSubCategoryToggle : KMonoBehaviour
{
	// Token: 0x06005AA8 RID: 23208 RVA: 0x0020E970 File Offset: 0x0020CB70
	protected override void OnSpawn()
	{
		base.OnSpawn();
		MultiToggle multiToggle = this.toggle;
		multiToggle.onClick = (System.Action)Delegate.Combine(multiToggle.onClick, new System.Action(delegate
		{
			this.open = !this.open;
			this.gridContainer.SetActive(this.open);
			this.toggle.ChangeState(this.open ? 0 : 1);
		}));
	}

	// Token: 0x04003D58 RID: 15704
	[SerializeField]
	private MultiToggle toggle;

	// Token: 0x04003D59 RID: 15705
	[SerializeField]
	private GameObject gridContainer;

	// Token: 0x04003D5A RID: 15706
	private bool open = true;
}
