using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x02000BF0 RID: 3056
[AddComponentMenu("KMonoBehaviour/scripts/TreeFilterableSideScreenRow")]
public class TreeFilterableSideScreenRow : KMonoBehaviour
{
	// Token: 0x170006B1 RID: 1713
	// (get) Token: 0x06006074 RID: 24692 RVA: 0x00234879 File Offset: 0x00232A79
	// (set) Token: 0x06006075 RID: 24693 RVA: 0x00234881 File Offset: 0x00232A81
	public TreeFilterableSideScreen Parent
	{
		get
		{
			return this.parent;
		}
		set
		{
			this.parent = value;
		}
	}

	// Token: 0x06006076 RID: 24694 RVA: 0x0023488C File Offset: 0x00232A8C
	public TreeFilterableSideScreenRow.State GetState()
	{
		bool flag = false;
		bool flag2 = false;
		foreach (TreeFilterableSideScreenElement treeFilterableSideScreenElement in this.rowElements)
		{
			if (this.parent.GetElementTagAcceptedState(treeFilterableSideScreenElement.GetElementTag()))
			{
				flag = true;
			}
			else
			{
				flag2 = true;
			}
		}
		if (flag && !flag2)
		{
			return TreeFilterableSideScreenRow.State.On;
		}
		if (!flag && flag2)
		{
			return TreeFilterableSideScreenRow.State.Off;
		}
		if (flag && flag2)
		{
			return TreeFilterableSideScreenRow.State.Mixed;
		}
		if (this.rowElements.Count <= 0)
		{
			return TreeFilterableSideScreenRow.State.Off;
		}
		return TreeFilterableSideScreenRow.State.On;
	}

	// Token: 0x06006077 RID: 24695 RVA: 0x00234920 File Offset: 0x00232B20
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		MultiToggle multiToggle = this.checkBoxToggle;
		multiToggle.onClick = (System.Action)Delegate.Combine(multiToggle.onClick, new System.Action(delegate
		{
			TreeFilterableSideScreenRow.State state = this.GetState();
			if (state > TreeFilterableSideScreenRow.State.Mixed)
			{
				if (state == TreeFilterableSideScreenRow.State.On)
				{
					this.ChangeCheckBoxState(TreeFilterableSideScreenRow.State.Off);
					return;
				}
			}
			else
			{
				this.ChangeCheckBoxState(TreeFilterableSideScreenRow.State.On);
			}
		}));
	}

	// Token: 0x06006078 RID: 24696 RVA: 0x0023494F File Offset: 0x00232B4F
	protected override void OnCmpEnable()
	{
		base.OnCmpEnable();
		this.SetArrowToggleState(this.GetState() > TreeFilterableSideScreenRow.State.Off);
	}

	// Token: 0x06006079 RID: 24697 RVA: 0x00234966 File Offset: 0x00232B66
	protected override void OnCmpDisable()
	{
		this.SetArrowToggleState(false);
		this.rowElements.ForEach(delegate(TreeFilterableSideScreenElement row)
		{
			row.OnSelectionChanged -= this.OnElementSelectionChanged;
		});
		base.OnCmpDisable();
	}

	// Token: 0x0600607A RID: 24698 RVA: 0x0023498C File Offset: 0x00232B8C
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
	}

	// Token: 0x0600607B RID: 24699 RVA: 0x00234994 File Offset: 0x00232B94
	public void UpdateCheckBoxVisualState()
	{
		this.checkBoxToggle.ChangeState((int)this.GetState());
		this.visualDirty = false;
	}

	// Token: 0x0600607C RID: 24700 RVA: 0x002349B0 File Offset: 0x00232BB0
	public void ChangeCheckBoxState(TreeFilterableSideScreenRow.State newState)
	{
		switch (newState)
		{
		case TreeFilterableSideScreenRow.State.Off:
			this.rowElements.ForEach(delegate(TreeFilterableSideScreenElement re)
			{
				re.SetCheckBox(false);
			});
			break;
		case TreeFilterableSideScreenRow.State.On:
			this.rowElements.ForEach(delegate(TreeFilterableSideScreenElement re)
			{
				re.SetCheckBox(true);
			});
			break;
		}
		this.visualDirty = true;
	}

	// Token: 0x0600607D RID: 24701 RVA: 0x00234A2E File Offset: 0x00232C2E
	private void ArrowToggleClicked()
	{
		this.SetArrowToggleState(this.arrowToggle.CurrentState != 1);
		this.UpdateArrowToggleState();
	}

	// Token: 0x0600607E RID: 24702 RVA: 0x00234A4E File Offset: 0x00232C4E
	private void SetArrowToggleState(bool state)
	{
		this.arrowToggle.ChangeState(state ? 1 : 0);
		this.UpdateArrowToggleState();
	}

	// Token: 0x0600607F RID: 24703 RVA: 0x00234A68 File Offset: 0x00232C68
	private void UpdateArrowToggleState()
	{
		bool currentState = this.arrowToggle.CurrentState != 0;
		this.elementGroup.SetActive(currentState);
		this.bgImg.enabled = currentState;
	}

	// Token: 0x06006080 RID: 24704 RVA: 0x00234A9F File Offset: 0x00232C9F
	private void ArrowToggleDisabledClick()
	{
		KMonoBehaviour.PlaySound(GlobalAssets.GetSound("Negative", false));
	}

	// Token: 0x06006081 RID: 24705 RVA: 0x00234AB1 File Offset: 0x00232CB1
	private void OnElementSelectionChanged(Tag t, bool state)
	{
		if (state)
		{
			this.parent.AddTag(t);
		}
		else
		{
			this.parent.RemoveTag(t);
		}
		this.visualDirty = true;
	}

	// Token: 0x06006082 RID: 24706 RVA: 0x00234AD8 File Offset: 0x00232CD8
	public void SetElement(Tag mainElementTag, bool state, Dictionary<Tag, bool> filterMap)
	{
		this.subTags.Clear();
		this.rowElements.Clear();
		this.elementName.text = mainElementTag.ProperName();
		this.bgImg.enabled = false;
		string text = string.Format(UI.UISIDESCREENS.TREEFILTERABLESIDESCREEN.CATEGORYBUTTONTOOLTIP, mainElementTag.ProperName());
		this.checkBoxToggle.GetComponent<ToolTip>().SetSimpleTooltip(text);
		if (filterMap.Count == 0)
		{
			if (this.elementGroup.activeInHierarchy)
			{
				this.elementGroup.SetActive(false);
			}
			this.arrowToggle.onClick = new System.Action(this.ArrowToggleDisabledClick);
			this.arrowToggle.ChangeState(0);
		}
		else
		{
			this.arrowToggle.onClick = new System.Action(this.ArrowToggleClicked);
			this.arrowToggle.ChangeState(0);
			foreach (KeyValuePair<Tag, bool> keyValuePair in filterMap)
			{
				TreeFilterableSideScreenElement freeElement = this.parent.elementPool.GetFreeElement(this.elementGroup, true);
				freeElement.Parent = this.parent;
				freeElement.SetTag(keyValuePair.Key);
				freeElement.SetCheckBox(keyValuePair.Value);
				freeElement.OnSelectionChanged += this.OnElementSelectionChanged;
				freeElement.SetCheckBox(this.parent.IsTagAllowed(keyValuePair.Key));
				this.rowElements.Add(freeElement);
				this.subTags.Add(keyValuePair.Key);
			}
		}
		this.UpdateCheckBoxVisualState();
	}

	// Token: 0x0400421B RID: 16923
	public bool visualDirty;

	// Token: 0x0400421C RID: 16924
	[SerializeField]
	private LocText elementName;

	// Token: 0x0400421D RID: 16925
	[SerializeField]
	private GameObject elementGroup;

	// Token: 0x0400421E RID: 16926
	[SerializeField]
	private MultiToggle checkBoxToggle;

	// Token: 0x0400421F RID: 16927
	[SerializeField]
	private MultiToggle arrowToggle;

	// Token: 0x04004220 RID: 16928
	[SerializeField]
	private KImage bgImg;

	// Token: 0x04004221 RID: 16929
	private List<Tag> subTags = new List<Tag>();

	// Token: 0x04004222 RID: 16930
	private List<TreeFilterableSideScreenElement> rowElements = new List<TreeFilterableSideScreenElement>();

	// Token: 0x04004223 RID: 16931
	private TreeFilterableSideScreen parent;

	// Token: 0x02001A8C RID: 6796
	public enum State
	{
		// Token: 0x040077D4 RID: 30676
		Off,
		// Token: 0x040077D5 RID: 30677
		Mixed,
		// Token: 0x040077D6 RID: 30678
		On
	}
}
