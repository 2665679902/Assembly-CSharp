using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000B67 RID: 2919
[AddComponentMenu("KMonoBehaviour/scripts/ReportScreenEntry")]
public class ReportScreenEntry : KMonoBehaviour
{
	// Token: 0x06005B27 RID: 23335 RVA: 0x002115E8 File Offset: 0x0020F7E8
	public void SetMainEntry(ReportManager.ReportEntry entry, ReportManager.ReportGroup reportGroup)
	{
		if (this.mainRow == null)
		{
			this.mainRow = Util.KInstantiateUI(this.rowTemplate.gameObject, base.gameObject, true).GetComponent<ReportScreenEntryRow>();
			MultiToggle toggle = this.mainRow.toggle;
			toggle.onClick = (System.Action)Delegate.Combine(toggle.onClick, new System.Action(this.ToggleContext));
			MultiToggle componentInChildren = this.mainRow.name.GetComponentInChildren<MultiToggle>();
			componentInChildren.onClick = (System.Action)Delegate.Combine(componentInChildren.onClick, new System.Action(this.ToggleContext));
			MultiToggle componentInChildren2 = this.mainRow.added.GetComponentInChildren<MultiToggle>();
			componentInChildren2.onClick = (System.Action)Delegate.Combine(componentInChildren2.onClick, new System.Action(this.ToggleContext));
			MultiToggle componentInChildren3 = this.mainRow.removed.GetComponentInChildren<MultiToggle>();
			componentInChildren3.onClick = (System.Action)Delegate.Combine(componentInChildren3.onClick, new System.Action(this.ToggleContext));
			MultiToggle componentInChildren4 = this.mainRow.net.GetComponentInChildren<MultiToggle>();
			componentInChildren4.onClick = (System.Action)Delegate.Combine(componentInChildren4.onClick, new System.Action(this.ToggleContext));
		}
		this.mainRow.SetLine(entry, reportGroup);
		this.currentContextCount = entry.contextEntries.Count;
		for (int i = 0; i < entry.contextEntries.Count; i++)
		{
			if (i >= this.contextRows.Count)
			{
				ReportScreenEntryRow component = Util.KInstantiateUI(this.rowTemplate.gameObject, base.gameObject, false).GetComponent<ReportScreenEntryRow>();
				this.contextRows.Add(component);
			}
			this.contextRows[i].SetLine(entry.contextEntries[i], reportGroup);
		}
		this.UpdateVisibility();
	}

	// Token: 0x06005B28 RID: 23336 RVA: 0x002117A7 File Offset: 0x0020F9A7
	private void ToggleContext()
	{
		this.mainRow.toggle.NextState();
		this.UpdateVisibility();
	}

	// Token: 0x06005B29 RID: 23337 RVA: 0x002117C0 File Offset: 0x0020F9C0
	private void UpdateVisibility()
	{
		int i;
		for (i = 0; i < this.currentContextCount; i++)
		{
			this.contextRows[i].gameObject.SetActive(this.mainRow.toggle.CurrentState == 1);
		}
		while (i < this.contextRows.Count)
		{
			this.contextRows[i].gameObject.SetActive(false);
			i++;
		}
	}

	// Token: 0x04003DCD RID: 15821
	[SerializeField]
	private ReportScreenEntryRow rowTemplate;

	// Token: 0x04003DCE RID: 15822
	private ReportScreenEntryRow mainRow;

	// Token: 0x04003DCF RID: 15823
	private List<ReportScreenEntryRow> contextRows = new List<ReportScreenEntryRow>();

	// Token: 0x04003DD0 RID: 15824
	private int currentContextCount;
}
