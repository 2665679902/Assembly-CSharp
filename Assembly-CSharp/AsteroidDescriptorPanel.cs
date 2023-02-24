using System;
using System.Collections.Generic;
using Database;
using ProcGen;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000A48 RID: 2632
[AddComponentMenu("KMonoBehaviour/scripts/AsteroidDescriptorPanel")]
public class AsteroidDescriptorPanel : KMonoBehaviour
{
	// Token: 0x06004FC3 RID: 20419 RVA: 0x001C6436 File Offset: 0x001C4636
	public bool HasDescriptors()
	{
		return this.labels.Count > 0;
	}

	// Token: 0x06004FC4 RID: 20420 RVA: 0x001C6446 File Offset: 0x001C4646
	public void EnableClusterDetails(bool setActive)
	{
		this.clusterNameLabel.gameObject.SetActive(setActive);
		this.clusterDifficultyLabel.gameObject.SetActive(setActive);
	}

	// Token: 0x06004FC5 RID: 20421 RVA: 0x001C646A File Offset: 0x001C466A
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
	}

	// Token: 0x06004FC6 RID: 20422 RVA: 0x001C6474 File Offset: 0x001C4674
	public void SetClusterDetailLabels(ColonyDestinationAsteroidBeltData cluster)
	{
		StringEntry stringEntry;
		Strings.TryGet(cluster.properName, out stringEntry);
		this.clusterNameLabel.SetText((stringEntry == null) ? "" : string.Format(WORLDS.SURVIVAL_CHANCE.CLUSTERNAME, stringEntry.String));
		int num = Mathf.Clamp(cluster.difficulty, 0, ColonyDestinationAsteroidBeltData.survivalOptions.Count - 1);
		global::Tuple<string, string, string> tuple = ColonyDestinationAsteroidBeltData.survivalOptions[num];
		string text = string.Format(WORLDS.SURVIVAL_CHANCE.TITLE, tuple.first, tuple.third);
		text = text.Trim(new char[] { '\n' });
		this.clusterDifficultyLabel.SetText(text);
	}

	// Token: 0x06004FC7 RID: 20423 RVA: 0x001C6518 File Offset: 0x001C4718
	public void SetParameterDescriptors(IList<AsteroidDescriptor> descriptors)
	{
		for (int i = 0; i < this.parameterWidgets.Count; i++)
		{
			UnityEngine.Object.Destroy(this.parameterWidgets[i]);
		}
		this.parameterWidgets.Clear();
		for (int j = 0; j < descriptors.Count; j++)
		{
			GameObject gameObject = global::Util.KInstantiateUI(this.prefabParameterWidget, base.gameObject, true);
			gameObject.GetComponent<LocText>().SetText(descriptors[j].text);
			ToolTip component = gameObject.GetComponent<ToolTip>();
			if (!string.IsNullOrEmpty(descriptors[j].tooltip))
			{
				component.SetSimpleTooltip(descriptors[j].tooltip);
			}
			this.parameterWidgets.Add(gameObject);
		}
	}

	// Token: 0x06004FC8 RID: 20424 RVA: 0x001C65CC File Offset: 0x001C47CC
	private void ClearTraitDescriptors()
	{
		for (int i = 0; i < this.traitWidgets.Count; i++)
		{
			UnityEngine.Object.Destroy(this.traitWidgets[i]);
		}
		this.traitWidgets.Clear();
		for (int j = 0; j < this.traitCategoryWidgets.Count; j++)
		{
			UnityEngine.Object.Destroy(this.traitCategoryWidgets[j]);
		}
		this.traitCategoryWidgets.Clear();
	}

	// Token: 0x06004FC9 RID: 20425 RVA: 0x001C6640 File Offset: 0x001C4840
	public void SetTraitDescriptors(IList<AsteroidDescriptor> descriptors, List<string> stories, bool includeDescriptions = true)
	{
		foreach (string text in stories)
		{
			WorldTrait storyTrait = Db.Get().Stories.Get(text).StoryTrait;
			string text2 = (DlcManager.IsPureVanilla() ? Strings.Get(storyTrait.description + "_SHORT") : Strings.Get(storyTrait.description));
			descriptors.Add(new AsteroidDescriptor(Strings.Get(storyTrait.name).String, text2, Color.white, null, storyTrait.icon));
		}
		this.SetTraitDescriptors(new List<IList<AsteroidDescriptor>> { descriptors }, includeDescriptions, null);
		if (stories.Count != 0)
		{
			this.storyTraitHeader.rectTransform().SetSiblingIndex(this.storyTraitHeader.rectTransform().parent.childCount - stories.Count - 1);
			this.storyTraitHeader.SetActive(true);
			return;
		}
		this.storyTraitHeader.SetActive(false);
	}

	// Token: 0x06004FCA RID: 20426 RVA: 0x001C6758 File Offset: 0x001C4958
	public void SetTraitDescriptors(IList<AsteroidDescriptor> descriptors, bool includeDescriptions = true)
	{
		this.SetTraitDescriptors(new List<IList<AsteroidDescriptor>> { descriptors }, includeDescriptions, null);
	}

	// Token: 0x06004FCB RID: 20427 RVA: 0x001C6770 File Offset: 0x001C4970
	public void SetTraitDescriptors(List<IList<AsteroidDescriptor>> descriptorSets, bool includeDescriptions = true, List<global::Tuple<string, Sprite>> headerData = null)
	{
		this.ClearTraitDescriptors();
		for (int i = 0; i < descriptorSets.Count; i++)
		{
			IList<AsteroidDescriptor> list = descriptorSets[i];
			GameObject gameObject = base.gameObject;
			if (descriptorSets.Count > 1)
			{
				global::Debug.Assert(headerData != null, "Asteroid Header data is null - traits wont have their world as contex in the selection UI");
				GameObject gameObject2 = global::Util.KInstantiate(this.prefabTraitCategoryWidget, base.gameObject, null);
				HierarchyReferences component = gameObject2.GetComponent<HierarchyReferences>();
				gameObject2.transform.localScale = Vector3.one;
				StringEntry stringEntry;
				Strings.TryGet(headerData[i].first, out stringEntry);
				component.GetReference<LocText>("NameLabel").SetText(stringEntry.String);
				component.GetReference<Image>("Icon").sprite = headerData[i].second;
				gameObject2.SetActive(true);
				gameObject = component.GetReference<RectTransform>("Contents").gameObject;
				this.traitCategoryWidgets.Add(gameObject2);
			}
			for (int j = 0; j < list.Count; j++)
			{
				GameObject gameObject3 = global::Util.KInstantiate(this.prefabTraitWidget, gameObject, null);
				HierarchyReferences component2 = gameObject3.GetComponent<HierarchyReferences>();
				gameObject3.SetActive(true);
				component2.GetReference<LocText>("NameLabel").SetText("<b>" + list[j].text + "</b>");
				Image reference = component2.GetReference<Image>("Icon");
				reference.color = list[j].associatedColor;
				if (list[j].associatedIcon != null)
				{
					Sprite sprite = Assets.GetSprite(list[j].associatedIcon);
					if (sprite != null)
					{
						reference.sprite = sprite;
					}
				}
				if (gameObject3.GetComponent<ToolTip>() != null)
				{
					gameObject3.GetComponent<ToolTip>().SetSimpleTooltip(list[j].tooltip);
				}
				LocText reference2 = component2.GetReference<LocText>("DescLabel");
				if (includeDescriptions && !string.IsNullOrEmpty(list[j].tooltip))
				{
					reference2.SetText(list[j].tooltip);
				}
				else
				{
					reference2.gameObject.SetActive(false);
				}
				gameObject3.transform.localScale = new Vector3(1f, 1f, 1f);
				gameObject3.SetActive(true);
				this.traitWidgets.Add(gameObject3);
			}
		}
	}

	// Token: 0x06004FCC RID: 20428 RVA: 0x001C69BC File Offset: 0x001C4BBC
	public void EnableClusterLocationLabels(bool enable)
	{
		this.startingAsteroidRowContainer.transform.parent.gameObject.SetActive(enable);
		this.nearbyAsteroidRowContainer.transform.parent.gameObject.SetActive(enable);
		this.distantAsteroidRowContainer.transform.parent.gameObject.SetActive(enable);
	}

	// Token: 0x06004FCD RID: 20429 RVA: 0x001C6A1C File Offset: 0x001C4C1C
	public void RefreshAsteroidLines(ColonyDestinationAsteroidBeltData cluster, AsteroidDescriptorPanel selectedAsteroidDetailsPanel, List<string> storyTraits)
	{
		foreach (KeyValuePair<ProcGen.World, GameObject> keyValuePair in this.asteroidLines)
		{
			if (!keyValuePair.Value.IsNullOrDestroyed())
			{
				UnityEngine.Object.Destroy(keyValuePair.Value);
			}
		}
		this.asteroidLines.Clear();
		this.SpawnAsteroidLine(cluster.GetStartWorld, this.startingAsteroidRowContainer, cluster);
		for (int i = 0; i < cluster.worlds.Count; i++)
		{
			ProcGen.World world = cluster.worlds[i];
			WorldPlacement worldPlacement = null;
			for (int j = 0; j < cluster.Layout.worldPlacements.Count; j++)
			{
				if (cluster.Layout.worldPlacements[j].world == world.filePath)
				{
					worldPlacement = cluster.Layout.worldPlacements[j];
					break;
				}
			}
			this.SpawnAsteroidLine(world, (worldPlacement.locationType == WorldPlacement.LocationType.InnerCluster) ? this.nearbyAsteroidRowContainer : this.distantAsteroidRowContainer, cluster);
		}
		using (Dictionary<ProcGen.World, GameObject>.Enumerator enumerator = this.asteroidLines.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<ProcGen.World, GameObject> line = enumerator.Current;
				MultiToggle component = line.Value.GetComponent<MultiToggle>();
				component.onClick = (System.Action)Delegate.Combine(component.onClick, new System.Action(delegate
				{
					this.SelectAsteroidInCluster(line.Key, cluster, selectedAsteroidDetailsPanel);
				}));
			}
		}
		this.SelectWholeClusterDetails(cluster, selectedAsteroidDetailsPanel, storyTraits);
	}

	// Token: 0x06004FCE RID: 20430 RVA: 0x001C6C24 File Offset: 0x001C4E24
	private void SelectAsteroidInCluster(ProcGen.World asteroid, ColonyDestinationAsteroidBeltData cluster, AsteroidDescriptorPanel selectedAsteroidDetailsPanel)
	{
		selectedAsteroidDetailsPanel.SpacedOutContentContainer.SetActive(true);
		this.clusterDetailsButton.GetComponent<MultiToggle>().ChangeState(0);
		foreach (KeyValuePair<ProcGen.World, GameObject> keyValuePair in this.asteroidLines)
		{
			keyValuePair.Value.GetComponent<MultiToggle>().ChangeState((keyValuePair.Key == asteroid) ? 1 : 0);
			if (keyValuePair.Key == asteroid)
			{
				this.SetSelectedAsteroid(keyValuePair.Key, selectedAsteroidDetailsPanel, cluster.GenerateTraitDescriptors(keyValuePair.Key, true));
			}
		}
	}

	// Token: 0x06004FCF RID: 20431 RVA: 0x001C6CD4 File Offset: 0x001C4ED4
	public void SelectWholeClusterDetails(ColonyDestinationAsteroidBeltData cluster, AsteroidDescriptorPanel selectedAsteroidDetailsPanel, List<string> stories)
	{
		selectedAsteroidDetailsPanel.SpacedOutContentContainer.SetActive(false);
		foreach (KeyValuePair<ProcGen.World, GameObject> keyValuePair in this.asteroidLines)
		{
			keyValuePair.Value.GetComponent<MultiToggle>().ChangeState(0);
		}
		this.SetSelectedCluster(cluster, selectedAsteroidDetailsPanel, stories);
		this.clusterDetailsButton.GetComponent<MultiToggle>().ChangeState(1);
	}

	// Token: 0x06004FD0 RID: 20432 RVA: 0x001C6D58 File Offset: 0x001C4F58
	private void SpawnAsteroidLine(ProcGen.World asteroid, GameObject parentContainer, ColonyDestinationAsteroidBeltData cluster)
	{
		if (this.asteroidLines.ContainsKey(asteroid))
		{
			return;
		}
		GameObject gameObject = global::Util.KInstantiateUI(this.prefabAsteroidLine, parentContainer.gameObject, true);
		HierarchyReferences component = gameObject.GetComponent<HierarchyReferences>();
		Image reference = component.GetReference<Image>("Icon");
		LocText reference2 = component.GetReference<LocText>("Label");
		RectTransform reference3 = component.GetReference<RectTransform>("TraitsRow");
		LocText reference4 = component.GetReference<LocText>("TraitLabel");
		ToolTip component2 = gameObject.GetComponent<ToolTip>();
		Sprite uisprite = ColonyDestinationAsteroidBeltData.GetUISprite(asteroid.asteroidIcon);
		reference.sprite = uisprite;
		StringEntry stringEntry;
		Strings.TryGet(asteroid.name, out stringEntry);
		reference2.SetText(stringEntry.String);
		List<WorldTrait> worldTraits = cluster.GetWorldTraits(asteroid);
		reference4.gameObject.SetActive(worldTraits.Count == 0);
		reference4.SetText(UI.FRONTEND.COLONYDESTINATIONSCREEN.NO_TRAITS);
		RectTransform reference5 = component.GetReference<RectTransform>("TraitIconPrefab");
		foreach (WorldTrait worldTrait in worldTraits)
		{
			Image component3 = global::Util.KInstantiateUI(reference5.gameObject, reference3.gameObject, true).GetComponent<Image>();
			Sprite sprite = Assets.GetSprite(worldTrait.filePath.Substring(worldTrait.filePath.LastIndexOf("/") + 1));
			if (sprite != null)
			{
				component3.sprite = sprite;
			}
			component3.color = global::Util.ColorFromHex(worldTrait.colorHex);
		}
		string text = "";
		if (worldTraits.Count > 0)
		{
			for (int i = 0; i < worldTraits.Count; i++)
			{
				StringEntry stringEntry2;
				Strings.TryGet(worldTraits[i].name, out stringEntry2);
				StringEntry stringEntry3;
				Strings.TryGet(worldTraits[i].description, out stringEntry3);
				text = string.Concat(new string[]
				{
					text,
					"<color=#",
					worldTraits[i].colorHex,
					">",
					stringEntry2.String,
					"</color>\n",
					stringEntry3.String
				});
				if (i != worldTraits.Count - 1)
				{
					text += "\n\n";
				}
			}
		}
		else
		{
			text = UI.FRONTEND.COLONYDESTINATIONSCREEN.NO_TRAITS;
		}
		component2.SetSimpleTooltip(text);
		this.asteroidLines.Add(asteroid, gameObject);
	}

	// Token: 0x06004FD1 RID: 20433 RVA: 0x001C6FC0 File Offset: 0x001C51C0
	private void SetSelectedAsteroid(ProcGen.World asteroid, AsteroidDescriptorPanel detailPanel, List<AsteroidDescriptor> traitDescriptors)
	{
		detailPanel.SetTraitDescriptors(traitDescriptors, true);
		detailPanel.selectedAsteroidIcon.sprite = ColonyDestinationAsteroidBeltData.GetUISprite(asteroid.asteroidIcon);
		detailPanel.selectedAsteroidIcon.gameObject.SetActive(true);
		StringEntry stringEntry;
		Strings.TryGet(asteroid.name, out stringEntry);
		detailPanel.selectedAsteroidLabel.SetText(stringEntry.String);
		StringEntry stringEntry2;
		Strings.TryGet(asteroid.description, out stringEntry2);
		detailPanel.selectedAsteroidDescription.SetText(stringEntry2.String);
	}

	// Token: 0x06004FD2 RID: 20434 RVA: 0x001C703C File Offset: 0x001C523C
	private void SetSelectedCluster(ColonyDestinationAsteroidBeltData cluster, AsteroidDescriptorPanel detailPanel, List<string> stories)
	{
		List<IList<AsteroidDescriptor>> list = new List<IList<AsteroidDescriptor>>();
		List<global::Tuple<string, Sprite>> list2 = new List<global::Tuple<string, Sprite>>();
		List<AsteroidDescriptor> list3 = cluster.GenerateTraitDescriptors(cluster.GetStartWorld, false);
		if (list3.Count != 0)
		{
			list2.Add(new global::Tuple<string, Sprite>(cluster.GetStartWorld.name, ColonyDestinationAsteroidBeltData.GetUISprite(cluster.GetStartWorld.asteroidIcon)));
			list.Add(list3);
		}
		foreach (ProcGen.World world in cluster.worlds)
		{
			List<AsteroidDescriptor> list4 = cluster.GenerateTraitDescriptors(world, false);
			if (list4.Count != 0)
			{
				list2.Add(new global::Tuple<string, Sprite>(world.name, ColonyDestinationAsteroidBeltData.GetUISprite(world.asteroidIcon)));
				list.Add(list4);
			}
		}
		list2.Add(new global::Tuple<string, Sprite>("STRINGS.UI.FRONTEND.COLONYDESTINATIONSCREEN.STORY_TRAITS_HEADER", Assets.GetSprite("codexIconStoryTraits")));
		List<AsteroidDescriptor> list5 = new List<AsteroidDescriptor>();
		foreach (string text in stories)
		{
			Story story = Db.Get().Stories.Get(text);
			string icon = story.StoryTrait.icon;
			AsteroidDescriptor asteroidDescriptor = new AsteroidDescriptor(Strings.Get(story.StoryTrait.name).String, Strings.Get(story.StoryTrait.description).String, Color.white, null, icon);
			list5.Add(asteroidDescriptor);
		}
		list.Add(list5);
		detailPanel.SetTraitDescriptors(list, false, list2);
		detailPanel.selectedAsteroidIcon.gameObject.SetActive(false);
		StringEntry stringEntry;
		Strings.TryGet(cluster.properName, out stringEntry);
		detailPanel.selectedAsteroidLabel.SetText(stringEntry.String);
		detailPanel.selectedAsteroidDescription.SetText("");
	}

	// Token: 0x04003573 RID: 13683
	[Header("Destination Details")]
	[SerializeField]
	private GameObject customLabelPrefab;

	// Token: 0x04003574 RID: 13684
	[SerializeField]
	private GameObject prefabTraitWidget;

	// Token: 0x04003575 RID: 13685
	[SerializeField]
	private GameObject prefabTraitCategoryWidget;

	// Token: 0x04003576 RID: 13686
	[SerializeField]
	private GameObject prefabParameterWidget;

	// Token: 0x04003577 RID: 13687
	[SerializeField]
	private GameObject startingAsteroidRowContainer;

	// Token: 0x04003578 RID: 13688
	[SerializeField]
	private GameObject nearbyAsteroidRowContainer;

	// Token: 0x04003579 RID: 13689
	[SerializeField]
	private GameObject distantAsteroidRowContainer;

	// Token: 0x0400357A RID: 13690
	[SerializeField]
	private LocText clusterNameLabel;

	// Token: 0x0400357B RID: 13691
	[SerializeField]
	private LocText clusterDifficultyLabel;

	// Token: 0x0400357C RID: 13692
	[SerializeField]
	public LocText headerLabel;

	// Token: 0x0400357D RID: 13693
	[SerializeField]
	public MultiToggle clusterDetailsButton;

	// Token: 0x0400357E RID: 13694
	[SerializeField]
	public GameObject storyTraitHeader;

	// Token: 0x0400357F RID: 13695
	private List<GameObject> labels = new List<GameObject>();

	// Token: 0x04003580 RID: 13696
	[Header("Selected Asteroid Details")]
	[SerializeField]
	private GameObject SpacedOutContentContainer;

	// Token: 0x04003581 RID: 13697
	public Image selectedAsteroidIcon;

	// Token: 0x04003582 RID: 13698
	public LocText selectedAsteroidLabel;

	// Token: 0x04003583 RID: 13699
	public LocText selectedAsteroidDescription;

	// Token: 0x04003584 RID: 13700
	[SerializeField]
	private GameObject prefabAsteroidLine;

	// Token: 0x04003585 RID: 13701
	private Dictionary<ProcGen.World, GameObject> asteroidLines = new Dictionary<ProcGen.World, GameObject>();

	// Token: 0x04003586 RID: 13702
	private List<GameObject> traitWidgets = new List<GameObject>();

	// Token: 0x04003587 RID: 13703
	private List<GameObject> traitCategoryWidgets = new List<GameObject>();

	// Token: 0x04003588 RID: 13704
	private List<GameObject> parameterWidgets = new List<GameObject>();
}
