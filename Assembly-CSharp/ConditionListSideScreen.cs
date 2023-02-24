using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000BA4 RID: 2980
public class ConditionListSideScreen : SideScreenContent
{
	// Token: 0x06005DCB RID: 24011 RVA: 0x00224CEC File Offset: 0x00222EEC
	public override bool IsValidForTarget(GameObject target)
	{
		return false;
	}

	// Token: 0x06005DCC RID: 24012 RVA: 0x00224CEF File Offset: 0x00222EEF
	public override void SetTarget(GameObject target)
	{
		base.SetTarget(target);
		if (target != null)
		{
			this.targetConditionSet = target.GetComponent<IProcessConditionSet>();
		}
	}

	// Token: 0x06005DCD RID: 24013 RVA: 0x00224D0D File Offset: 0x00222F0D
	protected override void OnShow(bool show)
	{
		base.OnShow(show);
		if (show)
		{
			this.Refresh();
		}
	}

	// Token: 0x06005DCE RID: 24014 RVA: 0x00224D20 File Offset: 0x00222F20
	private void Refresh()
	{
		bool flag = false;
		List<ProcessCondition> conditionSet = this.targetConditionSet.GetConditionSet(ProcessCondition.ProcessConditionType.All);
		foreach (ProcessCondition processCondition in conditionSet)
		{
			if (!this.rows.ContainsKey(processCondition))
			{
				flag = true;
				break;
			}
		}
		foreach (KeyValuePair<ProcessCondition, GameObject> keyValuePair in this.rows)
		{
			if (!conditionSet.Contains(keyValuePair.Key))
			{
				flag = true;
				break;
			}
		}
		if (flag)
		{
			this.Rebuild();
		}
		foreach (KeyValuePair<ProcessCondition, GameObject> keyValuePair2 in this.rows)
		{
			ConditionListSideScreen.SetRowState(keyValuePair2.Value, keyValuePair2.Key);
		}
	}

	// Token: 0x06005DCF RID: 24015 RVA: 0x00224E34 File Offset: 0x00223034
	public static void SetRowState(GameObject row, ProcessCondition condition)
	{
		HierarchyReferences component = row.GetComponent<HierarchyReferences>();
		ProcessCondition.Status status = condition.EvaluateCondition();
		component.GetReference<LocText>("Label").text = condition.GetStatusMessage(status);
		switch (status)
		{
		case ProcessCondition.Status.Failure:
			component.GetReference<LocText>("Label").color = ConditionListSideScreen.failedColor;
			component.GetReference<Image>("Box").color = ConditionListSideScreen.failedColor;
			break;
		case ProcessCondition.Status.Warning:
			component.GetReference<LocText>("Label").color = ConditionListSideScreen.warningColor;
			component.GetReference<Image>("Box").color = ConditionListSideScreen.warningColor;
			break;
		case ProcessCondition.Status.Ready:
			component.GetReference<LocText>("Label").color = ConditionListSideScreen.readyColor;
			component.GetReference<Image>("Box").color = ConditionListSideScreen.readyColor;
			break;
		}
		component.GetReference<Image>("Check").gameObject.SetActive(status == ProcessCondition.Status.Ready);
		component.GetReference<Image>("Dash").gameObject.SetActive(false);
		row.GetComponent<ToolTip>().SetSimpleTooltip(condition.GetStatusTooltip(status));
	}

	// Token: 0x06005DD0 RID: 24016 RVA: 0x00224F40 File Offset: 0x00223140
	private void Rebuild()
	{
		this.ClearRows();
		this.BuildRows();
	}

	// Token: 0x06005DD1 RID: 24017 RVA: 0x00224F50 File Offset: 0x00223150
	private void ClearRows()
	{
		foreach (KeyValuePair<ProcessCondition, GameObject> keyValuePair in this.rows)
		{
			Util.KDestroyGameObject(keyValuePair.Value);
		}
		this.rows.Clear();
	}

	// Token: 0x06005DD2 RID: 24018 RVA: 0x00224FB4 File Offset: 0x002231B4
	private void BuildRows()
	{
		foreach (ProcessCondition processCondition in this.targetConditionSet.GetConditionSet(ProcessCondition.ProcessConditionType.All))
		{
			if (processCondition.ShowInUI())
			{
				GameObject gameObject = Util.KInstantiateUI(this.rowPrefab, this.rowContainer, true);
				this.rows.Add(processCondition, gameObject);
			}
		}
	}

	// Token: 0x04004029 RID: 16425
	public GameObject rowPrefab;

	// Token: 0x0400402A RID: 16426
	public GameObject rowContainer;

	// Token: 0x0400402B RID: 16427
	[Tooltip("This list is indexed by the ProcessCondition.Status enum")]
	public static Color readyColor = Color.black;

	// Token: 0x0400402C RID: 16428
	public static Color failedColor = Color.red;

	// Token: 0x0400402D RID: 16429
	public static Color warningColor = new Color(1f, 0.3529412f, 0f, 1f);

	// Token: 0x0400402E RID: 16430
	private IProcessConditionSet targetConditionSet;

	// Token: 0x0400402F RID: 16431
	private Dictionary<ProcessCondition, GameObject> rows = new Dictionary<ProcessCondition, GameObject>();
}
