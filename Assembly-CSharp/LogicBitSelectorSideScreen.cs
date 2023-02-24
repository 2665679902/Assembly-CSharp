using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x02000BC1 RID: 3009
public class LogicBitSelectorSideScreen : SideScreenContent, IRenderEveryTick
{
	// Token: 0x06005E92 RID: 24210 RVA: 0x00228E77 File Offset: 0x00227077
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.activeColor = GlobalAssets.Instance.colorSet.logicOnText;
		this.inactiveColor = GlobalAssets.Instance.colorSet.logicOffText;
	}

	// Token: 0x06005E93 RID: 24211 RVA: 0x00228EB3 File Offset: 0x002270B3
	public void SelectToggle(int bit)
	{
		this.target.SetBitSelection(bit);
		this.target.UpdateVisuals();
		this.RefreshToggles();
	}

	// Token: 0x06005E94 RID: 24212 RVA: 0x00228ED4 File Offset: 0x002270D4
	private void RefreshToggles()
	{
		for (int i = 0; i < this.target.GetBitDepth(); i++)
		{
			int n = i;
			if (!this.toggles_by_int.ContainsKey(i))
			{
				GameObject gameObject = Util.KInstantiateUI(this.rowPrefab, this.rowPrefab.transform.parent.gameObject, true);
				gameObject.GetComponent<HierarchyReferences>().GetReference<LocText>("bitName").SetText(string.Format(UI.UISIDESCREENS.LOGICBITSELECTORSIDESCREEN.BIT, i + 1));
				gameObject.GetComponent<HierarchyReferences>().GetReference<KImage>("stateIcon").color = (this.target.IsBitActive(i) ? this.activeColor : this.inactiveColor);
				gameObject.GetComponent<HierarchyReferences>().GetReference<LocText>("stateText").SetText(this.target.IsBitActive(i) ? UI.UISIDESCREENS.LOGICBITSELECTORSIDESCREEN.STATE_ACTIVE : UI.UISIDESCREENS.LOGICBITSELECTORSIDESCREEN.STATE_INACTIVE);
				MultiToggle component = gameObject.GetComponent<MultiToggle>();
				this.toggles_by_int.Add(i, component);
			}
			this.toggles_by_int[i].onClick = delegate
			{
				this.SelectToggle(n);
			};
		}
		foreach (KeyValuePair<int, MultiToggle> keyValuePair in this.toggles_by_int)
		{
			if (this.target.GetBitSelection() == keyValuePair.Key)
			{
				keyValuePair.Value.ChangeState(0);
			}
			else
			{
				keyValuePair.Value.ChangeState(1);
			}
		}
	}

	// Token: 0x06005E95 RID: 24213 RVA: 0x00229074 File Offset: 0x00227274
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetComponent<ILogicRibbonBitSelector>() != null;
	}

	// Token: 0x06005E96 RID: 24214 RVA: 0x00229080 File Offset: 0x00227280
	public override void SetTarget(GameObject new_target)
	{
		if (new_target == null)
		{
			global::Debug.LogError("Invalid gameObject received");
			return;
		}
		this.target = new_target.GetComponent<ILogicRibbonBitSelector>();
		if (this.target == null)
		{
			global::Debug.LogError("The gameObject received is not an ILogicRibbonBitSelector");
			return;
		}
		this.titleKey = this.target.SideScreenTitle;
		this.readerDescriptionContainer.SetActive(this.target.SideScreenDisplayReaderDescription());
		this.writerDescriptionContainer.SetActive(this.target.SideScreenDisplayWriterDescription());
		this.RefreshToggles();
		this.UpdateInputOutputDisplay();
		foreach (KeyValuePair<int, MultiToggle> keyValuePair in this.toggles_by_int)
		{
			this.UpdateStateVisuals(keyValuePair.Key);
		}
	}

	// Token: 0x06005E97 RID: 24215 RVA: 0x00229158 File Offset: 0x00227358
	public void RenderEveryTick(float dt)
	{
		if (this.target.Equals(null))
		{
			return;
		}
		foreach (KeyValuePair<int, MultiToggle> keyValuePair in this.toggles_by_int)
		{
			this.UpdateStateVisuals(keyValuePair.Key);
		}
		this.UpdateInputOutputDisplay();
	}

	// Token: 0x06005E98 RID: 24216 RVA: 0x002291C8 File Offset: 0x002273C8
	private void UpdateInputOutputDisplay()
	{
		if (this.target.SideScreenDisplayReaderDescription())
		{
			this.outputDisplayIcon.color = ((this.target.GetOutputValue() > 0) ? this.activeColor : this.inactiveColor);
		}
		if (this.target.SideScreenDisplayWriterDescription())
		{
			this.inputDisplayIcon.color = ((this.target.GetInputValue() > 0) ? this.activeColor : this.inactiveColor);
		}
	}

	// Token: 0x06005E99 RID: 24217 RVA: 0x00229240 File Offset: 0x00227440
	private void UpdateStateVisuals(int bit)
	{
		MultiToggle multiToggle = this.toggles_by_int[bit];
		multiToggle.gameObject.GetComponent<HierarchyReferences>().GetReference<KImage>("stateIcon").color = (this.target.IsBitActive(bit) ? this.activeColor : this.inactiveColor);
		multiToggle.gameObject.GetComponent<HierarchyReferences>().GetReference<LocText>("stateText").SetText(this.target.IsBitActive(bit) ? UI.UISIDESCREENS.LOGICBITSELECTORSIDESCREEN.STATE_ACTIVE : UI.UISIDESCREENS.LOGICBITSELECTORSIDESCREEN.STATE_INACTIVE);
	}

	// Token: 0x040040B8 RID: 16568
	private ILogicRibbonBitSelector target;

	// Token: 0x040040B9 RID: 16569
	public GameObject rowPrefab;

	// Token: 0x040040BA RID: 16570
	public KImage inputDisplayIcon;

	// Token: 0x040040BB RID: 16571
	public KImage outputDisplayIcon;

	// Token: 0x040040BC RID: 16572
	public GameObject readerDescriptionContainer;

	// Token: 0x040040BD RID: 16573
	public GameObject writerDescriptionContainer;

	// Token: 0x040040BE RID: 16574
	[NonSerialized]
	public Dictionary<int, MultiToggle> toggles_by_int = new Dictionary<int, MultiToggle>();

	// Token: 0x040040BF RID: 16575
	private Color activeColor;

	// Token: 0x040040C0 RID: 16576
	private Color inactiveColor;
}
