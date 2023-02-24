using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000BD6 RID: 3030
public class ResearchSideScreen : SideScreenContent
{
	// Token: 0x06005F5F RID: 24415 RVA: 0x0022E2CC File Offset: 0x0022C4CC
	public ResearchSideScreen()
	{
		this.refreshDisplayStateDelegate = new Action<object>(this.RefreshDisplayState);
	}

	// Token: 0x06005F60 RID: 24416 RVA: 0x0022E2E8 File Offset: 0x0022C4E8
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.selectResearchButton.onClick += delegate
		{
			ManagementMenu.Instance.ToggleResearch();
		};
		Research.Instance.Subscribe(-1914338957, this.refreshDisplayStateDelegate);
		Research.Instance.Subscribe(-125623018, this.refreshDisplayStateDelegate);
		this.RefreshDisplayState(null);
	}

	// Token: 0x06005F61 RID: 24417 RVA: 0x0022E358 File Offset: 0x0022C558
	protected override void OnCmpEnable()
	{
		base.OnCmpEnable();
		this.RefreshDisplayState(null);
		this.target = SelectTool.Instance.selected.GetComponent<KMonoBehaviour>().gameObject;
		this.target.gameObject.Subscribe(-1852328367, this.refreshDisplayStateDelegate);
		this.target.gameObject.Subscribe(-592767678, this.refreshDisplayStateDelegate);
	}

	// Token: 0x06005F62 RID: 24418 RVA: 0x0022E3C4 File Offset: 0x0022C5C4
	protected override void OnCmpDisable()
	{
		base.OnCmpDisable();
		if (this.target)
		{
			this.target.gameObject.Unsubscribe(-1852328367, this.refreshDisplayStateDelegate);
			this.target.gameObject.Unsubscribe(187661686, this.refreshDisplayStateDelegate);
			this.target = null;
		}
	}

	// Token: 0x06005F63 RID: 24419 RVA: 0x0022E424 File Offset: 0x0022C624
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		Research.Instance.Unsubscribe(-1914338957, this.refreshDisplayStateDelegate);
		Research.Instance.Unsubscribe(-125623018, this.refreshDisplayStateDelegate);
		if (this.target)
		{
			this.target.gameObject.Unsubscribe(-1852328367, this.refreshDisplayStateDelegate);
			this.target.gameObject.Unsubscribe(187661686, this.refreshDisplayStateDelegate);
			this.target = null;
		}
	}

	// Token: 0x06005F64 RID: 24420 RVA: 0x0022E4AB File Offset: 0x0022C6AB
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetComponent<ResearchCenter>() != null || target.GetComponent<NuclearResearchCenter>() != null;
	}

	// Token: 0x06005F65 RID: 24421 RVA: 0x0022E4CC File Offset: 0x0022C6CC
	private void RefreshDisplayState(object data = null)
	{
		if (SelectTool.Instance.selected == null)
		{
			return;
		}
		string text = "";
		ResearchCenter component = SelectTool.Instance.selected.GetComponent<ResearchCenter>();
		NuclearResearchCenter component2 = SelectTool.Instance.selected.GetComponent<NuclearResearchCenter>();
		if (component != null)
		{
			text = component.research_point_type_id;
		}
		if (component2 != null)
		{
			text = component2.researchTypeID;
		}
		if (component == null && component2 == null)
		{
			return;
		}
		this.researchButtonIcon.sprite = Research.Instance.researchTypes.GetResearchType(text).sprite;
		TechInstance activeResearch = Research.Instance.GetActiveResearch();
		if (activeResearch == null)
		{
			this.DescriptionText.text = "<b>" + UI.UISIDESCREENS.RESEARCHSIDESCREEN.NOSELECTEDRESEARCH + "</b>";
			return;
		}
		string text2 = "";
		if (!activeResearch.tech.costsByResearchTypeID.ContainsKey(text) || activeResearch.tech.costsByResearchTypeID[text] <= 0f)
		{
			text2 += "<color=#7f7f7f>";
		}
		text2 = text2 + "<b>" + activeResearch.tech.Name + "</b>";
		if (!activeResearch.tech.costsByResearchTypeID.ContainsKey(text) || activeResearch.tech.costsByResearchTypeID[text] <= 0f)
		{
			text2 += "</color>";
		}
		foreach (KeyValuePair<string, float> keyValuePair in activeResearch.tech.costsByResearchTypeID)
		{
			if (keyValuePair.Value != 0f)
			{
				bool flag = keyValuePair.Key == text;
				text2 += "\n   ";
				text2 += "<b>";
				if (!flag)
				{
					text2 += "<color=#7f7f7f>";
				}
				text2 = string.Concat(new string[]
				{
					text2,
					"- ",
					Research.Instance.researchTypes.GetResearchType(keyValuePair.Key).name,
					": ",
					activeResearch.progressInventory.PointsByTypeID[keyValuePair.Key].ToString(),
					"/",
					activeResearch.tech.costsByResearchTypeID[keyValuePair.Key].ToString()
				});
				if (!flag)
				{
					text2 += "</color>";
				}
				text2 += "</b>";
			}
		}
		this.DescriptionText.text = text2;
	}

	// Token: 0x04004152 RID: 16722
	public KButton selectResearchButton;

	// Token: 0x04004153 RID: 16723
	public Image researchButtonIcon;

	// Token: 0x04004154 RID: 16724
	public GameObject content;

	// Token: 0x04004155 RID: 16725
	private GameObject target;

	// Token: 0x04004156 RID: 16726
	private Action<object> refreshDisplayStateDelegate;

	// Token: 0x04004157 RID: 16727
	public LocText DescriptionText;
}
