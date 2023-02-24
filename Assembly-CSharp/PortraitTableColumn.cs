using System;
using UnityEngine;

// Token: 0x02000AFE RID: 2814
public class PortraitTableColumn : TableColumn
{
	// Token: 0x0600565C RID: 22108 RVA: 0x001F451C File Offset: 0x001F271C
	public PortraitTableColumn(Action<IAssignableIdentity, GameObject> on_load_action, Comparison<IAssignableIdentity> sort_comparison, bool double_click_to_target = true)
		: base(on_load_action, sort_comparison, null, null, null, false, "")
	{
		this.double_click_to_target = double_click_to_target;
	}

	// Token: 0x0600565D RID: 22109 RVA: 0x001F454B File Offset: 0x001F274B
	public override GameObject GetDefaultWidget(GameObject parent)
	{
		GameObject gameObject = Util.KInstantiateUI(this.prefab_portrait, parent, true);
		gameObject.GetComponent<CrewPortrait>().targetImage.enabled = true;
		return gameObject;
	}

	// Token: 0x0600565E RID: 22110 RVA: 0x001F456B File Offset: 0x001F276B
	public override GameObject GetHeaderWidget(GameObject parent)
	{
		return Util.KInstantiateUI(this.prefab_portrait, parent, true);
	}

	// Token: 0x0600565F RID: 22111 RVA: 0x001F457C File Offset: 0x001F277C
	public override GameObject GetMinionWidget(GameObject parent)
	{
		GameObject gameObject = Util.KInstantiateUI(this.prefab_portrait, parent, true);
		if (this.double_click_to_target)
		{
			gameObject.GetComponent<KButton>().onClick += delegate
			{
				parent.GetComponent<TableRow>().SelectMinion();
			};
			gameObject.GetComponent<KButton>().onDoubleClick += delegate
			{
				parent.GetComponent<TableRow>().SelectAndFocusMinion();
			};
		}
		return gameObject;
	}

	// Token: 0x04003ACC RID: 15052
	public GameObject prefab_portrait = Assets.UIPrefabs.TableScreenWidgets.MinionPortrait;

	// Token: 0x04003ACD RID: 15053
	private bool double_click_to_target;
}
