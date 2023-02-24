using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000C26 RID: 3110
[AddComponentMenu("KMonoBehaviour/scripts/BreakdownList")]
public class BreakdownList : KMonoBehaviour
{
	// Token: 0x0600626C RID: 25196 RVA: 0x00245474 File Offset: 0x00243674
	public BreakdownListRow AddRow()
	{
		BreakdownListRow breakdownListRow;
		if (this.unusedListRows.Count > 0)
		{
			breakdownListRow = this.unusedListRows[0];
			this.unusedListRows.RemoveAt(0);
		}
		else
		{
			breakdownListRow = UnityEngine.Object.Instantiate<BreakdownListRow>(this.listRowTemplate);
		}
		breakdownListRow.gameObject.transform.SetParent(base.transform);
		breakdownListRow.gameObject.transform.SetAsLastSibling();
		this.listRows.Add(breakdownListRow);
		breakdownListRow.gameObject.SetActive(true);
		return breakdownListRow;
	}

	// Token: 0x0600626D RID: 25197 RVA: 0x002454F5 File Offset: 0x002436F5
	public GameObject AddCustomRow(GameObject newRow)
	{
		newRow.transform.SetParent(base.transform);
		newRow.gameObject.transform.SetAsLastSibling();
		this.customRows.Add(newRow);
		newRow.SetActive(true);
		return newRow;
	}

	// Token: 0x0600626E RID: 25198 RVA: 0x0024552C File Offset: 0x0024372C
	public void ClearRows()
	{
		foreach (BreakdownListRow breakdownListRow in this.listRows)
		{
			this.unusedListRows.Add(breakdownListRow);
			breakdownListRow.gameObject.SetActive(false);
			breakdownListRow.ClearTooltip();
		}
		this.listRows.Clear();
		foreach (GameObject gameObject in this.customRows)
		{
			gameObject.SetActive(false);
		}
	}

	// Token: 0x0600626F RID: 25199 RVA: 0x002455E4 File Offset: 0x002437E4
	public void SetTitle(string title)
	{
		this.headerTitle.text = title;
	}

	// Token: 0x06006270 RID: 25200 RVA: 0x002455F2 File Offset: 0x002437F2
	public void SetDescription(string description)
	{
		if (description != null && description.Length >= 0)
		{
			this.infoTextLabel.gameObject.SetActive(true);
			this.infoTextLabel.text = description;
			return;
		}
		this.infoTextLabel.gameObject.SetActive(false);
	}

	// Token: 0x06006271 RID: 25201 RVA: 0x0024562F File Offset: 0x0024382F
	public void SetIcon(Sprite icon)
	{
		this.headerIcon.sprite = icon;
	}

	// Token: 0x0400441F RID: 17439
	public Image headerIcon;

	// Token: 0x04004420 RID: 17440
	public Sprite headerIconSprite;

	// Token: 0x04004421 RID: 17441
	public Image headerBar;

	// Token: 0x04004422 RID: 17442
	public LocText headerTitle;

	// Token: 0x04004423 RID: 17443
	public LocText headerValue;

	// Token: 0x04004424 RID: 17444
	public LocText infoTextLabel;

	// Token: 0x04004425 RID: 17445
	public BreakdownListRow listRowTemplate;

	// Token: 0x04004426 RID: 17446
	private List<BreakdownListRow> listRows = new List<BreakdownListRow>();

	// Token: 0x04004427 RID: 17447
	private List<BreakdownListRow> unusedListRows = new List<BreakdownListRow>();

	// Token: 0x04004428 RID: 17448
	private List<GameObject> customRows = new List<GameObject>();
}
