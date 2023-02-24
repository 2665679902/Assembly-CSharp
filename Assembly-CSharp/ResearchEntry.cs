using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

// Token: 0x02000B6B RID: 2923
[AddComponentMenu("KMonoBehaviour/scripts/ResearchEntry")]
public class ResearchEntry : KMonoBehaviour
{
	// Token: 0x06005B3D RID: 23357 RVA: 0x00212060 File Offset: 0x00210260
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.techLineMap = new Dictionary<Tech, UILineRenderer>();
		this.BG.color = this.defaultColor;
		foreach (Tech tech in this.targetTech.requiredTech)
		{
			float num = this.targetTech.width / 2f + 18f;
			Vector2 zero = Vector2.zero;
			Vector2 zero2 = Vector2.zero;
			if (tech.center.y > this.targetTech.center.y + 2f)
			{
				zero = new Vector2(0f, 20f);
				zero2 = new Vector2(0f, -20f);
			}
			else if (tech.center.y < this.targetTech.center.y - 2f)
			{
				zero = new Vector2(0f, -20f);
				zero2 = new Vector2(0f, 20f);
			}
			UILineRenderer component = Util.KInstantiateUI(this.linePrefab, this.lineContainer.gameObject, true).GetComponent<UILineRenderer>();
			float num2 = 32f;
			component.Points = new Vector2[]
			{
				new Vector2(0f, 0f) + zero,
				new Vector2(-num2, 0f) + zero,
				new Vector2(-num2, tech.center.y - this.targetTech.center.y) + zero2,
				new Vector2(-(this.targetTech.center.x - num - (tech.center.x + num)) + 2f, tech.center.y - this.targetTech.center.y) + zero2
			};
			component.LineThickness = (float)this.lineThickness_inactive;
			component.color = this.inactiveLineColor;
			this.techLineMap.Add(tech, component);
		}
		this.QueueStateChanged(false);
		if (this.targetTech != null)
		{
			using (List<TechInstance>.Enumerator enumerator2 = Research.Instance.GetResearchQueue().GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					if (enumerator2.Current.tech == this.targetTech)
					{
						this.QueueStateChanged(true);
					}
				}
			}
		}
	}

	// Token: 0x06005B3E RID: 23358 RVA: 0x00212320 File Offset: 0x00210520
	public void SetTech(Tech newTech)
	{
		if (newTech == null)
		{
			global::Debug.LogError("The research provided is null!");
			return;
		}
		if (this.targetTech == newTech)
		{
			return;
		}
		foreach (ResearchType researchType in Research.Instance.researchTypes.Types)
		{
			if (newTech.costsByResearchTypeID.ContainsKey(researchType.id) && newTech.costsByResearchTypeID[researchType.id] > 0f)
			{
				GameObject gameObject = Util.KInstantiateUI(this.progressBarPrefab, this.progressBarContainer.gameObject, true);
				Image image = gameObject.GetComponentsInChildren<Image>()[2];
				Image component = gameObject.transform.Find("Icon").GetComponent<Image>();
				image.color = researchType.color;
				component.sprite = researchType.sprite;
				this.progressBarsByResearchTypeID[researchType.id] = gameObject;
			}
		}
		if (this.researchScreen == null)
		{
			this.researchScreen = base.transform.parent.GetComponentInParent<ResearchScreen>();
		}
		if (newTech.IsComplete())
		{
			this.ResearchCompleted(false);
		}
		this.targetTech = newTech;
		this.researchName.text = this.targetTech.Name;
		string text = "";
		foreach (TechItem techItem in this.targetTech.unlockedItems)
		{
			HierarchyReferences component2 = this.GetFreeIcon().GetComponent<HierarchyReferences>();
			if (text != "")
			{
				text += ", ";
			}
			text += techItem.Name;
			component2.GetReference<KImage>("Icon").sprite = techItem.UISprite();
			component2.GetReference<KImage>("Background");
			component2.GetReference<KImage>("DLCOverlay").gameObject.SetActive(!DlcManager.IsValidForVanilla(techItem.dlcIds));
			string text2 = string.Format("{0}\n{1}", techItem.Name, techItem.description);
			if (!DlcManager.IsValidForVanilla(techItem.dlcIds))
			{
				text2 += RESEARCH.MESSAGING.DLC.EXPANSION1;
			}
			component2.GetComponent<ToolTip>().toolTip = text2;
		}
		text = string.Format(UI.RESEARCHSCREEN_UNLOCKSTOOLTIP, text);
		this.researchName.GetComponent<ToolTip>().toolTip = string.Format("{0}\n{1}\n\n{2}", this.targetTech.Name, this.targetTech.desc, text);
		this.toggle.ClearOnClick();
		this.toggle.onClick += this.OnResearchClicked;
		this.toggle.onPointerEnter += delegate
		{
			this.researchScreen.TurnEverythingOff();
			this.OnHover(true, this.targetTech);
		};
		this.toggle.soundPlayer.AcceptClickCondition = () => !this.targetTech.IsComplete();
		this.toggle.onPointerExit += delegate
		{
			this.researchScreen.TurnEverythingOff();
		};
	}

	// Token: 0x06005B3F RID: 23359 RVA: 0x0021262C File Offset: 0x0021082C
	public void SetEverythingOff()
	{
		if (!this.isOn)
		{
			return;
		}
		this.borderHighlight.gameObject.SetActive(false);
		foreach (KeyValuePair<Tech, UILineRenderer> keyValuePair in this.techLineMap)
		{
			keyValuePair.Value.LineThickness = (float)this.lineThickness_inactive;
			keyValuePair.Value.color = this.inactiveLineColor;
		}
		this.isOn = false;
	}

	// Token: 0x06005B40 RID: 23360 RVA: 0x002126C0 File Offset: 0x002108C0
	public void SetEverythingOn()
	{
		if (this.isOn)
		{
			return;
		}
		this.UpdateProgressBars();
		this.borderHighlight.gameObject.SetActive(true);
		foreach (KeyValuePair<Tech, UILineRenderer> keyValuePair in this.techLineMap)
		{
			keyValuePair.Value.LineThickness = (float)this.lineThickness_active;
			keyValuePair.Value.color = this.activeLineColor;
		}
		base.transform.SetAsLastSibling();
		this.isOn = true;
	}

	// Token: 0x06005B41 RID: 23361 RVA: 0x00212764 File Offset: 0x00210964
	public void OnHover(bool entered, Tech hoverSource)
	{
		this.SetEverythingOn();
		foreach (Tech tech in this.targetTech.requiredTech)
		{
			ResearchEntry entry = this.researchScreen.GetEntry(tech);
			if (entry != null)
			{
				entry.OnHover(entered, this.targetTech);
			}
		}
	}

	// Token: 0x06005B42 RID: 23362 RVA: 0x002127E0 File Offset: 0x002109E0
	private void OnResearchClicked()
	{
		TechInstance activeResearch = Research.Instance.GetActiveResearch();
		if (activeResearch != null && activeResearch.tech != this.targetTech)
		{
			this.researchScreen.CancelResearch();
		}
		Research.Instance.SetActiveResearch(this.targetTech, true);
		if (DebugHandler.InstantBuildMode)
		{
			Research.Instance.CompleteQueue();
		}
		this.UpdateProgressBars();
	}

	// Token: 0x06005B43 RID: 23363 RVA: 0x0021283C File Offset: 0x00210A3C
	private void OnResearchCanceled()
	{
		if (this.targetTech.IsComplete())
		{
			return;
		}
		this.toggle.ClearOnClick();
		this.toggle.onClick += this.OnResearchClicked;
		this.researchScreen.CancelResearch();
		Research.Instance.CancelResearch(this.targetTech, true);
	}

	// Token: 0x06005B44 RID: 23364 RVA: 0x00212898 File Offset: 0x00210A98
	public void QueueStateChanged(bool isSelected)
	{
		if (isSelected)
		{
			if (!this.targetTech.IsComplete())
			{
				this.toggle.isOn = true;
				this.BG.color = this.pendingColor;
				this.titleBG.color = this.pendingHeaderColor;
				this.toggle.ClearOnClick();
				this.toggle.onClick += this.OnResearchCanceled;
			}
			else
			{
				this.toggle.isOn = false;
			}
			foreach (KeyValuePair<string, GameObject> keyValuePair in this.progressBarsByResearchTypeID)
			{
				keyValuePair.Value.transform.GetChild(0).GetComponentsInChildren<Image>()[1].color = Color.white;
			}
			Image[] array = this.iconPanel.GetComponentsInChildren<Image>();
			for (int i = 0; i < array.Length; i++)
			{
				array[i].material = this.StandardUIMaterial;
			}
			return;
		}
		if (this.targetTech.IsComplete())
		{
			this.toggle.isOn = false;
			this.BG.color = this.completedColor;
			this.titleBG.color = this.completedHeaderColor;
			this.defaultColor = this.completedColor;
			this.toggle.ClearOnClick();
			foreach (KeyValuePair<string, GameObject> keyValuePair2 in this.progressBarsByResearchTypeID)
			{
				keyValuePair2.Value.transform.GetChild(0).GetComponentsInChildren<Image>()[1].color = Color.white;
			}
			Image[] array = this.iconPanel.GetComponentsInChildren<Image>();
			for (int i = 0; i < array.Length; i++)
			{
				array[i].material = this.StandardUIMaterial;
			}
			return;
		}
		this.toggle.isOn = false;
		this.BG.color = this.defaultColor;
		this.titleBG.color = this.incompleteHeaderColor;
		this.toggle.ClearOnClick();
		this.toggle.onClick += this.OnResearchClicked;
		foreach (KeyValuePair<string, GameObject> keyValuePair3 in this.progressBarsByResearchTypeID)
		{
			keyValuePair3.Value.transform.GetChild(0).GetComponentsInChildren<Image>()[1].color = new Color(0.52156866f, 0.52156866f, 0.52156866f);
		}
	}

	// Token: 0x06005B45 RID: 23365 RVA: 0x00212B3C File Offset: 0x00210D3C
	public void UpdateFilterState(bool state)
	{
		this.filterLowlight.gameObject.SetActive(!state);
	}

	// Token: 0x06005B46 RID: 23366 RVA: 0x00212B5F File Offset: 0x00210D5F
	public void SetPercentage(float percent)
	{
	}

	// Token: 0x06005B47 RID: 23367 RVA: 0x00212B64 File Offset: 0x00210D64
	public void UpdateProgressBars()
	{
		foreach (KeyValuePair<string, GameObject> keyValuePair in this.progressBarsByResearchTypeID)
		{
			Transform child = keyValuePair.Value.transform.GetChild(0);
			float num;
			if (this.targetTech.IsComplete())
			{
				num = 1f;
				child.GetComponentInChildren<LocText>().text = this.targetTech.costsByResearchTypeID[keyValuePair.Key].ToString() + "/" + this.targetTech.costsByResearchTypeID[keyValuePair.Key].ToString();
			}
			else
			{
				TechInstance orAdd = Research.Instance.GetOrAdd(this.targetTech);
				if (orAdd == null)
				{
					continue;
				}
				child.GetComponentInChildren<LocText>().text = orAdd.progressInventory.PointsByTypeID[keyValuePair.Key].ToString() + "/" + this.targetTech.costsByResearchTypeID[keyValuePair.Key].ToString();
				num = orAdd.progressInventory.PointsByTypeID[keyValuePair.Key] / this.targetTech.costsByResearchTypeID[keyValuePair.Key];
			}
			child.GetComponentsInChildren<Image>()[2].fillAmount = num;
			child.GetComponent<ToolTip>().SetSimpleTooltip(Research.Instance.researchTypes.GetResearchType(keyValuePair.Key).description);
		}
	}

	// Token: 0x06005B48 RID: 23368 RVA: 0x00212D1C File Offset: 0x00210F1C
	private GameObject GetFreeIcon()
	{
		GameObject gameObject = Util.KInstantiateUI(this.iconPrefab, this.iconPanel, false);
		gameObject.SetActive(true);
		return gameObject;
	}

	// Token: 0x06005B49 RID: 23369 RVA: 0x00212D37 File Offset: 0x00210F37
	private Image GetFreeLine()
	{
		return Util.KInstantiateUI<Image>(this.linePrefab.gameObject, base.gameObject, false);
	}

	// Token: 0x06005B4A RID: 23370 RVA: 0x00212D50 File Offset: 0x00210F50
	public void ResearchCompleted(bool notify = true)
	{
		this.BG.color = this.completedColor;
		this.titleBG.color = this.completedHeaderColor;
		this.defaultColor = this.completedColor;
		if (notify)
		{
			this.unlockedTechMetric[ResearchEntry.UnlockedTechKey] = this.targetTech.Id;
			ThreadedHttps<KleiMetrics>.Instance.SendEvent(this.unlockedTechMetric, "ResearchCompleted");
		}
		this.toggle.ClearOnClick();
		if (notify)
		{
			ResearchCompleteMessage researchCompleteMessage = new ResearchCompleteMessage(this.targetTech);
			MusicManager.instance.PlaySong("Stinger_ResearchComplete", false);
			Messenger.Instance.QueueMessage(researchCompleteMessage);
		}
	}

	// Token: 0x04003DEB RID: 15851
	[Header("Labels")]
	[SerializeField]
	private LocText researchName;

	// Token: 0x04003DEC RID: 15852
	[Header("Transforms")]
	[SerializeField]
	private Transform progressBarContainer;

	// Token: 0x04003DED RID: 15853
	[SerializeField]
	private Transform lineContainer;

	// Token: 0x04003DEE RID: 15854
	[Header("Prefabs")]
	[SerializeField]
	private GameObject iconPanel;

	// Token: 0x04003DEF RID: 15855
	[SerializeField]
	private GameObject iconPrefab;

	// Token: 0x04003DF0 RID: 15856
	[SerializeField]
	private GameObject linePrefab;

	// Token: 0x04003DF1 RID: 15857
	[SerializeField]
	private GameObject progressBarPrefab;

	// Token: 0x04003DF2 RID: 15858
	[Header("Graphics")]
	[SerializeField]
	private Image BG;

	// Token: 0x04003DF3 RID: 15859
	[SerializeField]
	private Image titleBG;

	// Token: 0x04003DF4 RID: 15860
	[SerializeField]
	private Image borderHighlight;

	// Token: 0x04003DF5 RID: 15861
	[SerializeField]
	private Image filterHighlight;

	// Token: 0x04003DF6 RID: 15862
	[SerializeField]
	private Image filterLowlight;

	// Token: 0x04003DF7 RID: 15863
	[SerializeField]
	private Sprite hoverBG;

	// Token: 0x04003DF8 RID: 15864
	[SerializeField]
	private Sprite completedBG;

	// Token: 0x04003DF9 RID: 15865
	[Header("Colors")]
	[SerializeField]
	private Color defaultColor = Color.blue;

	// Token: 0x04003DFA RID: 15866
	[SerializeField]
	private Color completedColor = Color.yellow;

	// Token: 0x04003DFB RID: 15867
	[SerializeField]
	private Color pendingColor = Color.magenta;

	// Token: 0x04003DFC RID: 15868
	[SerializeField]
	private Color completedHeaderColor = Color.grey;

	// Token: 0x04003DFD RID: 15869
	[SerializeField]
	private Color incompleteHeaderColor = Color.grey;

	// Token: 0x04003DFE RID: 15870
	[SerializeField]
	private Color pendingHeaderColor = Color.grey;

	// Token: 0x04003DFF RID: 15871
	private Sprite defaultBG;

	// Token: 0x04003E00 RID: 15872
	[MyCmpGet]
	private KToggle toggle;

	// Token: 0x04003E01 RID: 15873
	private ResearchScreen researchScreen;

	// Token: 0x04003E02 RID: 15874
	private Dictionary<Tech, UILineRenderer> techLineMap;

	// Token: 0x04003E03 RID: 15875
	private Tech targetTech;

	// Token: 0x04003E04 RID: 15876
	private bool isOn = true;

	// Token: 0x04003E05 RID: 15877
	private Coroutine fadeRoutine;

	// Token: 0x04003E06 RID: 15878
	public Color activeLineColor;

	// Token: 0x04003E07 RID: 15879
	public Color inactiveLineColor;

	// Token: 0x04003E08 RID: 15880
	public int lineThickness_active = 6;

	// Token: 0x04003E09 RID: 15881
	public int lineThickness_inactive = 2;

	// Token: 0x04003E0A RID: 15882
	public Material StandardUIMaterial;

	// Token: 0x04003E0B RID: 15883
	private Dictionary<string, GameObject> progressBarsByResearchTypeID = new Dictionary<string, GameObject>();

	// Token: 0x04003E0C RID: 15884
	public static readonly string UnlockedTechKey = "UnlockedTech";

	// Token: 0x04003E0D RID: 15885
	private Dictionary<string, object> unlockedTechMetric = new Dictionary<string, object> { 
	{
		ResearchEntry.UnlockedTechKey,
		null
	} };
}
