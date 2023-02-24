using System;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000BE8 RID: 3048
public class TelescopeSideScreen : SideScreenContent
{
	// Token: 0x06006004 RID: 24580 RVA: 0x0023234C File Offset: 0x0023054C
	public TelescopeSideScreen()
	{
		this.refreshDisplayStateDelegate = new Action<object>(this.RefreshDisplayState);
	}

	// Token: 0x06006005 RID: 24581 RVA: 0x00232368 File Offset: 0x00230568
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.selectStarmapScreen.onClick += delegate
		{
			ManagementMenu.Instance.ToggleStarmap();
		};
		SpacecraftManager.instance.Subscribe(532901469, this.refreshDisplayStateDelegate);
		this.RefreshDisplayState(null);
	}

	// Token: 0x06006006 RID: 24582 RVA: 0x002323C2 File Offset: 0x002305C2
	protected override void OnCmpEnable()
	{
		base.OnCmpEnable();
		this.RefreshDisplayState(null);
		this.target = SelectTool.Instance.selected.GetComponent<KMonoBehaviour>().gameObject;
	}

	// Token: 0x06006007 RID: 24583 RVA: 0x002323EB File Offset: 0x002305EB
	protected override void OnCmpDisable()
	{
		base.OnCmpDisable();
		if (this.target)
		{
			this.target = null;
		}
	}

	// Token: 0x06006008 RID: 24584 RVA: 0x00232407 File Offset: 0x00230607
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		if (this.target)
		{
			this.target = null;
		}
	}

	// Token: 0x06006009 RID: 24585 RVA: 0x00232423 File Offset: 0x00230623
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetComponent<Telescope>() != null;
	}

	// Token: 0x0600600A RID: 24586 RVA: 0x00232434 File Offset: 0x00230634
	private void RefreshDisplayState(object data = null)
	{
		if (SelectTool.Instance.selected == null)
		{
			return;
		}
		if (SelectTool.Instance.selected.GetComponent<Telescope>() == null)
		{
			return;
		}
		if (!SpacecraftManager.instance.HasAnalysisTarget())
		{
			this.DescriptionText.text = "<b><color=#FF0000>" + UI.UISIDESCREENS.TELESCOPESIDESCREEN.NO_SELECTED_ANALYSIS_TARGET + "</color></b>";
			return;
		}
		string text = UI.UISIDESCREENS.TELESCOPESIDESCREEN.ANALYSIS_TARGET_SELECTED;
		this.DescriptionText.text = text;
	}

	// Token: 0x040041C3 RID: 16835
	public KButton selectStarmapScreen;

	// Token: 0x040041C4 RID: 16836
	public Image researchButtonIcon;

	// Token: 0x040041C5 RID: 16837
	public GameObject content;

	// Token: 0x040041C6 RID: 16838
	private GameObject target;

	// Token: 0x040041C7 RID: 16839
	private Action<object> refreshDisplayStateDelegate;

	// Token: 0x040041C8 RID: 16840
	public LocText DescriptionText;
}
