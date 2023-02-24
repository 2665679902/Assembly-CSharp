using System;
using UnityEngine;

// Token: 0x02000C33 RID: 3123
[AddComponentMenu("KMonoBehaviour/scripts/SimpleUIShowHide")]
public class SimpleUIShowHide : KMonoBehaviour
{
	// Token: 0x060062C5 RID: 25285 RVA: 0x002478A5 File Offset: 0x00245AA5
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		MultiToggle multiToggle = this.toggle;
		multiToggle.onClick = (System.Action)Delegate.Combine(multiToggle.onClick, new System.Action(this.OnClick));
	}

	// Token: 0x060062C6 RID: 25286 RVA: 0x002478D4 File Offset: 0x00245AD4
	private void OnClick()
	{
		this.toggle.NextState();
		this.content.SetActive(this.toggle.CurrentState == 0);
	}

	// Token: 0x0400449B RID: 17563
	[MyCmpReq]
	private MultiToggle toggle;

	// Token: 0x0400449C RID: 17564
	[SerializeField]
	public GameObject content;
}
