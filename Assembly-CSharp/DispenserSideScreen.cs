using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000BAC RID: 2988
public class DispenserSideScreen : SideScreenContent
{
	// Token: 0x06005E01 RID: 24065 RVA: 0x0022573E File Offset: 0x0022393E
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetComponent<IDispenser>() != null;
	}

	// Token: 0x06005E02 RID: 24066 RVA: 0x00225749 File Offset: 0x00223949
	public override void SetTarget(GameObject target)
	{
		base.SetTarget(target);
		this.targetDispenser = target.GetComponent<IDispenser>();
		this.Refresh();
	}

	// Token: 0x06005E03 RID: 24067 RVA: 0x00225764 File Offset: 0x00223964
	private void Refresh()
	{
		this.dispenseButton.ClearOnClick();
		foreach (KeyValuePair<Tag, GameObject> keyValuePair in this.rows)
		{
			UnityEngine.Object.Destroy(keyValuePair.Value);
		}
		this.rows.Clear();
		foreach (Tag tag in this.targetDispenser.DispensedItems())
		{
			GameObject gameObject = Util.KInstantiateUI(this.itemRowPrefab, this.itemRowContainer.gameObject, true);
			this.rows.Add(tag, gameObject);
			HierarchyReferences component = gameObject.GetComponent<HierarchyReferences>();
			component.GetReference<Image>("Icon").sprite = Def.GetUISprite(tag, "ui", false).first;
			component.GetReference<LocText>("Label").text = Assets.GetPrefab(tag).GetProperName();
			gameObject.GetComponent<MultiToggle>().ChangeState((tag == this.targetDispenser.SelectedItem()) ? 0 : 1);
		}
		if (this.targetDispenser.HasOpenChore())
		{
			this.dispenseButton.onClick += delegate
			{
				this.targetDispenser.OnCancelDispense();
				this.Refresh();
			};
			this.dispenseButton.GetComponentInChildren<LocText>().text = UI.UISIDESCREENS.DISPENSERSIDESCREEN.BUTTON_CANCEL;
		}
		else
		{
			this.dispenseButton.onClick += delegate
			{
				this.targetDispenser.OnOrderDispense();
				this.Refresh();
			};
			this.dispenseButton.GetComponentInChildren<LocText>().text = UI.UISIDESCREENS.DISPENSERSIDESCREEN.BUTTON_DISPENSE;
		}
		this.targetDispenser.OnStopWorkEvent -= this.Refresh;
		this.targetDispenser.OnStopWorkEvent += this.Refresh;
	}

	// Token: 0x06005E04 RID: 24068 RVA: 0x00225948 File Offset: 0x00223B48
	private void SelectTag(Tag tag)
	{
		this.targetDispenser.SelectItem(tag);
		this.Refresh();
	}

	// Token: 0x04004049 RID: 16457
	[SerializeField]
	private KButton dispenseButton;

	// Token: 0x0400404A RID: 16458
	[SerializeField]
	private RectTransform itemRowContainer;

	// Token: 0x0400404B RID: 16459
	[SerializeField]
	private GameObject itemRowPrefab;

	// Token: 0x0400404C RID: 16460
	private IDispenser targetDispenser;

	// Token: 0x0400404D RID: 16461
	private Dictionary<Tag, GameObject> rows = new Dictionary<Tag, GameObject>();
}
