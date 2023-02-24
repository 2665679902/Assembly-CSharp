using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000B91 RID: 2961
public class ArtifactAnalysisSideScreen : SideScreenContent
{
	// Token: 0x06005D11 RID: 23825 RVA: 0x00220554 File Offset: 0x0021E754
	public override string GetTitle()
	{
		if (this.targetArtifactStation != null)
		{
			return string.Format(base.GetTitle(), this.targetArtifactStation.GetProperName());
		}
		return base.GetTitle();
	}

	// Token: 0x06005D12 RID: 23826 RVA: 0x00220581 File Offset: 0x0021E781
	public override void ClearTarget()
	{
		this.targetArtifactStation = null;
		base.ClearTarget();
	}

	// Token: 0x06005D13 RID: 23827 RVA: 0x00220590 File Offset: 0x0021E790
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetSMI<ArtifactAnalysisStation.StatesInstance>() != null;
	}

	// Token: 0x06005D14 RID: 23828 RVA: 0x0022059C File Offset: 0x0021E79C
	private void RefreshRows()
	{
		if (this.undiscoveredRow == null)
		{
			this.undiscoveredRow = Util.KInstantiateUI(this.rowPrefab, this.rowContainer, true);
			HierarchyReferences component = this.undiscoveredRow.GetComponent<HierarchyReferences>();
			component.GetReference<LocText>("label").SetText(UI.UISIDESCREENS.ARTIFACTANALYSISSIDESCREEN.NO_ARTIFACTS_DISCOVERED);
			component.GetComponent<ToolTip>().SetSimpleTooltip(UI.UISIDESCREENS.ARTIFACTANALYSISSIDESCREEN.NO_ARTIFACTS_DISCOVERED_TOOLTIP);
			component.GetReference<Image>("icon").sprite = Assets.GetSprite("unknown");
			component.GetReference<Image>("icon").color = Color.grey;
		}
		List<string> analyzedArtifactIDs = ArtifactSelector.Instance.GetAnalyzedArtifactIDs();
		this.undiscoveredRow.SetActive(analyzedArtifactIDs.Count == 0);
		foreach (string text in analyzedArtifactIDs)
		{
			if (!this.rows.ContainsKey(text))
			{
				GameObject gameObject = Util.KInstantiateUI(this.rowPrefab, this.rowContainer, true);
				this.rows.Add(text, gameObject);
				GameObject artifactPrefab = Assets.GetPrefab(text);
				HierarchyReferences component2 = gameObject.GetComponent<HierarchyReferences>();
				component2.GetReference<LocText>("label").SetText(artifactPrefab.GetProperName());
				component2.GetReference<Image>("icon").sprite = Def.GetUISprite(artifactPrefab, text, false).first;
				component2.GetComponent<KButton>().onClick += delegate
				{
					this.OpenEvent(artifactPrefab);
				};
			}
		}
	}

	// Token: 0x06005D15 RID: 23829 RVA: 0x0022074C File Offset: 0x0021E94C
	private void OpenEvent(GameObject artifactPrefab)
	{
		SimpleEvent.StatesInstance statesInstance = GameplayEventManager.Instance.StartNewEvent(Db.Get().GameplayEvents.ArtifactReveal, -1).smi as SimpleEvent.StatesInstance;
		statesInstance.artifact = artifactPrefab;
		artifactPrefab.GetComponent<KPrefabID>();
		artifactPrefab.GetComponent<InfoDescription>();
		string text = artifactPrefab.PrefabID().Name.ToUpper();
		text = text.Replace("ARTIFACT_", "");
		string text2 = "STRINGS.UI.SPACEARTIFACTS." + text + ".ARTIFACT";
		string text3 = string.Format("<b>{0}</b>", artifactPrefab.GetProperName());
		StringEntry stringEntry;
		Strings.TryGet(text2, out stringEntry);
		if (stringEntry != null && !stringEntry.String.IsNullOrWhiteSpace())
		{
			text3 = text3 + "\n\n" + stringEntry.String;
		}
		if (text3 != null && !text3.IsNullOrWhiteSpace())
		{
			statesInstance.SetTextParameter("desc", text3);
		}
		statesInstance.ShowEventPopup();
	}

	// Token: 0x06005D16 RID: 23830 RVA: 0x00220821 File Offset: 0x0021EA21
	public override void SetTarget(GameObject target)
	{
		this.targetArtifactStation = target;
		base.SetTarget(target);
		this.RefreshRows();
	}

	// Token: 0x04003FA2 RID: 16290
	[SerializeField]
	private GameObject rowPrefab;

	// Token: 0x04003FA3 RID: 16291
	private GameObject targetArtifactStation;

	// Token: 0x04003FA4 RID: 16292
	[SerializeField]
	private GameObject rowContainer;

	// Token: 0x04003FA5 RID: 16293
	private Dictionary<string, GameObject> rows = new Dictionary<string, GameObject>();

	// Token: 0x04003FA6 RID: 16294
	private GameObject undiscoveredRow;
}
